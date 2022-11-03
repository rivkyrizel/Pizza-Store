﻿using DalFacade.DO;

namespace DalList;
public class DalOrderItem
{


    /// <summary>
    /// create new order item
    /// </summary>
    /// <param name="orderItem"> neworder item details </param>
    /// <returns> index of new order item </returns>
    public static int CreateOrderItem(OrderItem orderItem)
    {
        DataSource.OrderItem[DataSource.Config.orderItemIdx++] = orderItem;
        return DataSource.Config.orderItemIdx;
    }

    /// <summary>
    ///  returns all items in specified order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns> List of items in requested order </returns>
    /// <exception cref="ArgumentException"> No order with the given id found </exception>
    public static OrderItem[] ReadOrderItems(int orderId)
    {
        OrderItem[] orderItems = new OrderItem[DataSource.Config.orderItemIdx];
        int counter = 0;

        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
            if (orderId == DataSource.OrderItem[i].OrderID)
                orderItems[counter++] = DataSource.OrderItem[i];

        if (counter == 0) throw new ArgumentException(" No order found ");
        return orderItems;
    }

    /// <summary>
    /// returns all ordered items
    /// </summary>
    /// <returns> All ordered items </returns>
    public static OrderItem[] ReadAllItems()
    {
        OrderItem[] allItems = new OrderItem[DataSource.Config.orderItemIdx];
        for (int i = 0; i < allItems.Length; i++)
            allItems[i] = DataSource.OrderItem[i];

        return allItems;
    }

    /// <summary>
    /// returns details of specific ordered item by the product's id and the order id
    /// </summary>
    /// <param name="orderId"> Id of order that desired item is in</param>
    /// <param name="productId"> Id of product </param>
    /// <returns> Details of ordered item </returns>
    /// <exception cref="ArgumentException"> no order </exception>
    public static OrderItem ReadOrderItem(int orderId, int productId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID && productId == DataSource.OrderItem[i].ProductID)
            {
                return DataSource.OrderItem[i];
            }
        }
        throw new ArgumentException(" requested item  not found ");
    }

    /// <summary>
    /// Updates ordered item
    /// </summary>
    /// <param name="updateOrderItem"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void UpdateOrderItem(OrderItem updateOrderItem)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (updateOrderItem.ProductID == DataSource.OrderItem[i].ProductID
                && updateOrderItem.OrderID == DataSource.OrderItem[i].OrderID)
            {
                DataSource.OrderItem[i] = updateOrderItem;
                return;
            }

        }
        throw new ArgumentException("error");
    }

    public static void DeleteOrder(int orderId, int producId)
    {
        for (int i = 0; i < DataSource.Config.orderItemIdx; i++)
        {
            if (orderId == DataSource.OrderItem[i].OrderID && producId == DataSource.OrderItem[i].ProductID)
            {
                for (int j = i; j < DataSource.Config.orderItemIdx; j++)
                {
                    DataSource.OrderItem[j] = DataSource.OrderItem[j + 1];
                }
                DataSource.Config.orderItemIdx--;
                return;
            }

        }
        throw new Exception("error");
    }

}

