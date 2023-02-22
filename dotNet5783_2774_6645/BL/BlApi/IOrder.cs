using BO;
using System.Runtime.CompilerServices;

namespace BlApi;

public interface IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderForList?> OrderList();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order GetOrder(int orderId);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderForList?> GetOrdersForUser(int userId);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order UpdateShipedOrder(int orderId);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order UpdateDeliveryOrder(int orderId);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? UpdateOrder(Order updateOrder);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderTracking OrderTracking(int orderId);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? SelectOrder();
}

