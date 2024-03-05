using Common;
using Common.RequestModels;
using Common.ResponseModel;
using Manager.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Context;
using Repository.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fundo_Notes1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IBus bus;
        private readonly DemoContext demoContext;
        private readonly ILogger <UserController> logger;

        public UserController(IUserManager userManager,IBus bus,DemoContext demoContext, ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.bus = bus;
            this.demoContext = demoContext;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Reg")]
        public ActionResult Register(RegisterModel model)
        {
            try
            {
                var response = userManager.UserRegistration(model);
                if (response != null)
                {
                    logger.LogInformation("Register successful");
                    return Ok(new ResModel<User> { Success = true, Message = "register successfull", Data = response });
                }
                else
                {
                    logger.LogWarning("Register Unsuccessful");
                    return BadRequest(new ResModel<User> { Success = false, Message = "Register failed", Data = response });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<User> { Success = false, Message =ex.Message, Data = null });

            }
           
        }
        [HttpPost]
        [Route("log")]
        public ActionResult Login(LoginModel model)
        {
            try
            {
                string response = userManager.UserLogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<string> { Success = true, Message = "Login successful", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = "Login Failed", Data = response });
                }
            }

            catch (Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });

            }
        }
        [HttpPost]
        [Route("Forgot")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                if (userManager.CheckEmail(Email))
                {
                   Send mail = new Send();
                   var  forgotPasswordModel = userManager.ForgetPass(Email);
                   var checkmail=demoContext.UserTable.FirstOrDefault(x => x.Email== Email);
                    var token = userManager.GenerateToken(checkmail.Email, checkmail.Id);
                    mail.SendMail(Email,token);
                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endPoint=await bus.GetSendEndpoint(uri);
                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResModel<string> {Success=true, Message ="Mail sent successfully",Data=token});

                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = "Email Does Not Exit", Data = Email });

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        [Authorize]
        [HttpPost]
        [Route("Reset")]
        public ActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("Email").Value;
                if (userManager.ResetPassword(Email, reset))
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Password Changed", Data = true });

                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Password Not Changed", Data = false });
                }


            }
            catch(Exception ex)
            { 
                throw ex; 
            }
        }


    }
}
