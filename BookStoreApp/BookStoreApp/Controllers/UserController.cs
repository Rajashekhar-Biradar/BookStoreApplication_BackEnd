using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserManagerLayer userManagerLayer;
        public UserController(IUserManagerLayer userManagerLayer)
        {
            this.userManagerLayer = userManagerLayer;
        }

        [HttpPost("UserRegistration")]
        public IActionResult UserRegister(UserDetailsModel userDetails)
        {
            try
            {
                var response = this.userManagerLayer.UserRegister(userDetails);
                if (response != null)
                {
                    return this.Ok(new {success = true,message = "Registration Successful",data=response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "registration failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("UserLogin")]
        public IActionResult UserLogIn(UserLoginModel LoginDetails)
        {
            try
            {
                var response = this.userManagerLayer.UserLogin(LoginDetails);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "login Successful", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "login failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string Email,string Password,string ConfirmPassword)
        {
            try
            {
                var response = this.userManagerLayer.ResetPassword(Email,Password,ConfirmPassword);
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Password has been reset successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Reset Password failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost ("ForgotPassword")]

        public IActionResult ForgotPassword(string EmailID)
        {
            try
            {
                var response = this.userManagerLayer.ForgotPassword(EmailID);
                if (response == "Invalid EmailID")
                {
                    return this.BadRequest(new { success = false, message = "InValid Email",data=response });
                }
                else if (response != null)
                {
                    return this.Ok(new { success = true, message = "Message sent successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
               
            }
            catch(Exception ex)
            {  
                throw ex; 
            }
        }
    }
}
