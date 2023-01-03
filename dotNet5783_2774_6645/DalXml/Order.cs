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

        IEnumerable<XElement> orderElements = from v in root.Elements()
                                              select v;
        DO.Order d = new();
        var e = from orderElement in orderElements
                from element in orderElement.Elements()
                select func2(ref d, element);


        ////from l in element.Elements()
        ////select new { element, l.Name, l.Value };
        //foreach (XElement o in orderElements)
        //{

        //    foreach (XElement mx in o.Elements())
        //    {
        //        func2(ref d, mx);
        //    }
        //}
        throw new Exception("hhh");
    }

    private int func2(ref DO.Order d, XElement element)
    {
        if (element.Name.ToString() != "ID")
            d.GetType().GetProperty(element.Name.ToString()).SetValue(d, element.Value);
        return 2;
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

