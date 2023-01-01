
namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class Product : IProduct
{
    public int Add(DO.Product t)
    {
        throw new NotImplementedException();
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

