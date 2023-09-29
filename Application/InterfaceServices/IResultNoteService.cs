using Domain.Entities;

namespace Application.InterfaceServices
{
    public interface IResultNoteService
    {
        IEnumerable<ResultNote> GetAllResultNotes();
        ResultNote GetResultNoteById(int id);
        void AddResultNote(ResultNote resultNote);
        void UpdateResultNote(ResultNote resultNote);
        void ArchiveResultNote(ResultNote resultNote);
    }
}
