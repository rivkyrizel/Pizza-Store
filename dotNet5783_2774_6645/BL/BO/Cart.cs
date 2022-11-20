namespace BO;

public class Cart
{
    public string CustomerName { set; get; }
    public string CustomerEmail { set; get; }
    public string CustomerAddress { set; get; }
    public OrderItem Items { set; get; }
    public double Totalprice { set; get; }
}


