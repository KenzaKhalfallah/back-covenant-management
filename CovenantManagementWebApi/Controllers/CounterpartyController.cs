using Application.InterfaceServices;
using Microsoft.AspNetCore.Mvc;

namespace CovenantManagementWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private readonly ICounterpartyService _counterpartyService;

        public CounterpartyController(ICounterpartyService counterpartyService)
        {
            _counterpartyService = counterpartyService;
        }

        [HttpGet]
        public IActionResult GetAllCounterparties()
        {
            var counterparties = _counterpartyService.GetAllCounterparties();
            return Ok(counterparties);
        }

        [HttpGet("{id}")]
        public IActionResult GetCounterpartyById(int id)
        {
            var counterparty = _counterpartyService.GetCounterpartytById(id);
            if (counterparty == null)
            {
                return NotFound();
            }
            return Ok(counterparty);
        }
    }
}
