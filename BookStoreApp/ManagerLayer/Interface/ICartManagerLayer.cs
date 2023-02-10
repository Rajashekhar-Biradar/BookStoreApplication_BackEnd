using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICartManagerLayer
    {
        public bool AddBookToCart(CartModel cartModel, long userID);
        public bool UpdateQuantity(long CartID, long CartQuantity);
        public bool DeleteFromCart(long CartID);
        public List<GetCartModel> GetAllBooksFromCart(long userID);
    }
}
