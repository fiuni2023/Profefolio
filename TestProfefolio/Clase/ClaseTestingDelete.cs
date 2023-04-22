using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Repository;
using profefolio.Models.Entities;

namespace TestProfefolio.Clase
{
    public class ClaseTestingDelete
    {
        /*
            Testea un caso exitoso de eliminacion
        */

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Delete_Ok(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> cicloService = new Mock<ICiclo>();
            Mock<IClase> claseService = new Mock<IClase>();
            Mock<IColegio> colegioService = new Mock<IColegio>();

            ClaseController controller = new ClaseController(
                mapper.Object, 
                claseService.Object, 
                cicloService.Object, 
                colegioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var clase = new profefolio.Models.Entities.Clase(){
                Id = id,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseEliminado = new profefolio.Models.Entities.Clase(){
                Id = id,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = true,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com",
                ModifiedBy = "user1",
                Modified = DateTime.Now
            };
            
            claseService.Setup(c => c.FindById(It.IsAny<int>())).ReturnsAsync(clase);

            claseService.Setup(c => c.Edit(It.IsAny<profefolio.Models.Entities.Clase>())).Returns(claseEliminado);
        
            claseService.Setup(c => c.Save()).Returns(Task.CompletedTask);

            var result = controller.Delete(id);

            Assert.IsType<OkResult>(result.Result);
        }



        /*
            Testea un caso de error en el que el ID mandado por parametro es 
            menor a cero
        */

        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-4)]
        [InlineData(-5)]
        public void Delete_InvalidId_BadRequest(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> cicloService = new Mock<ICiclo>();
            Mock<IClase> claseService = new Mock<IClase>();
            Mock<IColegio> colegioService = new Mock<IColegio>();

            ClaseController controller = new ClaseController(
                mapper.Object, 
                claseService.Object, 
                cicloService.Object, 
                colegioService.Object);

            var result = controller.Delete(id);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Identificador invalido", response.Value);
        }



        /*
            Testea un caso de fallo porque el id mandado por parametro
            no se encuentra disponible.
        */

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Delete_NotFoundId_NotFound(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> cicloService = new Mock<ICiclo>();
            Mock<IClase> claseService = new Mock<IClase>();
            Mock<IColegio> colegioService = new Mock<IColegio>();

            ClaseController controller = new ClaseController(
                mapper.Object, 
                claseService.Object, 
                cicloService.Object, 
                colegioService.Object);
            
            claseService.Setup(c => c.FindById(It.IsAny<int>()));

            var result = controller.Delete(id);

            Assert.IsType<NotFoundResult>(result.Result);
        }



        /*
            Testea un caso de fallo cuando se guarda los cambios de eliminacion
        */

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Delete_SaveChangeFailed_BadRequest(int id)
        {
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> cicloService = new Mock<ICiclo>();
            Mock<IClase> claseService = new Mock<IClase>();
            Mock<IColegio> colegioService = new Mock<IColegio>();

            ClaseController controller = new ClaseController(
                mapper.Object, 
                claseService.Object, 
                cicloService.Object, 
                colegioService.Object);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var clase = new profefolio.Models.Entities.Clase(){
                Id = id,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseEliminado = new profefolio.Models.Entities.Clase(){
                Id = id,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = true,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com",
                ModifiedBy = "user1",
                Modified = DateTime.Now
            };
            
            claseService.Setup(c => c.FindById(It.IsAny<int>())).ReturnsAsync(clase);

            claseService.Setup(c => c.Edit(It.IsAny<profefolio.Models.Entities.Clase>())).Returns(claseEliminado);
        
            claseService.Setup(c => c.Save()).ThrowsAsync(new Exception("Fallo de eliminacion"));

            var result = controller.Delete(id);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error durante la eliminacion", response.Value);
        }
    }
}