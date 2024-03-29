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
                ColegioId = idColegio,
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
            claseService.Setup(m => m.GetByIdColegio(It.IsAny<int>())).ReturnsAsync(clases);

            mapper.Setup(m => m.Map<List<ClaseResultSimpleDTO>>(It.IsAny<List<profefolio.Models.Entities.Clase>>())).Returns(resultClases);
            
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

            claseService.Setup(m => m.GetByIdColegio(It.IsAny<int>())).ThrowsAsync(new Exception());

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
                ColegioId = idColegio,
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

            claseService.Setup(c => c.GetAllByIdColegio(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(clases);
            
            claseService.Setup(c => c.Count(It.IsAny<int>())).ReturnsAsync(clases.Count);
            
            mapper.Setup(m => m.Map<List<ClaseResultSimpleDTO>>(It.IsAny<List<profefolio.Models.Entities.Clase>>())).Returns(resultClases);


            var result = await controller.GetAll(idColegio, pag);

            var response = Assert.IsType<OkObjectResult>(result.Result);

        }
    
    

        /*
            Testea un caso de fallo del metodo Get por Id de Colegio con paginacion en la 
            que el numero de pagina es menor a cero
        */
        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, -8)]
        [InlineData(3, -31)]
        [InlineData(4, -12)]
        [InlineData(5, -10)]
        [InlineData(6, -36)]
        [InlineData(7, -2)]
        [InlineData(8, -5)]
        public async void GetAllByIdColegioWithPage_PageInvalid_BadRequest(int idColegio, int pag)
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

            var result = await controller.GetAll(idColegio, pag);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("El numero de pagina debe ser mayor o igual que cero", response.Value);
        }
    
    


        /*
            Testea un caso de fallo del metodo Get por Id de Colegio con paginacion en la 
            que el ID del colegio es menor a cero
        */
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(-2, 8)]
        [InlineData(-3, 31)]
        [InlineData(-4, 12)]
        [InlineData(-5, 10)]
        [InlineData(-6, 36)]
        [InlineData(-7, 2)]
        [InlineData(-8, 5)]
        public async void GetAllByIdColegioWithPage_ColiegioIdInvalid_BadRequest(int idColegio, int pag)
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

            var result = await controller.GetAll(idColegio, pag);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("El campo de colegio es invalido", response.Value);
        }
    



        /*
            Testea el metodod de Get por Id de Colegio con paginacion
            el cual sufre un error porque el numero de pagina recibido 
            es mayor que el numero de paginas disponible
        */
        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 4)]
        [InlineData(6, 5)]
        [InlineData(7, 6)]
        [InlineData(8, 11)]
        public async void GetAllByIdColegioWithPage_FearchedFailed_BadRequest(int idColegio, int pag)
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
                ColegioId = idColegio,
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

            claseService.Setup(c => c.GetAllByIdColegio(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(clases);
            
            claseService.Setup(c => c.Count(It.IsAny<int>())).ReturnsAsync(clases.Count);

            var result = await controller.GetAll(idColegio, pag);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal($"No existe la pagina: {pag} ", response.Value);

        }
    


        /*
            Testea un caso exitoso de Get por Id de Clase 
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
        public async void GetById_Ok(int id)
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

            var clase = new profefolio.Models.Entities.Clase()
            {
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


            var resultClase = new ClaseResultDTO() {
                    Id = id,
                    Ciclo = "Primero",
                    IdCiclo = 1,
                    Colegio = "San Juan",
                    IdColegio = 1,
                    Anho = 2023,
                    Turno = "Tarde",
                    Nombre = "Primer grado"
                };

            claseService.Setup(c => c.FindByIdAndUser(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(clase);

            mapper.Setup(m => m.Map<ClaseResultDTO>(It.IsAny<List<profefolio.Models.Entities.Clase>>()))
                .Returns(resultClase);

            var result = await controller.GetById(id);

            var response = Assert.IsType<OkObjectResult>(result.Result);

        }




        
        /*
            Testea un caso fallido de Get por Id de Clase porque no es 
            encontro la Clase con el Id recibido
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
        public async void GetById_IdNotFound_NotFound(int id)
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

            var clase = new profefolio.Models.Entities.Clase()
            {
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

            claseService.Setup(c => c.FindByIdAndUser(It.IsAny<int>(), It.IsAny<string>()));

            var result = await controller.GetById(id);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error durante la busqueda", response.Value);
        }




            /*
            Testea un caso fallido de Get por Id de Clase porque sucedio 
            un error durante la busqueda
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
        public async void GetById_SearchedFailed_BadRequest(int id)
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

            var clase = new profefolio.Models.Entities.Clase()
            {
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

            claseService.Setup(c => c.FindById(It.IsAny<int>())).ThrowsAsync(new Exception());

            var result = await controller.GetById(id);

            var response = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Error durante la busqueda", response.Value);
        }

    }
}