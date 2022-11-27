using BlApi;
using Dal;

namespace BlImplementation;

internal class BlCart : ICart
{
    private DalApi.IDal dal = new Dal.DalList();
    private bool IsValidEmail(string email)
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

    private DO.OrderItem castBOtoDO(BO.OrderItem boItem)
    {
        DO.OrderItem doItem = new DO.OrderItem();
        doItem.ID = boItem.ID;
        doItem.Price = boItem.Price;
        doItem.ProductID = boItem.ProductId;
        doItem.Amount = boItem.Amount;
        return doItem;
    }
    public BO.Cart AddToCart(BO.Cart cart, int productId)
    {
        try
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
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

    public void confirmOrder(BO.Cart cart, string name, string email, string address)
    {
        try
        {
            foreach (BO.OrderItem item in cart.Items)
            {
                DO.Product p = dal.Product.Get(item.ProductId);
                if (p.InStock < item.Amount)
                    throw new BlOutOfStockException();
                if (item.Amount < 0)
                    throw new BlNegativeAmountException();
            }
            if (name == "" || email == "" || address == "")
                throw new BlNullValueException();
            if (!IsValidEmail(email))
                throw new BlInvalidEmailException();

            DO.Order order = new DO.Order();
            order.CustomerAdress = address;
            order.CustomerEmail = email;
            order.CustomerName = name;
            order.DeliveryDate = DateTime.MinValue;
            order.ShipDate = DateTime.MinValue;
            order.OrderDate = DateTime.Now;
            int orderId = dal.Order.Add(order);

            foreach (BO.OrderItem item in cart.Items)
            {
                DO.OrderItem oItem = castBOtoDO(item);
                oItem.OrderID = orderId;
                dal.OrderItem.Add(oItem);

                DO.Product product = dal.Product.Get(oItem.ProductID);
                product.InStock = product.InStock - oItem.Amount;
                dal.Product.Update(product);
            }
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

    public BO.Cart updateAmount(BO.Cart cart, int productId, int newAmount)
    {
        try
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

        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }
}

