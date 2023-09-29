using Application.InterfaceServices;
using Application.RabbitMQ;
using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace CovenantManagementWebApi.Controllers
{
    [Route("api/Covenant/[action]")]
    [ApiController]
    public class CovenantController : ControllerBase
    {
        private readonly ICovenantService _covenantService;
        private readonly ICovenantConditionService _covenantConditionService;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly IMapper _mapper;

        public CovenantController(ICovenantService covenantService, ICovenantConditionService covenantConditionService, IRabbitMQ rabbitMQ, IMapper mapper)
        {
            _covenantService = covenantService;
            _covenantConditionService = covenantConditionService;
            _rabbitMQ = rabbitMQ;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllCovenants()
        {
            var covenants = _covenantService.GetAllCovenants();
            return Ok(covenants);
        }

        [HttpGet("{id}")]
        public IActionResult GetCovenantById(int id)
        {
            var covenant = _covenantService.GetCovenantById(id);
            if (covenant == null)
            {
                return NotFound();
            }
            return Ok(covenant);
        }

        [HttpPost]
        public IActionResult CreateCovenant([FromBody] CovenantDTO covenantDto)
        {
            var covenant = _mapper.Map<Covenant>(covenantDto);
            _covenantService.CreateCovenant(covenant);
            _rabbitMQ.SendCovenantMessage("New covenant added: \n" + covenant + "\n at :" + DateTime.Now);
            return Ok(covenant);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCovenant(int id, CovenantDTO covenantDto)
        {
            var existingCovenant = _covenantService.GetCovenantById(id);
            if (existingCovenant == null)
            {
                return NotFound();
            }
            covenantDto.IdCovenant = existingCovenant.IdCovenant;
            _mapper.Map(covenantDto, existingCovenant);
            _covenantService.UpdateCovenant(existingCovenant);
            _rabbitMQ.SendCovenantMessage("Covenant updated: \n" + existingCovenant + "\n at :" + DateTime.Now);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCovenant(int id)
        {
            var covenant = _covenantService.GetCovenantById(id);
            if (covenant == null)
            {
                return NotFound();
            }
            _covenantService.DeleteCovenant(covenant);
            if (covenant.CovenantConditions != null)
            {
                foreach (var covenantCondition in covenant.CovenantConditions)
                {
                    _covenantConditionService.DeleteCovenantCondition(covenantCondition);
                }
            }
            _rabbitMQ.SendCovenantMessage("Covenant deleted." + "\n at :" + DateTime.Now);
            return Ok();
        }
       
    }
}
