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
    public class CartRepoLayer : ICartRepoLayer
    {
        public readonly IConfiguration config;
        public CartRepoLayer(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddBookToCart(CartModel cartModel,long userID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAddBookToCart", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserID",userID);
                    sqlCommand.Parameters.AddWithValue("@BookID", cartModel.BookID);
                    sqlCommand.Parameters.AddWithValue("@CartQuantity", cartModel.CartQuantity);

                    var result = sqlCommand.ExecuteNonQuery();

                    if(result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
        }

        public bool UpdateQuantity(long CartID,long CartQuantity)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpUpdateCartQuantity", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CartID", CartID);
                    sqlCommand.Parameters.AddWithValue("@CartQuantity",CartQuantity);

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
        public bool DeleteFromCart(long CartID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Delete from CartTable Where CartID = @CartID", sqlConnection);

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@CartID", CartID);
                    

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
        public List<GetCartModel> GetAllBooksFromCart(long userID)
        {
            List<GetCartModel> result = new List<GetCartModel>();
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Select * from CartTable WHERE UserID = @UserID", sqlConnection);
                    
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    GetCartModel getCartModel = null;
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            getCartModel = new GetCartModel();

                            getCartModel.CartId = Convert.ToInt32(dataReader["CartID"]);
                            getCartModel.BookID = Convert.ToInt32(dataReader["BookID"]);
                            getCartModel.CartQuantity = Convert.ToInt32(dataReader["CartQuantity"]);
                            result.Add(getCartModel);
                        }
                    }
                    return result;

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
    }
}
