using api.Constants;
using api.Controllers;
using api.Interfaces;
using api.Models;
using api.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Tests.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        public AuthControllerTests()
        {
            _userManagerMock = MockUserManager.CreateMockUserManager<AppUser>();
            _tokenServiceMock = new Mock<ITokenService>();

            _authController = new AuthController
            (
                _userManagerMock.Object,
                _tokenServiceMock.Object
            );
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WithToken()
        {
            //arrange
            var loginModel = new LoginModel
            {
                Email = "test@gmail.com",
                Password = "testPassword"
            };

            var fakeUser = new AppUser
            {
                Id = "testId",
                Email = "test@gmail.com",
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginModel.Email)).ReturnsAsync(fakeUser);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(fakeUser, loginModel.Password)).ReturnsAsync(true);

            var jwtToken = "prettyCoolToken";

            _tokenServiceMock.Setup(ts => ts.CreateToken(It.IsAny<AppUser>())).ReturnsAsync(jwtToken);

            //act
            var result = await _authController.Login(loginModel);

            //assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(new { Token = jwtToken });
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserEmailNotFound()
        {
            //arrange
            var loginModel = new LoginModel
            {
                Email = "test@gmail.com",
                Password = "testPassword"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginModel.Email)).ReturnsAsync((AppUser)null);

            //act
            var result = await _authController.Login(loginModel);

            //assert
            var objectResult = result as UnauthorizedObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().Be("Invalid credentials");
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenPasswordIncorrect()
        {
            //arrange
            var loginModel = new LoginModel
            {
                Email = "testuser",
                Password = "userpassword"
            };
            var fakeUser = new AppUser
            {
                Id = "1",
                Email = loginModel.Email,
                UserName = "testuser"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginModel.Email)).ReturnsAsync(fakeUser);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(fakeUser, loginModel.Password)).ReturnsAsync(false);

            //act
            var result = await _authController.Login(loginModel);

            //assert
            var objectResult = result as UnauthorizedObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().Be("Invalid credentials");
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenUserSuccessfullyCreated()
        {
            //arrange
            var registerModel = new RegisterModel
            {
                Email = "newuser@gmail.com",
                Password = "newuserpassword"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(registerModel.Email)).ReturnsAsync((AppUser)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), registerModel.Password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), UserRoles.User)).ReturnsAsync(IdentityResult.Success);

            //act
            var result = await _authController.Register(registerModel);

            //assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be("User created successfully");
        }

        [Fact]
        public async Task Register_ShouldReturnConflictResult_WhenUserAlreadyExists()
        {
            //arrange
            var registerModel = new RegisterModel
            {
                Email = "newuser@gmail.com",
                Password = "newuserpassword"
            };

            var existingUser = new AppUser
            {
                Email = registerModel.Email,
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(registerModel.Email)).ReturnsAsync(existingUser);

            //act
            var result = await _authController.Register(registerModel);

            //assert
            var objectResult = result as ConflictObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.Value.Should().Be("User with this email already exists");
        }

        [Fact]
        public async Task Register_ShouldReturnStatusCode400_WhenUserCreationFails()
        {
            //arrange
            var registerModel = new RegisterModel
            {
                Email = "newuser@gmail.com",
                Password = "newuserpassword"
            };

            _userManagerMock.Setup(um => um.FindByEmailAsync(registerModel.Email)).ReturnsAsync((AppUser)null);

            var identityResult = IdentityResult.Failed(new IdentityError { Description = "Password too weak." });

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), registerModel.Password)).ReturnsAsync(identityResult);

            //act
            var result = await _authController.Register(registerModel);

            //assert
            var objectResult = result as ObjectResult;
            objectResult.Should().NotBeNull();
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            objectResult.Value.Should().Be("User creation failed: Password too weak.");
        }
    }
}
