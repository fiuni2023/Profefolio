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
using profefolio.Models.DTOs.Clase;
using profefolio.Repository;
using profefolio.Models.DTOs;

namespace TestProfefolio.Clase
{
    public class ClaseTestingGet
    {
        /*
            Testea un caso exitoso de Get por Id de Colegio
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
        public async void GetAllByIdColegio_Ok(int idColegio)
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


            var clase1 = new profefolio.Models.Entities.Clase()
            {
                Id = 1,
                Anho = 2023,
                CicloId = 1,
                ColegioId = idColegio,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var clase2 = new profefolio.Models.Entities.Clase()
            {
                Id = 2,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Segundo grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var clases = new List<profefolio.Models.Entities.Clase>(){
                clase1, clase2
            };

            var resultClases = new List<ClaseResultSimpleDTO>(){
                new ClaseResultSimpleDTO() {
                    Id = 1,
                    Ciclo = "Primero",
                    CicloId = 1,
                    Anho = 2023,
                    Turno = "Tarde",
                    Nombre = "Primer grado"
                },
                new ClaseResultSimpleDTO() {
                    Id = 2,
                    Ciclo = "Primero",
                    CicloId = 1,
                    Anho = 2023,
                    Turno = "Tarde",
                    Nombre = "Segundo grado"
                }
            };
            claseService.Setup(m => m.GetByIdColegio(idColegio)).ReturnsAsync(clases);

            mapper.Setup(m => m.Map<List<ClaseResultSimpleDTO>>(clases)).Returns(resultClases);
            var result = await controller.GetAllByColegioId(idColegio);

            Assert.IsType<OkObjectResult>(result.Result);
        }
    
    
    
        /*
            Testea el caso de que el Id de Colegio recibido es menor a cero 
            y retorna una error de NotFound
        */
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        [InlineData(-4)]
        [InlineData(-5)]
        [InlineData(-6)]
        [InlineData(-7)]
        [InlineData(-8)]
        public async void GetAllByIdColegio_IdColegioInvalid_NotFound(int idColegio)
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


            var result = await controller.GetAllByColegioId(idColegio);

            Assert.IsType<NotFoundResult>(result.Result);
        }
    

        /*
            Testea el caso de error al momento de obtener 
            las clases por medio del id del Colegio
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
        public async void GetAllByIdColegio_GetFailed_BadRequest(int idColegio)
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

            claseService.Setup(m => m.GetByIdColegio(idColegio)).ThrowsAsync(new Exception());

            var result = await controller.GetAllByColegioId(idColegio);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
        
            Assert.Equal("Error durante la busqueda", response.Value);
        }




        /*
            Testea un caso exitoso de Get por Id de Colegio con paginacion
        */
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(4, 0)]
        [InlineData(5, 0)]
        [InlineData(6, 0)]
        [InlineData(7, 0)]
        [InlineData(8, 0)]
        public async void GetAllByIdColegioWithPage_Ok(int idColegio, int pag)
        {
            int cantidadPorPag = 20;
            Mock<IMapper> mapper = new Mock<IMapper>();
            Mock<ICiclo> cicloService = new Mock<ICiclo>();
            Mock<IClase> claseService = new Mock<IClase>();
            Mock<IColegio> colegioService = new Mock<IColegio>();

            ClaseController controller = new ClaseController(
                mapper.Object,
                claseService.Object,
                cicloService.Object,
                colegioService.Object);

            var clase1 = new profefolio.Models.Entities.Clase()
            {
                Id = 1,
                Anho = 2023,
                CicloId = 1,
                ColegioId = idColegio,
                Deleted = false,
                Nombre = "Primer grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var clase2 = new profefolio.Models.Entities.Clase()
            {
                Id = 2,
                Anho = 2023,
                CicloId = 1,
                ColegioId = 1,
                Deleted = false,
                Nombre = "Segundo grado",
                Turno = "Tarde",
                Created = DateTime.Now,
                CreatedBy = "juan.perez@gmail.com"
            };

            var clases = new List<profefolio.Models.Entities.Clase>(){
                clase1, clase2
            };

            var resultClases = new List<ClaseResultSimpleDTO>(){
                new ClaseResultSimpleDTO() {
                    Id = 1,
                    Ciclo = "Primero",
                    CicloId = 1,
                    Anho = 2023,
                    Turno = "Tarde",
                    Nombre = "Primer grado"
                },
                new ClaseResultSimpleDTO() {
                    Id = 2,
                    Ciclo = "Primero",
                    CicloId = 1,
                    Anho = 2023,
                    Turno = "Tarde",
                    Nombre = "Segundo grado"
                }
            };

            claseService.Setup(c => c.GetAllByIdColegio(pag, cantidadPorPag, idColegio)).ReturnsAsync(clases);
            
            claseService.Setup(c => c.Count(idColegio)).ReturnsAsync(clases.Count);
            
            mapper.Setup(m => m.Map<List<ClaseResultSimpleDTO>>(clases)).Returns(resultClases);


            var result = await controller.GetAll(idColegio, 0);

            var response = Assert.IsType<OkObjectResult>(result.Result);

        }
    
    
    }
}