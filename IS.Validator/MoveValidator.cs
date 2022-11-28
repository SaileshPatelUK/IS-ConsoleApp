using FluentValidation;
using IS.Core.Constants;
using IS.Core.Enums;
using IS.Domain.DomainModels;

namespace IS.Validator
{
    public class MoveValidator : AbstractValidator<MovementModel>
    {
        public MoveValidator()
        {
            RuleFor(_ => _.XCoordinate)
                .NotNull()
                .WithMessage("'XCoordinate' must not be null or empty")
                .GreaterThanOrEqualTo(Table.MinTableXSize)
                .WithMessage("'XCoordinate' must not be less than 0")
                .LessThanOrEqualTo(Table.MaxTableXSize)
                .WithMessage("'XCoordinate' must not be more than than 5");

            RuleFor(_ => _.YCoordinate)
                .NotNull()
                .WithMessage("'YCoordinate' must not be null or empty")
                .GreaterThanOrEqualTo(Table.MinTableYSize)
                .WithMessage("'YCoordinate' must not be less than 0")
                .LessThanOrEqualTo(Table.MaxTableYSize)
                .WithMessage("'XCoordinate' must not be more than than 5");

            RuleFor(_ => _.FaceDirection)
                .NotEmpty()
                .WithMessage("'FaceDirection' must not be null or empty")
                .Must(_ => Movements.ValidFaceDirections.Contains(_))
                .WithMessage("'FaceDirection' must be either NORTH, SOUTH, EAST or WEST.");

            RuleFor(_ => _.EventType)
                .NotEmpty()
                .WithMessage("'EventType' must not be null or empty")
                .Must(_ => Enum.IsDefined(typeof(Events), _))
                .WithMessage("'EventType' must be between 1 and 5. Check Enum List if unknown.");
        }
    }
}
