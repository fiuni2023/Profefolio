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
    }
}