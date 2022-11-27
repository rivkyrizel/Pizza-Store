using BO;

namespace BlApi;

public interface ICart
{
    public Cart AddToCart(Cart cart, int productId);
    public Cart updateAmount(Cart cart, int productId, int newAmount);
    public void confirmOrder(Cart cart, string name, string email, string address);
}

