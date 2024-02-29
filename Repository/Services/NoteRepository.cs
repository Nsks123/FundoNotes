using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Common.RequestModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly DemoContext context;
        public NoteRepository(DemoContext context)
        {
            this.context = context;
        }
        //Create Note
        public NoteEntity CreateNote(CreateNoteModel model, int Id)
        {
            NoteEntity entity = new NoteEntity();
            entity.Id = Id;
            entity.Title = model.Title;
            entity.Description = model.Description;
            entity.Reminder = model.Reminder;
            entity.Colour = model.Colour;
            entity.Image = model.Image;
            entity.IsArchive = model.IsArchive;
            entity.IsPin = model.IsPin;
            entity.IsTrash = model.IsTrash;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            context.NoteTable.Add(entity);
            context.SaveChanges();
            return entity;
        }
        //Get Note
        public List<NoteEntity> GetNote(int id)
        {
            return context.NoteTable.Where<NoteEntity>(a => a.Id == id).ToList();
        }
        //Update Note
        public NoteEntity UpdateNote(int NotesId, UpdateNoteModel model)
        {
            var noteToUpdate = context.NoteTable.FirstOrDefault(note => note.NotesId == NotesId);
            if (noteToUpdate != null)
            {
                noteToUpdate.Title = model.Title;
                noteToUpdate.Description = model.Description;
                noteToUpdate.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
                return noteToUpdate;

            }
            return null;
        }
        //Trash Note
        public NoteEntity Trash(int NotesId)
        {
            var trash=context.NoteTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (trash != null)
            {
                if (trash.IsTrash)
                {
                    trash.IsTrash = false;
                    context.SaveChanges();

                }
                else
                {
                    trash.IsTrash = true;
                }
            }
            return trash;

        }
        //Delete Note
        public NoteEntity DeleteNote(int NotesId,int id)
        {
            var del =context.NoteTable.FirstOrDefault(o =>  (o.NotesId == NotesId && o.Id==id));
            if (del != null)
            {
                context.NoteTable.Remove(del);
                context.SaveChanges() ;
            }
            else
            {
                throw new Exception("Wrong DATA");
            }
            return del;        
        }
        //Is Archive
        public NoteEntity Archive(int NotesId)
        {
            var archive=context.NoteTable.FirstOrDefault(o => o.NotesId==NotesId);
            if(archive != null)
            {
                if (archive.IsArchive)
                {
                    archive.IsArchive = false;
                    context.SaveChanges();
                }
                else
                {
                    archive.IsArchive = true;
                }
            return archive;
            }
            else
            {
                throw new Exception("IsArchive not found");
            }
            
        }
        //Is Pin
        public NoteEntity Pin(int NotesId)
        {
            var pin = context.NoteTable.FirstOrDefault(o => o.NotesId == NotesId);
            if (pin != null)
            {
                if (pin.IsPin)
                {
                    pin.IsPin = false;
                    context.SaveChanges();
                }
                else
                {
                    pin.IsPin = true;
                }
                return pin;
            }
            else
            {
                throw new Exception("IsPin not found");
            }

        }
        //Colour
        public NoteEntity Colour(int NotesId)
        {
            var color = context.NoteTable.FirstOrDefault(o => o.NotesId == NotesId);
            if(color != null)
            {
                color.Colour = "Blue";
                context.SaveChanges();
            }
            return color;

        }
        //Reminder
        public NoteEntity Reminder(int NotesId)
        {
            var remind=context.NoteTable.FirstOrDefault(o=>o.NotesId == NotesId);
            if (remind != null)
            {
                remind.Reminder = DateTime.UtcNow;                
                context.SaveChanges();
            }
            return remind;
        }
        //Image
        public string UploadImage(string filepath,int NotesId,int Id)
        {
            try
            {
                var filter = context.NoteTable.Where(e => e.Id == Id);
                if (filter != null)
                {
                    var findNotes = filter.FirstOrDefault(e => e.NotesId == NotesId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dygoi0kzf", "822117938224726", "***************************");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filepath),
                            PublicId = findNotes.Title
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        findNotes.UpdatedAt = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        context.SaveChanges();
                        return "Upload Successfull";
                    }
                    return null;
                }
                else { return null; }

            }
            catch (Exception ex) {  return null; }

        }
    }
}
