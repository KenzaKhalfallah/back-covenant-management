using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class CovenantResultService : ICovenantResultService
    {
        private readonly IGenericRepository<CovenantResult> _covenantResultRepository;

        public CovenantResultService(IGenericRepository<CovenantResult> covenantResultRepository)
        {
            _covenantResultRepository = covenantResultRepository;
        }
        public void CreateCovenantResult(CovenantResult covenantResult)
        {
            _covenantResultRepository.Add(covenantResult);
        }

        public void DeleteCovenantResult(CovenantResult covenantResult)
        {
            _covenantResultRepository.Delete(covenantResult);
        }

        public IEnumerable<CovenantResult> GetAllCovenantResults()
        {
            return _covenantResultRepository.GetAllResult();
        }

        public CovenantResult GetCovenantResultById(int id)
        {
            return _covenantResultRepository.GetCovenantResultById(id);
        }

        public void UpdateCovenantResult(CovenantResult covenantResult)
        {
            _covenantResultRepository.Update(covenantResult);
        }
    }
}
