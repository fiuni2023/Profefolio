using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.EntityFrameworkCore;

namespace profefolio.Controllers
{
    [ApiController]
    /* [Authorize(Roles = "Administrador")] */
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersona _personasService;
        private readonly IRol _rolService;

        public ProfesorController(IMapper mapper, IPersona personasService, IRol rolService)
        {
            _mapper = mapper;
            _personasService = personasService;
            _rolService = rolService;
        }

        /*public ProfesorController()
        {
            
        }*/

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonaResultDTO>>> GetProfesor()
        {
            var profesores = _personasService.GetAll();
            if (profesores == null)
            {
                return BadRequest("");
            }

            var profesDTO = profesores.Select(p => _mapper.Map<PersonaResultDTO>(p));

            return Ok(profesDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDTO>> GetProfesor(string id)
        {
            if (id != null && id.Length > 0)
            {
                var profesor = await _personasService.FindById(id);
                if (profesor != null)
                {
                    return Ok(_mapper.Map<ProfesorDTO>(profesor));
                }
                else
                {
                    return NotFound("No se encontro al profesor");
                }
            }
            else
            {
                return (BadRequestObjectResult)BadRequest("ID invalido");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonaResultDTO>> CreateProfesor(PersonaDTO dto)
        {
            if (dto.Password == null)
            {
                return BadRequest("Falta el Password");
            }
            else if (dto.ConfirmPassword == null)
            {
                return BadRequest("Falta confirmacion de Password");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("");
            }

            string mailLogged = await _personasService.UserLogged();

            var entity = _mapper.Map<Persona>(dto);
            entity.Deleted = false;
            entity.CreatedBy = mailLogged;

            var saved = await _personasService.CreateUser(entity, dto.Password);
            if (await _rolService.AsignToUser("Profesor", saved))
            {
                return Ok(_mapper.Map<PersonaResultDTO>(saved));
            }

            return BadRequest($"Error al crear el Profesor ${dto.Email}");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> PutProfesor(string id, PersonaDTO dto)
        {
            if (dto.Password != null)
            {
                if (ModelState.IsValid)
                {
                    var profesor = _mapper.Map<Persona>(dto);
                    if (profesor == null)
                    {
                        return BadRequest("Error al tratar de crear");
                    }

                    profesor.Id = id;
                    try
                    {
                        Persona p = await _personasService.EditProfile(id, profesor);
                        if (p != null)
                        {
                            await _personasService.Save();
                            return NoContent();
                        }

                        return NotFound("No existe el profesor con el id indicado");
                    }
                    catch (FileNotFoundException e)
                    {
                        return BadRequest("No se encontro el registro a editar");
                    }

                    
                }
                else
                {
                    return BadRequest("");
                }
            }
            else
            {
                return BadRequest("Error, no se tiene contraseña");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> removeById(string id)
        {
            if (id != null && id.Length > 0 && await _personasService.DeleteUser(id))
            {
                if (_personasService.Save() != null)
                {
                    return Ok("Eliminacion exitosa!!!");
                }

                return Conflict("Problemas al eliminar");
            }

            return BadRequest("No existe el profesor");
        }
    }
}