using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using profefolio.Controllers;
using profefolio.Models.DTOs.Auth;
using profefolio.Repository;


namespace TestProfefolio.Auth
{
    public class AuthControllerTest
    {
        private Mock<IAuth> _authServiceMock = new ();
        private AuthController _authController;


        public AuthControllerTest()
        {
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task LoginControllerOkTest()
        {
            var login = new Login()
            {
                Email = "Carlos.Torres123@mail.com",
                Password = "Carlos.Torres123"
            };

            _authServiceMock.Setup(x => x.Login(It.IsAny<Login>())).ReturnsAsync(new AuthPersonaDTO()
            {
                Email = "Carlos.Torres123@mail.com",
                Token = "token123",
                Expires = DateTime.UtcNow,
                Roles = new List<string>
                {
                    "Master"
                }
            });

            var result = await _authController.Login(login);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var loginResult = Assert.IsType<AuthPersonaDTO>(okResult.Value);

            Assert.Equal(login.Email, loginResult.Email);
        }

        [Fact]
        public async Task LoginModelStateNoValidEmailRequiredTest()
        {
            var login = new Login
            {
                Password = "1233456"
            };

            _authController.ModelState.AddModelError("email", "El email es requerido");

            var result = await _authController.Login(login);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task LoginModelStateNoValidEmailFormatTest()
        {
            var login = new Login
            {
                Email = "carlos1223",
                Password = "1233456"
            };

            _authController.ModelState.AddModelError("email", "Debe ser de formato mail");

            var result = await _authController.Login(login);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task LoginModelStateNoValidPasswordRequiredTest()
        {
            var login = new Login
            {
                Email = "Carlos.Torres123@mail.com"
            };

            _authController.ModelState.AddModelError("password", "El password es requerido");

            var result = await _authController.Login(login);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async Task LoginModelRequiredNoValidTest()
        {
            var login = new Login();

            _authController.ModelState.AddModelError("", "");

            var result = await _authController.Login(login);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task LoginNoAuthorizedTest()
        {
            var login = new Login()
            {
                Email = "Carlos.Torres123@mail.com",
                Password = "Carlos.Torres123"
            };

            _authServiceMock.Setup(x => x.Login(It.IsAny<Login>()))
                .ThrowsAsync(new UnauthorizedAccessException());

            var result = await _authController.Login(login);

            Assert.IsType<UnauthorizedResult>(result.Result);

        }

        [Fact]
        public async Task BadRequestException()
        {
            var login = new Login()
            {
                Email = "Carlos.Torres123@mail.com",
                Password = "Carlos.Torres123"
            };

            _authServiceMock.Setup(x => x.Login(It.IsAny<Login>()))
                .ThrowsAsync(new BadHttpRequestException("Credenciales no validas"));

            var result = await _authController.Login(login);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

    }
}
