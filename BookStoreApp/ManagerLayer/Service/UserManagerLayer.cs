using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class UserManagerLayer : IUserManagerLayer
    {
        public readonly IUserRepoLayer userRepoLayer;

        public UserManagerLayer(IUserRepoLayer userRepoLayer)
        {
            this.userRepoLayer = userRepoLayer;
        }

        public bool UserRegister(UserDetailsModel userDetails)
        {
            try
            {
                return userRepoLayer.UserRegister(userDetails);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string UserLogin(UserLoginModel LoginDetails)
        {
            try
            {
                return userRepoLayer.UserLogin(LoginDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ResetPassword(string Email, string Password, string ConfirmPassword)
        {
            try
            {
                return userRepoLayer.ResetPassword(Email, Password, ConfirmPassword);
            }
            catch (Exception ex)
            { 
            throw ex;
            }
        }
        public string ForgotPassword(String Email)
        {
            try
            {
                return userRepoLayer.ForgotPassword(Email);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
