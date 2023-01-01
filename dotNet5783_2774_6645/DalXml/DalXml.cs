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
        d.CustomerAddress = "444";
        d.CustomerEmail = "444";
        d.CustomerName = "444";
        d.DeliveryDate = null;
        d.OrderDate = DateTime.Now;
        d.ShipDate = null;
        Order.Add(d);
    }
}