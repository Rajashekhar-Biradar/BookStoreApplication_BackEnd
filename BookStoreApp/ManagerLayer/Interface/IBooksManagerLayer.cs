﻿using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IBooksManagerLayer
    {
        public bool AddBooks(BooksModel booksModel);
        public bool UpdateBooksDetails(BooksModel booksModel, int BookID);
        public bool DeleteBookDetails(int bookID);
        public BooksModel GetBookByID(int bookID);
        public List<BooksModel> GetAllBooks();
    }
}
