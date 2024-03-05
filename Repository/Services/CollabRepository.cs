using Common.RequestModels;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Repository.Services
{
    public class CollabRepository : ICollabRepository
    {
        private readonly DemoContext context; 
        public CollabRepository(DemoContext context) { 
            this.context = context;
        }
        public CollaborativeEntity AddColab(int id,int NotesId,CreateCollabModel model)
        {
            
            var check =context.UserTable.FirstOrDefault(o=>o.Email==model.CollabEmail);
            CollaborativeEntity entity = new CollaborativeEntity();
            if (check != null)
            {
                entity.UserId = id;
                entity.NotesId = NotesId;
                entity.CollabEmail = model.CollabEmail;  
                entity.Trash = model.Trash;
                entity.UpdatedAt = DateTime.Now;
                entity.CreatedAt = DateTime.Now;
                context.CollaborativeTable.Add(entity);
                context.SaveChanges();
                return entity;
            }
            else
            {
                throw new Exception();
            }                        
        }
        public List<CollaborativeEntity> FetchCollab(int id,int NotesId)
        {
            return context.CollaborativeTable.Where<CollaborativeEntity>(a => a.UserId == id && a.NotesId==NotesId).ToList();
        }
        public CollaborativeEntity RemoveCollab(int id,int NotesId,string CollabEmail)
        {
            var check = context.CollaborativeTable.FirstOrDefault(o => o.UserId == id && o.NotesId == NotesId && o.CollabEmail == CollabEmail);
            if (check != null)
            {
                if (check.Trash)
                {
                    check.Trash = false;
                }
                else
                {
                    check.Trash=true;
                }
                context.SaveChanges();
                return check;
                
            }
            else
            {
                throw new Exception();
            }
            
            
        }
    }
}
