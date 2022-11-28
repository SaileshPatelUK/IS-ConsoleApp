using FluentValidation;
using IS.Core.Constants;
using IS.Domain.DomainModels;
using IS.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace IS.Services.Validator
{
    public class ValidatorService<TInputType> : IValidatorService<TInputType>
        where TInputType : IBaseInput
    {
        private readonly IValidator<TInputType> _validator;
        private readonly ILogger<ValidatorService<TInputType>> _logger;

        public ValidatorService(IValidator<TInputType> validator, ILogger<ValidatorService<TInputType>> logger)
        {
            _validator = validator;
            _logger = logger;
        }
        public string ValidateInput(TInputType input)
        {
            string? validationError = null;

            try
            {
                var validationResult = _validator.Validate(input);

                if (!validationResult.IsValid)
                {
                    if (validationResult.Errors.Any(x => x.Severity == Severity.Warning))
                    {
                        _logger.LogWarning($"Validation Warning: Request with Id: {input.GetId()}. " +
                            $"{string.Join("; ", validationResult.Errors.Where(x => x.Severity == Severity.Warning))}. WARNING logged from Assembly '{AssemblyName.ISService}'");
                    }

                    if (validationResult.Errors.Any(x => x.Severity == Severity.Error))
                    {
                        validationError = $"Validation Failed: Request with Id: {input.GetId()}.: {string.Join("; ", validationResult.Errors.Where(x => x.Severity == Severity.Error))}.";
                    }
                }
            }
            catch (Exception ex)
            {
                validationError = $"Validation Exception: {ex.Message}.";
            }

            return validationError;
        }
    }
}
