using Common.RequestModels;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interfaces
{
    public interface IUserManager
    {
        public User UserRegistration(RegisterModel model);
        public string UserLogin(LoginModel model);
        public string ForgetPass(string Email);
        public bool CheckEmail(string Email);
        public string GenerateToken(string Email, int Id);
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
    }
}
