namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class OrderItem : IOrderItem
{
    public XmlRootAttribute xRoot()
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ArrayOfOrderItem";
        xRoot.IsNullable = true;
        return xRoot;
    }

    public int Add(DO.OrderItem orderItem)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot());
        StreamReader r = new(@"..\..\xml\OrderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r)??throw new Exception("The xml file is empty");
        orderItem.ID = lst.Last().ID + 1;
        lst?.Add(orderItem);
        r.Close();
        StreamWriter w = new(@"..\..\xml\OrderItem.xml");
        ser.Serialize(w, lst);
        w.Close();
        return orderItem.ID;
    }

    public void Delete(int id)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot());
        StreamReader r = new(@"..\..\xml\OrderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        lst?.Remove(lst.Where(o => o.ID == id).FirstOrDefault());
        r.Close();
        StreamWriter w = new(@"..\..\xml\OrderItem.xml");
        ser.Serialize(w, lst);
        w.Close();
    }

    public DO.OrderItem Get(Func<DO.OrderItem, bool> func)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot());
        StreamReader r = new(@"..\..\xml\OrderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        r.Close();
        return lst?.Where(func) != null ? lst.Where(func).First() : throw new ItemNotFound("product not found");
    }

    public IEnumerable<DO.OrderItem>? GetList(Func<DO.OrderItem, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot());
        StreamReader r = new(@"..\..\xml\OrderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(r);
        r.Close();
        return (func == null ? lst : lst?.Where(func));
    }

    public void Update(DO.OrderItem o)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.OrderItem>), xRoot());
        StreamReader readFile = new(@"..\..\xml\OrderItem.xml");
        List<DO.OrderItem>? lst = (List<DO.OrderItem>?)ser.Deserialize(readFile)??throw new Exception();
        int idx = lst.FindIndex(pr => pr.ID == o.ID);
        if (idx >= 0) lst[idx] = o;
        else
            throw new ItemNotFound("could not update Order Item");
        readFile.Close();
        StreamWriter writeFile = new(@"..\..\xml\OrderItem.xml");
        ser.Serialize(writeFile, lst);
        writeFile.Close();
    }
}

