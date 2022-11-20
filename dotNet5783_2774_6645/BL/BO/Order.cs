﻿namespace BO;

public class Order
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAddress { set; get; }
    public DateTime PaymentDate { set; get; }
    public OrderStatus Status { set; get; }
    public DateTime ShipDate { set; get; }
    public DateTime DeliveryDate { set; get; }
    public OrderItem Items { set; get; }
    public double TotalPrice { set; get; }
}


