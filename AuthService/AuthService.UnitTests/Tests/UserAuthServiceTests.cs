using AuthService.Application.Services;
using AuthService.Application.Utils;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Repositories;
using AuthService.UnitTests.Fixtures;
using AuthService.UnitTests.Extensions;
using NUnit.Framework;

namespace AuthService.UnitTests.Tests;

public class UserAuthServiceTests
{
    private IUserAuthService _userAuthService;


    [SetUp]
    public void Setup()
    {
        _userAuthService =
            new UserAuthService(PasswordHashFixture.PasswordHasher, new UserAuthRepo(DbContextFixture.Context));
    }

    [Test]
    public async Task RegisterAndGetId()
    {
        var registerUserId = await _userAuthService.Register("login@mail.ru", "password");
        var loginUserId = await _userAuthService.Login("login@mail.ru", "password");

        Assert.That(registerUserId, Is.EqualTo(loginUserId));
    }
}