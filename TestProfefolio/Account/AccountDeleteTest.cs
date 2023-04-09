using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
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
        private readonly string pathEntities = "../../../Account/Data/PersonasEnt.json";
        private readonly JsonParser<Persona> _entityParser;
        private IEnumerable<Persona> _personas; 

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
            _personas = _entityParser.ToIEnumerable();
        }

        [Fact]
        public async Task Delete_Test_Ok()
        {
            _personaServiceMock.Setup(x => x.DeleteByUserAndRole(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>((id, role) =>
                {
                    _personas = _personas.ToList()
                        .ConvertAll(x =>
                        {
                            if (x.Id.Equals(id))
                            {
                                x.Deleted = true;
                            }

                            return x;
                            
                        });

                    var deleted = _personas.FirstOrDefault(x => x.Id == id);

                    return Task.FromResult(deleted.Deleted);

                });

            var result = await _accountController.Delete("1");

            Assert.IsType<OkResult>(result);
                
        }
    }
}
