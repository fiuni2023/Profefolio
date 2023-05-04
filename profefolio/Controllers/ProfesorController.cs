using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profefolio.Models.DTOs;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using profefolio.Helpers;
using profefolio.Models.DTOs.ColegioProfesor;
namespace profefolio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPersona _personasService;
        private readonly IRol _rolService;
        private readonly IColegioProfesor _colegioProfesor;
        private static int CantPorPage => Constantes.CANT_ITEMS_POR_PAGE;
        private readonly IProfesor _profesorService;

        private const string PROFESOR_ROLE = "Profesor";


        public ProfesorController(IMapper mapper, IPersona personasService, IRol rolService, IColegioProfesor colProf, IProfesor profesorService)
        {
            _mapper = mapper;
            _personasService = personasService;
            _rolService = rolService;
            _colegioProfesor = colProf;
            _profesorService = profesorService;
        }


        [HttpGet("page/{page:int}")]
        [Authorize(Roles = "Administrador de Colegio")]
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
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
        public async Task<ActionResult<PersonaResultDTO>> Get(string id)
        {
            if (id.Length > 0)
            {
                try
                {
                    var adminEmail = User.FindFirstValue(ClaimTypes.Name);
                    var userRole = User.FindFirstValue(ClaimTypes.Role);
                    var profesor = await _personasService.FindByIdAndRole(id, PROFESOR_ROLE);


                    if (profesor != null && PROFESOR_ROLE.Equals(userRole) && adminEmail.Equals(profesor.Email))
                    {
                        /* se verifica si el usuario es un profesor y si es asi se verifica su email con el profesor 
                            obtenido y si son iguales se retorna el valor*/
                        return Ok(_mapper.Map<PersonaResultDTO>(profesor));
                    }
                    else if (profesor == null || !(await _colegioProfesor.Exist(profesor.Id, adminEmail)))
                    {
                        //verificar que el profesor exista en la relacion colegioProfesor por medio de su id y el email del administrador
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


        [HttpGet("MisProfesores/")]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<List<PersonaSimpleDTO>>> GetAllProfesorOfAdmin()
        {

            try
            {
                var adminEmail = User.FindFirstValue(ClaimTypes.Name);

                var admin = await _personasService.FindByEmail(adminEmail);
                if (admin != null && admin.Colegio != null)
                {
                    var profesores = await _profesorService.FindAllProfesoresOfColegio(admin.Colegio.Id);
                    if (profesores != null)
                    {
                        return Ok(_mapper.Map<List<PersonaSimpleDTO>>(profesores));
                    }
                    return BadRequest("Error al obtener los profesores");
                }
                return BadRequest("Problemas al comprobar sus credenciales");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Error inesperado durante la busqueda");
            }

        }


        [HttpPost]
        [Authorize(Roles = "Administrador de Colegio")]
        public async Task<ActionResult<ColegioProfesorResultOfCreatedDTO>> Post([FromBody] PersonaDTO dto)
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

            if (await _personasService.ExistMail(dto.Email))
            {
                return BadRequest("El email al cual quiere registrarse ya existe");
            }

            try
            {
                var adminEmail = User.FindFirstValue(ClaimTypes.Name);
                var admin = await _personasService.FindByEmail(adminEmail);

                if (admin == null || admin.Colegio == null)
                {
                    return BadRequest("Hay problemas con sus credenciales");
                }
                else
                {
                    var result = await _profesorService.Add(entity, dto.Password, PROFESOR_ROLE, admin.Colegio.Id);


                    if (result.resultado != null)
                    {
                        return Ok(result.resultado);
                    }
                    if (result.ex != null)
                    {
                        return BadRequest(result.ex.Message);
                    }
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
            catch(FileNotFoundException e)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error durante el guardado");
            }

            return BadRequest($"Error al crear al Usuario ${dto.Email}");
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador de Colegio")]
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

                    var persona = await _personasService.FindByIdAndRole(id, PROFESOR_ROLE);
                    if (persona == null)
                    {
                        return NotFound("No se encontro el profesor");
                    }
                    var adminEmail = User.FindFirstValue(ClaimTypes.Name);

                    // se verifica que el profesor sea del colegio del administrador
                    if (!(await _colegioProfesor.Exist(id, adminEmail)))
                    {
                        return BadRequest("No pertenece a su colegio");
                    }
                    if ((!persona.Email.Equals(dto.Email)) && await _personasService.ExistMail(dto.Email))
                    {
                        return BadRequest("El email nuevo que queres actualizar ya existe");
                    }

                    MapOldToNew(persona, dto, adminEmail);

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
        [Authorize(Roles = "Administrador de Colegio,Profesor")]
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

                bool result = await _personasService.DeleteByUserAndRole(id, PROFESOR_ROLE);
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