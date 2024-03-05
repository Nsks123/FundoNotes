using Common.RequestModels;
using Common.ResponseModel;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Context;
using Repository.Entity;
using System.Collections.Generic;
using System;

namespace Fundo_Notes1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly INoteManger noteManger;
        public LabelController(INoteManger noteManger)
        {
            this.noteManger = noteManger;
        }
        [Authorize]
        [HttpPost]
        [Route("Add_Label")]
        public ActionResult AddLable(int NoteId, AddLabel label)
        {
            int id = Convert.ToInt32(User.FindFirst("User Id").Value);
            var response = noteManger.AddLabel(NoteId, id, label);
            if (response != null)
            {
                return Ok(new ResModel<LabelNote> { Success = true, Message = "Lable Add Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<LabelNote> { Success = false, Message = "Lable Add Failed", Data = response });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Fetch")]
        public ActionResult Fetch(int NoteId, string LabelName)
        {
            int id = Convert.ToInt32(User.FindFirst("User Id").Value);
            List<LabelNote> response = noteManger.FetchLabel(NoteId, LabelName);
            if (response != null)
            {
                return Ok(new ResModel<List<LabelNote>> { Success = true, Message = "Fetch Lable Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<List<LabelNote>> { Success = false, Message = "Fetch Lable Failed", Data = response });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateLabel")]
        public ActionResult UpdateLabel(int NoteId, string LabelName)
        {
            try
            {
                int id = Convert.ToInt32(User.FindFirst("User Id").Value);
                var response = noteManger.UpdateLabel(NoteId, LabelName);
                if (response != null)
                {
                    return Ok(new ResModel<LabelNote> { Success = true, Message = "Update Label Success", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<LabelNote> { Success = false, Message = "Update Label Failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<LabelNote> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("Remove")]
        public ActionResult RemoveLabel(int NoteId, int LabelId)
        {
            int id = Convert.ToInt32(User.FindFirst("User Id").Value);
            var response = noteManger.RemoveLabel(NoteId, LabelId);
            if (response != null)
            {
                return Ok(new ResModel<LabelNote> { Success = true, Message = "Lable Remove Success", Data = response });
            }
            else
            {
                return BadRequest(new ResModel<LabelNote> { Success = false, Message = "Lable Remove Failed", Data = response });
            }
        }

    }

}
