using BlApi;
using BO;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    ///  converts from BO object to DO object
    /// </summary>
    /// <param name="boItem"></param>
    /// <returns> DO OredrItem object </returns>
    private DO.OrderItem castBOtoDO(BO.OrderItem boItem, int order=0)
    {
        DO.OrderItem doItem = new DO.OrderItem();
        doItem.ID = (int)boItem.ID;
        doItem.Price = (double)boItem.Price;
        doItem.ProductID = (int)boItem.ProductId;
        doItem.OrderID = order;
        doItem.Amount = (int)boItem.Amount;
        return doItem;
    }

    private DO.Order castBOOrdertDO(BO.Order oBo)
    {
        DO.Order o = new();
        o.CustomerAdress = oBo.CustomerAddress;
        o.CustomerEmail = oBo.CustomerEmail;
        o.CustomerName = oBo.CustomerName;
        o.DeliveryDate = (DateTime)oBo.DeliveryDate;
        o.ID = oBo.ID;
        o.OrderDate = (DateTime)oBo.PaymentDate;
        o.ShipDate = (DateTime)oBo.ShipDate;
        return o;
    }
    private BO.Order castDOtoBO(DO.Order oDO)
    {
        double totalprice = 0;
        IEnumerable<DO.OrderItem> listOrderItem = Dal.OrderItem.GetList(o=>o.OrderID==oDO.ID);
        BO.Order oBO = new();
        oBO.ID = oDO.ID;
        oBO.PaymentDate = oDO.OrderDate;
        oBO.ShipDate = oDO.ShipDate;
        oBO.CustomerAddress = oDO.CustomerAdress;
        oBO.CustomerEmail = oDO.CustomerEmail;
        oBO.CustomerName = oDO.CustomerName;
        oBO.DeliveryDate = oDO.DeliveryDate;
        foreach (DO.OrderItem item in listOrderItem)
            totalprice += Dal.Product.Get(o=>o.ID==item.ProductID).Price * item.Amount;
        oBO.TotalPrice = totalprice;
        if (oDO.ShipDate == DateTime.MinValue)
            oBO.Status = (BO.OrderStatus)0;
        else if (oDO.DeliveryDate == DateTime.MinValue)
            oBO.Status = (BO.OrderStatus)1;
        else
            oBO.Status = (BO.OrderStatus)2;
        return oBO;
    }
    private BO.OrderForList castDOtoBOOrderForList(DO.Order oDO)
    {
        double totalprice = 0;
        BO.OrderForList oBO = new();
        IEnumerable<DO.OrderItem> listOrderItem =Dal.OrderItem.GetList(o=>o.OrderID==oDO.ID);
        oBO.ID = oDO.ID;
        oBO.CustomerName = oDO.CustomerName;
        oBO.AmountOfItems = listOrderItem.Count();
        if (oDO.ShipDate == DateTime.MinValue) oBO.Status = (BO.OrderStatus)0;
        else if (oDO.DeliveryDate == DateTime.MinValue) oBO.Status = (BO.OrderStatus)1;
        else oBO.Status = (BO.OrderStatus)2;
        foreach (DO.OrderItem item in listOrderItem)
            totalprice += Dal.Product.Get(o => o.ID == item.ProductID).Price * item.Amount;
        oBO.TotalPrice = totalprice;
        return oBO;
    }

    /// <summary>
    /// gets list of all orders
    /// </summary>
    /// <returns> list of orders </returns>
    public IEnumerable<BO.OrderForList?> OrderList()
    {
        IEnumerable<DO.Order> DOlist = (IEnumerable<DO.Order>)Dal.Order.GetList();
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
            if (oDO.ShipDate != DateTime.MinValue && oDO.DeliveryDate == DateTime.MinValue)
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
            List<BO.OrderItem> list=new();
            if (updateOrder.Status == (BO.OrderStatus)0)
            {
                IEnumerable<DO.OrderItem> oList =Dal.OrderItem.GetList(o=>o.ID==updateOrder.ID);
                //foreach (DO.OrderItem item in oList)
                //{
                //    BO.OrderItem newOrder = new();
                //    newOrder.ProductId = item.ProductID;
                //    newOrder.Amount = item.Amount;
                //    newOrder.ID = item.OrderID;
                //    updateOrder.Items.Append(newOrder);
                //}
                foreach (BO.OrderItem item in updateOrder.Items)
                {
                    DO.Product p = Dal.Product.Get(o => o.ID == item.ProductId);
                    item.Name = p.Name;
                    item.Price = p.Price;
                    item.TotalPrice = p.Price * item.Amount;
                    if (item.Amount > p.InStock)
                        throw new BlInvalidAmount();
                    if (item.Amount < 0)
                        throw new BlNegativeAmountException();
                    foreach (DO.OrderItem oItem in oList)
                    {
                        if (item.ProductId == oItem.ProductID)
                        {
                            Dal.OrderItem.Update(castBOtoDO(item));
                            foundInOrder = true;
                        }
                    }
                    if (!foundInOrder)
                    {
                        Dal.OrderItem.Add(castBOtoDO(item));

                    }
                }
                IEnumerable<DO.OrderItem> oUpdateList =Dal.OrderItem.GetList(o=>o.ID==updateOrder.ID);
                foreach (DO.OrderItem item in oUpdateList)
                {
                    totalPrice += Dal.Product.Get(o => o.ID == item.ProductID).Price * item.Amount;
                }
                updateOrder.TotalPrice += totalPrice;
                updateOrder.CustomerAddress = order.CustomerAddress;
                updateOrder.CustomerEmail = order.CustomerEmail;
                updateOrder.CustomerName = order.CustomerName;
                updateOrder.DeliveryDate = order.DeliveryDate;
                updateOrder.PaymentDate = order.PaymentDate;
                updateOrder.ShipDate = order.ShipDate;
                updateOrder.Status = order.Status;
                Dal.Order.Update(castBOOrdertDO(updateOrder));
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
            if (oDO.ShipDate == DateTime.MinValue)
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
            orderTracking.TrackList.Add((order.OrderDate, OrderStatus._____Confirmed_____));
            orderTracking.Status = OrderStatus._____Confirmed_____;
            if (order.ShipDate > DateTime.MinValue)
            {
                orderTracking.TrackList.Add((order.ShipDate, OrderStatus._______Sent________));
                orderTracking.Status = OrderStatus._______Sent________;
                if (order.DeliveryDate > DateTime.MinValue)
                {
                    orderTracking.TrackList.Add((order.DeliveryDate, OrderStatus.DeliveredToCustomer));
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


