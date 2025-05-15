using api.Models;
using api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<UserManager<AppUser>> _userManagerMock;
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["JWT:Secret"]).Returns("ThisIsASecureKeyForTesting123SuperSecure!");
            _configurationMock.Setup(c => c["JWT:ValidIssuer"]).Returns("TestIssuer");
            _configurationMock.Setup(c => c["JWT:ValidAudience"]).Returns("TestAudience");

            var store = new Mock<IUserStore<AppUser>>();
            _userManagerMock = new Mock<UserManager<AppUser>>(
                store.Object, null, null, null, null, null, null, null, null
            );

            _tokenService = new TokenService(_configurationMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateToken_ShouldGenerateValidJwtToken()
        {
            // Arrange
            var user = new AppUser
            {
                Id = "user-id-123",
                Email = "test@example.com"
            };

            var roles = new List<string> { "Admin", "User" };
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);

            // Act
            var token = await _tokenService.CreateToken(user);

            // Assert
            token.Should().NotBeNullOrWhiteSpace();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            jwtToken.Issuer.Should().Be("TestIssuer");
            jwtToken.Audiences.Should().Contain("TestAudience");
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == user.Email);
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id);
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            jwtToken.Claims.Should().Contain(c => c.Type == ClaimTypes.Role && c.Value == "User");
        }

        [Fact]
        public async Task CreateToken_ShouldIncludeJtiClaim()
        {
            // Arrange
            var user = new AppUser
            {
                Id = "user-id-456",
                Email = "another@example.com"
            };

            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string>());

            // Act
            var token = await _tokenService.CreateToken(user);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            jwtToken.Claims.Should().Contain(c => c.Type == JwtRegisteredClaimNames.Jti);
        }
    }
}
