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
using profefolio.Models.DTOs.Alumno;
using profefolio.Models.DTOs.ColegiosAlumnos;
using profefolio.Models.Entities;
using profefolio.Repository;

namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColegiosAlumnosController : ControllerBase
    {
        private IColegiosAlumnos _cAlumnosService;
        private IPersona _personaService;
        private IColegio _colegioService;
        private readonly IRol _rolService;
        private IMapper _mapper;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;

        public ColegiosAlumnosController(IColegiosAlumnos cAlumnosService, IRol rolService, IMapper mapper, IColegio colegioService, IPersona personaService)
        {
            _cAlumnosService = cAlumnosService;
            _mapper = mapper;
            _rolService = rolService;
            _colegioService = colegioService;
            _personaService = personaService;
        }

        [HttpGet("page/{page:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<DataListDTO<ColegioAlumnoListPageDTO>>> GetPage(int page)
        {
            if (page < 0)
            {
                return BadRequest("El numero de pagina debe ser mayor o igual que cero");
            }

            var adminEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                var alumnoColegios = await _cAlumnosService.FindAllByAdminEmail(page, CantPorPage, adminEmail);

                int cantPages = (int)Math.Ceiling((double)(await _cAlumnosService.Count(adminEmail)) / (double)CantPorPage);

                var result = new DataListDTO<ColegioAlumnoListPageDTO>();

                if (page >= cantPages)
                {
                    return BadRequest(page != 0 ? $"No existe la pagina: {page}" : $"No existen Alumnos en el Colegio");
                }

                result.CantItems = alumnoColegios.Count();
                result.CurrentPage = page;
                result.Next = result.CurrentPage + 1 < cantPages;
                result.DataList = _mapper.Map<List<ColegioAlumnoListPageDTO>>(alumnoColegios.ToList());
                result.TotalPage = cantPages;

                return Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los Alumnos.");
            }
        }

        [HttpGet("all/page/{page:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<DataListDTO<ColegioAlumnosDTO>>> FindAll(int page)
        {
            if (page < 0)
            {
                return BadRequest("El numero de pagina debe ser mayor o igual que cero");
            }

            var adminEmail = User.FindFirstValue(ClaimTypes.Name);

            try
            {
                var alumnoColegios = await _cAlumnosService.FindAll(adminEmail, page, CantPorPage);

                int cantPages = (int)Math.Ceiling((double)(await _cAlumnosService.Count(adminEmail)) / (double)CantPorPage);

                var result = new DataListDTO<ColegioAlumnosDTO>();

                if (page >= cantPages)
                {
                    return BadRequest(page != 0 ? $"No existe la pagina: {page}" : $"No existen Alumnos en el Colegio");
                }

                result.CantItems = alumnoColegios.Count();
                result.CurrentPage = page;
                result.Next = result.CurrentPage + 1 < cantPages;
                result.DataList = _mapper.Map<List<ColegioAlumnosDTO>>(alumnoColegios.ToList());
                result.TotalPage = cantPages;

                return Ok(result);

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante la obtencion de los Alumnos.");
            }
        }

        [HttpGet("NoAssignedAlumnos/{claseId:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<List<ColegioAlumnoToSelectDTO>>> GetAll(int claseId)
        {
            var adminEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var listAlumnosColegio = await _cAlumnosService.FindAllNoAssignedToClaseByEmailAdminAndIdClase(adminEmail, claseId);

                if (listAlumnosColegio == null)
                {
                    BadRequest("No se encontraron alumnos para la clase");
                }

                var result = _mapper.Map<List<ColegioAlumnoToSelectDTO>>(listAlumnosColegio);
                return Ok(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                return BadRequest("Error durarnte la obtencion de alumnos de la clase");
            }
        }

        [HttpGet("NoAssignedAlumnos/{claseId:int}/page/{page:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<DataListDTO<ColegioAlumnosDTO>>> GetAll(int claseId, int page)
        {

            var adminEmail = User.FindFirstValue(ClaimTypes.Name);
            try
            {
                var listAlumnosColegio = await _cAlumnosService.FindNotAssigned(adminEmail, claseId, page, CantPorPage);

                if (listAlumnosColegio == null)
                {
                    BadRequest("No se encontraron alumnos para la clase");
                }

                var cantTotal = await _cAlumnosService.CountNotAssigned(adminEmail, claseId);

                var cantPages = (int)Math.Ceiling((double)listAlumnosColegio.Count() / CantPorPage);

                if (page < 0 || cantPages < page)
                {
                    return BadRequest($"No existe la pagina {page}");
                }


                var data = _mapper.Map<List<ColegioAlumnosDTO>>(listAlumnosColegio);



                var result = new DataListDTO<ColegioAlumnosDTO>();

                result.CantItems = listAlumnosColegio.Count();
                result.CurrentPage = page;
                result.DataList = data;
                result.Next = result.CurrentPage + 1 < cantPages;
                result.TotalPage = cantPages;
                return Ok(result);

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            catch (BadHttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                return BadRequest("Error durarnte la obtencion de alumnos de la clase");
            }

        }

        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<ColegiosAlumnosResultDTO>> Post([FromBody] ColegiosAlumnosDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Peticion invalido");
            }

            try
            {

                if (await _cAlumnosService.Exist(dto.AlumnoId, dto.ColegioId))
                {
                    return BadRequest("Ya se registro el Alumno en este colegio");
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
                    return Unauthorized("No puede agregar alumnos en otros colegios");
                }

                /*
                    Se verifica que exista el alumno --- no se verifica que no sea nulo porque retorna una 
                    excepcion si no encunetra
                */
                var persona = await _personaService.FindById(dto.AlumnoId);




                var roles = await _personaService.GetRolesPersona(persona);
                //se verifica que el id recibido sea de un alumno
                if (!roles.Contains("Alumno"))
                {
                    return BadRequest("No se peden asignar usuarios no asignados como alumnos a las clases");
                }

                var colAlumno = _mapper.Map<ColegiosAlumnos>(dto);

                colAlumno.CreatedBy = nameUser;
                colAlumno.Created = DateTime.Now;
                colAlumno.Deleted = false;

                await _cAlumnosService.Add(colAlumno);
                await _cAlumnosService.Save();

                return Ok(_mapper.Map<ColegiosAlumnosResultDTO>(colAlumno));
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound("El alumno no esta disponible");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }



        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var colAlumno = await _cAlumnosService.FindById(id);
                if (colAlumno == null)
                {
                    return NotFound("No encontrado");
                }

                //verificar que el administrador que hizo la peticion sea el administrador del colegio que quiere eliminar
                var adminEmail = User.FindFirstValue(ClaimTypes.Name);
                var admin = await _personaService.FindByEmail(adminEmail);
                if (admin == null)
                {
                    return NotFound("El administrador de colegio no existe");
                }

                var colegio = await _colegioService.FindByIdAdmin(admin.Id);

                if (colegio == null)
                {
                    return BadRequest("El administrador no tiene un colegio asignado");
                }

                colAlumno.Deleted = true;
                colAlumno.ModifiedBy = adminEmail;
                colAlumno.Modified = DateTime.Now;

                _cAlumnosService.Edit(colAlumno);
                await _cAlumnosService.Save();

                return Ok();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}");
                return BadRequest("Error durante el eliminado");

            }


        }
        [HttpGet]
        [Route("not/assigned/year/{idClase}")]
        public async Task<ActionResult<List<ColegioAlumnosDTO>>> NotAssigned(int idClase)
        {
            try
            {
                var adminEmail = User.FindFirstValue(ClaimTypes.Name);

                var date = DateTime.Now;

                var year = date.Year;

                var query = await _cAlumnosService.GetNotAssignedByYear(year, adminEmail, idClase);

                var queryMapper = _mapper.Map<List<ColegioAlumnosDTO>>(query.ToList())
                    .OrderBy(p => p.Apellido)
                    .ThenBy(p => p.Nombre)
                    .ThenBy(p => p.Documento);

                return Ok(queryMapper);

            }
            catch(BadHttpRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch(FileNotFoundException)
            {
                return NotFound();
            }



        }
    }
}