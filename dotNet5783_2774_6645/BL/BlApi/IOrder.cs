﻿using BO;

namespace BlApi;

public interface IOrder
{
    public IEnumerable<OrderForList?> OrderList();
    public Order GetOrder(int orderId);
    public Order UpdateShipedOrder(int orderId);
    public Order UpdateDeliveryOrder(int orderId);
    public int? UpdateOrder(Order updateOrder);
    public OrderTracking OrderTracking(int orderId);

}

