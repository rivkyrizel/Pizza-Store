namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

public class Order : IOrder
{
    XElement? root = XDocument.Load(@"..\..\..\..\..\xml\Order.xml").Root;

    public int Add(DO.Order order)
    {
        XElement? rootConfig = XDocument.Load(@"..\..\..\..\..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("orderID");
        int orderID = Convert.ToInt32(id?.Value);
        orderID++;
        id?.SetValue(orderID.ToString());
        rootConfig?.Save(@"..\..\..\..\..\xml\config.xml");
        XElement o = new("Order",
                        new XElement("ID", orderID),
                        new XElement("CustomerName", order.CustomerName),
                        new XElement("CustomerEmail", order.CustomerEmail),
                        new XElement("CustomerAddress", order.CustomerAddress),
                        new XElement("OrderDate", order.OrderDate),
                        new XElement("ShipDate", order.ShipDate),
                        new XElement("DeliveryDate", order.DeliveryDate));
        root?.Add(o);
        root?.Save(@"..\..\..\..\..\xml\Order.xml");
        return orderID;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load(@"..\..\..\..\..\xml\Order.xml").Root;
        root?.Elements("Order").Where(o => int.Parse(o.Element("ID").Value.ToString()) == id).Remove();
        root?.Save(@"..\..\..\..\..\xml\Order.xml");
    }

    public DO.Order Get(Func<DO.Order, bool> func)
    {
        IEnumerable<XElement> ie = root.Elements("Order");
        DO.Order d = new();
        IEnumerable<DO.Order> i = from v in ie.Elements()
                                  select (
                                          from element in v.Elements()
                                          select new
                                          {
                                           { d.ID}   
                                          }
                                          );
          //select (d.GetType().GetProperty(element.ToString()).SetValue(d, element.Value)));

        throw new Exception("hii");


    }
    private XElement func2(DO.Order d, XElement element)
    {
        d.GetType().GetProperty(element.ToString()).SetValue(d, element.Value);
        return element;
    }
    public IEnumerable<DO.Order>? GetList(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order order)
    {
        XElement? o = new("Order",
                        new XElement("ID", order.ID),
                        new XElement("CustomerName", order.CustomerName),
                        new XElement("CustomerEmail", order.CustomerEmail),
                        new XElement("CustomerAddress", order.CustomerAddress),
                        new XElement("OrderDate", order.OrderDate),
                        new XElement("ShipDate", order.ShipDate),
                        new XElement("DeliveryDate", order.DeliveryDate));
        root?.Elements("Order")?.Where(o => int.Parse(o.Element("ID").Value.ToString()) == order.ID)?.FirstOrDefault()?.ReplaceWith(o);
        root?.Save(@"..\..\..\..\..\xml\Order.xml");
    }
}

