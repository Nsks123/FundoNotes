using Common.RequestModels;
using Common.ResponseModel;
using Manager.Interfaces;
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
    public class NoteController : ControllerBase
    {
        private readonly INoteManger noteManger;
        public NoteController(INoteManger noteManger)
        {
            this.noteManger = noteManger;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public ActionResult AddNote(CreateNoteModel model)
        {
            int id = Convert.ToInt32(User.FindFirst("User Id").Value);
            var response = noteManger.CreateNote(model,id);
            if (response != null)
            {

                return Ok(new ResModel<NoteEntity> { Success=true,Message="Created Note Success",Data=response} );

            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Create Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("{id}",Name = "GetNote")]
        public ActionResult FetchData(int id)
        {
            List<NoteEntity> data = noteManger.GetNote(id);
            if (data != null)
            {

                return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Get Note Successful", Data = data });

            }
            else
            {
                return BadRequest(new ResModel<List<NoteEntity>> { Success = false, Message = "Get Note Failure", Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateNote(int NotesId, UpdateNoteModel model)
        {
            
            var response = noteManger.UpdateNote(NotesId, model);
            if (response != null)
            {

                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Update Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Update Note Failed", Data = response });
            }


        }
        [Authorize]
        [HttpPut]
        [Route("Trash")]
        public ActionResult Trash(int NotesId)
        {

            var response = noteManger.Trash(NotesId);
            if (response != null)
            {

                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Trash Note Success", Data = response });

            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Trash Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Delete")]
        public ActionResult DeleteNote(int NotesId, int id)
        {
            
            try
            {
                int idd = Convert.ToInt32(User.FindFirst("User Id").Value);
                var response = noteManger.DeleteNote(NotesId, idd);
                if (response != null)
                {

                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "Delete Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Delete Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Archive")]
        public ActionResult IsArchive(int NotesId)
        {
            try
            {
                var response = noteManger.Archive(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "IsArchive Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "IsArchive Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Pin")]
        public ActionResult IsPin(int NotesId)
        {
            try
            {
                var response = noteManger.Pin(NotesId);
                if (response != null)
                {
                    return Ok(new ResModel<NoteEntity> { Success = true, Message = "IsPin Note Success", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "IsPin Note Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("Colour")]
        public ActionResult Colour(int NotesId) 
        { 

            var response= noteManger.Colour(NotesId);
            if(response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Colour Note Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Colour Note Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Remind")]
        public ActionResult Remind(int NotesId) 
        {
            var response = noteManger.Reminder(NotesId);
            if (response != null)
            {
                return Ok(new ResModel<NoteEntity> { Success = true, Message = "Reminder Note Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Reminder Note Failed", Data = response });
            }

        }
        [Authorize]
        [HttpPut]
        [Route("UploadImage")]
        public ActionResult UploadImage(string filepath, int NotesId, int Id)
        {
            var response=noteManger.UploadImage(filepath, NotesId,Id);
            if (response != null)
            {
                return Ok(new ResModel<string> { Success = true, Message = "Upload Image Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<string> { Success = false, Message = "Upload Image Failed", Data = response });
            }
        }
    }
}
