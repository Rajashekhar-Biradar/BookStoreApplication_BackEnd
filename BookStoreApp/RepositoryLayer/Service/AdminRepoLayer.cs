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
    public class AdminRepoLayer : IAdminRepoLayer
    {
        public readonly IConfiguration config;
        public AdminRepoLayer(IConfiguration config)
        {
            this.config = config;
        }
        public string AdminLogin(UserLoginModel adminLoginDetails)
        {
            SqlConnection sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            using (sqlConnection)
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("SpAdminLogin", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@EmailId", adminLoginDetails.EmailID);
                    sqlCommand.Parameters.AddWithValue("PassWord", adminLoginDetails.Password);
                    var result = sqlCommand.ExecuteScalar();

                    if (result != null)
                    {
                        string query = "SELECT AdminID from AdminTable WHERE EmailID = '" + result + "'";
                        SqlCommand command = new SqlCommand(query, sqlConnection);
                        var Id = command.ExecuteScalar();
                        var token = GenerateToken(adminLoginDetails.EmailID, Id.ToString());
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
                        new Claim(ClaimTypes.Role,"Admin"),
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
    }   
}
