﻿using Ergenekon.Application.Common.Behaviours;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.TodoItems.Commands.CreateTodoItem;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;

namespace Ergenekon.Application.UnitTests.Common.Behaviours;

public class RequestLoggerTests
{
    private Mock<ILogger<CreateTodoItemCommand>> _logger = null!;
    private Mock<ICurrentUser> _currentUser = null!;
    private Mock<IIdentityService> _identityService = null!;

    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CreateTodoItemCommand>>();
        _currentUser = new Mock<ICurrentUser>();
        _identityService = new Mock<IIdentityService>();
    }

    [Test]
    public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
    {
        _currentUser.Setup(x => x.Id).Returns(Guid.NewGuid().ToString());

        CancellationToken cancellationToken = new CancellationToken();

        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUsernameAsync(It.IsAny<string>(), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
    {
        CancellationToken cancellationToken = new CancellationToken();

        var requestLogger = new LoggingBehaviour<CreateTodoItemCommand>(_logger.Object, _currentUser.Object, _identityService.Object);

        await requestLogger.Process(new CreateTodoItemCommand { ListId = 1, Title = "title" }, new CancellationToken());

        _identityService.Verify(i => i.GetUsernameAsync(It.IsAny<string>(), cancellationToken), Times.Never);
    }
}
