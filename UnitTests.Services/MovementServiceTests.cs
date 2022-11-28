namespace UnitTests.Services
{
    using IS.Domain.DomainModels;
    using IS.Services;
    using IS.Services.Interfaces;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class MovementServiceTests
    {
        private readonly IMovementService<RobotModel, MovementModel> _movementService;
        private readonly Mock<IValidatorService<MovementModel>> _validatorService;
        private readonly Mock<ILogger<RobotService>> _logger;
        public MovementServiceTests()
        {
            _validatorService = new Mock<IValidatorService<MovementModel>>();
            _logger = new Mock<ILogger<RobotService>>();
            _movementService = new RobotService(_logger.Object, _validatorService.Object);
        }

        private MovementModel CreateMovementModel()
        {
            return new MovementModel()
            {
                Id = "1",
                XCoordinate = 0,
                YCoordinate = 0,
                FaceDirection = "NORTH",
                EventType = 1
            };
        }

        private RobotModel CreateRobotModel()
        {
            return new RobotModel()
            {
                Id = "1",
                XCoordinate = 0,
                YCoordinate = 0,
                FaceDirection = "NORTH",
                EventType = 1
            };
        }

        [Fact]
        public async void Movement_Success()
        {
            // Arrange
            var initialRobot = CreateRobotModel();
            var traceId = "1";
            var movementModel = CreateMovementModel();
            movementModel.YCoordinate = 1;

            var expectedRobot = CreateRobotModel();
            expectedRobot.YCoordinate = 1;

            _validatorService.Setup(x => x.ValidateInput(movementModel));

            // Act
            var result = await _movementService.MovementTableItem(initialRobot, movementModel, traceId);

            // Assert
            Assert.True(result.Successful);
            Assert.Equal(result.Result.XCoordinate, expectedRobot.XCoordinate);
            Assert.Equal(result.Result.YCoordinate, expectedRobot.YCoordinate);
            Assert.Equal(result.Result.FaceDirection, expectedRobot.FaceDirection);
            Assert.Equal(result.Result.EventType, expectedRobot.EventType);
        }

        [Fact]
        public void DrawMovement_FirstEventError_ThrowsTechnicalMessage()
        {
            // Arrange
            var initialRobot = CreateRobotModel();
            var traceId = "1";
            var movementModel = CreateMovementModel();
            movementModel.YCoordinate = 1;
            var errorMessage = "First Event has to be Place";

            _validatorService.Setup(x => x.ValidateInput(movementModel)).Returns(errorMessage);

            // Act
            var result = _movementService.MovementTableItem(initialRobot, movementModel, traceId);

            // Assert
            Assert.False(result.Result.Successful);
            Assert.Equal(result.Result.TechnicalMessage, errorMessage);
        }
    }
}
