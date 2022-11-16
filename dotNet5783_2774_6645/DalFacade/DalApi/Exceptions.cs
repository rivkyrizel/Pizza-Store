namespace DalApi;

public class ItemNotFound:Exception
{
   public ItemNotFound(string message) : base(message) { }
}
public class DuplicateValue:Exception
{
    public DuplicateValue(string message) : base(message) {}
}
