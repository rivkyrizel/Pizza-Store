using DalFacade.DO;
using System.Diagnostics.Metrics;

namespace DalList;
public class DalOrderItem
{
    internal static OrderItem[] orderItems = new OrderItem[DataSource.Config.orderItemIdx];

    public static int createOrderItem(OrderItem orderItem)
    {
        DataSource.OrderItem[DataSource.Config.orderItemIdx++] = orderItem;
        return DataSource.Config.orderItemIdx;
    }
    public static OrderItem[] readOrderItems(int orderId)
    {
        int counter = 0;

        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
            if (orderId == DataSource.OrderItem[i].OrderID)
                orderItems[counter++] = DataSource.OrderItem[i];

        if (counter == 0) throw new Exception("error");
        return orderItems;
    }

    public static OrderItem[] readAllItems()
    {
        OrderItem[] allItems = new OrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i < allItems.Length; i++)
            allItems[i] = DataSource.OrderItem[i];

        return allItems;
    }

    public static OrderItem readOrderItem(int orderId, int producId)
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

    public static void updateOrderItem(OrderItem updateOrderItem)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (updateOrderItem.ProductID == DataSource.OrderItem[i].ProductID
                && updateOrderItem.OrderID == DataSource.OrderItem[i].OrderID)
            {
                DataSource.OrderItem[i] = updateOrderItem;
                return;
            }

        }
        throw new Exception("error");
    }

    public static void deleteOrder(int orderId, int producId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID && producId == DataSource.OrderItem[i].ProductID)
            {
                for (int j = i; j < DataSource.Config.orderItemIdx; j++)
                {
                    DataSource.OrderItem[j] = DataSource.OrderItem[j + 1];
                }
                DataSource.Config.orderItemIdx--;
                return;
            }

        }
        throw new Exception("error");
    }

}

