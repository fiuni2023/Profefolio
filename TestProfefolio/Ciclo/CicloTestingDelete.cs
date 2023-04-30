using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Ciclo;
using profefolio.Repository;

namespace TestProfefolio.Ciclo
{
    public class CicloTestingDelete
    {
        /*
            Testea cuando se elimina exitosamente un Ciclo
        */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async void Delete_Ok(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            profefolio.Models.Entities.Ciclo ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = id,
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


            service.Setup(c => c.FindById(It.IsAny<int>())).ReturnsAsync(ciclo);

            service.Setup(s => s.Edit(It.IsAny<profefolio.Models.Entities.Ciclo>())).Returns(ciclo);

            service.Setup(a => a.Save()).Returns(Task.CompletedTask);


            var result = await controller.Delete(id);

            Assert.IsType<OkResult>(result);
        }



        /*
            Testea cuando se elimina exitosamente un Ciclo
        */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async void Delete_NoFindId_BadRequest(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };


            service.Setup(c => c.FindById(It.IsAny<int>()));

            var result = await controller.Delete(id);

            var jsonResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Ciclo no encontrado", jsonResult.Value);
        }



        /*
            Testea cuando se elimina y ocurre un error cuando se guarda el cambio
        */
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async void Delete_FailedSave_BadRequest(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> service = new Mock<ICiclo>();
            CicloController controller = new CicloController(mapper.Object, service.Object);

            profefolio.Models.Entities.Ciclo ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = id,
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


            service.Setup(c => c.FindById(It.IsAny<int>())).ReturnsAsync(ciclo);

            service.Setup(s => s.Edit(It.IsAny<profefolio.Models.Entities.Ciclo>())).Returns(ciclo);

            service.Setup(a => a.Save()).ThrowsAsync(new Exception());


            var result = await controller.Delete(id);

            var jsonResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error durante la eliminacion", jsonResult.Value);
            
        }
    }


}