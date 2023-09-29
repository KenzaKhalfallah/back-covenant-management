using Application.InterfaceServices;
using Application.RabbitMQ;
using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Entities;
using Infrastucture;
using Microsoft.AspNetCore.Mvc;

namespace CovenantManagementWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResultNoteController : ControllerBase
    {
        private readonly IResultNoteService _resultNoteService;
        private readonly ICovenantResultService _covenantResultService;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly IMapper _mapper;

        public ResultNoteController(IResultNoteService resultNoteService, ICovenantResultService covenantResultService, IMapper mapper, IRabbitMQ rabbitMQ)
        {
            _resultNoteService = resultNoteService;
            _covenantResultService = covenantResultService;
            _mapper = mapper;
            _rabbitMQ = rabbitMQ;
        }

        [HttpGet]
        public IActionResult GetAllResultNotes()
        {
            var resultNotes = _resultNoteService.GetAllResultNotes().Where(c => c.IsArchived == false);
            return Ok(resultNotes);
        }

        [HttpGet("{id}")]
        public IActionResult GetResultNoteById(int id)
        {
            var resultNote = _resultNoteService.GetResultNoteById(id);
            if (resultNote==null || resultNote.IsArchived==true)
            {
                return NotFound();
            }
            return Ok(resultNote);
        }

        [HttpPost("{CovenantResultId}")]
        public IActionResult AddResultNote(int CovenantResultId, [FromBody] ResultNoteDTO resultNoteDTO)
        {
            var existingResult = _covenantResultService.GetCovenantResultById(CovenantResultId);
            if (existingResult == null)
            {
                return NotFound();
            }
            resultNoteDTO.IdCovenantResult = CovenantResultId;
            var resultNote = _mapper.Map<ResultNote>(resultNoteDTO);
            existingResult.ResultNotes.Add(resultNote);
            _resultNoteService.AddResultNote(resultNote);
            _rabbitMQ.SendCovenantMessage("New note added: \n" + resultNote + "\n at :" + DateTime.Now);
            return Ok(resultNote);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateResultNote(int id, ResultNoteDTO resultNoteDTO)
        {
            var existingResultNote = _resultNoteService.GetResultNoteById(id);
            if (existingResultNote == null)
            {
                return NotFound();
            }
            resultNoteDTO.IdCovenantResult = existingResultNote.IdCovenantResult;
            _mapper.Map(resultNoteDTO, existingResultNote);
            _resultNoteService.UpdateResultNote(existingResultNote);
            _rabbitMQ.SendCovenantMessage("Note updated: \n"+existingResultNote + "\n at :" + DateTime.Now);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ArchiveResultNote(int id)
        {
            var resultNote = _resultNoteService.GetResultNoteById(id);
            if (resultNote == null)
            {
                return NotFound();
            }
            _resultNoteService.ArchiveResultNote(resultNote);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetArchivedNotes()
        {
            var resultNotes = _resultNoteService.GetAllResultNotes().Where(c => c.IsArchived);
            return Ok(resultNotes);
        }
    }
}
