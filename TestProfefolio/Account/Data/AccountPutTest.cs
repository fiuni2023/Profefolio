using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using TestProfefolio.Helpers;

namespace TestProfefolio
{
    public class AccountPutTest
    {
        private readonly Mock<IPersona> _personaServiceMock = new();
        private readonly Mock<IRol> _rolServiceMock = new();
        private readonly Mock<IColegio> _colegioServiceMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly AccountController _accountController;
        private readonly string pathDtos = "../../../Account/Data/PersonasDTO.json";
        private readonly string pathEntities = "../../../Account/Data/PersonasEnt.json";
        private readonly JsonParser<Persona> _entityParser;
        private readonly JsonParser<PersonaResultDTO> _dtoResultParser;
        private readonly IEnumerable<PersonaResultDTO> _personasDto;

        public AccountPutTest()
        {
            var userId = "testUserId";
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) });
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext();
            httpContext.User = claimsPrincipal;


            _accountController =
               new AccountController
               (
                   _mapperMock.Object,
                   _personaServiceMock.Object,
                   _rolServiceMock.Object,
                   _colegioServiceMock.Object
               );

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

            _entityParser = new JsonParser<Persona>(pathEntities);
            _dtoResultParser = new JsonParser<PersonaResultDTO>(pathDtos);
        }

        [Fact]
        public async Task Edit_Persona_Test_Ok()
        {

            var edit = new PersonaEditDTO()
            {
                Nombre = "Carlos Cristobal",
                Apellido = "Torres Carballo",
                Documento = "2222222222",
                DocumentoTipo = "Cedula",
                Direccion = "ddd 444 fff",
                Nacimiento = new DateTime(1999, 07, 10),
                Telefono = "eeee",
                Genero = "M",
                Email = "Carlos.C.Torres.C@mail.com"
            };

            var result = new Persona();
            result.Nombre = edit.Nombre;
            result.Apellido = edit.Apellido;
            result.Documento = edit.Documento;
            result.Direccion = edit.Direccion;
            result.DocumentoTipo = edit.DocumentoTipo;
            result.Nacimiento = edit.Nacimiento;
            result.PhoneNumber = edit.Telefono;
            result.EsM = true;
            result.Email = edit.Email;



            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) =>
                {
                    var db = _entityParser.ToIEnumerable();
                    var query = db.First(c => c.Id.Equals(x));


                    var r = Task.FromResult(query);
                    return r;
                }
            );

            _personaServiceMock.Setup(x => x.ExistMail(It.IsAny<string>()))
                .ReturnsAsync(false);



            _personaServiceMock.Setup(x => x.EditProfile(It.IsAny<Persona>()))
                .Returns<Persona>(x =>
                {
                    result.Id = x.Id;
                    return Task.FromResult(result);
                });


            _mapperMock.Setup(x => x.Map<PersonaResultDTO>(It.IsAny<Persona>()))
                .Returns<Persona>(x =>
                {
                    var db = _dtoResultParser.ToIEnumerable();

                    if (db == null) return new PersonaResultDTO();

                    return db.First(z =>
                    {
                        if (z.Id == null) return false;
                        else
                        {
                            return z.Id.Equals(x.Id);
                        }
                    });
                });

            var response = await _accountController.Put("1", edit);

            var ok = Assert.IsType<OkObjectResult>(response.Result);

            var value = Assert.IsType<PersonaResultDTO>(ok.Value);

            Assert.NotNull(value);
            Assert.Equal("1", value.Id);

        }

        [Fact]
        public async Task Edit_Persona_Test_Not_Found()
        {

            var edit = new PersonaEditDTO()
            {
                Nombre = "Carlos Cristobal",
                Apellido = "Torres Carballo",
                Documento = "2222222222",
                DocumentoTipo = "Cedula",
                Direccion = "ddd 444 fff",
                Nacimiento = new DateTime(1999, 07, 10),
                Telefono = "eeee",
                Genero = "M",
                Email = "Carlos.C.Torres.C@mail.com"
            };

            var result = new Persona();
            result.Nombre = edit.Nombre;
            result.Apellido = edit.Apellido;
            result.Documento = edit.Documento;
            result.Direccion = edit.Direccion;
            result.DocumentoTipo = edit.DocumentoTipo;
            result.Nacimiento = edit.Nacimiento;
            result.PhoneNumber = edit.Telefono;
            result.EsM = true;
            result.Email = edit.Email;



            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<FileNotFoundException>();


            var response = await _accountController.Put("1", edit);

            Assert.IsType<NotFoundResult>(response.Result);
        }


        [Fact]
        public async Task Edit_Persona_Test_BadRequest()
        {

            var edit = new PersonaEditDTO()
            {
                Nombre = "Carlos Cristobal",
                Apellido = "Torres Carballo",
                Documento = "2222222222",
                DocumentoTipo = "Cedula",
                Direccion = "ddd 444 fff",
                Nacimiento = new DateTime(1999, 07, 10),
                Telefono = "eeee",
                Genero = "M",
                Email = "Carlos.C.Torres.C@mail.com"
            };

            var result = new Persona();
            result.Nombre = edit.Nombre;
            result.Apellido = edit.Apellido;
            result.Documento = edit.Documento;
            result.Direccion = edit.Direccion;
            result.DocumentoTipo = edit.DocumentoTipo;
            result.Nacimiento = edit.Nacimiento;
            result.PhoneNumber = edit.Telefono;
            result.EsM = true;
            result.Email = edit.Email;



            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) =>
                {
                    var db = _entityParser.ToIEnumerable();
                    var query = db.First(c => c.Id.Equals(x));


                    var r = Task.FromResult(query);
                    return r;
                }
            );

            _personaServiceMock.Setup(x => x.ExistMail(It.IsAny<string>()))
                .ReturnsAsync(true);



            _personaServiceMock.Setup(x => x.EditProfile(It.IsAny<Persona>()))
                .Returns<Persona>(x =>
                {
                    result.Id = x.Id;
                    return Task.FromResult(result);
                });


            _mapperMock.Setup(x => x.Map<PersonaResultDTO>(It.IsAny<Persona>()))
                .Returns<Persona>(x =>
                {
                    var db = _dtoResultParser.ToIEnumerable();

                    if (db == null) return new PersonaResultDTO();

                    return db.First(z =>
                    {
                        if (z.Id == null) return false;
                        else
                        {
                            return z.Id.Equals(x.Id);
                        }
                    });
                });

            var response = await _accountController.Put("1", edit);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact] 
        public async Task Change_Password_Ok_Test()
        {
            var changePassword = new ChangePasswordDTO
            {
                Password = "Carlos.T0rres123",
                ConfirmPassword = "Carlos.T0rres123"
            };

            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) =>
                {
                    var db = _entityParser.ToIEnumerable();
                    var query = db.First(c => c.Id.Equals(x));


                    var r = Task.FromResult(query);
                    return r;
                }
            );

            _personaServiceMock.Setup(x => x.ChangePassword(It.IsAny<Persona>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await _accountController.ChangePassword("1", changePassword);

            Assert.IsType<OkResult>(result);
        }
        
        [Fact] 
        public async Task Change_Password_BadRequest_Test()
        {
            var changePassword = new ChangePasswordDTO
            {
                Password = "Carlos.T0rres123",
                ConfirmPassword = "Carlos.T0rres123"
            };

            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((x, y) =>
                {
                    var db = _entityParser.ToIEnumerable();
                    var query = db.First(c => c.Id.Equals(x));


                    var r = Task.FromResult(query);
                    return r;
                }
            );

            _personaServiceMock.Setup(x => x.ChangePassword(It.IsAny<Persona>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            var result = await _accountController.ChangePassword("1", changePassword);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact] 
        public async Task Change_Password_Not_Found_Test()
        {
            var changePassword = new ChangePasswordDTO
            {
                Password = "Carlos.T0rres123",
                ConfirmPassword = "Carlos.T0rres123"
            };

            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<FileNotFoundException>();

            

            var result = await _accountController.ChangePassword("1", changePassword);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}