﻿namespace BlApi;

public class BlIdNotFound : Exception
{
    public BlIdNotFound(Exception inner) : base("ID does not exist", inner)
    { }
    public override string Message => "ID does not exist";
}

public class BlIdAlreadyExist : Exception
{
    public BlIdAlreadyExist(Exception inner) : base("ID already exist", inner)
    {

    }
    public override string Message => "ID already exist";
}

public class NoEntitiesFound : Exception
{
    public NoEntitiesFound(string message) : base(message)
    {

    }
    public override string Message => "No entities found";
}

public class BlProductFoundInOrders : Exception
{
    public override string Message =>
                        "product found in order";
}

public class BlInvalideData : Exception
{
    public override string Message =>
                      "invalid data";
}


public class BlOutOfStockException : Exception
{
    public override string Message =>
                   "item out of stock exception";
}
public class BlNullValueException : Exception
{
    public override string Message =>
                    "null value exception";

}
public class BlInvalidEmailException : Exception
{
    public override string Message =>
                    "invalid email exception";

}
public class BlNegativeAmountException : Exception
{
    public override string Message =>
                    "amount cant be negative";

}

public class BlInvalidStatusException : Exception
{
    public override string Message =>
                    "invalid status of order";

}










