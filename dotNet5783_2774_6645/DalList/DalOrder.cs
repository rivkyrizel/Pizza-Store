using System.Collections.Generic;
using DalFacade.DO;

namespace DalList;
public static class DalOrder
{
    public static int createOrder(Order order)
    {
        order.ID = DataSource.Config.OrderID;
        DataSource.OrderList[DataSource.Config.orderIdx++] = order;
        return DataSource.Config.orderIdx;
    }

    public static Order readOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (id == DataSource.OrderList[i].ID)
                return DataSource.OrderList[i];

        }
        throw new Exception("error order not found");
    }
    public static Order[] readOrderList()
    {
        Order[] orderList = new Order[DataSource.Config.orderIdx];
        for (int i = 0; i < orderList.Length; i++) 
            orderList[i] = DataSource.OrderList[i];
   
        return orderList;
    }

    public static void updateOrder(Order updateOrder)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (updateOrder.ID == DataSource.OrderList[i].ID)
            {
                DataSource.OrderList[i] = updateOrder;
                return;
            }

        }
        throw new Exception("error");
    }

    public static void deleteOrder(int id)
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
        throw new Exception("error can't delete order");
    }
}

