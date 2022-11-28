namespace UnitTests.Validator
{
    using FluentValidation.TestHelper;
    using IS.Domain.DomainModels;
    using IS.Validator;
    using Xunit;

    namespace UnitTests.Validator
    {
        public class MoveValidatorTests
        {
            private readonly MoveValidator _validator;

            public MoveValidatorTests()
            {
                _validator = new MoveValidator();
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

            [Fact]
            public void ValidateStandardMovementModel_ShouldBeValid()
            {
                var result = _validator.TestValidate(CreateMovementModel());

                Assert.True(result.IsValid);
                Assert.True(result.Errors.Count == 0);
            }

            #region XCoorinate
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            [InlineData(5)]
            public void ValidateXCoordinate_IsValid(int xCoordinate)
            {
                //Arrange
                var model = CreateMovementModel();
                model.XCoordinate = xCoordinate;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.True(result.IsValid);
                Assert.True(result.Errors.Count == 0);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(-100)]
            [InlineData(-1.2323)]
            [InlineData(6)]
            public void ValidateXCoordinate_IsInvalid(int xCoordinate)
            {
                //Arrange
                var model = CreateMovementModel();
                model.XCoordinate = xCoordinate;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.False(result.IsValid);
                Assert.True(result.Errors.Count == 1);
            }
            #endregion

            #region YCoordinate
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            [InlineData(5)]
            public void ValidateYCoordinate_IsValid(int yCoordinate)
            {
                //Arrange
                var model = CreateMovementModel();
                model.YCoordinate = yCoordinate;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.True(result.IsValid);
                Assert.True(result.Errors.Count == 0);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(-100)]
            [InlineData(-1.2323)]
            [InlineData(6)]
            public void ValidateYCoordinate_IsInvalid(int yCoordinate)
            {
                //Arrange
                var model = CreateMovementModel();
                model.YCoordinate = yCoordinate;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.False(result.IsValid);
                Assert.True(result.Errors.Count == 1);
            }
            #endregion

            #region Height
            [Theory]
            [InlineData("NORTH")]
            [InlineData("SOUTH")]
            [InlineData("EAST")]
            [InlineData("WEST")]
            public void ValidateFaceDirection_IsValid(string faceDirection)
            {
                //Arrange
                var model = CreateMovementModel();
                model.FaceDirection = faceDirection;

                //Act
                var result = _validator.TestValidate(model);

                //Assert
                Assert.True(result.IsValid);
                Assert.True(result.Errors.Count == 0);
            }

            [Theory]
            [InlineData("north")]
            [InlineData("SOUth")]
            [InlineData("hi")]
            public void ValidateFaceDirection_IsInvalid(string faceDirection)
            {
                //Arrange
                var model = CreateMovementModel();
                model.FaceDirection = faceDirection;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.False(result.IsValid);
                Assert.True(result.Errors.Count == 1);
            }
            #endregion

            #region EventType
            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            [InlineData(5)]
            public void ValidateEventType_IsValid(int eventType)
            {
                //Arrange
                var model = CreateMovementModel();
                model.EventType = eventType;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.True(result.IsValid);
                Assert.True(result.Errors.Count == 0);
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(-100)]
            [InlineData(-1.2323)]
            [InlineData(6)]
            public void ValidateEventType_IsInvalid(int eventType)
            {
                //Arrange
                var model = CreateMovementModel();
                model.EventType = eventType;

                //Act
                var result = _validator.TestValidate(model);


                //Assert
                Assert.False(result.IsValid);
                Assert.True(result.Errors.Count == 1);
            }
            #endregion
        }
    }
}