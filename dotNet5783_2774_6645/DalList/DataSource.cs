using DalFacade.DO;

namespace DalList;
internal static class DataSource
{
    static readonly Random rand = new Random();
    

    static Product[] ProductList = new Product[50];
    static Order[] OrderList = new Order[100];
    static OrderItem[] OrderItem = new OrderItem[200];
    static DataSource()
    {
        SInitialize();
    }
    private static void CreateProductList()
    {
        string[] productNames = { "כסא", "שולחן" };
        int uniqueID = 100000;
        for (int i = 0; i < 10; i++, uniqueID++)
        {
            ProductList[i] = new Product();
            int numberForName = (int)rand.NextInt64(productNames.Length);
            int numberForPrice = (int)rand.NextInt64(10,100);
            ProductList[i].Name = productNames[numberForName];
            ProductList[i].Price = numberForPrice;
            ProductList[i].ID = uniqueID;
            ProductList[i].InStock = (int)rand.NextInt64(10, 5000);
            int x = 1;
            ProductList[i].Category = (eCategory)x;
        }
    }
    private static void CreateOrderList()
    {
        string[] CustomerName = { "aaa", "bbb","ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };

        int uniqueID = 500000;
        for (int i = 0; i < 20; uniqueID++, i++)
        {
            OrderList[i] = new Order();
            int numberForName =
            uniqueID++;
            int numberForAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int numberForEmail = (int)rand.NextInt64(CustomerEmail.Length);
            OrderList[i].ID = uniqueID;
            OrderList[i].CustomerName = CustomerName[numberForName];
            OrderList[i].CustomerAdress = CustomerAdress[numberForAdress];
            OrderList[i].CustomerEmail = CustomerEmail[numberForEmail];
            OrderList[i].DeliveryDate = DateTime.MinValue;
            OrderList[i].OrderDate = DateTime.MinValue;
        }
    }
    private static void CreateOrderItemList()
    {
        string[] productNames = { "כסא", "שולחן" };
        int uniqueID = 100000;
        for (int i = 0; i < 40; i++, uniqueID++)
        {
            int orderAmount = (int)rand.NextInt64(1, 4);
            int orderIdx = i % 20;
            for (int j = 0; j < orderAmount; j++) {
                int productIdx = i % 10;
                int itemAmount = (int)rand.NextInt64(1, 15);
                OrderItem[i] = new OrderItem();
                OrderItem[i].OrderID = OrderList[orderIdx].ID;
                OrderItem[i].Amount = itemAmount;
                OrderItem[i].Price = ProductList[productIdx].Price*itemAmount;
                OrderItem[i].ProductID = ProductList[productIdx].ID;
            }
        }
    }

    private static void SInitialize()
    {
        CreateProductList();
        CreateOrderList();
        CreateOrderItemList();
    }
}

