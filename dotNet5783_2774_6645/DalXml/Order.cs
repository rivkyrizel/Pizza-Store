namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;

public class Order : IOrder
{
    XElement? root = XDocument.Load(@"..\..\..\..\..\xml\Order.xml").Root;

    private List<DO.Order> createList()
    {
        IEnumerable<XElement>? ie = root?.Elements("Order")??throw new Exception();
        object dd = new DO.Order();
        List<DO.Order> list = new();

        foreach (XElement o in ie)
        {
            o.Elements().ToList().ForEach(mx => initializeXelement(dd, mx));
            list.Add((DO.Order)dd);
        }

        return list;
    }

    private XElement convertToXelement(DO.Order order)
    {
        XElement orderXelemnt = new XElement("Order");
        order.GetType().GetProperties().ToList().ForEach(p => orderXelemnt.Add(new XElement(p.Name.ToString(), p.GetValue(order, null))));
        return orderXelemnt;
    }

    private void initializeXelement(object d, XElement element)
    {
        if (element.Name.ToString() != "ID" && element.Name.ToString().EndsWith("Date"))
            d?.GetType()?.GetProperty(element.Name.ToString())?.SetValue(d, element.Value);
        else if (element.Name.ToString() == "ID")
            d?.GetType()?.GetProperty(element.Name.ToString())?.SetValue(d, int.Parse(element.Value));
        else if (element.Value != "")
            d?.GetType()?.GetProperty(element.Name.ToString())?.SetValue(d, DateTime.Parse(element.Value));

    }
    public int Add(DO.Order order)
    {
        XElement? rootConfig = XDocument.Load(@"..\..\..\..\..\xml\config.xml").Root;
        XElement? id = rootConfig?.Element("orderID");
        int orderID = Convert.ToInt32(id?.Value) + 1;
        id?.SetValue(orderID.ToString());
        rootConfig?.Save(@"..\..\..\..\..\xml\config.xml");
        order.ID = orderID;

     
        root?.Add(convertToXelement(order));
        root?.Save(@"..\..\xml\Order.xml");
        return orderID;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load(@"..\..\xml\Order.xml").Root;
        root?.Elements("Order").Where(o => int.Parse(o.Element("ID")?.Value.ToString()??"0") == id).Remove();
        root?.Save(@"..\..\xml\Order.xml");
    }

    public DO.Order Get(Func<DO.Order, bool> func)
    {
        List<DO.Order> list = createList();
        return list?.Where(func) != null ? list.Where(func).First() : throw new ItemNotFound("order not found");
    }


    public IEnumerable<DO.Order>? GetList(Func<DO.Order, bool>? func = null)
    {
        List<DO.Order> list = createList();

        return (func == null ? list : list?.Where(func));
    }

    public void Update(DO.Order order)
    {
        XElement o = convertToXelement(order);
        root?.Elements("Order")?.Where(o => int.Parse(o.Element("ID")?.Value.ToString()??"0") == order.ID)?.FirstOrDefault()?.ReplaceWith(o);
        root?.Save(@"..\..\xml\Order.xml");
    }
}

