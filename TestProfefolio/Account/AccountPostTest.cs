using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using System.Security.Claims;

namespace TestProfefolio.Account
{
    public class AccountPostTest
    {
        private Mock<IPersona> _personaServiceMock = new();
        private Mock<IRol> _rolServiceMock = new();
        private Mock<IMapper> _mapperMock = new();
        private Mock<IColegio> _colegioSeriveMock = new();
        private AccountController _accountController;

        public AccountPostTest()
        {
            _accountController = new AccountController
                (
                _mapperMock.Object,
                _personaServiceMock.Object, 
                _rolServiceMock.Object,
                _colegioSeriveMock.Object
                );

            var userId = "testUserId";
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) });
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext();
            httpContext.User = claimsPrincipal;

            _accountController.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext,
            };

        }

        [Fact]
        public async Task Post_WithValidData_ReturnsOk()
        {
            var personaEnt = new Persona
            {
                Id = "11331313131441fae--4reaf",
                Nombre = "John",
                Apellido ="Due",
                Email = "John.Due123@mail.com",
                NormalizedEmail = "JOHN.DUE123@MAIL.COM",
                UserName = "John.Due123@mail.com",
                PasswordHash = "Hashed",
                PhoneNumber = "1234567890",
                CreatedBy =  "Me",
                EsM = true,
                NormalizedUserName = "JOHN.DUE123@MAIL.COM",
                Nacimiento = new DateTime(1999, 01, 01),
                Created = DateTime.Now,
                Documento = "12331211",
                DocumentoTipo = "CI",
                Direccion = "Avda 123"

            };

            var personaDto = new PersonaDTO
            {
                Nombre = "John",
                Apellido = "Due",
                Email = "John.Due123@mail.com",
                Password = "John.Due123",
                Genero = "M",
                ConfirmPassword = "John.Due123",
                Documento = "12331211",
                DocumentoTipo = "CI",
                Telefono = "1234567890",
                Direccion = "Avda 123"
            };

            var personaResultDTO = new PersonaResultDTO
            {
                Id = "11331313131441fae--4reaf",
                Nombre = "John",
                Apellido = "Due",
                Email = "John.Due123@mail.com",
                Genero = "M",
                Documento = "12331211",
                DocumentoTipo = "CI",
                Telefono = "1234567890",
                Direccion = "Avda 123"
            };

            _mapperMock.Setup(x => x.Map<Persona>(It.IsAny<PersonaDTO>()))
                .Returns(personaEnt);

            _personaServiceMock.Setup(x => x.CreateUser(It.IsAny<Persona>(), It.IsAny<string>()))
                .ReturnsAsync(personaEnt);

            _rolServiceMock.Setup(x => x.AsignToUser(It.IsAny<string>(), It.IsAny<Persona>()))
                .ReturnsAsync(true);


            _mapperMock.Setup(x => x.Map<PersonaResultDTO>(It.IsAny<Persona>()))
                .Returns(personaResultDTO);

            var result = await _accountController.Post(personaDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var value = Assert.IsType<PersonaResultDTO>(okResult.Value);

            Assert.Equal(value, personaResultDTO);


        }
    }
}
