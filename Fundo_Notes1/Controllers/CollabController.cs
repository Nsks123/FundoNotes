using Common.RequestModels;
using Common.ResponseModel;
using Manager.Interfaces;
using Manager.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Entity;
using System;
using System.Collections.Generic;

namespace Fundo_Notes1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollabManager manager;
        public CollabController(ICollabManager manager)
        {
            this.manager = manager;
        }
        [Authorize]
        [HttpPost]
        [Route("Collab")]
        public ActionResult CreateCollab(int NotesId, CreateCollabModel model)
        {
            try
            {


                int id = Convert.ToInt32(User.FindFirst("User Id").Value);
                var response = manager.AddColab(id, NotesId, model);
                if (response != null)
                {
                    return Ok(new ResModel<CollaborativeEntity> { Success = true, Message = "Collab Add Success", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<CollaborativeEntity> { Success = false, Message = "Collab Add Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<CollaborativeEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("FetchCollab")]
        public ActionResult FetchCollab(int id,int NotesId)
        {
            var response = manager.FetchCollab(id, NotesId);
            if (response != null)
            {
                return Ok(new ResModel<List<CollaborativeEntity>> { Success = true, Message = "Fetch Collab  Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<List<CollaborativeEntity>> { Success = false, Message = "Fetch Collab Failed", Data = response });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("TrashCollab")]
        public ActionResult TrashCollab(int id, int NotesId,String CollabEmail)
        {
            try
            {
                return Ok(new ResModel<CollaborativeEntity> { Success = true, Message = " Trash Collab Success", Data = manager.RemoveCollab(id, NotesId, CollabEmail) });
            }
            
            catch (Exception ex)
            {
                return BadRequest(new ResModel<CollaborativeEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }


    }
}
