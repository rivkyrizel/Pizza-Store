using BlApi;
using BO;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<Order> OrderForList()
    {
        throw new NotImplementedException();
    }

    public Order OrderForList(int orderId)
    {
        throw new NotImplementedException();
    }

    public Order UpdateDeliveryOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public Order UpdateOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public Order UpdateShipedOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}

