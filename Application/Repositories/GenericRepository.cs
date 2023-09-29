using Domain.Entities;
using Infrastucture;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            // Only execute this logic if the entity type is CovenantCondition
            if (entity is CovenantCondition covenantCondition)
            {
                _dbSet.Add(entity); // Add the CovenantCondition to the DbSet
                _dbContext.SaveChanges(); // Save to generate the IdCondition

                int generatedIdCondition = covenantCondition.IdCondition;
                covenantCondition.CovenantResult.IdCondition = generatedIdCondition;
                covenantCondition.FinancialData.IdCondition = generatedIdCondition;
                // Create a new CovenantResult
                var covenantResult = new CovenantResult{};
                covenantResult = covenantCondition.CovenantResult;
                // Create a new FinancialData
                var financialData = new FinancialData{};
                financialData = covenantCondition.FinancialData;
                // Update CovenantResult and FinancialData so they get the same IdCondition
                _dbContext.CovenantResults.Update(covenantResult);
                _dbContext.FinancialDatas.Update(financialData);

                _dbContext.SaveChanges();
            }
            else
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            // Only execute this logic if the entity type is CovenantCondition
            if (entity is CovenantCondition covenantCondition)
            {
                //var covenantResult = new CovenantResult { };
                //covenantResult = covenantCondition.CovenantResult;
                var covenantResult = _dbContext.CovenantResults
                    .Include(cr => cr.ResultNotes) // Include ResultNotes
                    .SingleOrDefault(cr => cr.IdCondition == covenantCondition.IdCondition);
                // Create a new FinancialData
                var financialData = new FinancialData { };
                financialData = covenantCondition.FinancialData;
                // Remove CovenantResult and FinancialData / Archive ResultNotes
                if (covenantResult.ResultNotes != null)
                {
                    foreach (var resultNote in covenantResult.ResultNotes)
                    {
                        resultNote.IsArchived = true;
                    }
                }
                _dbContext.CovenantResults.Remove(covenantResult);
                _dbContext.FinancialDatas.Remove(financialData);
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }
        public void Archive(T entity)
        {
            _dbContext.SaveChanges();
        }
        public List<Covenant> GetAllCovenant()
        {
            var Results = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var result in Results)
            {
                result.ResultNotes = result.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return _dbContext.Covenants.Include(c => c.CovenantConditions)
                .ThenInclude(cc => cc.CovenantResult)
                .Include(c => c.CovenantConditions)
                .ThenInclude(cc => cc.FinancialData).ToList();
        }

        public Covenant GetCovenantById(int id)
        {
            var Results = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var result in Results)
            {
                result.ResultNotes = result.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return _dbContext.Covenants.Include(c => c.CovenantConditions)
                .ThenInclude(cc => cc.CovenantResult)
                .Include(c => c.CovenantConditions)
                .ThenInclude(cc => cc.FinancialData)
                .Where(c => c.IdCovenant == id).FirstOrDefault();
        }

        public List<CovenantCondition> GetAllCovenantCondition()
        {
            var Results = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var result in Results)
            {
                result.ResultNotes = result.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return _dbContext.CovenantConditions.Include(c => c.CovenantResult)
                .Include(c => c.FinancialData)
                .ToList();
        }
        public CovenantCondition GetCovenantConditionById(int id)
        {
            var Results = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var result in Results)
            {
                result.ResultNotes = result.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return _dbContext.CovenantConditions.Include(c => c.CovenantResult)
                .Include(c => c.FinancialData)
                .Where(c => c.IdCondition == id)
                .FirstOrDefault();
        }
        public List<CovenantResult> GetAllResult()
        {
            var covenantResults = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var covenantResult in covenantResults)
            {
                covenantResult.ResultNotes = covenantResult.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return covenantResults;
        }
        public CovenantResult GetCovenantResultById(int id)
        {
            var Results = _dbContext.CovenantResults.Include(c => c.ResultNotes).ToList();
            // Filter out archived ResultNotes for each CovenantResult
            foreach (var result in Results)
            {
                result.ResultNotes = result.ResultNotes?
                    .Where(note => !note.IsArchived)
                    .ToList();
            }
            return _dbContext.CovenantResults
                .Include(c => c.ResultNotes)
                .Where(c => c.IdResult == id)
                .FirstOrDefault(); ;
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
