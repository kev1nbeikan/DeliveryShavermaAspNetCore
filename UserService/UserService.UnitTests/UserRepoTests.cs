using BarsGroupProjectN1.Core.Models;
using NUnit.Framework;
using UserService.Core;
using UserService.Core.Abstractions;
using UserService.DataAccess.Repositories;
using UserService.UnitTests.Fixtures;

namespace UserService.UnitTests;

public class UserRepoTests
{
    private IUserRepository _userRepository;

    [SetUp]
    public void Setup()
    {
        _userRepository = new UserRepository(DbContextFixture.Context);
    }

    [Test]
    public async Task AddAndGet()
    {
        var newUser = MyUser.Create(Guid.NewGuid(), ["address1", "address2", "address3"], "comment").myUser;

        var result = await _userRepository.Add(newUser!);
        var user = await _userRepository.Get(result.UserId);

        Assert.That(user.IsEqual(newUser), Is.True);
    }


    [Test]
    public async Task AddGetUpdateGet()
    {
        var newUser = MyUser.Create(Guid.NewGuid(), ["address1", "address2", "address3"], "comment").myUser;

        var userId = await _userRepository.Add(newUser!);
        var userToUpdate = await _userRepository.Get(newUser.UserId);
        userToUpdate!.Addresses.Add("ул бебринска");
        await _userRepository.Update(userToUpdate);
        var updatedUser = await _userRepository.Get(userToUpdate.UserId);

        Assert.That(updatedUser.IsEqual(userToUpdate), Is.True);
    }
}