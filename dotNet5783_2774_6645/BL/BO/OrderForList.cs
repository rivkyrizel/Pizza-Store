using static BO.Enums;
namespace BO;

public class OrderForList
{
    public int ID { set; get }
    public string CustomerName { set; get }
    public OrderStatue Status { set; get }
    public int AmountOfItems { set; get }
    public double TotalPrice { set; get }
}

