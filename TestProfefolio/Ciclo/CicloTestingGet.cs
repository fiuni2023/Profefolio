using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Ciclo;
using profefolio.Repository;

namespace TestProfefolio.Ciclo
{
    public class CicloTestingGet
    {
        /*
            Testea cuando se realiza un GetAll sin paginacion exitosa
        */
        [Fact]
        public async void GetAll_WithoutPage_Ok()
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);


            CicloResultDTO dto = new CicloResultDTO()
            {
                Id = 1,
                Nombre = "Primero"
            };

            CicloResultDTO dto2 = new CicloResultDTO()
            {
                Id = 2,
                Nombre = "Segundo"
            };

            profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero",
                Created = DateTime.Now,
                Deleted = false,
                CreatedBy = "123456"
            };
            var modelo2 = modelo;
            modelo2.Id = 2;
            modelo2.Nombre = "Segundo";

            List<profefolio.Models.Entities.Ciclo> ciclos = new List<profefolio.Models.Entities.Ciclo>(){
            modelo, modelo2
        };

            List<CicloResultDTO> resultDto = new List<CicloResultDTO>(){
            dto, dto2
        };

            service.Setup(a => a.GetAll()).ReturnsAsync(ciclos.AsEnumerable());

            mapper.Setup(m => m.Map<List<CicloResultDTO>>(ciclos)).Returns(resultDto);

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);
        }



        /*
            Testea cuando se realiza un GetAll sin paginacion fallida 
        */
        [Fact]
        public async void GetAll_WithoutPage_BadRequest()
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);


            CicloResultDTO dto = new CicloResultDTO()
            {
                Id = 1,
                Nombre = "Primero"
            };

            CicloResultDTO dto2 = new CicloResultDTO()
            {
                Id = 2,
                Nombre = "Segundo"
            };

            profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero",
                Created = DateTime.Now,
                Deleted = false,
                CreatedBy = "123456"
            };
            var modelo2 = modelo;
            modelo2.Id = 2;
            modelo2.Nombre = "Segundo";

            List<profefolio.Models.Entities.Ciclo> ciclos = new List<profefolio.Models.Entities.Ciclo>(){
            modelo, modelo2
        };

            List<CicloResultDTO> resultDto = new List<CicloResultDTO>(){
            dto, dto2
        };

            service.Setup(a => a.GetAll()).ThrowsAsync(new Exception("Error durante la busqueda"));

            var result = await controller.GetAll();
            var r = (BadRequestObjectResult)result.Result;

            Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error durante la busqueda", r.Value);
        }
    }
}