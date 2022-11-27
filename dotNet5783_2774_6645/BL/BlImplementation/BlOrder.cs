using BlApi;
namespace BlImplementation;

internal class BlOrder : IOrder
{
    private DalApi.IDal Dal = new Dal.DalList();

    private BO.Order castDOtoBO(DO.Order oDO)
    {
        BO.Order oBO = new();
        oBO.ID = oDO.ID;
        oBO.PaymentDate = oDO.OrderDate;
        oBO.ShipDate = oDO.ShipDate;
        oBO.CustomerAddress = oDO.CustomerAdress;
        oBO.CustomerEmail = oDO.CustomerEmail;
        oBO.CustomerName = oDO.CustomerName;
        oBO.DeliveryDate = oDO.DeliveryDate;
        return oBO;
    }
    public IEnumerable<BO.OrderForList> OrderList()
    {
        return (IEnumerable<BO.OrderForList>)Dal.Order.GetList();
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

