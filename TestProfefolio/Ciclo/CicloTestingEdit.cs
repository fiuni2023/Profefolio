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

            service.Setup(a => a.ExisitOther(id, dto.Nombre)).ReturnsAsync(false);

            service.Setup(a => a.FindById(id)).ReturnsAsync(modelo);

            var modelo2 = modelo;

            service.Setup(s => s.Edit(modelo)).Returns(modelo);

            service.Setup(a => a.Save()).Returns(Task.CompletedTask);


            var result = await controller.Put(id, dto);

            Assert.IsType<NoContentResult>(result);
        }


    }
}