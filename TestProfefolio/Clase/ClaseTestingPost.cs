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
    public class ClaseTestingPost
    {
        /*
            Testea un caso exitoso de Guardado
        */
        [Fact]
        public async void Post_Ok()
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
                Id = 1,
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

            mapper.Setup(m => m.Map<profefolio.Models.Entities.Clase>(dto)).Returns(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId)).ReturnsAsync(ciclo);

            colegioService.Setup(c => c.FindById(dto.ColegioId)).ReturnsAsync(colegio);

            claseService.Setup(c => c.Add(clase)).ReturnsAsync(clase);

            claseService.Setup(c => c.Save()).Returns(Task.CompletedTask);

            mapper.Setup(m => m.Map<ClaseResultDTO>(clase)).Returns(claseResult);

            var result = await controller.Post(dto);

            Assert.IsType<OkObjectResult>(result.Result);
        }




        /*
            Testea un caso de fallo cuando el modelo mandado es invalido
        */
        [Fact]
        public async void Post_ModelInvalid_BadRequest()
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

            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Nombre", "Propiedad invalida");
            controller.ModelState.Merge(modelState);

            var dto = new ClaseDTO()
            {
                Nombre = "Primer grado",
                Turno = "Tarde",
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1
            };

            var result = await controller.Post(dto);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Datos invalidos", response.Value);
        }
    
    
        
        /*
            Testea un caso de fallo en el que no es encontrado un ciclo con el id mandado en el dto
        */
        [Fact]
        public async void Post_IdCicloNotFound_BadRequest()
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

            mapper.Setup(m => m.Map<profefolio.Models.Entities.Clase>(dto)).Returns(clase);

            cicloService.Setup(c => c.FindById(dto.CicloId));

            var result = await controller.Post(dto);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal("El campo de Ciclo es invalido", response.Value);
        }

    }
}