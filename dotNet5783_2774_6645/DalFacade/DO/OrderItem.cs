﻿

namespace DO;

internal struct OrderItem
{
    public int ProductID { get; set; }
    public int OrderID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() =>
    $@"OrderItem ProductID={ProductID}: OrderID {OrderID},   Price: {Price} Amount: {Amount}";

}
