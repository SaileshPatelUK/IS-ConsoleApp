using IS.Core.Constants;
using IS.Domain.DomainModels;
using Xunit;

namespace UnitTests.DomainModels
{
    public class IBaseInputTests
    {
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
        public void IBaseInputTests_MovementModel_Trim()
        {
            // Arrange
            var movementModel = CreateMovementModel();
            movementModel.FaceDirection = "   NORTH    ";

            // Act
            movementModel.Trim();

            // Assert
            Assert.Equal(movementModel.FaceDirection, Movements.North);
        }

        [Fact]
        public void IBaseInputTests_MovementModel_GetId()
        {
            // Arrange
            var movementModel = CreateMovementModel();
            var expectedId = "1";
            // Act
            movementModel.GetId();

            // Assert
            Assert.Equal(movementModel.Id, expectedId);
        }

        [Fact]
        public void IBaseInputTests_RobotModel_Trim()
        {
            // Arrange
            var robotModel = CreateRobotModel();
            robotModel.FaceDirection = "   NORTH    ";

            // Act
            robotModel.Trim();

            // Assert
            Assert.Equal(robotModel.FaceDirection, Movements.North);
        }

        [Fact]
        public void IBaseInputTests_RobotModel_GetId()
        {
            // Arrange
            var robotModel = CreateRobotModel();
            var expectedId = "1";
            // Act
            robotModel.GetId();

            // Assert
            Assert.Equal(robotModel.Id, expectedId);
        }
    }
}