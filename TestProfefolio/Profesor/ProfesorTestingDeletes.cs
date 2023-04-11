using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Repository;

namespace TestProfefolio.Profesor;

public class ProfesorTestingDeletes
{

    [Fact]
    public async void Delete_Ok()
    {
        string id = "sdasd4adaddg465g4d6fg4";
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        service.Setup(s => s.DeleteByUserAndRole(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

        var result = await controller.Delete(id);
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async void Delete_IdNotFound()
    {
        string id = "sdasd4adaddg465g4d6fg4";
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        service.Setup(s => s.DeleteUser(It.IsAny<string>())).ReturnsAsync(false);

        var result = await controller.Delete(id);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async void Delete_UnespectedError()
    {
        string id = "sdasd4adaddg465g4d6fg4";
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IPersona> service = new Mock<IPersona>();
        Mock<IRol> rol = new Mock<IRol>();

        ProfesorController controller = new ProfesorController(mapper.Object, service.Object, rol.Object);

        service.Setup(s => s.DeleteUser(It.IsAny<string>())).ThrowsAsync(new Exception());

        var result = await controller.Delete(id);
        Assert.IsType<NotFoundResult>(result);
    }

}
