using BO;
using System.Runtime.CompilerServices;

namespace BlApi;

public interface ICart
{

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Cart AddToCart(Cart? cart, int productId, bool isRegistered = false);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Cart updateAmount(Cart cart, int productId, int newAmount, bool isRegistered = false);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void confirmOrder(Cart cart, bool isRegistered = false);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Cart GetCart(int userId);
}

