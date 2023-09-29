using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class CovenantService : ICovenantService
    {

        private readonly IGenericRepository<Covenant> _covenantRepository;

        public CovenantService(IGenericRepository<Covenant> covenantRepository)
        {
            _covenantRepository = covenantRepository;
        }
        public void CreateCovenant(Covenant covenant)
        {
            _covenantRepository.Add(covenant);
        }

        public void DeleteCovenant(Covenant covenant)
        {
            _covenantRepository.Delete(covenant);
        }

        public IEnumerable<Covenant> GetAllCovenants()
        {
            return _covenantRepository.GetAllCovenant();
        }

        public Covenant GetCovenantById(int id)
        {
            return _covenantRepository.GetCovenantById(id);
        }

        public void UpdateCovenant(Covenant covenant)
        {
            _covenantRepository.Update(covenant);
        }
    }
}
