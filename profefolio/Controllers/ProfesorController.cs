using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace profefolio.Controllers
{
    [ApiController]
    [Authorize(Roles = "Administrador de Colegio,Profesor")]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersona _personasService;
        private readonly IRol _rolService;
        private readonly IColegioProfesor _colegioProfesor;
        private const int CantPorPage = 20;

        private const string PROFESOR_ROLE = "Profesor";


        public ProfesorController(IMapper mapper, IPersona personasService, IRol rolService, IColegioProfesor colProf)
        {
            _mapper = mapper;
            _personasService = personasService;
            _rolService = rolService;
            _colegioProfesor = colProf;
        }


        [HttpGet("page/{page:int}")]
        public async Task<ActionResult<DataListDTO<PersonaResultDTO>>> Get(int page)
        {
            if (page < 0)
            {
                return BadRequest("El numero de pagina debe ser mayor o igual que cero");
            }

            var profesores = await _personasService.FilterByRol(page, CantPorPage, PROFESOR_ROLE);


            int cantPages = (int)Math.Ceiling((double)(await _personasService.CountByRol(PROFESOR_ROLE)) / (double)CantPorPage);


            var result = new DataListDTO<PersonaResultDTO>();

            if (page >= cantPages)
            {
                return BadRequest($"No existe la pagina: {page}");
            }
            var enumerable = profesores as Persona[] ?? profesores.ToArray();
            result.CantItems = enumerable.Length;
            result.CurrentPage = page;
            result.Next = result.CurrentPage + 1 < cantPages;
            result.DataList = _mapper.Map<List<PersonaResultDTO>>(enumerable.ToList());
            result.TotalPage = cantPages;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> Get(string id)
        {
            if (id.Length > 0)
            {
                try
                {
                    var adminEmail = User.FindFirstValue(ClaimTypes.Name); 
                    var profesor = await _personasService.FindByIdAndRole(id, PROFESOR_ROLE);
                    //verificar que el profesor exista en la relacion colegioProfesor por medio de su id y el email del administrador
                    if(profesor!=null || ! (await _colegioProfesor.Exist(profesor.Id, adminEmail))){
                        return NotFound("No se encontro al profesor");
                    }

                    return Ok(_mapper.Map<PersonaResultDTO>(profesor));
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine((e.Message));
                    return NotFound("No se encontro al profesor");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest("Error inesperado");
                }
            }
            else
            {
                return BadRequest("ID invalido");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PersonaResultDTO>> Post([FromBody] PersonaDTO dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Nacimiento > DateTime.Now)
            {
                return BadRequest("El nacimiento no puede ser mayor a la fecha de hoy");
            }

            if (dto.Genero == null)
            {
                return BadRequest("El genero no puede ser nulo");
            }

            if (!(dto.Genero.Equals("M") || dto.Genero.Equals("F")))
            {
                return BadRequest("Solo se aceptan valores F para femenino y M para masculino");
            }

            if (dto.Password == null)
            {
                return BadRequest("Falta el Password");
            }

            if (dto.ConfirmPassword == null)
            {
                return BadRequest("Falta confirmacion de Password");
            }


            var name = User.FindFirstValue(ClaimTypes.Name);
            var entity = _mapper.Map<Persona>(dto);

            entity.Deleted = false;
            entity.CreatedBy = name;

            try
            {
                var saved = await _personasService.CreateUser(entity, dto.Password);

                if (await _rolService.AsignToUser(PROFESOR_ROLE, saved))
                {
                    return Ok(_mapper.Map<PersonaResultDTO>(saved));
                }
            }
            catch (BadHttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Formato invalido de constraseña. Debe contener mayusculas, minusculas, numeros y caracteres.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado");
            }

            return BadRequest($"Error al crear al Usuario ${dto.Email}");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<PersonaResultDTO>> Put(string id, [FromBody] PersonaEditDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Nacimiento > DateTime.Now)
                {
                    return BadRequest("El nacimiento no puede ser mayor a la fecha de hoy");
                }
                if (dto.Genero == null)
                {
                    return BadRequest("Se tiene que incluir el genero");
                }
                if (!(dto.Genero.Equals("M") || dto.Genero.Equals("F")))
                {
                    return BadRequest("Solo se aceptan valores F para femenino y M para masculino");
                }
                if (dto.Email == null)
                {
                    return BadRequest("No se mando el email");
                }
                try
                {
                    //if (dto.Password == null) return BadRequest("Password es requerido");

                    var persona = await _personasService.FindById(id);

                    var name = User.FindFirstValue(ClaimTypes.Name);

                    if ((!persona.Email.Equals(dto.Email)) && await _personasService.ExistMail(dto.Email))
                    {
                        return BadRequest("El email nuevo que queres actualizar ya existe");
                    }
                    
                    MapOldToNew(persona, dto, name);
                    //var personaNew = _mapper.Map<Persona>(dto);


                    var query = await _personasService.EditProfile(persona);

                    return query != null ? Ok(_mapper.Map<PersonaResultDTO>(query)) : BadRequest("Error al actualizar!!!");
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    return NotFound("No se encontro el registro con el identificador indicado");
                }
                catch (BadHttpRequestException e)
                {
                    _personasService.Dispose();
                    Console.WriteLine(e.Message);
                    return BadRequest("El email que desea actualizar ya existe");
                }
                catch (Exception e)
                {
                    _personasService.Dispose();
                    Console.WriteLine(e.Message);
                    return Conflict(e.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut]
        [Route("change/password/{id}")]
        public async Task<ActionResult> ChangePassword(string id, [FromBody] Models.DTOs.Auth.ChangePasswordDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (dto.Password == null)
            {
                return BadRequest("La contraseña no puede ser nula");
            }
            if (dto.ConfirmPassword == null)
            {
                return BadRequest("La contraseña de confirmacion no puede ser nula");
            }
            if (dto.Password.Length < 8 && dto.ConfirmPassword.Length < 8)
            {
                return BadRequest("La contraseña y la  confirmacion de la contraseña tienen que tener por lo menos 8 caracteres.");
            }
            try
            {
                var personaOld = await _personasService.FindById(id);

                //Console.WriteLine(personaOld.Id);

                if (await _personasService.ChangePassword(personaOld, dto.Password))
                {
                    return Ok();
                }

                return BadRequest("No se pudo actualizar");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Formato invalido de constraseña. Debe contener mayusculas, minusculas, numeros y caracteres.");
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                bool result = await _personasService.DeleteUser(id);
                return result ? Ok() : NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound();
            }
        }

        private void MapOldToNew(Persona persona, PersonaEditDTO dto, string userId)
        {
            persona.Nombre = dto.Nombre;
            persona.Apellido = dto.Apellido;
            persona.Email = dto.Email;
            persona.EsM = dto.Genero.Equals("M");
            persona.Nacimiento = dto.Nacimiento;
            persona.Documento = dto.Documento;
            persona.Direccion = dto.Direccion;
            persona.Modified = DateTime.Now;
            persona.DocumentoTipo = dto.DocumentoTipo;
            persona.ModifiedBy = userId;
            persona.PhoneNumber = dto.Telefono;
            persona.UserName = dto.Email;
            persona.NormalizedUserName = dto.Email.ToUpper();
            persona.NormalizedEmail = dto.Email.ToUpper();
        }
    }
}