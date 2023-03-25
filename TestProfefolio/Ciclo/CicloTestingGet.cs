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
    
    
        /*
            Testea cuando se realiza un get por id de Ciclo exitosa
        */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public async void Get_ById_Ok(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);


            CicloResultDTO resultDTO = new CicloResultDTO()
            {
                Id = id,
                Nombre = "Primero"
            };


            profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
            {
                Id = id,
                Nombre = "Primero",
                Created = DateTime.Now,
                Deleted = false,
                CreatedBy = "juan"
            };

            service.Setup(a => a.FindById(id)).ReturnsAsync(modelo);
            mapper.Setup(m => m.Map<CicloResultDTO>(modelo)).Returns(resultDTO);

            var result = await controller.Get(id);

            var jsonResult = Assert.IsType<OkObjectResult>(result.Result);
            var cicloResult = Assert.IsType<CicloResultDTO>(jsonResult.Value);
            Assert.Same(resultDTO, cicloResult);
        }
    
    }
}