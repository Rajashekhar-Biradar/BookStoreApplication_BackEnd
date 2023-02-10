using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStoreApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WhislistController : ControllerBase
    {
        public readonly IWhislistManagerLayer whislistManagerLayer;
        public WhislistController(IWhislistManagerLayer whislistManagerLayer)
        {
            this.whislistManagerLayer = whislistManagerLayer;
        }

        [HttpPost("AddToWhislist")]
        public IActionResult AddToWhislist(long bookID)
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e=> e.Type == "userID").Value);

            var response = this.whislistManagerLayer.AddToWhislist(userID, bookID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Book added to Whislist", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed" });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpDelete("DeleteFromWhislist")]
        public IActionResult DeleteFromWhislist(long whislistID)
        {
            var response = this.whislistManagerLayer.DeleteFromWhislist(whislistID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Book deleted from Whislist", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed" });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllBookFromWhislist")]
        public IActionResult GetAllBooksFromWhislist()
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.whislistManagerLayer.GetAllBooksFromWhislist(userID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "All book fetched from whislist", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed" });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
