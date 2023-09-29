using Domain.Entities;

namespace Application.InterfaceServices
{
    public interface ICovenantConditionService
    {
        IEnumerable<CovenantCondition> GetAllCovenantConditions();
        CovenantCondition GetCovenantConditionById(int id);
        void AddCovenantCondition(CovenantCondition covenantCondition);
        void UpdateCovenantCondition(CovenantCondition covenantCondition);
        void DeleteCovenantCondition(CovenantCondition covenantCondition);
    }
}
