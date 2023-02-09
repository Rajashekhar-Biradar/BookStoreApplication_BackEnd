using ModelLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IUserManagerLayer
    {
        public bool UserRegister(UserDetailsModel userDetails);
        public string UserLogin(UserLoginModel LoginDetails);
        public bool ResetPassword(string Email, string Password, string ConfirmPassword);
        public string ForgotPassword(String Email);
    }
}
