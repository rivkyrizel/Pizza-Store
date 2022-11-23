using BlApi;
using Dal;

namespace BlImplementation;

internal class BlCart : ICart
{
    private DalApi.IDal dal = new Dal.DalList();
   private  bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
    public BO.Cart AddToCart(BO.Cart cart, int productId)
    {
        DO.Product p = dal.Product.Get(productId);
        foreach (BO.OrderItem item in cart.Items)
            if (p.InStock > 1)
                if (item.ProductId == productId)
                {
                    item.Amount += 1;
                    item.TotalPrice += p.Price;
                }
                else
                {
                    BO.OrderItem oItem = new BO.OrderItem();
                    oItem.ID = DataSource.Config.OrderItemID;
                    oItem.Name = p.Name;
                    oItem.ProductId = p.ID;
                    oItem.Price = p.Price;
                    oItem.Amount = 1;
                    oItem.TotalPrice = p.Price;
                }
        return cart;
    }

    public BO.Cart confirmOrder(BO.Cart cart, string name, string email, string address)
    {
        TimeSpan timeSpan;
        timeSpan = TimeSpan.Zero;
        foreach (BO.OrderItem item in cart.Items)
        {
            DO.Product p = dal.Product.Get(item.ProductId);
            if (p.InStock < item.Amount)
                throw new Exception("hiiiiiiii");
        }
        if (name == "" || email == "" || address == "")
            throw new Exception("one or more is empty");
        if(!IsValidEmail(email))
            throw new Exception("email not valid");
        BO.Order order = new BO.Order();
        order.CustomerAddress = address;
        order.CustomerEmail = email;
        order.CustomerName = name;
        order.DeliveryDate = DateTime.MinValue;
        order.ShipDate = DateTime.MinValue;
        order.PaymentDate = DateTime.Now;
        order.Items = cart.Items;
        order.Status = 0;
        order.TotalPrice = cart.Totalprice;



    }

    public BO.Cart updateAmount(BO.Cart cart, int productId, int newAmount)
    {
        DO.Product p = dal.Product.Get(productId);
        foreach (BO.OrderItem item in cart.Items)
        {
            if (item.ProductId == productId)
            {
                if (item.Amount > newAmount)
                {
                    item.TotalPrice -= p.Price * (item.Amount - newAmount);
                    item.Amount = newAmount;
                }
                else if (item.Amount < newAmount)
                {
                    if (p.InStock >= newAmount)
                    {
                        item.Amount = newAmount;
                        item.TotalPrice += p.Price * newAmount;
                    }
                }
                else if (newAmount == 0)
                {
                    cart.Items = cart.Items.Where(i => i != item);
                    break;
                }
            }
        }
        return cart;
    }
}

