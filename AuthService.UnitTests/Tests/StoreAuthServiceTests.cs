using AuthService.Application.Services;
using AuthService.Application.Utils;
using AuthService.DataAccess.Repositories;
using AuthService.UnitTests.Fixtures;
using NUnit.Framework;

namespace AuthService.UnitTests.Tests;

public class StoreAuthServiceTests
{
    private StoreAuthService _storeAuthService;

    [SetUp]
    public void Setup()
    {
        _storeAuthService =
            new StoreAuthService(PasswordHashFixture.PasswordHasher, new StoreAuthRepo(DbContextFixture.Context));
    }

    [Test]
    public async Task RegisterAndGetId()
    {
        var storeAuth = await _storeAuthService.Register("login", "password");
        var storeId = await _storeAuthService.Login(storeAuth.Login, "password");

        Assert.That(storeAuth.Id, Is.EqualTo(storeId));
    }
}