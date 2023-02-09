using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepoLayer
    {
        public string AdminLogin(UserLoginModel adminLoginDetails);
    }
}
