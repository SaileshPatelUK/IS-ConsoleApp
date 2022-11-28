using IS.Services.ErrorHandling;
using IS.Domain.DomainModels;
using IS.Services.Interfaces;
using Microsoft.Extensions.Logging;
using IS.Core.Enums;
using IS.Core.Constants;

namespace IS.Services
{
    public abstract class MovementService<TItemBaseType, TMovementBaseType> : ServiceBase, IMovementService<TItemBaseType, TMovementBaseType>
        where TItemBaseType : BaseInputModel
        where TMovementBaseType : BaseInputModel
    {
        private readonly ILogger<MovementService<TItemBaseType, TMovementBaseType>> _logger;
        private readonly IValidatorService<TMovementBaseType> _validatorService;

        public MovementService(
            ILogger<MovementService<TItemBaseType, TMovementBaseType>> logger,
            IValidatorService<TMovementBaseType> validatorService)
        : base()
        {
            _logger = logger;
            _validatorService = validatorService;
        }

        public virtual async Task<ServiceOperationResultWrapper<TItemBaseType>> MovementTableItem(TItemBaseType oldModel, TMovementBaseType input, string traceId)
        {
            try
            {
                // Validate Input
                var errorMessage = _validatorService.ValidateInput(input);
                if (errorMessage != null)
                {
                    return ProvideFailureWrapper<TItemBaseType>(errorMessage, traceId);
                }

                // Check if first command is Place Command
                if(oldModel == null && input.EventType != 1)
                {
                    return ProvideFailureWrapper<TItemBaseType>("First Event has to be Place", traceId);
                }

                var result = oldModel;

                switch (input.EventType)
                {
                    case (int)Events.Place: // Place
                        result.XCoordinate = input.XCoordinate;
                        result.YCoordinate = input.YCoordinate;
                        result.FaceDirection = input.FaceDirection;
                        break;
                    case (int)Events.Move: // Move
                        Move(result, input);
                        break;
                    case (int)Events.Left: // Left
                    case (int)Events.Right: // Right
                        LeftOrRight(result, input);
                        break;
                    case (int)Events.Report: // Report
                        Console.WriteLine($"Output: {result.XCoordinate} {result.YCoordinate} {result.FaceDirection}");
                        break;
                    default:                        
                        break;
                }

                if (result != null)
                {
                    return new ServiceOperationResultWrapper<TItemBaseType>()
                    {
                        Result = result,
                        Successful = true,
                        StatusMessage = new StatusWithTraceId { Message = StandardResults.SuccessStatus, TraceId = traceId }
                    };
                }
                else
                {
                    return ProvideFailureWrapper<TItemBaseType>("Back end error - Unable to do something;", traceId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed in doing something", ex);

                var result = ProvideFailureWrapper<TItemBaseType>(ex, StandardResults.StandardDisplayError, traceId);

                result.ComposeExceptionMessage();
                return result;
            }
        }

        public virtual void Move(TItemBaseType model, TMovementBaseType input)
        {
            switch (input.FaceDirection)
            {
                case Movements.North:
                    model.YCoordinate = model.YCoordinate + 1;
                    break;
                case Movements.South:
                    model.YCoordinate = model.YCoordinate - 1;
                    break;
                case Movements.East:
                    model.XCoordinate = model.XCoordinate + 1;
                    break;
                case Movements.West:
                    model.XCoordinate = model.XCoordinate - 1;
                    break;
                default:
                    break;
            }
        }

        public virtual void LeftOrRight(TItemBaseType model, TMovementBaseType input)
        {
            var fdIndex = Array.IndexOf(Movements.ValidFaceDirections, input.FaceDirection);

            switch (input.FaceDirection)
            {
                case Movements.Left:
                    model.FaceDirection = fdIndex < 1 ?
                        Movements.ValidFaceDirections.ElementAt(Movements.ValidFaceDirections.Length - 1) :
                        Movements.ValidFaceDirections.ElementAt(fdIndex - 1);
                    break;
                case Movements.Right:
                    model.FaceDirection = fdIndex > Movements.ValidFaceDirections.Length ?
                        Movements.ValidFaceDirections.ElementAt(fdIndex + 1) :
                        Movements.ValidFaceDirections.ElementAt(0);
                    break;
                default:
                    break;
            }

        }
    }
}
