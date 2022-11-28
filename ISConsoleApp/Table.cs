using IS.Core.Constants;
using IS.Domain.DomainModels;
using IS.Services.Interfaces;
using ISConsoleApp.Interfaces;
using ISConsoleApp.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace ISConsoleApp
{
    public class Table<TItemBaseType, TMovementBaseType> : ITable<TItemBaseType, TMovementBaseType>
        where TItemBaseType : BaseInputModel
        where TMovementBaseType : BaseInputModel
    {
        private readonly ILogger<Table<TItemBaseType, TMovementBaseType>> _logger;
        private readonly IMovementService<TItemBaseType, TMovementBaseType> _movementService;

        public Table(ILogger<Table<TItemBaseType, TMovementBaseType>> logger, IMovementService<TItemBaseType, TMovementBaseType> movementService)
        {
            _logger = logger;
            _movementService = movementService;
        }

        public async void DoSomethingToTableItem(TItemBaseType robot, TMovementBaseType movement)
        {
            try
            {
                // Call Service
                var result = await _movementService.MovementTableItem(robot, movement, Guid.NewGuid().ToString());

                if (!result.Successful)
                {
                    _logger.LogError("Report Operation failed due to an error, which was - " + result.TechnicalMessage);
                }

                robot = result.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
