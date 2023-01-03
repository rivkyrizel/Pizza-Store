
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

internal class Product : IProduct
{
    public int Add(DO.Product product)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
        //FileStream file = new(@"..\..\..\..\..\xml\Product.xml",FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamReader r = new(@"..\..\..\..\..\xml\Product.xml");
        List<DO.Product>? lst = (List<DO.Product>?)ser.Deserialize(r);
        product.ID= lst.Last().ID+1;//???????????????????
        lst?.Add(product);
        r.Close();
        StreamWriter w = new(@"..\..\..\..\..\xml\Product.xml");
        ser.Serialize(w, lst);
        w.Close();
        return product.ID;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Product Get(Func<DO.Product, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Product>? GetList(Func<DO.Product, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Product t)
    {
        throw new NotImplementedException();
    }
}

