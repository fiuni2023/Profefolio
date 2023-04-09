using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using profefolio.Controllers;
using profefolio.Models.DTOs;
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

            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((id, role) =>
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

        [Fact]
        public async Task Find_By_Id_Persona_Not_Found_Test()
        {
            _personaServiceMock.Setup(x => x.FindByIdAndRole(It.IsAny<string>(), It.IsAny<string>()))
             .Throws<FileNotFoundException>();

            var result = await _accountController.Get("-1");

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task Get_All_Ok_Test()
        {
            var personas = _entityParser.ToIEnumerable();


            _personaServiceMock.Setup(x => x.GetAllByRol(It.IsAny<string>()))
                .ReturnsAsync(personas);


            var expected = personas.ToList()
                .ConvertAll(x => new PersonaSimpleDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre
                });

            _mapperMock.Setup(x => x.Map<List<PersonaSimpleDTO>>(It.IsAny<List<Persona>>()))
                .Returns(expected);



            var result = await _accountController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var value = Assert.IsType<List<PersonaSimpleDTO>>(okResult.Value);

            Assert.Equal(expected, value);

        }

        [Fact]
        public async Task Get_All_Not_Found()
        {
            _personaServiceMock.Setup(x => x.GetAllByRol(It.IsAny<string>()))
                .Throws<FileNotFoundException>();


            var result = await _accountController.GetAll();

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task Get_All_With_Page_Ok()
        {

            var personas = _entityParser.ToIEnumerable();

            _personaServiceMock.Setup(x => x.FilterByRol(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns<int, int, string>((page, cantPerPage, role) =>
                {
                    var result = personas.Skip(page*cantPerPage).Take(cantPerPage);

                    return Task.FromResult(result);
                });

            _personaServiceMock.Setup(x => x.CountByRol(It.IsAny<string>()))
                .ReturnsAsync(22);

            _mapperMock.Setup(x => x.Map<List<PersonaResultDTO>>(It.IsAny<List<Persona>>))
                .Returns<List<Persona>>( p =>
                {
                    return p.ConvertAll(y => new PersonaResultDTO
                    {
                        Id = y.Id,
                        Nombre = y.Nombre,
                        Apellido = y.Apellido,
                        Genero = y.EsM ? "Masculino" : "Femenino",
                        Email = y.Email,
                        Telefono = y.PhoneNumber,
                        Direccion = y.Direccion,
                        DocumentoTipo = y.DocumentoTipo,
                        Documento = y.Documento
                    });
                } );


            var result0 = await _accountController.Get(0);
            var result1 = await _accountController.Get(1);

            var resultOk0 = Assert.IsType<OkObjectResult>(result0.Result);
            var resultOk1 = Assert.IsType<OkObjectResult>(result1.Result);

            var value0 = Assert.IsType<DataListDTO<PersonaResultDTO>>(resultOk0.Value);
            var value1 = Assert.IsType<DataListDTO<PersonaResultDTO>>(resultOk1.Value);

        }
    }

    
}
