using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;

namespace BookStoreApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly IBooksManagerLayer booksManagerLayer;
        public BooksController(IBooksManagerLayer booksManagerLayer)
        {
            this.booksManagerLayer = booksManagerLayer;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("AddBooks")]
        public IActionResult AddBooks(BooksModel bookModel)
        {
            var response = booksManagerLayer.AddBooks(bookModel);
            try
            {               
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "Books Added Successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Failed" });
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPut("UpdateBooks")]
        public IActionResult UpdateBooksDetails(BooksModel bookModel,int BookID)
        {
            var response = booksManagerLayer.UpdateBooksDetails(bookModel,BookID);
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "Books are Updated successfully", data = response });
                }
                else
                {
                    return this.BadRequest(new { success = true, message = "Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("DeleteBooks")]
        public IActionResult DeleteBookDetails(int bookID)
        {
            var response = booksManagerLayer.DeleteBookDetails(bookID);
            try
            {
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "Book deleted successfully" });
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

        [HttpGet("GetBookByID")]
        public IActionResult GetBookByID(int bookID)
        {
            var response = booksManagerLayer.GetBookByID(bookID);
            try
            {
                if(response != null)
                {
                    return this.Ok(new { success = true, message = "successfully fetched the book details",data=response});
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
        [HttpGet("GetAllBooks")]

        public IActionResult GetAllBooks()
        {
            var response = booksManagerLayer.GetAllBooks();
            try
            {
                if (response != null)
                {
                    return this.Ok(new { success = true, message = "All books fetched successfully ",data=response});
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
