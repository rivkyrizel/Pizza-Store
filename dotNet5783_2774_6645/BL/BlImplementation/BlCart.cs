using BlApi;
using Dal;

namespace BlImplementation;

internal class BlCart : ICart
{

    private DalApi.IDal dal = new Dal.DalList();

    /// <summary>
    /// checks if email is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns> true or false </returns>
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

    /// <summary>
    ///  converts from BO object to DO object
    /// </summary>
    /// <param name="boItem"></param>
    /// <returns> DO OredrItem object </returns>
    private DO.OrderItem castBOtoDO(BO.OrderItem boItem)
    {
        DO.OrderItem doItem = new DO.OrderItem();
        doItem.ID = boItem.ID;
        doItem.Price = boItem.Price;
        doItem.ProductID = boItem.ProductId;
        doItem.Amount = boItem.Amount;
        return doItem;
    }

    /// <summary>
    ///  adds new item to cart
    /// </summary>
    /// <param name="cart"> users cart </param>
    /// <param name="productId"> id of product to add to cart </param>
    /// <returns> updated cart </returns>
    /// <exception cref="BlOutOfStockException"> item not in stock </exception>
    /// <exception cref="BlIdNotFound"> product ID is invalid </exception>
    public BO.Cart AddToCart(BO.Cart cart, int productId)
    {
        try
        {
            DO.Product p = dal.Product.Get(productId);
            if (p.InStock > 1)
            {
                if (cart.Items != null)
                    foreach (BO.OrderItem item in cart.Items)
                        if (item.ProductId == productId)
                        {
                            item.Amount += 1;
                            item.TotalPrice += p.Price;
                            return cart;
                        }


                BO.OrderItem oItem = new BO.OrderItem();
                oItem.ID = DataSource.Config.OrderItemID;
                oItem.Name = p.Name;
                oItem.ProductId = p.ID;
                oItem.Price = p.Price;
                oItem.Amount = 1;
                if (cart.Items != null)
                    cart.Items = cart.Items.Append(oItem);
                else
                {
                    List<BO.OrderItem> items = new List<BO.OrderItem>();
                    items.Add(oItem);   
                    cart.Items = items; 
                }
                return cart;
            }
            throw new BlOutOfStockException();

        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

    /// <summary>
    ///  confirms order and sends carts details to datasource
    /// </summary>
    /// <param name="cart"> users cart </param>
    /// <param name="name"> users name</param>
    /// <param name="email"> users email</param>
    /// <param name="address"> users address </param>
    /// <exception cref="BlOutOfStockException"> item out of stock </exception>
    /// <exception cref="BlNegativeAmountException"> cannot order negative amount </exception>
    /// <exception cref="BlNullValueException"> user details missing </exception>
    /// <exception cref="BlInvalidEmailException"> invalid email </exception>
    /// <exception cref="BlIdNotFound"> id does not exist </exception>
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

    /// <summary>
    ///  updates amount of product in users cart
    /// </summary>
    /// <param name="cart"> users cart </param>
    /// <param name="productId"> id of product to update amount</param>
    /// <param name="newAmount"> the new amount to update in the order </param>
    /// <returns> updated cart </returns>
    /// <exception cref="BlIdNotFound"> id of product is invalid </exception>
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

