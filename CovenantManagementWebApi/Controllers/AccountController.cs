using Application.InterfaceServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CovenantManagementWebApi.Controllers
{
    [Route("api/Account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] Account account)
        {
            _accountService.CreateAccount(account);
            return Ok(account);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, Account account)
        {
            var existingAccount = _accountService.GetAccountById(id);
            if (existingAccount == null)
            {
                return NotFound();
            }
            account.IdAccount = existingAccount.IdAccount;
            _accountService.UpdateAccount(existingAccount);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            var account = _accountService.GetAccountById(id);
            if (account == null)
            {
                return NotFound();
            }
            _accountService.DeleteAccount(account);
            return Ok();
        }

    }
}
