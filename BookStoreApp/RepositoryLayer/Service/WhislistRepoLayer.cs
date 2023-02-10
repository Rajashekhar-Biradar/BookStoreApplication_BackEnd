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
    public class WhislistRepoLayer : IWhislistRepoLayer
    {
        public readonly IConfiguration config;
        public WhislistRepoLayer(IConfiguration config)
        {
            this.config = config;
        }

        public bool AddToWhislist(long userID,long bookID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("AddToWhislist", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);
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
        public bool DeleteFromWhislist(long whislistID)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Delete from WhislistTable Where WhislistID = @WhislistID", sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@WhislistID", whislistID);

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
        public List<GetWhislistModel> GetAllBooksFromWhislist(long userID)
        {
            List<GetWhislistModel> result = new List<GetWhislistModel>();
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Select * from WhislistTable WHERE UserID = @UserID", sqlConnection);

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);

                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    GetWhislistModel getWhislistModel = null;
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            getWhislistModel = new GetWhislistModel();

                            getWhislistModel.WhislistID = Convert.ToInt32(dataReader["WhislistID"]);
                            getWhislistModel.BookID = Convert.ToInt32(dataReader["BookID"]);
                            getWhislistModel.UserID = Convert.ToInt32(dataReader["UserID"]);
                            result.Add(getWhislistModel);
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
