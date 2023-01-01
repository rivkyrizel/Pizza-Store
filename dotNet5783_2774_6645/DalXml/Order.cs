namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

public class Order : IOrder
{

    
    public int Add(DO.Order order)
    {
        XElement? rootConfig = XDocument.Load(@"..\..\..\..\..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("orderID");
        int orderID = Convert.ToInt32(id?.Value);
        orderID++;
        id.Value = orderID.ToString();
        rootConfig?.Save(@"..\..\..\..\..\xml\dal-config.xml");
        XElement o = new("Order",
                        new XElement("ID", orderID),
                        new XElement("CustomerName", order.CustomerName),
                        new XElement("CustomerEmail", order.CustomerEmail),
                        new XElement("CustomerAddress", order.CustomerAddress),
                        new XElement("OrderDate", order.OrderDate),
                        new XElement("ShipDate", order.ShipDate),
                        new XElement("DeliveryDate", order.DeliveryDate));
        XElement? root = XDocument.Load(@"..\..\..\..\..\xml\Order.xml").Root;
        root?.Add(o);
        root?.Save(@"..\..\..\..\..\xml\Order.xml");
        return orderID;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load(@"..\..\..\..\..\xml\Order.xml").Root;
        root.Descendants("order").Where(o => int.Parse(o.Attribute("ID").Value) == id).Remove();
        /*StreamReader reader = new StreamReader("../../xml/Order.xml");
        XmlSerializer serializer = new XmlSerializer(typeof(DO.Order));
        StreamWriter writer = new StreamWriter("../../xml/Order.xml");
        List<DO.Order> list = (List<DO.Order>)serializer.Deserialize(reader);
        DO.Order order=list.Where(o=>o.ID==id).FirstOrDefault();
        list.Remove(order);
        serializer.Serialize(writer, list);
        writer.Close();
        reader.Close();*/

    }

    public DO.Order Get(Func<DO.Order, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Order>? GetList(Func<DO.Order, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Order t)
    {
        throw new NotImplementedException();
    }
}

