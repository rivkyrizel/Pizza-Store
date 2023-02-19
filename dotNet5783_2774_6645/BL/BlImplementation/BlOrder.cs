﻿using BlApi;
using BO;
using DalApi;
using System.Reflection;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private DalApi.IDal Dal = DalApi.Factory.Get() ?? throw new BlNullValueException();


    private BO.OrderItem castDoOitemtoBoOitem(DO.OrderItem doItem)
    {
        BO.OrderItem boItem = BlUtils.cast< BO.OrderItem,DO.OrderItem > (doItem);
        DO.Product p = Dal.Product.Get(p => p.ID == boItem.ProductID);
        boItem.Name= p.Name;
        boItem.TotalPrice = boItem.Price * boItem.Amount;
        return boItem;
    }

    private BO.Order castDOtoBO(DO.Order oDO)
    {
        BO.Order oBO = BlUtils.cast<BO.Order, DO.Order>(oDO);
        oBO.TotalPrice = calculateTotalPrice(oDO.ID);
        oBO.Status = (BO.OrderStatus)findOrderStatus(oDO);
        return oBO;
    }
    private BO.OrderForList castDOtoBOOrderForList(DO.Order oDO)
    {
        BO.OrderForList oBO = BlUtils.cast<BO.OrderForList, DO.Order>(oDO);
        IEnumerable<DO.OrderItem> listOrderItem = Dal.OrderItem.GetList(o => o.OrderID==oDO.ID)??throw new BlNullValueException();
        oBO.AmountOfItems = listOrderItem.Count();
        oBO.TotalPrice = calculateTotalPrice(oDO.ID);
        oBO.Status = (BO.OrderStatus)findOrderStatus(oDO);
        return oBO;
    }

    private double calculateTotalPrice(int id)
    {
        IEnumerable<DO.OrderItem> listOrderItem = Dal?.OrderItem.GetList(o => o.OrderID == id) ?? throw new BlNullValueException();
        var totalprice = (from product in listOrderItem
                          select product.Price * product.Amount).Sum();
        return totalprice;
    }

    private int findOrderStatus(DO.Order oDO)
    {
        if (oDO.ShipDate == null) return 0;
        else if (oDO.DeliveryDate == null) return 1;
        return 2;
    }


    /// <summary>
    /// gets list of all orders
    /// </summary>
    /// <returns> list of orders </returns>
    public IEnumerable<BO.OrderForList?> OrderList()
    {
        IEnumerable<DO.Order> DOlist = Dal.Order.GetList() ?? throw new BlNullValueException();
        IEnumerable<BO.OrderForList> BoList = from item in DOlist
                                             select castDOtoBOOrderForList(item);
        return BoList;
    }

    /// <summary>
    /// gets order by id
    /// </summary>
    /// <param name="orderId"> id of order to receive </param>
    /// <returns> order details </returns>
    /// <exception cref="BlInvalideData"> invalid order id </exception>
    /// <exception cref="BlIdNotFound"> order with specified id not found </exception>
    public BO.Order GetOrder(int orderId)
    {
        try
        {
            if (orderId < 0) throw new BlInvalideData();
            DO.Order o = Dal.Order.Get(o => o.ID == orderId);
            BO.Order b= castDOtoBO(o);
            IEnumerable<DO.OrderItem> n = Dal.OrderItem.GetList(o=>o.OrderID==b.ID)??throw new BlNullValueException();
            IEnumerable<BO.OrderItem> a = from item in n
                    select castDoOitemtoBoOitem(item);
            b.Items= a;
            return b;
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }

    }

    /// <summary>
    /// updates delivery date in order to current date
    /// </summary>
    /// <param name="orderId"> id of order to update</param>
    /// <returns> updated order </returns>
    /// <exception cref="BlInvalidStatusException"> order status not correct </exception>
    /// <exception cref="BlIdNotFound"> order with specified id not found </exception>
    public BO.Order UpdateDeliveryOrder(int orderId)
    {
        try
        {
            DO.Order oDO = Dal.Order.Get(o => o.ID == orderId);
            if (oDO.ShipDate != null && oDO.DeliveryDate == null)
            {
                oDO.DeliveryDate = DateTime.Now;
                BO.Order oBO = castDOtoBO(oDO);
                oBO.Status = (BO.OrderStatus)2;
                Dal.Order.Update(oDO);
                return oBO;
            }
            throw new BlInvalidStatusException();

        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }


    /// <summary>
    /// update details of order for manager
    /// </summary>
    /// <param name="updateOrder">updated order</param>
    /// <exception cref="BlInvalidAmount">invalid amount</exception>
    /// <exception cref="BlNegativeAmountException">negative amount</exception>
    /// <exception cref="BlIdNotFound">id not found</exception>
    //public void UpdateOrder(BO.Order updateOrder)
    //{
    //    try
    //    {
    //        bool foundInOrder = false;
    //        double totalPrice = 0;
    //        BO.Order order = castDOtoBO(Dal.Order.Get(o => o.ID == updateOrder.ID));
    //        List<BO.OrderItem> list = new();

    //        if (order.Status == (BO.OrderStatus)0)
    //        {
    //            IEnumerable<DO.OrderItem> oList = Dal.OrderItem.GetList(o => o.OrderID == updateOrder.ID) ?? throw new BlNullValueException();
    //            foreach (BO.OrderItem? item in updateOrder.Items ?? throw new BlNullValueException())
    //            {

    //                DO.Product p = Dal.Product.Get(o => o.ID == item?.ProductID);
    //                item.Name = p.Name;
    //                item.Price = p.Price;
    //                item.TotalPrice = p.Price * item.Amount;
    //                if (item.Amount > p.Amount)
    //                    throw new BlInvalidAmount();
    //                if (item.Amount < 0)
    //                    throw new BlNegativeAmountException();
    //                foreach (DO.OrderItem oItem in oList)
    //                {
    //                    if (oItem.Amount == 0)
    //                    {
    //                        Dal.OrderItem.Delete(oItem.OrderID);
    //                        foundInOrder = true;
    //                    }
    //                    else if (item.ProductID == oItem.ProductID)
    //                    {
    //                        DO.OrderItem o = castBOtoDO(item);
    //                        o.OrderID = updateOrder.ID;
    //                        o.ID = oItem.ID;
    //                        Dal.OrderItem.Update(o);
    //                        foundInOrder = true;
    //                    }
    //                }
    //                if (!foundInOrder)
    //                {
    //                    DO.OrderItem o = castBOtoDO(item);
    //                    o.OrderID = updateOrder.ID;
    //                    Dal.OrderItem.Add(o);

    //                }
    //            }
    //            IEnumerable<DO.OrderItem> oUpdateList = Dal.OrderItem.GetList(o => o.OrderID == updateOrder.ID) ?? throw new BlNullValueException();
    //            totalPrice = calculateTotalPrice(updateOrder.ID);
    //            order.TotalPrice = totalPrice;
    //            Dal.Order.Update(BlUtils.cast<DO.Order, BO.Order>(order));
    //        }
    //    }
    //    catch (DalApi.ItemNotFound e)
    //    {
    //        throw new BlIdNotFound(e);
    //    }
    //}



    public void UpdateOrder(Order updateOrder)
    {
        Dal.OrderItem.GetList(o => o.OrderID == updateOrder.ID)?.ToList().ForEach(oi => Dal.OrderItem.Delete(oi.ID));
        foreach (var item in updateOrder.Items)
        {
            if (Dal.Product.Get(p => p.ID == item.ProductID).Amount < item?.Amount) throw new BlOutOfStockException();
            else if (item.Amount < 0) throw new BlNegativeAmountException();

            if (item.Amount == 0)
                continue;

            DO.OrderItem oItem = BlUtils.cast<DO.OrderItem, BO.OrderItem>(item);
            oItem.OrderID = updateOrder.ID;
            Dal.OrderItem.Add(oItem);
            DO.Product product = Dal.Product.Get(p => p.ID == oItem.ProductID);
            product.Amount = product.Amount - oItem.Amount;
            Dal.Product.Update(product);
        }
    }


    /// <summary>
    /// updates shipped date in order to current date
    /// </summary>
    /// <param name="orderId"> id of order to update </param>
    /// <returns> updated order </returns>
    /// <exception cref="BlInvalidStatusException"> order status not correct </exception>
    /// <exception cref="BlIdNotFound"> order with specified id not found </exception>
    public BO.Order UpdateShipedOrder(int orderId)
    {
        try
        {
            DO.Order oDO = Dal.Order.Get(o => o.ID == orderId);
            if (oDO.ShipDate == null)
            {
                oDO.ShipDate = DateTime.Now;
                BO.Order oBO = castDOtoBO(oDO);
                oBO.Status = (BO.OrderStatus)1;
                Dal.Order.Update(oDO);
                return oBO;
            }
            throw new BlInvalidStatusException();
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

    public OrderTracking OrderTracking(int orderId)
    {
        try
        {
            DO.Order order = Dal.Order.Get(o => o.ID == orderId);
            BO.OrderTracking orderTracking = new();
            orderTracking.ID = orderId;
            orderTracking.TrackList?.Add((order.OrderDate, OrderStatus.Confirmed));
            orderTracking.Status = OrderStatus.Confirmed;
            if (order.ShipDate != null)
            {
                orderTracking.TrackList?.Add((order.ShipDate, OrderStatus.Sent));
                orderTracking.Status = OrderStatus.Sent;
                if (order.DeliveryDate != null)
                {
                    orderTracking.TrackList?.Add((order.DeliveryDate, OrderStatus.DeliveredToCustomer));
                    orderTracking.Status = OrderStatus.DeliveredToCustomer;
                }
            }
            return orderTracking;
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

}


