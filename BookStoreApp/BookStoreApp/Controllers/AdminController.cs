using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public readonly IAdminManagerLayer adminManagerLayer;
        public AdminController(IAdminManagerLayer adminManagerLayer)
        {
            this.adminManagerLayer = adminManagerLayer;
        }

        [HttpPost("AdminLogin")]
        public IActionResult AdminLogin(UserLoginModel adminLoginDetails)
        {
            var response = this.adminManagerLayer.AdminLogin(adminLoginDetails);
            try
            {
                if(response != null)
                {
                    return this.Ok(new {success= true,message="Login Successful",data=response});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
