using DalFacade.DO;

namespace DalList;
public struct DalOrder
{
    public int createOrder(Order order)
    {
        DataSource.OrderList[DataSource.Config.orderIdx++] = order;
        return DataSource.Config.orderIdx;
    }

    public Order readOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (id == DataSource.OrderList[i].ID)
                return DataSource.OrderList[i];

        }
        throw new Exception("error");
    }
    public Order[] readOrders()
    {
        return DataSource.OrderList;
    }

    public void updateOrder(Order updateOrder)
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

    public void deleteOrder(int id)
    {
        for (int i = 0; i < DataSource.Config.orderIdx; i++)
        {
            if (id == DataSource.OrderList[i].ID)
            {
                DataSource.OrderList[i] = DataSource.OrderList[DataSource.Config.orderIdx--];
                return;
            }

        }
        throw new Exception("error");
    }
}

