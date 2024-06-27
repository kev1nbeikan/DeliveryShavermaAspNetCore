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
        var storeAuthId = await _storeAuthService.Register("login", "password");
        var storeId = await _storeAuthService.Login("login", "password");

        Assert.That(storeAuthId, Is.EqualTo(storeId));
    }
}