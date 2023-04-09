using AutoMapper;
using profefolio.Controllers;
using profefolio.Models.DTOs.Persona;
using profefolio.Models.Entities;
using profefolio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProfefolio.Helpers;

namespace TestProfefolio.Account
{
    public class AccountDeleteTest
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

        public AccountDeleteTest()
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


    }
}
