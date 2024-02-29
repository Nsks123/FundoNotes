using Common.RequestModels;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces
{
    public interface INoteRepository
    {
        public NoteEntity CreateNote(CreateNoteModel model,int Id);
        public List<NoteEntity> GetNote(int id);
        public NoteEntity UpdateNote(int NotesId, UpdateNoteModel model);
        public NoteEntity Trash(int NotesId);
        public NoteEntity DeleteNote(int NotesId, int id);
        public NoteEntity Archive(int NotesId);
        public NoteEntity Pin(int NotesId);
        public NoteEntity Colour(int NotesId);
        public NoteEntity Reminder(int NotesId);
        public string UploadImage(string filepath, int NotesId, int Id);
    }
}
