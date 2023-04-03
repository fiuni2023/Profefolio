using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ColegioProfesorController(IPersona personaService, IColegioProfesor colegioProfesorService, IColegio colegioService, IMapper mapper)
        {
            _personaService = personaService;
            _cProfService = colegioProfesorService;
            _colegioService = colegioService;
            _mapper = mapper;    
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
                if(!colegio.personas.Email.Equals(nameUser)){
                    return Unauthorized("No puede agregar Profesores en otros colegios");
                }

                /*
                    Se verifica que exista el profesor --- no se verifica que no sea nulo porque retorna una 
                    excepcion si no encunetra
                */
                var persona = await _personaService.FindById(dto.ProfesorId);



                var roles = await _personaService.GetRolesPersona(persona);
                
                //se verifica que el id recibido sea de un profesor
                if(!roles.Contains("Profesor")){
                    return BadRequest("No se pueden asignar usuarios no asignados como profesor a los colegios");
                }
                

                var colProf = _mapper.Map<ColegioProfesor>(dto);

                colProf.CreatedBy = nameUser;
                colProf.Created = DateTime.Now;
                colProf.Deleted = false;
                
                
                await _cProfService.Add(colProf);
                await _cProfService.Save();

                var colProfNew = await _cProfService.FindById(colProf.Id);
                return Ok(_mapper.Map<ColegioProfesorResultDTO>(colProfNew));
            }
            catch(FileNotFoundException e){
                Console.WriteLine(e);
                return NotFound("El Profesor no esta disponible");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado.");
            }
        }
    }
}