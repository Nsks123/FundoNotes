using Common.RequestModels;
using Manager.Interfaces;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Services
{
    public class NoteManager : INoteManger
    {
        private readonly INoteRepository repository;
        public NoteManager(INoteRepository repository)
        {
            this.repository = repository;
        }
        public NoteEntity CreateNote(CreateNoteModel model,int Id)
        {
           return repository.CreateNote(model,Id);
        }
        public List<NoteEntity> GetNote(int id)
        {
            return repository.GetNote(id);
        }
        public NoteEntity UpdateNote(int NotesId, UpdateNoteModel model)
        {
            return repository.UpdateNote(NotesId, model); 
        }
        public NoteEntity Trash(int NotesId)
        {
            return repository.Trash(NotesId);
        }
        public NoteEntity DeleteNote(int NotesId, int id)
        {
            return repository.DeleteNote(NotesId,id);
        }
        public NoteEntity Archive(int NotesId)
        {
            return repository.Archive(NotesId);
        }
        public NoteEntity Pin(int NotesId)
        {
            return repository.Pin(NotesId);
        }
        public NoteEntity Colour(int NotesId, string Colour)
        {
            return repository.Colour(NotesId, Colour);
        }
        public NoteEntity Reminder(int NotesId, DateTime Reminder)
        {
            return repository.Reminder(NotesId, Reminder);
        }
        public string UploadImage(string filepath, int NotesId, int Id)
        {
            return repository.UploadImage(filepath, NotesId, Id);
        }
        public LabelNote AddLabel(int NoteId, int id, AddLabel label)
        {
            return repository.AddLabel(NoteId, id, label);  
        }
        public List<LabelNote> FetchLabel(int id, string LabelName)
        {
            return repository.FetchLabel(id, LabelName);
        }
        public LabelNote UpdateLabel(int LabelId, string LabelName)
        {
            return repository.UpdateLabel(LabelId, LabelName);
        }
        public LabelNote RemoveLabel(int NoteId, int LabelId)
        {
            return repository.RemoveLabel(NoteId, LabelId);
        }
    }
        
}
