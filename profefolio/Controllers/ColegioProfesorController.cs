using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        private const int CantPorPage = 20;


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
                var colProf = await _cProfService.FindAllByIdColegio(page, CantPorPage, idColegio);

                var cantItmed = await _cProfService.Count(idColegio); 

                int cantPages = (int)Math.Ceiling((double)cantItmed / (double)CantPorPage);


                var result = new DataListDTO<ColegioProfesorByIdResult>();

                if (page >= cantPages)
                {
                    return BadRequest($"No existe la pagina: {page} ");
                }

                result.CantItems = cantItmed;
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
                var colegioProfesores = await _cProfService.FindAllByIdColegio(idColegio);
                if(colegioProfesores == null){
                    return NotFound("El Colegio no fue encontrado");
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

        
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult> Delete(int id){
            try{
                //Obtenemos la relacion Colegio-Profesor
                var colProf = await _cProfService.FindById(id);
                if(colProf == null){
                    return NotFound("No se puede eliminar no esta disponible");
                }


                var nameUser = User.FindFirstValue(ClaimTypes.Name);

                var admin = await _personaService.FindByEmail(nameUser);
                if(admin == null){
                    return BadRequest("Error. El administradir no existe");
                }
                
                
                //se verifica que exista un colegio en donde este asignado el administrador 
                Colegio colegio = await _colegioService.FindByIdAdmin(admin.Id);
                if (colegio == null)
                {
                    return NotFound("El colegio no esta disponible");
                }

                //se verifica que el administrador tenga el mismo email que el del administrador que hizo la peticion
                if (!colegio.personas.Email.Equals(nameUser))
                {
                    return Unauthorized("No puede eliminar Profesores en otros colegios");
                }

                colProf.Deleted = true;
                colProf.Modified = DateTime.Now;
                colProf.ModifiedBy = nameUser;

                _cProfService.Edit(colProf);
                await _cProfService.Save();

                return Ok();

            }catch(Exception e){
                Console.WriteLine($"{e}");
                return BadRequest("Error de servidor mientras se intentaba eliminar");
            }
        }
    }
}