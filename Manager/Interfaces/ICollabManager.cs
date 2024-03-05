using Common.RequestModels;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interfaces
{
   public interface ICollabManager
    {
        public CollaborativeEntity AddColab(int id, int NotesId, CreateCollabModel model);
        public List<CollaborativeEntity> FetchCollab(int id, int NotesId);
        public CollaborativeEntity RemoveCollab(int id, int NotesId, string CollabEmail);
    }
}
