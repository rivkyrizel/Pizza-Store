
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class Product : IProduct
{
    public XmlRootAttribute xRoot()
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ArrayOfProduct";
        xRoot.IsNullable = true;
        return xRoot;
    }
    public int Add(DO.Product product)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        product.ID= lst?.Last().ID+1??throw new Exception();
        lst?.Add(product);
        r.Close();
        StreamWriter w = new(@"..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
        return product.ID;
    }

    public void Delete(int id)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        lst?.Remove(lst.Where(p => p.ID == id).FirstOrDefault());
        r.Close();
        StreamWriter w = new(@"..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
    }

    public DO.Product Get(Func<DO.Product, bool> func)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        r.Close();
        return lst?.Where(func) != null ? lst.Where(func).First() : throw new ItemNotFound("product not found");
    }

    public IEnumerable<DO.Product>? GetList(Func<DO.Product, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader r = new(@"..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        r.Close();
        return (func == null ? lst : lst?.Where(func));
    }

    public void Update(DO.Product p)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>), xRoot());
        StreamReader readFile = new(@"..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(readFile)?? throw new Exception();
        int idx = lst.FindIndex(pr => pr.ID == p.ID);
        if (idx >= 0) lst[idx] = p;
        else
            throw new ItemNotFound("could not update product");
        readFile.Close();
        StreamWriter writeFile = new(@"..\..\xml\Product.xml");
        ser.Serialize(writeFile, lst);
        writeFile.Close();
    }
}

