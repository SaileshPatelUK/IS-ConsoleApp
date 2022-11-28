using IS.Domain.DomainModels;
using IS.Services.ErrorHandling;

namespace IS.Services.Interfaces
{
    public interface IMovementService<TItemBaseType, TMovementBaseType>
        where TItemBaseType : BaseInputModel
        where TMovementBaseType : BaseInputModel
    {
        Task<ServiceOperationResultWrapper<TItemBaseType>> MovementTableItem(TItemBaseType oldModel, TMovementBaseType input, string traceId);
    }
}
