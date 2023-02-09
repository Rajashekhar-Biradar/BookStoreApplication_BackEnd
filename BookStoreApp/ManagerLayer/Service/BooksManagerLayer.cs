using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class BooksManagerLayer : IBooksManagerLayer
    {
        private readonly IBooksRepoLayer booksRepoLayer;
        public BooksManagerLayer(IBooksRepoLayer booksRepoLayer)
        {
            this.booksRepoLayer = booksRepoLayer;
        }

        public bool AddBooks(BooksModel booksModel)
        {
            try
            {
                return booksRepoLayer.AddBooks(booksModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateBooksDetails(BooksModel booksModel, int BookID)
        {
            try
            {
                return booksRepoLayer.UpdateBooksDetails(booksModel, BookID);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteBookDetails(int bookID)
        {
            try
            {
                return booksRepoLayer.DeleteBookDetails(bookID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public BooksModel GetBookByID(int bookID)
        {
            try
            {
                return booksRepoLayer.GetBookByID(bookID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<BooksModel> GetAllBooks()
        {
            try
            {
                return booksRepoLayer.GetAllBooks();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
