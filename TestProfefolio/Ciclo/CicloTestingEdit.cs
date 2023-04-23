using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Ciclo;
using profefolio.Repository;

namespace TestProfefolio.Ciclo
{
    public class CicloTestingEdit
    {
        /*
            Testea cuando se edita exitosamente el nuevo Ciclo
        */
        [Fact]
        public async void Edit_Ok_NoContent()
        {
            int id = 1;
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            CicloDTO dto = new CicloDTO()
            {
                Nombre = "Primero"
            };

            profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero",
                Created = DateTime.Now,
                Deleted = false,
                CreatedBy = "123456"
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            service.Setup(a => a.ExisitOther(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            service.Setup(a => a.FindById(It.IsAny<int>())).ReturnsAsync(modelo);


            service.Setup(s => s.Edit(It.IsAny<profefolio.Models.Entities.Ciclo>())).Returns(modelo);

            service.Setup(a => a.Save()).Returns(Task.CompletedTask);


            var result = await controller.Put(id, dto);

            Assert.IsType<NoContentResult>(result);
        }



        /*
            Testea cuando se edita y sucede un error porque ya existe un Ciclo con el nuevo nombre
        */
        [Fact]
        public async void Edit_ExistOtherWithEqualName_BadRequest()
        {
            int id = 1;
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            CicloDTO dto = new CicloDTO()
            {
                Nombre = "Primero"
            };


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            service.Setup(a => a.ExisitOther(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(true);

            var result = await controller.Put(id, dto);

            var resultBad = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Ya existe un Ciclo con ese nombre", resultBad.Value);
        }


        /*
            Testea cuando se edita y el id mandado como parametro no es encontrado
        */
        [Fact]
        public async void Edit_IdNotFound_BadRequest()
        {
            int id = 1;
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            CicloDTO dto = new CicloDTO()
            {
                Nombre = "Primero"
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            service.Setup(a => a.ExisitOther(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            service.Setup(a => a.FindById(It.IsAny<int>()));

            var result = await controller.Put(id, dto);

            var resultBad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("El Ciclo no encontrado", resultBad.Value);
            

        }



        /*
            Testea cuando se edita y sucede un error durante el guardado
        */
        [Fact]
        public async void Edit_SaveError_BadRequest()
        {
            int id = 1;
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            CicloDTO dto = new CicloDTO()
            {
                Nombre = "Primero"
            };

            profefolio.Models.Entities.Ciclo modelo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero",
                Created = DateTime.Now,
                Deleted = false,
                CreatedBy = "123456"
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            service.Setup(a => a.ExisitOther(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(false);

            service.Setup(a => a.FindById(It.IsAny<int>())).ReturnsAsync(modelo);

            service.Setup(a => a.Edit(It.IsAny<profefolio.Models.Entities.Ciclo>())).Returns(modelo);
            
            service.Setup(a => a.Save()).ThrowsAsync(new Exception("Error durante el guardado"));
            
            var result = await controller.Put(id, dto);

            var resultBad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error durante la edicion", resultBad.Value);

        }


    }
}