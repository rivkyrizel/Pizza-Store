using BlApi;
using System.Text.RegularExpressions;

namespace BlImplementation;

internal class BlCart : ICart
{

    private DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// checks if email is valid
    /// </summary>
    /// <param name="email"></param>
    /// <returns> true or false </returns>
    private bool IsValidEmail(string email)
    {
        Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        return regex.IsMatch(email);

    }

    private void btnValidate_Click(object sender, EventArgs e)
    {
       
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
            DO.Product p = dal?.Product.Get(p=>p.ID == productId) ?? throw new Exception();
            if (p.Amount > 1)
            {
                if (cart.Items != null)
                    foreach (BO.OrderItem? item in cart.Items)
                        if (item?.ProductID == productId)
                        {
                            item.Amount += 1;
                            item.TotalPrice += p.Price;
                            return cart;
                        }


                BO.OrderItem oItem = BlUtils.cast<BO.OrderItem, DO.Product>(p);
                oItem.ProductID = p.ID;
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
    public void confirmOrder(BO.Cart cart)
    {
        try
        {
            foreach (BO.OrderItem? item in cart?.Items??throw new Exception())
            {
                DO.Product p = dal?.Product.Get(p => p.ID == item?.ProductID) ?? throw new Exception();
                if (p.Amount < item?.Amount)
                    throw new BlOutOfStockException();
                if (item?.Amount < 0)
                    throw new BlNegativeAmountException();
            }
            if (cart.CustomerName == "" || cart.CustomerEmail == "" || cart.CustomerAddress == "")
                throw new BlNullValueException();
            if (!IsValidEmail(cart?.CustomerEmail??throw new Exception()))
                throw new BlInvalidEmailException();

            DO.Order order = BlUtils.cast<DO.Order, BO.Cart>(cart);
            order.OrderDate = DateTime.Now;
            int orderId = dal?.Order.Add(order)?? throw new Exception();

            foreach (BO.OrderItem? item in cart?.Items ?? throw new Exception())
            {
                DO.OrderItem oItem = BlUtils.cast<DO.OrderItem, BO.OrderItem>(item??throw new Exception());
                oItem.OrderID = orderId;
                dal.OrderItem.Add(oItem);
                DO.Product product = dal.Product.Get(p => p.ID == oItem.ProductID);
                product.Amount = product.Amount - oItem.Amount;
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
            DO.Product p = dal?.Product.Get(p => p.ID == productId)??throw new Exception();
            foreach (BO.OrderItem? item in cart?.Items ?? throw new Exception())
            {
                if (item?.ProductID == productId)
                {
                    if (item.Amount > newAmount)
                    {
                        item.TotalPrice -= p.Price * (item.Amount - newAmount);
                        item.Amount = newAmount;
                    }
                    else if (item.Amount < newAmount)
                    {
                        if (p.Amount >= newAmount)
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

