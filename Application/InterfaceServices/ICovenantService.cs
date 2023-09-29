using Domain.Entities;

namespace Application.InterfaceServices
{
    public interface ICovenantService
    {
        IEnumerable<Covenant> GetAllCovenants();
        Covenant GetCovenantById(int id);
        void CreateCovenant(Covenant covenant);
        void UpdateCovenant(Covenant covenant);
        void DeleteCovenant(Covenant covenant);
    }
}
