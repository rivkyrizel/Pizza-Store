using DO;
using Dal;
using System.Xml.Serialization;

namespace IntilizeXmlFile;
public class Program
{

    static void Main()
    {
        //using reflection (call static constructor)
        Type staticClassInfo = typeof(Dal.DataSource);
        var staticClassConstructorInfo = staticClassInfo.TypeInitializer;
        staticClassConstructorInfo.Invoke(null, null);

        List<Product> PrdouctList = DataSource.ProductList;
        List<Order> OrderList = DataSource.OrderList;
        List<OrderItem> OrderItemList = DataSource.OrderItemList;

        StreamWriter wProduct = new(@"C:\Users\ריזל רבקה\source\repos\dotNet5783_2774_66450\xml\Product.xml");
        XmlSerializer serProduct = new(typeof(List<Product>));
        serProduct.Serialize(wProduct, PrdouctList);
        wProduct.Close();

        StreamWriter wOrder = new(@"C:\Users\ריזל רבקה\source\repos\dotNet5783_2774_66450\xml\Order.xml");
        XmlSerializer serOrder = new(typeof(List<Order>));
        serOrder.Serialize(wOrder, OrderList);
        wOrder.Close();

        StreamWriter wOrderItem = new(@"C:\Users\ריזל רבקה\source\repos\dotNet5783_2774_66450\xml\OrderItem.xml");
        XmlSerializer serOrderItem = new(typeof(List<OrderItem>));
        serOrderItem.Serialize(wOrderItem, OrderItemList);
        wOrderItem.Close();
    }
}