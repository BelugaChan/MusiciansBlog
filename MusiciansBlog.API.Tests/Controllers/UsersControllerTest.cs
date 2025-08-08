using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MusiciansBlog.API.Authentication.Options;
using MusiciansBlog.API.Authentication.Providers;
using MusiciansBlog.API.Controllers;
using MusiciansBlog.API.Infrastructure.Users.LoginUser;
using MusiciansBlog.API.Infrastructure.Users.RegisterUser;

namespace MusiciansBlog.API.Tests.Controllers
{
    public class UsersControllerTest
    {
        private readonly IMediator _mediator;
        private readonly ICookieProvider _cookieProvider;
        private readonly IConfiguration _configuration;
        private readonly IOptions<JwtOptions> _options;

        public UsersControllerTest()
        {
            _cookieProvider = A.Fake<ICookieProvider>();
            _configuration = A.Fake<IConfiguration>();
            _options = A.Fake<IOptions<JwtOptions>>();
            _mediator = A.Fake<IMediator>();
        }

        [Fact]
        public async Task UsersController_Login_ReturnsOkResult()
        {
            //Arrange
            var loginData = A.Fake<LoginUserCommand>();

            var controller = new UsersController(
                _mediator, 
                _cookieProvider, 
                _configuration, 
                _options
            );

            //Act
            var result = await controller.Login(loginData, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            //Проверка, что тип объекта okResult можно присвоить базовому типу.
            //В данном случае - это тип LoginUserResponse.
            okResult.Value.Should().BeAssignableTo<LoginUserResponse>();
        }

        [Fact]
        public async Task UsersController_Register_ReturnsOkResult()
        {
            //Arrange
            var registerData = A.Fake<RegisterUserCommand>();

            var controller = new UsersController(
                _mediator,
                _cookieProvider,
                _configuration,
                _options
            );

            //Act
            var result = await controller.Register(registerData, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            okResult.Value.Should().BeAssignableTo<RegisterUserResponse>();
        }

        public async Task UsersController_Refresh_ReturnsOk()
        {

        }
    }
}