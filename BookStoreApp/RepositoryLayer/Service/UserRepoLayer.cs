using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class UserRepoLayer : IUserRepoLayer
    {
        private readonly IConfiguration config;

        public UserRepoLayer(IConfiguration config)
        {
            this.config = config;
        }

        public bool UserRegister(UserDetailsModel userDetails)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using(sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpUserRegister_Table", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@UserName", userDetails.FullName);
                    sqlCommand.Parameters.AddWithValue("@Email", userDetails.EmailID);
                    sqlCommand.Parameters.AddWithValue("@Password", Encrypt(userDetails.Password));
                    sqlCommand.Parameters.AddWithValue("@MobileNo", userDetails.MobileNo);

                    int result = sqlCommand.ExecuteNonQuery();
                    if(result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch(Exception e)
                {
                    throw e;
                }
                finally
                {   
                    sqlConnection.Close();
                }
        }

        public string UserLogin(UserLoginModel LoginDetails)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
           try
           {
              SqlCommand sqlCommand = new SqlCommand("SpUserLogin", sqlConnection);
              sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

              sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmailId", LoginDetails.EmailID);
                    sqlCommand.Parameters.AddWithValue("PassWord", Encrypt(LoginDetails.Password));
                    var result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        string query = "SELECT UserID from UserRegister_Table WHERE EmailID = '" + result + "'";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        var Id = command.ExecuteScalar();
                        var token = GenerateToken(LoginDetails.EmailID,Id.ToString());
                        return token;
                    }
                    else
                    {
                        return null;
                    }
               
           }
           catch (Exception e)
           {
              throw e;
           }
           finally
           {
             sqlConnection.Close();
           }
        }
        
        public string GenerateToken(string EmailID, string userID)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config[("Jwt:Key")]));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Role,"User"),
                        new Claim(ClaimTypes.Email, EmailID),
                        new Claim("userID", userID.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Encrypt(string passWord)
        {
            byte[] b = Encoding.UTF8.GetBytes(passWord);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public bool ResetPassword(string Email,string Password,string ConfirmPassword)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    if (Password.Equals(ConfirmPassword))
                    {
                        SqlCommand sqlCommand = new SqlCommand("SpResetPassword", sqlConnection);
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlConnection.Open();
                        sqlCommand.Parameters.AddWithValue("@EmailID",Email);
                        sqlCommand.Parameters.AddWithValue("@ConfirmPassword", Encrypt(ConfirmPassword));
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
        public string ForgotPassword(String Email)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    sqlConnection.Open();
                   SqlCommand sqlCommand = new SqlCommand("Select * from UserRegister_Table WHERE EmailID=@EmailID", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@EmailID", Email);

                    SqlDataReader reader= sqlCommand.ExecuteReader();
                    string id="";
                    string Name="";
                    string EmailId = "";
                    while(reader.Read())
                    {
                        id = reader["UserID"].ToString();
                        Name = reader["FullName"].ToString();
                        EmailId = reader["EmailID"].ToString();
                    }
                    //var token = this.GenerateToken(EmailId, id);
                    //MSMQ mssq = new MSMQ();
                    //mssq.SendMessage(token, EmailId, Name);
                    //return token.ToString();

                    if (EmailId != "")
                    {
                        var token = this.GenerateToken(EmailId, id);
                        MSMQ mssq = new MSMQ();
                        mssq.SendMessage(token, EmailId, Name);
                        return token.ToString();
                    }
                    else
                    {
                        return "Invalid EmailID";
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
    }
}
