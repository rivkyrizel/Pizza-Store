﻿using DO;
using DalApi;

namespace Dal;
internal class DalOrderItem : IOrderItem
{


    /// <summary>
    /// create new order item
    /// </summary>
    /// <param name="orderItem"> neworder item details </param>
    /// <returns> index of new order item </returns>
    public int Add(OrderItem o)
    {
        DataSource.OrderItemList.Add(o);
        return o.OrderID;
    }

    /// <summary>
    ///  deletes ordered item by product id and order id
    /// </summary>
    /// <param name="orderId"> Id of order with desired item </param>
    /// <param name="productId"> Id of product </param>
    /// <exception cref="Exception"> No such product in given order </exception>
    public void Delete(int id)
    {
        foreach (OrderItem item in DataSource.OrderItemList)
        {
            if (id == item.ID)
            {
                DataSource.OrderItemList.Remove(item);
                return;
            }
        }

        throw new ItemNotFound("couldn't delete item");
    }


    /// <summary>
    /// Updates ordered item
    /// </summary>
    /// <param name="updateOrderItem"> Details of ordered item to update</param>
    /// <exception cref="Exception"> No order with given id found </exception>

    public void Update(OrderItem o)
    {
        foreach (OrderItem item in DataSource.OrderItemList)
        {
            if (item.ID == o.ID)
            {
                OrderItem newO = item;
                newO = o;
                return;
            }
        }
        throw new ItemNotFound("could not update ordered item ");
    }

    /// <summary>
    /// returns all ordered items
    /// </summary>
    /// <returns> All ordered items </returns>
    public IEnumerable<OrderItem> GetList()
    {
        return DataSource.OrderItemList;
    }

    /// <summary>
    /// returns details of specific ordered item by ID
    /// </summary>
    /// <param name="orderId"> Id of order that desired item is in</param>
    /// <param name="productId"> Id of product </param>
    /// <returns> Details of ordered item </returns>
    /// <exception cref="Exception"> no order </exception>
    public OrderItem Get(int id)
    {
        foreach (OrderItem item in DataSource.OrderItemList)

            if (id == item.ID)
                return item;

        throw new ItemNotFound(" requested item  not found ");
    }


    /// <summary>
    ///  returns all items in specified order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns> List of items in requested order </returns>
    /// <exception cref="Exception"> No order with the given id found </exception>
    public IEnumerable<OrderItem> GetOrderItems(int orderId)
    {
        List<OrderItem> list = new List<OrderItem>();

        foreach (OrderItem item in DataSource.OrderItemList)
        {
            if (orderId == item.OrderID)
            {
                list.Add(item);
            }
        }

        if (list.Count() == 0) throw new ItemNotFound("No order found");
        return list;
    }
}

