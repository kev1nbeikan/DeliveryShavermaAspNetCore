using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using UserService.Core.Abstractions;
using UserService.DataAccess.Repositories;
using UserService.Main.Contracts;
using UserService.Main.Controllers;
using UserService.UnitTests.Fixtures;

namespace UserService.UnitTests;

public class UseControllerTests
{
    private UserController _userController;
    private IUserRepository _userRepository;

    [SetUp]
    public void Setup()
    {
        var mockLogger = new Mock<ILogger<UserController>>();

        _userRepository = new UserRepository(DbContextFixture.Context);
        _userController = new UserController(mockLogger.Object,
            new Application.UserService(_userRepository));
    }

    [Test]
    public async Task AddNewOrUpdate()
    {
        var userId = Guid.NewGuid();
        await _userController.Upsert(new UpsertUserRequest
        (
            userId,
            "comment",
            "street Bebra",
            "+7894649189789"
        ));

        await _userController.Upsert(new UpsertUserRequest
        (
            userId,
            "comment",
            "street Bebra2",
            "+7894649189789"
        ));

        var user = await _userRepository.Get(userId);

        Console.WriteLine(string.Join(", ", user!.Addresses));
    }
}