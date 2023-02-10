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
    public class CartController : ControllerBase
    {
        public readonly ICartManagerLayer cartManagerLayer;
        public CartController(ICartManagerLayer cartManagerLayer)
        {
            this.cartManagerLayer = cartManagerLayer;
        }
        [HttpPost("AddToCart")]
        public IActionResult AddBookToCart(CartModel cartModel)
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = cartManagerLayer.AddBookToCart(cartModel, userID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Book added to cart successfully"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "failed" });
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("UpdateCartQuantity")]
        public IActionResult UpdateQuantity(long CartID, long CartQuantity)
        {
            var response = cartManagerLayer.UpdateQuantity(CartID, CartQuantity);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Cart Quantity updated successfully" });
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
        [HttpDelete("DeleteFromCart")]
        public IActionResult DeleteFromCart(long CartID)
        {
            var response = cartManagerLayer.DeleteFromCart(CartID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Deleted from Cart" });
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
        [HttpGet("GetBooksFromCart")]

        public IActionResult GetAllBooksFromCart()
        {
            long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
            var response = this.cartManagerLayer.GetAllBooksFromCart(userID);
            try
            {
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "All Details fetched successfully", data = response });
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
