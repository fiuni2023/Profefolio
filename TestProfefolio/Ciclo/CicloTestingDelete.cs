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


            service.Setup(c => c.FindById(id)).ReturnsAsync(ciclo);

            service.Setup(s => s.Edit(ciclo)).Returns(ciclo);

            service.Setup(a => a.Save()).Returns(Task.CompletedTask);


            var result = await controller.Delete(id);

            Assert.IsType<OkResult>(result);
        }

    }
}