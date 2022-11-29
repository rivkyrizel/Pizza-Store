using DO;
using DalApi;

namespace Dal;

internal class DalOrder : IOrder
{

    /// <summary>
    /// create order
    /// </summary>
    /// <param name="order">the new order</param>
    /// <returns> id of the order</returns>
    /// 
    public int Add(Order o)
    {
        o.ID = DataSource.Config.OrderID;
        DataSource.OrderList.Add(o);
        return o.ID;
    }

    /// <summary>
    ///  Deletes order by given id
    /// </summary>
    /// <param name="id"> Id of order to delete </param>
    /// <exception cref="Exception"> No order found with the given id </exception>
    public void Delete(int id)
    {
        foreach (Order item in DataSource.OrderList)
        {
            if (id == item.ID)
            {
                DataSource.OrderList.Remove(item);
                return;
            }
        }
        throw new ItemNotFound("could not delete order");

    }

    /// <summary>
    /// Updates an order
    /// </summary>
    /// <param name="updateOrder"> The updated order </param>
    /// <exception cref="Exception"> No order with the given id found </exception>


    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.OrderList.Count; i++)
        {
            if (o.ID == DataSource.OrderList[i].ID)
            {
                DataSource.OrderList[i]= o;
                return;
            }
        }
        throw new ItemNotFound(" could not update order ");
    }

    /// <summary>
    /// returns all orders
    /// </summary>
    /// <returns> all orders in system </returns>
    public IEnumerable<Order> GetList()
    {
        return DataSource.OrderList;
    }


    /// <summary>
    /// functiod receives id if order and returns the orders details
    /// </summary>
    /// <param name="id"> id of specific order </param>
    /// <returns> order details of given id </returns>
    /// <exception cref="Exception"> no order with requested id found </exception>


    public Order Get(int id)
    {
        foreach (Order item in DataSource.OrderList)

            if (id == item.ID) return item;

        throw new ItemNotFound("order not found");
    }
}

