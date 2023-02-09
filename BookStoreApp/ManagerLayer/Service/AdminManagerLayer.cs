using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class AdminManagerLayer :IAdminManagerLayer
    {
        public readonly IAdminRepoLayer adminRepoLayer;
        public AdminManagerLayer(IAdminRepoLayer adminRepoLayer)
        {
            this.adminRepoLayer = adminRepoLayer;
        }

        public string AdminLogin(UserLoginModel adminLoginDetails)
        {
            try
            {
                return adminRepoLayer.AdminLogin(adminLoginDetails);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
