using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class BooksRepoLayer : IBooksRepoLayer
    {
        private readonly IConfiguration config;
        public BooksRepoLayer(IConfiguration config) 
        { 
            this.config = config;
        }

        public bool AddBooks(BooksModel booksModel)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using(sqlConnection)
            try
            {
                    SqlCommand sqlCommand = new SqlCommand("SpBookDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookName", booksModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@BookAuthor", booksModel.BookAuthor);
                    sqlCommand.Parameters.AddWithValue("@Rating", booksModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@NoOfPeopleRated", booksModel.NoOfPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@BookImage", booksModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@ActualPrice", booksModel.ActualPrice);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", booksModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", booksModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookQuatity", booksModel.BookQuantity);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateBooksDetails(BooksModel booksModel,int BookID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpUpdateBookDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookID", BookID);
                    sqlCommand.Parameters.AddWithValue("@BookName", booksModel.BookName);
                    sqlCommand.Parameters.AddWithValue("@BookAuthor", booksModel.BookAuthor);
                    sqlCommand.Parameters.AddWithValue("@Rating", booksModel.Rating);
                    sqlCommand.Parameters.AddWithValue("@NoOfPeopleRated", booksModel.NoOfPeopleRated);
                    sqlCommand.Parameters.AddWithValue("@BookImage", booksModel.BookImage);
                    sqlCommand.Parameters.AddWithValue("@ActualPrice", booksModel.ActualPrice);
                    sqlCommand.Parameters.AddWithValue("@DiscountPrice", booksModel.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("@BookDescription", booksModel.BookDescription);
                    sqlCommand.Parameters.AddWithValue("@BookQuatity", booksModel.BookQuantity);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public bool DeleteBookDetails(int bookID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpDeleteBookDetails", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@BookID", bookID);
                    var result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
        public BooksModel GetBookByID(int bookID)
        {
            BooksModel booksModel= new BooksModel();
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using(sqlConnection)
            try
            {
                
                SqlCommand sqlCommand = new SqlCommand("SpGetBookByID", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@BookID", bookID);

                var result = sqlCommand.ExecuteReader();

                if(result.HasRows)
                {
                    while(result.Read())
                    {
                        booksModel.BookName = result["BookName"].ToString();
                        booksModel.BookAuthor = result["BookAuthor"].ToString();
                        booksModel.Rating = result["Rating"].ToString();
                        booksModel.NoOfPeopleRated = result["NoOfPeopleRated"].ToString();
                        booksModel.BookImage = result["BookImage"].ToString();
                        booksModel.ActualPrice = Convert.ToInt32(result["ActualPrice"]);
                        booksModel.DiscountPrice = Convert.ToInt32(result["DiscountPrice"]);
                        booksModel.BookDescription = result["BookDescription"].ToString();
                        booksModel.BookQuantity = Convert.ToInt32(result["BookQuatity"]);

                    }

                }
                return booksModel;
            }
            catch(Exception ex)
            {
               throw ex;
            }
            finally
            {
               sqlConnection.Close();
            }
        }

        public List<BooksModel> GetAllBooks()
        {
            List<BooksModel> result = new List<BooksModel>();
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection) 
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpGetAllBooks", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlConnection.Open();

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    BooksModel booksModel = null;
                    if (dataReader.HasRows) 
                    {
                        while(dataReader.Read())
                        {
                            booksModel = new BooksModel();

                            booksModel.BookName = dataReader["BookName"].ToString();
                            booksModel.BookAuthor = dataReader["BookAuthor"].ToString();
                            booksModel.Rating = dataReader["Rating"].ToString();
                            booksModel.NoOfPeopleRated = dataReader["NoOfPeopleRated"].ToString();
                            booksModel.BookImage = dataReader["BookImage"].ToString();
                            booksModel.ActualPrice = Convert.ToInt32(dataReader["ActualPrice"]);
                            booksModel.DiscountPrice = Convert.ToInt32(dataReader["DiscountPrice"]);
                            booksModel.BookDescription = dataReader["BookDescription"].ToString();
                            booksModel.BookQuantity = Convert.ToInt32(dataReader["BookQuatity"]);

                            result.Add(booksModel);
                        }
                    }
                    return result;

                }catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }
    }
}
