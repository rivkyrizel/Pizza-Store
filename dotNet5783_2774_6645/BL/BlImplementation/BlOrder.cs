using BlApi;
namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    private BO.Order castDOtoBO(DO.Order oDO)
    {
        double totalprice = 0;
        IEnumerable<DO.OrderItem> listOrderItem = Dal.OrderItem.GetOrderItems(oDO.ID);
        BO.Order oBO = new();
        oBO.ID = oDO.ID;
        oBO.PaymentDate = oDO.OrderDate;
        oBO.ShipDate = oDO.ShipDate;
        oBO.CustomerAddress = oDO.CustomerAdress;
        oBO.CustomerEmail = oDO.CustomerEmail;
        oBO.CustomerName = oDO.CustomerName;
        oBO.DeliveryDate = oDO.DeliveryDate;
        foreach (DO.OrderItem item in listOrderItem)
            totalprice += Dal.Product.Get(item.ProductID).Price * item.Amount;
        oBO.TotalPrice = totalprice;
        return oBO;
    }
    private BO.OrderForList castDOtoBOOrderForList(DO.Order oDO)
    {
        double totalprice=0;
        BO.OrderForList oBO = new();
        IEnumerable<DO.OrderItem> listOrderItem = Dal.OrderItem.GetOrderItems(oDO.ID);
        oBO.ID = oDO.ID;
        oBO.CustomerName = oDO.CustomerName;
        oBO.AmountOfItems = listOrderItem.Count();
        if (oDO.ShipDate == DateTime.MinValue) oBO.Status = (BO.OrderStatus)0;
        else if (oDO.DeliveryDate == DateTime.MinValue) oBO.Status = (BO.OrderStatus)1;
        else oBO.Status = (BO.OrderStatus)2;
        foreach (DO.OrderItem item in listOrderItem)
            totalprice += Dal.Product.Get(item.ProductID).Price * item.Amount;
        oBO.TotalPrice = totalprice;
        return oBO;
    }
    public IEnumerable<BO.OrderForList> OrderList()
    {
        IEnumerable<DO.Order> DOlist = Dal.Order.GetList();
        List<BO.OrderForList> BOlist=new();
        foreach (DO.Order item in DOlist)
        {
            BOlist.Add(castDOtoBOOrderForList(item));
        }
        return BOlist;
    }

    public BO.Order GetOrder(int orderId)
    {
        try
        {
            if (orderId < 0) throw new BlInvalideData();
            DO.Order o = Dal.Order.Get(orderId);
            return castDOtoBO(o);
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }

    }

    public BO.Order UpdateDeliveryOrder(int orderId)
    {
        try
        {
            DO.Order oDO = Dal.Order.Get(orderId);
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

    public BO.Order UpdateOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public BO.Order UpdateShipedOrder(int orderId)
    {
        try
        {
            DO.Order oDO = Dal.Order.Get(orderId);
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
}

