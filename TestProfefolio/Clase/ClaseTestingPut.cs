using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using profefolio.Controllers;
using profefolio.Models.DTOs.Clase;
using profefolio.Repository;

namespace TestProfefolio.Clase
{
    public class ClaseTestingPut
    {
        /*
            Testea un caso exitoso de edicion de una Clase
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
        public async void Put_Ok_NoContent(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = 2023,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            claseService.Setup(c => c.FindById(id)).ReturnsAsync(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId)).ReturnsAsync(ciclo);

            colegioService.Setup(c => c.FindById(dto.ColegioId)).ReturnsAsync(colegio);

            claseService.Setup(c => c.Edit(clase)).Returns(clase);

            claseService.Setup(c => c.Save()).Returns(Task.CompletedTask);

            var result = await controller.Put(id, dto);

            Assert.IsType<NoContentResult>(result);
        }
    
    

        /*
            Testea un caso de fallo cuando el modelo es invalido
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
        public async void Put_ModelInvalid_BadRequest(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Nombre", "Propiedad invalida");
            controller.ModelState.Merge(modelState);

            var result = await controller.Put(id, dto);

            var response = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Peticion Invalida", response.Value);
        }



        /*
            Testea un caso de fallo en el que el anho mandado en el DTO es menor o igual a 1950
        */
        [Theory]
        [InlineData(1, 1800)]
        [InlineData(2, 1950)]
        [InlineData(3, 1949)]
        [InlineData(4, 1941)]
        [InlineData(5, 1910)]
        [InlineData(6, 0)]
        [InlineData(7, 2)]
        [InlineData(8, 1200)]
        public async void Put_InvalidYear_BadRequest(int id, int anho)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = anho,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = anho,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = anho,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            var result = await controller.Put(id, dto);

            var response = Assert.IsType<BadRequestObjectResult>(result);

            Assert.Equal("Anho invalido", response.Value);
        }
    
    
    
        /*
            Testea un caso de fallo cuando no se encuentra una Clase con el Id que se recibio
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
        public async void Put_IdClaseNotFound_NotFound(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = 2023,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            claseService.Setup(c => c.FindById(id));


            var result = await controller.Put(id, dto);

            var response = Assert.IsType<NotFoundObjectResult>(result);
        
            Assert.Equal("No se ha encontrado la Clase a editar", response.Value);
        }
    
    
    
    
        /*
            Testea un caso de fallo cuando no se encuentra un Ciclo con el Id que se recibio en el DTO
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
        public async void Put_IdCicloNotFound_NotFound(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = 2023,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            claseService.Setup(c => c.FindById(id)).ReturnsAsync(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId));


            var result = await controller.Put(id, dto);

            var response = Assert.IsType<NotFoundObjectResult>(result);
        
            Assert.Equal("El campo de Ciclo es invalido", response.Value);
        }





        /*
            Testea un caso de fallo cuando no se encuentra un Colegio con el Id que se recibio en el DTO
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
        public async void Put_IdColegioNotFound_NotFound(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = 2023,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            claseService.Setup(c => c.FindById(id)).ReturnsAsync(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId)).ReturnsAsync(ciclo);;

            colegioService.Setup(c => c.FindById(dto.ColegioId));

            var result = await controller.Put(id, dto);

            var response = Assert.IsType<NotFoundObjectResult>(result);
        
            Assert.Equal("El campo de Colegio es invalido", response.Value);
        }
    



        /*
            Testea un caso de fallo sucede un error durante el guardado
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
        public async void Put_SaveFailed_BadRequest(int id)
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

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var clase = new profefolio.Models.Entities.Clase()
            {
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var claseResult = new ClaseResultDTO()
            {
                Id = id,
                Anho = 2023,
                Nombre = "Primer grado",
                Turno = "Tarde",
                IdCiclo = 1,
                IdColegio = 1,
                Ciclo = "Primero",
                Colegio = "San Juan"
            };

            var ciclo = new profefolio.Models.Entities.Ciclo()
            {
                Id = 1,
                Nombre = "Primero"
            };

            var colegio = new profefolio.Models.Entities.Colegio()
            {
                Id = 1,
                Nombre = "San Juan",
                PersonaId = "123456789"
            };

            claseService.Setup(c => c.FindById(id)).ReturnsAsync(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId)).ReturnsAsync(ciclo);

            colegioService.Setup(c => c.FindById(dto.ColegioId)).ReturnsAsync(colegio);

            claseService.Setup(c => c.Edit(clase)).Returns(clase);

            claseService.Setup(c => c.Save()).ThrowsAsync(new Exception());

            var result = await controller.Put(id, dto);

            var response = Assert.IsType<BadRequestObjectResult>(result);
        
            Assert.Equal("Error durante la edicion", response.Value);
        }
    }
}