using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<Order> OrderForList();
    public Order OrderForList(int orderId);
    public Order UpdateShipedOrder(int orderId);
    public Order UpdateDeliveryOrder(int orderId);
    public Order UpdateOrder(int orderId);

}

