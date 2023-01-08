using BlApi;
using BO;
using System.Reflection;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = DalApi.Factory.Get()??throw new BlNullValueException();

    /// <summary>
    ///  converts from BO object to DO object
    /// </summary>
    /// <param name="boItem"></param>
    /// <returns> DO OredrItem object </returns>
    private DO.OrderItem castBOtoDO(BO.OrderItem boItem, int order = 0)
    {
        DO.OrderItem doItem = BlUtils.cast<DO.OrderItem, BO.OrderItem>(boItem);
        doItem.OrderID = order;
        return doItem;
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
        double totalprice = 0;
        foreach (DO.OrderItem item in listOrderItem)
            totalprice += Dal.Product.Get(o => o.ID == item.ProductID).Price * item.Amount;
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
        List<BO.OrderForList> BOlist = new();
        foreach (DO.Order item in DOlist)
        {
            BOlist.Add(castDOtoBOOrderForList(item));
        }
        return BOlist;
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
            return castDOtoBO(o);
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
    public void UpdateOrder(BO.Order updateOrder)
    {
        try
        {
            bool foundInOrder = false;
            double totalPrice = 0;
            BO.Order order = castDOtoBO(Dal.Order.Get(o => o.ID == updateOrder.ID));
            List<BO.OrderItem> list = new();

            if (order.Status == (BO.OrderStatus)0)
            {
                IEnumerable<DO.OrderItem> oList = Dal.OrderItem.GetList(o => o.OrderID == updateOrder.ID) ?? throw new BlNullValueException();
                foreach (BO.OrderItem? item in updateOrder.Items ?? throw new BlNullValueException())
                {
           
                    DO.Product p = Dal.Product.Get(o => o.ID == item?.ProductID);
                    item.Name = p.Name;
                    item.Price = p.Price;
                    item.TotalPrice = p.Price * item.Amount;
                    if (item.Amount > p.Amount)
                        throw new BlInvalidAmount();
                    if (item.Amount < 0)
                        throw new BlNegativeAmountException();
                    foreach (DO.OrderItem oItem in oList)
                    {
                        if (oItem.Amount == 0)
                        {
                            Dal.OrderItem.Delete(oItem.OrderID);
                            foundInOrder = true;
                        }
                        if (item.ProductID == oItem.ProductID)
                        {
                            DO.OrderItem o = castBOtoDO(item);
                            o.OrderID = updateOrder.ID;
                            o.ID = oItem.ID;
                            Dal.OrderItem.Update(o);
                            foundInOrder = true;
                        }
                    }
                    if (!foundInOrder)
                    {
                        DO.OrderItem o = castBOtoDO(item);
                        o.OrderID = updateOrder.ID;
                        Dal.OrderItem.Add(o);

                    }
                }
                IEnumerable<DO.OrderItem> oUpdateList = Dal.OrderItem.GetList(o => o.OrderID==updateOrder.ID) ?? throw new BlNullValueException();
                foreach (DO.OrderItem item in oUpdateList)
                {
                    totalPrice += Dal.Product.Get(o => o.ID == item.ProductID).Price * item.Amount;
                }
                order.TotalPrice = totalPrice;
                Dal.Order.Update(BlUtils.cast<DO.Order, BO.Order>(order));
            }
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
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
            orderTracking.TrackList?.Add((order.OrderDate, OrderStatus._____Confirmed_____));
            orderTracking.Status = OrderStatus._____Confirmed_____;
            if (order.ShipDate != null)
            {
                orderTracking.TrackList?.Add((order.ShipDate, OrderStatus._______Sent________));
                orderTracking.Status = OrderStatus._______Sent________;
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


