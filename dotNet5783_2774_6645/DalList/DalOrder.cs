using DO;

namespace Dal;

public class DalOrder
{

    /// <summary>
    /// create order
    /// </summary>
    /// <param name="order">the new order</param>
    /// <returns> id of the order</returns>
    public static int CreateOrder(Order order)
    {
        order.ID = DataSource.Config.OrderID;
        DataSource.OrderList[DataSource.Config.orderIdx++] = order;
        return DataSource.Config.orderIdx;
    }

    /// <summary>
    /// functiod receives id if order and returns the orders details
    /// </summary>
    /// <param name="id"> id of specific order </param>
    /// <returns> order details of given id </returns>
    /// <exception cref="Exception"> no order with requested id found </exception>
    public static Order ReadOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
            if (id == DataSource.OrderList[i].ID)
                return DataSource.OrderList[i];

        throw new Exception("order not found");
    }

    /// <summary>
    /// returns all orders
    /// </summary>
    /// <returns> all orders in system </returns>
    public static Order[] ReadOrderList()
    {
        Order[] orderList = new Order[DataSource.Config.orderIdx];
        for (int i = 0; i < orderList.Length; i++)
            orderList[i] = DataSource.OrderList[i];

        return orderList;
    }

    /// <summary>
    /// Updates an order
    /// </summary>
    /// <param name="updateOrder"> The updated order </param>
    /// <exception cref="Exception"> No order with the given id found </exception>
    public static void UpdateOrder(Order updateOrder)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (updateOrder.ID == DataSource.OrderList[i].ID)
            {
                DataSource.OrderList[i] = updateOrder;
                return;
            }

        }
        throw new Exception(" could not update order ");
    }

    /// <summary>
    ///  Deletes order by given id
    /// </summary>
    /// <param name="id"> Id of order to delete </param>
    /// <exception cref="Exception"> No order found with the given id </exception>
    public static void DeleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (id == DataSource.OrderList[i].ID)
            {
                for (int j = i; j < DataSource.Config.orderIdx; j++)
                {
                    DataSource.OrderList[j] = DataSource.OrderList[j + 1];
                }
                DataSource.Config.orderIdx--;
                return;
            }

        }
        throw new Exception(" could not delete order");
    }
}

