using NUnit.Framework;
using UserService.Core;
using UserService.Core.abstractions;
using UserService.DataAccess.Repositories;
using UserService.UnitTests.Fixtures;

namespace UserService.UnitTests;

public class UserServiceTests
{
    private IUserService _userService;

    [SetUp]
    public void Setup()
    {
        _userService = new Application.UserService(new UserRepository(DbContextFixture.Context));
    }


    [Test]
    public async Task Add()
    {
        var createdUser =
            await _userService.Add(Guid.NewGuid(), ["address1", "address2", "address3"], "123123", "comment");

        var fromServiceUser = await _userService.Get(createdUser.UserId);
        Assert.That(fromServiceUser.IsEqual(createdUser), Is.True);
    }

    [Test]
    public async Task AddNew_then_AddNewOrUpdate()
    {
        var createdUser =
            await _userService.Add(Guid.NewGuid(), ["address1", "address2", "address3"], "111", "comment");

        await _userService.AddNewOrUpdate(createdUser.UserId, "address4", "123", "new comment");

        var fromServiceUser = await _userService.Get(createdUser.UserId);
        Assert.That(fromServiceUser.IsEqual(createdUser), Is.False);
    }


    [Test]
    public async Task AddNewOrUpdate()
    {
        var userId = Guid.NewGuid();
        var userAfterAddNewOrUpdate = await _userService.AddNewOrUpdate(userId, "address4", "123", "new comment");

        var userGotByService = await _userService.Get(userId);
        Assert.That(userAfterAddNewOrUpdate.IsEqual(userGotByService), Is.True);
    }
}