using Common.RequestModels;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interfaces
{
    public interface INoteManger
    {
        public NoteEntity CreateNote(CreateNoteModel model,int Id);
        public List<NoteEntity> GetNote(int id);
        public NoteEntity UpdateNote(int NotesId, UpdateNoteModel model);
        public NoteEntity Trash(int NotesId);
        public NoteEntity DeleteNote(int NotesId, int id);
        public NoteEntity Archive(int NotesId);
        public NoteEntity Pin(int NotesId);
        public NoteEntity Colour(int NotesId, string Colour);
        public NoteEntity Reminder(int NotesId, DateTime Reminder);
        public string UploadImage(string filepath, int NotesId, int Id);
        public LabelNote AddLabel(int NoteId, int id, AddLabel label);
        public List<LabelNote> FetchLabel(int id, string LabelName);
        public LabelNote UpdateLabel(int LabelId, string LabelName);
        public LabelNote RemoveLabel(int NoteId, int LabelId);
    }
        
}
