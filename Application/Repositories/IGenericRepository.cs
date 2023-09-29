using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        List<Covenant> GetAllCovenant();
        Covenant GetCovenantById(int id);
        List<CovenantCondition> GetAllCovenantCondition();
        CovenantCondition GetCovenantConditionById(int id);
        List<CovenantResult> GetAllResult();
        CovenantResult GetCovenantResultById(int id);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Archive(T entity);
    }
}
