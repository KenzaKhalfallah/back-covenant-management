using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _accountRepository;

        public AccountService(IGenericRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void CreateAccount(Account account)
        {
            _accountRepository.Add(account);
        }

        public void DeleteAccount(Account account)
        {
            _accountRepository.Delete(account);
        }

        public Account GetAccountById(int id)
        {
            return _accountRepository.GetById(id);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepository.GetAll();
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.Update(account);
        }
    }
}
