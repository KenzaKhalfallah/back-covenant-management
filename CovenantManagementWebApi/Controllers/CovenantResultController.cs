using Application.InterfaceServices;
using Application.RabbitMQ;
using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CovenantManagementWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CovenantResultController : ControllerBase
    {
        private readonly ICovenantResultService _covenantResultService;
        private readonly ICovenantConditionService _covenantConditionService;
        private readonly IResultNoteService _resultNoteService;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly IMapper _mapper;

        public CovenantResultController(ICovenantResultService covenantResultService, ICovenantConditionService covenantConditionService, IResultNoteService resultNoteService, IMapper mapper, IRabbitMQ rabbitMQ)
        {
            _covenantResultService = covenantResultService;
            _covenantConditionService = covenantConditionService;
            _resultNoteService = resultNoteService;
            _mapper = mapper;
            _rabbitMQ = rabbitMQ;
        }

        [HttpGet]
        public IActionResult GetAllCovenantResults()
        {
            var covenantResults = _covenantResultService.GetAllCovenantResults();
            return Ok(covenantResults);
        }

        [HttpGet("{id}")]
        public IActionResult GetCovenantResultById(int id)
        {
            var covenantResult = _covenantResultService.GetCovenantResultById(id);
            if (covenantResult == null)
            {
                return NotFound();
            }
            return Ok(covenantResult);
        }

        [HttpPost("{covenantConditionId}")]
        public IActionResult CreateCovenantResult(int conditionId, [FromBody] CovenantResultDTO covenantResultDto)
        {
            var existingCondition = _covenantConditionService.GetCovenantConditionById(conditionId);
            if (existingCondition == null)
            {
                return NotFound();
            }
            var covenantResult = _mapper.Map<CovenantResult>(covenantResultDto);
            existingCondition.CovenantResult = covenantResult;
            _covenantResultService.CreateCovenantResult(covenantResult);
            return Ok(covenantResult);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCovenantResult(int id, CovenantResultDTO covenantResultDTO)
        {
            var existingCovenantResult = _covenantResultService.GetCovenantResultById(id);
            if (existingCovenantResult == null)
            {
                return NotFound();
            }
            covenantResultDTO.IdResult = existingCovenantResult.IdResult;
            //covenantResultDTO.CovenantCondition = existingCovenantResult.CovenantCondition;
            _mapper.Map(covenantResultDTO, existingCovenantResult);
            _covenantResultService.UpdateCovenantResult(existingCovenantResult);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCovenantResult(int id)
        {
            var covenantResult = _covenantResultService.GetCovenantResultById(id);
            if (covenantResult == null)
            {
                return NotFound();
            }
            if (covenantResult.ResultNotes != null)
            {
                foreach (var resultNote in covenantResult.ResultNotes)
                {
                    _resultNoteService.ArchiveResultNote(resultNote);
                }
            }
            _covenantResultService.DeleteCovenantResult(covenantResult);
            return Ok();
        }
    }
}
