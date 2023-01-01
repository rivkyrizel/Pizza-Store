namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem t)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.OrderItem Get(Func<DO.OrderItem, bool> func)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.OrderItem>? GetList(Func<DO.OrderItem, bool>? func = null)
    {
        throw new NotImplementedException();
    }

    public void Update(DO.OrderItem t)
    {
        throw new NotImplementedException();
    }
}

