using AuthService.Application.Services;
using AuthService.Application.Utils;
using AuthService.Core.Abstractions;
using AuthService.DataAccess.Repositories;
using AuthService.UnitTests.Fixtures;
using NUnit.Framework;

namespace AuthService.UnitTests.Tests;

public class CourierAuthServiceTests
{
    private ICourierAuthService _courierAuthService;

    [SetUp]
    public void Setup()
    {
        _courierAuthService =
            new CourierAuthService(PasswordHashFixture.PasswordHasher, new CourierAuthRepo(DbContextFixture.Context));
    }

    [Test]
    public async Task RegisterAndGetId()
    {
        var courierAuth = await _courierAuthService.Register("login", "password");
        var curierId = await _courierAuthService.Login(courierAuth.Login, "password");

        Assert.That(courierAuth.Id, Is.EqualTo(curierId));
    }
}