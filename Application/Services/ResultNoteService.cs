using Application.InterfaceServices;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ResultNoteService : IResultNoteService
    {
        private readonly IGenericRepository<ResultNote> _resultNoteRepository;

        public ResultNoteService(IGenericRepository<ResultNote> resultNoteRepository)
        {
            _resultNoteRepository = resultNoteRepository;
        }
        public void AddResultNote(ResultNote resultNote)
        {
            resultNote.IsArchived = false;
            _resultNoteRepository.Add(resultNote);
        }

        public void ArchiveResultNote(ResultNote resultNote)
        {
            resultNote.IsArchived = true;
            _resultNoteRepository.Archive(resultNote);
        }

        public IEnumerable<ResultNote> GetAllResultNotes()
        {
            return _resultNoteRepository.GetAll() ?? new List<ResultNote>();
        }

        public ResultNote GetResultNoteById(int id)
        {
            return _resultNoteRepository.GetById(id);
        }

        public void UpdateResultNote(ResultNote resultNote)
        {
            resultNote.IsArchived = false;
            _resultNoteRepository.Update(resultNote);
        }
    }
}
