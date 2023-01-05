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
        //DO.OrderItem d= new();
        //d.OrderID = 500001;
        //d.Price = 100;
        //d.Amount = 100;
        //d.ProductID = 100001;
        //d.ID = 600001;
        //OrderItem.Update(d);
        // OrderItem.Delete(600001);
        OrderItem.GetList();

    }
}