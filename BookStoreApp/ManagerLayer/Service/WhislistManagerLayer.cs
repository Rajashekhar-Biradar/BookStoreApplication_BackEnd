using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class WhislistManagerLayer : IWhislistManagerLayer
    {
        public readonly IWhislistRepoLayer whislistRepoLayer;
        public WhislistManagerLayer(IWhislistRepoLayer whislistRepoLayer)
        {
            this.whislistRepoLayer = whislistRepoLayer;
        }
        public bool AddToWhislist(long userID, long bookID)
        {
            try
            {
                return whislistRepoLayer.AddToWhislist(userID, bookID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteFromWhislist(long whislistID)
        {
            try
            {
                return whislistRepoLayer.DeleteFromWhislist(whislistID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<GetWhislistModel> GetAllBooksFromWhislist(long userID)
        {
            try
            {
                return whislistRepoLayer.GetAllBooksFromWhislist(userID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
