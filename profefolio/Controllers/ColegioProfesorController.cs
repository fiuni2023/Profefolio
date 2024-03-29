using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Helpers;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.ColegioProfesor;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColegioProfesorController : ControllerBase
    {
        private IPersona _personaService;
        private IColegioProfesor _cProfService;
        private IColegio _colegioService;
        private IMapper _mapper;

        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;


        public ColegioProfesorController(IPersona personaService, IColegioProfesor colegioProfesorService, IColegio colegioService, IMapper mapper)
        {
            _personaService = personaService;
            _cProfService = colegioProfesorService;
            _colegioService = colegioService;
            _mapper = mapper;
        }




        [HttpGet("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<ColegioProfesorByIdResult>> GetById(int id)
        {
            try
            {

                var colProf = await _cProfService.FindById(id);

                if (colProf == null)
                {
                    return NotFound();
                }

                //validacion de que sea un administrador o profesor dado su name en el token
                var userMail = User.FindFirstValue(ClaimTypes.Name);

                if (!userMail.Equals(colProf.Colegio.personas.Email) && !userMail.Equals(colProf.Persona.Email))
                {
                    return Unauthorized("No tiene permiso de acceso a los datos");
                }

                return Ok(_mapper.Map<ColegioProfesorByIdResult>(colProf));
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");

                return BadRequest("Error durante la busqueda");
            }
        }


        [HttpGet("page/{idColegio:int}/{page:int}")]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<DataListDTO<ColegioProfesorByIdResult>>> GetPageByIdColegio(int idColegio, int page)
        {
            try
            {
                if (!(await _cProfService.Exist(idColegio)))
                {
                    return NotFound("No se encontraron datos asociados al colegio");
                }
                
                var userEmail = User.FindFirstValue(ClaimTypes.Name);

                //en el service se valida que el usuario sea el administrador del colegio o el profesor de la relacion
                var colProf = await _cProfService.FindAllByIdColegio(page, CantPorPage, idColegio, userEmail);
                var cantItmed = await _cProfService.Count(idColegio, userEmail);

                int cantPages = (int)Math.Ceiling((double)cantItmed / (double)CantPorPage);


                var result = new DataListDTO<ColegioProfesorByIdResult>();

                if (page >= cantPages)
                {
                    return BadRequest($"No existe la pagina: {page} ");
                }

                result.CantItems = colProf.ToList().Count;
                result.CurrentPage = page;
                result.Next = result.CurrentPage + 1 < cantPages;
                result.DataList = _mapper.Map<List<ColegioProfesorByIdResult>>(colProf.ToList());
                result.TotalPage = cantPages;

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");

                return BadRequest("Error durante la busqueda");
            }
        }


        [HttpGet("ByColegio/{idColegio:int}")]
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<List<ColegioProfesorSimpleDTO>>> GetByIdColegio(int idColegio)
        {
            try
            {
                if (!(await _cProfService.Exist(idColegio)))
                {
                    return NotFound("No se encontraron datos asociados al colegio");
                }

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                //se obtiene la lista de ColegioProfesores en la cual este asociado el administrador o profesor
                var colegioProfesores = await _cProfService.FindAllByIdColegio(idColegio, userEmail);
                //se valida que la lista tenga algun valor 
                if (colegioProfesores == null || (!colegioProfesores.Any()))
                {
                    var userRole = User.FindFirstValue(ClaimTypes.Role);
                    if ("Profesor".Equals(userRole))
                    {
                        return BadRequest("Como profesor no fue asignado a este colegio todavia");
                    }
                    return NotFound("El Colegio no tiene datos, verifique que sea su colegio o que tenga profesores en su colegio");
                }

                var result = _mapper.Map<List<ColegioProfesorSimpleDTO>>(colegioProfesores.ToList());

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");

                return BadRequest("Error durante la busqueda");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<ColegioProfesorResultDTO>> Post([FromBody] ColegioProfesorDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalido");
            }

            try
            {

                if (await _cProfService.Exist(dto.ProfesorId, dto.ColegioId))
                {
                    return BadRequest("Ya se registro el Profesor en este colegio");
                }


                var nameUser = User.FindFirstValue(ClaimTypes.Name);


                //se verifica que exista un colegio en donde este asignado el administrador 
                Colegio colegio = await _colegioService.FindById(dto.ColegioId);
                if (colegio == null)
                {
                    return NotFound("El colegio no esta disponible");
                }

                //se verifica que el administrador tenga el mismo email que el del administrador que hizo la peticion
                if (!colegio.personas.Email.Equals(nameUser))
                {
                    return Unauthorized("No puede agregar Profesores en otros colegios");
                }

                /*
                    Se verifica que exista el profesor --- no se verifica que no sea nulo porque retorna una 
                    excepcion si no encunetra
                */
                var persona = await _personaService.FindById(dto.ProfesorId);



                var roles = await _personaService.GetRolesPersona(persona);

                //se verifica que el id recibido sea de un profesor
                if (!roles.Contains("Profesor"))
                {
                    return BadRequest("No se pueden asignar usuarios no asignados como profesor a los colegios");
                }


                var colProf = _mapper.Map<ColegioProfesor>(dto);

                colProf.CreatedBy = nameUser;
                colProf.Created = DateTime.Now;
                colProf.Deleted = false;


                await _cProfService.Add(colProf);
                await _cProfService.Save();

                return Ok(_mapper.Map<ColegioProfesorResultDTO>(colProf));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound("El Profesor no esta disponible");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }



        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Put(int id, [FromBody] ColegioProfesorEditDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalido");
            }

            try
            {

                var userEmail = User.FindFirstValue(ClaimTypes.Name);
                
                //se verifica que exista un colegio en donde este asignado el administrador 
                Colegio colegio = await _colegioService.FindById(dto.ColegioId);
                if (colegio == null)
                {
                    return NotFound("El colegio no esta disponible");
                }
                
                

                //validar que el usuario sea administrador del colegio esto a traves del role
                var colProf = await _cProfService.FindById(id);
                if (colProf == null)
                {
                    return NotFound("No se puede editar, no esta disponible");
                }

                //se verifica que el administrador tenga el mismo email que el del administrador que hizo la peticion
                if (!colegio.personas.Email.Equals(userEmail) || !userEmail.Equals(colProf.Colegio.personas.Email))
                {
                    return Unauthorized("No puede modificar datos de otros colegios");
                }


                if (await _cProfService.Exist(dto.ProfesorId, dto.ColegioId))
                {
                    return BadRequest("Ya se registro el Profesor en este colegio");
                }


                /*
                    Se verifica que exista el profesor --- no se verifica que no sea nulo porque retorna una 
                    excepcion si no encunetra
                */
                var persona = await _personaService.FindById(dto.ProfesorId);



                var roles = await _personaService.GetRolesPersona(persona);

                //se verifica que el id recibido sea de un profesor
                if (!roles.Contains("Profesor"))
                {
                    return BadRequest("No se pueden asignar usuarios no asignados como profesor a los colegios");
                }


                colProf.ModifiedBy = userEmail;
                colProf.Modified = DateTime.Now;
                colProf.PersonaId = dto.ProfesorId;
                colProf.ColegioId = dto.ColegioId;


                _cProfService.Edit(colProf);
                await _cProfService.Save();

                return NoContent();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound("El Profesor no esta disponible");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la edicion.");
            }
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                //Obtenemos la relacion Colegio-Profesor
                var colProf = await _cProfService.FindById(id);
                if (colProf == null)
                {
                    return NotFound("No se puede eliminar no esta disponible");
                }


                var nameUser = User.FindFirstValue(ClaimTypes.Name);

                var admin = await _personaService.FindByEmail(nameUser);
                if (admin == null)
                {
                    return BadRequest("Error. El administradir no existe");
                }


                //se verifica que exista un colegio en donde este asignado el administrador 
                Colegio colegio = await _colegioService.FindByIdAdmin(admin.Id);
                if (colegio == null)
                {
                    return NotFound("El colegio no esta disponible");
                }
                
                //se verifica que el Id del colegio de la relacion no sea igual al Id del colegio al que pertenece le administrador
                if (colProf.ColegioId != colegio.Id)
                {
                    return Unauthorized("No puede eliminar datos de otros colegios");
                }

                colProf.Deleted = true;
                colProf.Modified = DateTime.Now;
                colProf.ModifiedBy = nameUser;

                _cProfService.Edit(colProf);
                await _cProfService.Save();

                return Ok();

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error de servidor mientras se intentaba eliminar");
            }
        }

    }
}