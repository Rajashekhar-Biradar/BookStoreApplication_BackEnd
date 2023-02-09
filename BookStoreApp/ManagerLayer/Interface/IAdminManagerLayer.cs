using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IAdminManagerLayer
    {
        public string AdminLogin(UserLoginModel adminLoginDetails);
    }
}
