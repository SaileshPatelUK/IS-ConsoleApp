using IS.Domain.DomainModels;
using IS.Services.ErrorHandling;
using IS.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IS.Services
{
    public class RobotService : MovementService<RobotModel, MovementModel>, IMovementService<RobotModel, MovementModel>
    {
        private readonly ILogger<RobotService> _logger;
        private readonly IValidatorService<MovementModel> _validatorService;

        public RobotService(ILogger<RobotService> logger, IValidatorService<MovementModel> validatorService)
            :base(logger, validatorService)
        {
            _logger = logger;
            _validatorService = validatorService;
        }

        public override Task<ServiceOperationResultWrapper<RobotModel>> MovementTableItem(RobotModel oldModel, MovementModel input, string traceId)
        {
            return base.MovementTableItem(oldModel, input, traceId);
        }

    }
}
