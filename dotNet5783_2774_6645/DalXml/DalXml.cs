namespace Dal;
using DalApi;
using DO;

sealed public class DalXml : IDal
{
    public IProduct Product { get ; } = new Dal.Product();
    public IOrder Order { get ; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();
   public DalXml()
    {
        DO.Order d = new();
        d.CustomerAddress = "000";
        d.CustomerEmail = "000";
        d.CustomerName = "000";
        d.DeliveryDate = null;
        d.OrderDate = DateTime.Now;
        d.ShipDate = null;
        d.ID = 500001;
        //  Order.Add(d);
        //Order.Get(o=>o.ID==55);
        Order.Update(d);
    }
}