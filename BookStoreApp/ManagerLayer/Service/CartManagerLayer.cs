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
    public class CartManagerLayer : ICartManagerLayer
    {
        public readonly ICartRepoLayer cartRepoLayer;
        public CartManagerLayer(ICartRepoLayer cartRepoLayer)
        {
            this.cartRepoLayer = cartRepoLayer;
        }

        public bool AddBookToCart(CartModel cartModel, long userID)
        {
            try
            {
                return cartRepoLayer.AddBookToCart(cartModel, userID);

            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateQuantity(long CartID, long CartQuantity)
        {
            try
            {
                return cartRepoLayer.UpdateQuantity(CartID,CartQuantity);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteFromCart(long CartID)
        {
            try
            {
                return cartRepoLayer.DeleteFromCart(CartID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<GetCartModel> GetAllBooksFromCart(long userID)
        {
            try
            {
                return cartRepoLayer.GetAllBooksFromCart(userID);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
