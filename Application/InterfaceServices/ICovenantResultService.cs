using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.InterfaceServices
{
    public interface ICovenantResultService
    {
        IEnumerable<CovenantResult> GetAllCovenantResults();
        CovenantResult GetCovenantResultById(int id);
        void CreateCovenantResult(CovenantResult covenantResult);
        void UpdateCovenantResult(CovenantResult covenantResult);
        void DeleteCovenantResult(CovenantResult covenantResult);
    }
}
