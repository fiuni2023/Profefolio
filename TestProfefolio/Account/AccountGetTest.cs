using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using System.Runtime.CompilerServices;
using TestProfefolio.Helpers;

namespace TestProfefolio.Account
{
    public class AccountGetTest
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

        public AccountGetTest()
        {
            _accountController =
                new AccountController
                (
                    _mapperMock.Object,
                    _personaServiceMock.Object,
                    _rolServiceMock.Object,
                    _colegioServiceMock.Object
                );

            _entityParser = new JsonParser<Persona>(pathEntities);
            _dtoResultParser = new JsonParser<PersonaResultDTO>(pathDtos);
        }

        [Fact]
        public async Task Find_By_Id_Ok_Test()
        {
            var personaResultDTO = new PersonaResultDTO
            {
                Id = "1",
                Nombre = "Carlos",
                Apellido = "Torres",
                Email = "Carlos.Torres123@mail.com",
                Genero = "M",
                Telefono = "+12 332 111",
                Documento = "293929292",
                DocumentoTipo = "CI",
                Nacimiento = new DateTime(1999, 01, 01),
                Direccion = "Avda 123"
            };

            var persona = new Persona
            {
                PasswordHash = "hash",
                Nombre = "Carlos",
                Apellido = "Torres",
                Email = "Carlos.Torres123@mail.com",
                PhoneNumber = "+12 332 111",
                UserName = "Carlos.Torres123@mail.com",
                EsM = true,
                Deleted = false,
                Id = "1",
                Direccion = "Avda 123",
                Documento = "293929292",
                DocumentoTipo = "CI",
                Nacimiento = new DateTime(1999, 01, 01)
            };

            _personaServiceMock.Setup(x => x.FindById(It.IsAny<string>()))
                .Returns<string>((id) =>
                {
                    var datas = _entityParser.ToIEnumerable();

                    var result = datas.FirstOrDefault(y => y.Id == id);

                    return Task.FromResult(result);
                  
                });
            
            _mapperMock.Setup(x => x.Map<PersonaResultDTO>(It.IsAny<Persona>()))
                .Returns(personaResultDTO);

            var result = await _accountController.Get("1");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var value = Assert.IsType<PersonaResultDTO>(okResult.Value);
            Assert.Equal(personaResultDTO, value);

        }


    }
}
