using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IWhislistManagerLayer
    {
        public bool AddToWhislist(long userID, long bookID);
        public bool DeleteFromWhislist(long whislistID);
        public List<GetWhislistModel> GetAllBooksFromWhislist(long userID);
    }
}
