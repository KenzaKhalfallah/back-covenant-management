using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class CovenantConditionService : ICovenantConditionService
    {
        private readonly IGenericRepository<CovenantCondition> _covenantConditionRepository;

        public CovenantConditionService(IGenericRepository<CovenantCondition> covenantConditionRepository)
        {
            _covenantConditionRepository = covenantConditionRepository;
        }

        public void AddCovenantCondition(CovenantCondition covenantCondition)
        {
            _covenantConditionRepository.Add(covenantCondition);
        }

        public void DeleteCovenantCondition(CovenantCondition covenantCondition)
        {
            _covenantConditionRepository.Delete(covenantCondition);
        }

        public IEnumerable<CovenantCondition> GetAllCovenantConditions()
        {
            return _covenantConditionRepository.GetAllCovenantCondition();
        }

        public CovenantCondition GetCovenantConditionById(int id)
        {
            return _covenantConditionRepository.GetCovenantConditionById(id);
        }

        public void UpdateCovenantCondition(CovenantCondition covenantCondition)
        {
            _covenantConditionRepository.Update(covenantCondition);
        }
    }
}
