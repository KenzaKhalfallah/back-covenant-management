using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private readonly IGenericRepository<Counterparty> _counterpartyRepository;

        public CounterpartyService(IGenericRepository<Counterparty> counterpartyRepository)
        {
            _counterpartyRepository = counterpartyRepository;
        }
        public IEnumerable<Counterparty> GetAllCounterparties()
        {
            return _counterpartyRepository.GetAll();
        }

        public Counterparty GetCounterpartytById(int id)
        {
            return _counterpartyRepository.GetById(id);
        }
    }
}
