

namespace DalFacade.DO;

public struct OrderItem
{
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() =>
    $@"|   {ProductID}  |   {OrderID}  |  {Price}  | {Amount} |";

}
