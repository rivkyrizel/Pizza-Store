using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace PL.PO;
/// <summary>
/// A PO entity of an item in the order 
/// (represents a row in the order) 
/// for a list of items in the shopping cart screen and in the order details screen
/// </summary>
public class Order : DependencyObject
{
    public int ID
    {
        get { return (int)GetValue(IDProperty); }
        set { SetValue(IDProperty, value); }
    }
    public string? CustomerName
    {
        get { return (string)GetValue(CustomerNameProperty); }
        set { SetValue(CustomerNameProperty, value); }
    }

    public string? CustomerEmail
    {
        get { return (string)GetValue(CustomerEmailProperty); }
        set { SetValue(CustomerEmailProperty, value); }
    }

    public string? CustomerAddress
    {
        get { return (string)GetValue(CustomerAddressProperty); }
        set { SetValue(CustomerAddressProperty, value); }
    }
    public DateTime OrderDate
    {
        get { return (DateTime)GetValue(OrderDateProperty); }
        set { SetValue(OrderDateProperty, value); }
    }

    public DateTime ShipDate
    {
        get { return (DateTime)GetValue(ShipDateProperty); }
        set { SetValue(ShipDateProperty, value); }
    }

    public DateTime DeliveryDate
    {
        get { return (DateTime)GetValue(DeliveryDateProperty); }
        set { SetValue(DeliveryDateProperty, value); }
    }
    public BO.OrderStatus Status
    {
        get { return (BO.OrderStatus)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }
    public IEnumerable<PO.OrderItem> Items
    {
        get { return (IEnumerable<PO.OrderItem>)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }

    public double TotalPrice
    {
        get { return (double)GetValue(TotalPriceProperty); }
        set { SetValue(TotalPriceProperty, value); }
    }

    public Order(BO.Order o)
    {
        //ID = o.ID;
        //OrderDate = o.OrderDate;
        //ID = o.OrderDate;
        //ID = o.ID;
        //ID = o.ID;
        //ID = o.ID;
        //ID = o.ID;

    }

    public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(OrderItem), new UIPropertyMetadata(0));

    public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(OrderItem), new UIPropertyMetadata(""));

    public static readonly DependencyProperty CustomerAddressProperty = DependencyProperty.Register("CustomerAddress", typeof(string), typeof(OrderItem), new UIPropertyMetadata(""));

    public static readonly DependencyProperty CustomerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(OrderItem));

    public static readonly DependencyProperty OrderDateProperty = DependencyProperty.Register("OrderDate", typeof(DateTime), typeof(OrderItem));

    public static readonly DependencyProperty DeliveryDateProperty = DependencyProperty.Register("DeliveryDate", typeof(DateTime), typeof(OrderItem));

    public static readonly DependencyProperty ShipDateProperty = DependencyProperty.Register("ShipDate", typeof(DateTime), typeof(OrderItem));

    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("StatusProperty", typeof(BO.OrderStatus), typeof(OrderItem));

    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IEnumerable<OrderItem>), typeof(OrderItem));

    public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(OrderItem), new UIPropertyMetadata(0.0));
}
