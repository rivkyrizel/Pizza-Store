using DalFacade.DO;
using System.Diagnostics.Metrics;

namespace DalList;
public class DalOrderItem
{
    internal static OrderItem[] orderItem = new OrderItem[DataSource.Config.orderItemIdx];

    public OrderItem[] readOrderItems(int orderId)
    {
        int counter = 0;

        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID)
            {
                orderItem[counter] = DataSource.OrderItem[i];
                counter++;
            }
        }

        if (counter == 0) throw new Exception("error");
        return orderItem;
    }

    public OrderItem readOrders(int orderId, int producId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID && producId == DataSource.OrderItem[i].ProductID)
            {
                return DataSource.OrderItem[i];
            }
        }
        throw new Exception("error");
    }

    public void deleteOrder(int orderId, int producId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID && producId == DataSource.OrderItem[i].ProductID)
            {
                DataSource.OrderItem[i] = DataSource.OrderItem[DataSource.Config.orderItemIdx--];
                return;
            }

        }
        throw new Exception("error");
    }

}

