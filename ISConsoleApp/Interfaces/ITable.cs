using IS.Domain.DomainModels;

namespace ISConsoleApp.Interfaces
{
    public interface ITable<TItemBaseType, TMovementBaseType>
    {
        void DoSomethingToTableItem(TItemBaseType robot, TMovementBaseType movement);
    }
}
