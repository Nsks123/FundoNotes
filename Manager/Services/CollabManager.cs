using Common.RequestModels;
using Manager.Interfaces;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Services
{
    public class CollabManager : ICollabManager
    {
        private readonly ICollabRepository repository;
        public CollabManager(ICollabRepository repository) { 
            this.repository = repository;
        }
        public CollaborativeEntity AddColab(int id, int NotesId, CreateCollabModel model)
        {
            return repository.AddColab(id, NotesId, model);
        }
        public List<CollaborativeEntity> FetchCollab(int id, int NotesId)
        {
            return repository.FetchCollab(id, NotesId);
        }
        public CollaborativeEntity RemoveCollab(int id, int NotesId, string CollabEmail)
        {
            return repository.RemoveCollab(id,NotesId, CollabEmail);
        }
    }
}
