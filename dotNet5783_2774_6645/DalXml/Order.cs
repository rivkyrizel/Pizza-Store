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
   static string orderSrc = @"..\..\xml\Order.xml";
   static string configSrc = @"..\..\xml\config.xml";
    XElement? root = XDocument.Load(orderSrc).Root;

    private List<DO.Order> createList()
    {
        IEnumerable<XElement>? rootXelement = root?.Elements("Order")??throw new XMLFileNullExeption();
        object orderObj = new DO.Order();
        List<DO.Order> list = new();

        foreach (XElement xmlOrder in rootXelement)
        {
            xmlOrder.Elements().ToList().ForEach(element => initializeXelement(orderObj, element));
            list.Add((DO.Order)orderObj);
        }
        
        return list;
    }

    private XElement convertToXelement(DO.Order order)
    {
        XElement xmlOrder = new XElement("Order");
        order.GetType().GetProperties().ToList().ForEach(p => xmlOrder.Add(new XElement(p.Name.ToString(), p.GetValue(order, null))));
        return xmlOrder;
    }

    private void initializeXelement(object orderObj, XElement xmlElement)
    {
        if (xmlElement.Name.ToString() != "ID" && xmlElement.Name.ToString().EndsWith("Date"))
            orderObj?.GetType()?.GetProperty(xmlElement.Name.ToString())?.SetValue(orderObj, xmlElement.Value);
        else if (xmlElement.Name.ToString() == "ID")
            orderObj?.GetType()?.GetProperty(xmlElement.Name.ToString())?.SetValue(orderObj, int.Parse(xmlElement.Value));
        else if (xmlElement.Value != "")
            orderObj?.GetType()?.GetProperty(xmlElement.Name.ToString())?.SetValue(orderObj, DateTime.Parse(xmlElement.Value));

    }
    public int Add(DO.Order order)
    {
        XElement? rootConfig = XDocument.Load(configSrc).Root;
        XElement? id = rootConfig?.Element("orderID");
        order.ID = Convert.ToInt32(id?.Value) + 1;
        id?.SetValue(order.ID.ToString());
        rootConfig?.Save(configSrc);
       
        root?.Add(convertToXelement(order));
        root?.Save(orderSrc);
        return order.ID;
    }

    public void Delete(int id)
    {
        XElement? root = XDocument.Load(orderSrc).Root;
        root?.Elements("Order").Where(o => int.Parse(o.Element("ID")?.Value.ToString()??"0") == id).Remove();
        root?.Save(orderSrc);
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
        XElement xmlOrder = convertToXelement(order);
        root?.Elements("Order")?.Where(o => int.Parse(o.Element("ID")?.Value.ToString()??"0") == order.ID)?.FirstOrDefault()?.ReplaceWith(xmlOrder);
        root?.Save(orderSrc);
    }
}

