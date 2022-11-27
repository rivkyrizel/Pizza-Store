using static BO.OrderStatus;
namespace BO;

public class OrderForList
{
    public int ID { set; get; }
    public string CustomerName { set; get; }
    public OrderStatus Status { set; get; }
    public int AmountOfItems { set; get; }
    public double TotalPrice { set; get; }
}

