using Common.RequestModels;
using Repository.Entity;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.Interfaces
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public User UserRegistration(RegisterModel model)
        {
            return repository.UserRegistration(model);
        }
        public string UserLogin(LoginModel model)
        {
            return repository.UserLogin(model);
        }
        public string ForgetPass(string Email)
        {
            return repository.ForgetPass(Email);
        }
        public bool CheckEmail(string Email)
        {
            return repository.CheckEmail(Email);
        }
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            return  repository.ResetPassword(Email, resetPasswordModel);
        }
        public string GenerateToken(string Email, int Id)
        {
            return repository.GenerateToken(Email, Id);
        }

    }
}
