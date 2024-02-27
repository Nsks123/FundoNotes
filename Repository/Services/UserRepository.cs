using Common.RequestModels;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Entity;
using Repository.Interfaces;
using Repository.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly DemoContext context;

        private readonly IConfiguration config;
        public UserRepository(DemoContext context,IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        public User UserRegistration(RegisterModel model)
        {
            User entity = new User();
            entity.FullName = model.FullName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.Password = Encryption("s25ed698a3q89dnc982jsuenm874mdk9", model.Password);
            User user= context.UserTable.SingleOrDefault(o => o.Email == model.Email);
            if (user != null )
            {
                throw new Exception("User Already Exist!");
            }
            else
            {
                context.UserTable.Add(entity);
                context.SaveChanges();
                return entity;

            }

        }
        public string UserLogin(LoginModel model)
        {

            var user = context.UserTable.SingleOrDefault(o => o.Email == model.Email);
            string password = Decryption("s25ed698a3q89dnc982jsuenm874mdk9", user.Password);
            

            if (user != null)
            {
                
                if ( password==model.Password)
                {
                var token = GenerateToken(user.Email, user.Id);
                    return token;
                }
                else
                {
                    throw new Exception("Incorrect password");
                }
            }

            else
            {
                throw new Exception("User not found");
            }


        }
        public  string Encryption(string key, string Password)
        {
            byte[] Initial_Vector = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Initial_Vector;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(Password);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public  string Decryption(string key, string Password)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(Password);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }


            }
        }
        public bool CheckEmail(string Email)
        {
            return context.UserTable.Any(x => x.Email == Email);    
        }
        public string GenerateToken(string Email,int Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("User Id",Id.ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string ForgetPass(string Email)
        {
            var user=context.UserTable.FirstOrDefault(x => x.Email == Email); 
            if(user != null)
            {
                var token = GenerateToken(user.Email, user.Id);
                return token;
            }
            else
            {
                return null;
            }
        }
        public bool ResetPassword(string Email,ResetPasswordModel resetPasswordModel)
        {
            User user=context.UserTable.ToList().Find(user => user.Email == Email);
            if (user !=null)
            {
                user.Password = Encryption("s25ed698a3q89dnc982jsuenm874mdk9", resetPasswordModel.ConfirmPassword);
             
                context.SaveChanges();
                return true;
                
            }
            else
            {
                return false;
            }

        }
        
        
    }
}
