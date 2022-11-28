using IS.Domain.DomainModels;

namespace IS.Services.Interfaces
{
    public interface IValidatorService<TInputType>
        where TInputType : IBaseInput
    {
        public string ValidateInput(TInputType robot);
    }
}