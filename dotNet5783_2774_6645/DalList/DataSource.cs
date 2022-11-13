using DO;

namespace Dal;

public static class DataSource
{
    static readonly Random rand = new Random();
    internal static Product[] ProductList = new Product[50];
    internal static Order[] OrderList = new Order[100];
    internal static OrderItem[] OrderItem = new OrderItem[200];
    static DataSource()
    {
        sInitialize();
    }
    internal static class Config
    {
        private static int productID = 100000;
        public static int ProductID { get { return productID++; } }

        private static int orderID = 500000;
        public static int OrderID { get { return orderID++; } }
        internal static int productIdx = 0;
        internal static int orderIdx = 0;
        internal static int orderItemIdx = 0;
    }

    private static void createProductList()
    {
        (string, eCategory)[] products = { ("  Ravioli  ", (eCategory)1), ("   Pizza   ", (eCategory)0), (" Sandwich  ", (eCategory)3), ("Ice coffee ", (eCategory)4), ("  Coffee   ", (eCategory)4), ("   Baguet  ", (eCategory)3), ("Greek Salad", (eCategory)4), (" XL pizza  ", (eCategory)0), ("Basic Salad", (eCategory)3), (" Coca Cola ", (eCategory)4) };
        for (int i = 0; i < 10; i++)
        {
            Config.productIdx++;
            ProductList[i] = new Product();
            int numberForPrice = (int)rand.NextInt64(10, 100);
            ProductList[i].Name = products[i].Item1;
            ProductList[i].Category = products[i].Item2;
            ProductList[i].Price = numberForPrice;
            ProductList[i].ID = Config.ProductID;
            ProductList[i].InStock = (int)rand.NextInt64(1000, 5000);
            int x = 1;

        }
    }
    private static void createOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };

        for (int i = 0; i < 20; i++)
        {
            Config.orderIdx++;
            OrderList[i] = new Order();
            int numberForName = (int)rand.NextInt64(CustomerName.Length);
            int numberForAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int numberForEmail = (int)rand.NextInt64(CustomerEmail.Length);
            OrderList[i].ID = Config.OrderID;
            OrderList[i].CustomerName = CustomerName[numberForName];
            OrderList[i].CustomerAdress = CustomerAdress[numberForAdress];
            OrderList[i].CustomerEmail = CustomerEmail[numberForEmail];

            Random ran = new Random();
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            OrderList[i].OrderDate = start.AddDays(ran.Next(range));
            int dateShipExist = (int)rand.NextInt64(0, 5);
            if (dateShipExist > 0)
            {
                TimeSpan spanOrderShip = TimeSpan.FromDays(5);
                OrderList[i].ShipDate = OrderList[i].OrderDate + spanOrderShip;
                int dateDeliveryExist = (int)rand.NextInt64(0, 5);
                if (dateDeliveryExist > 0)
                {
                    TimeSpan spanShipDelivery = TimeSpan.FromDays(30);
                    OrderList[i].DeliveryDate = OrderList[i].ShipDate + spanShipDelivery;
                }
                else
                    OrderList[i].DeliveryDate = DateTime.MinValue;
            }
            else
            {
                OrderList[i].ShipDate = DateTime.MinValue;
                OrderList[i].DeliveryDate = DateTime.MinValue;
            }
        }
    }
    private static void createOrderItemList()
    {
        for (int i = 0; i < 40; Config.orderItemIdx++, i++)
        {
            int orderAmount = (int)rand.NextInt64(1, 20);
            int orderIdx = i % 20;
            for (int j = 0; j < orderAmount; j++)
            {
                int productIdx = i % 10;
                int itemAmount = (int)rand.NextInt64(1, 9);
                OrderItem[i] = new OrderItem();
                OrderItem[i].OrderID = OrderList[orderIdx].ID;
                OrderItem[i].Amount = itemAmount;
                OrderItem[i].Price = ProductList[productIdx].Price;
                OrderItem[i].ProductID = ProductList[productIdx].ID;
            }
        }
    }

    private static void sInitialize()
    {
        createProductList();
        createOrderList();
        createOrderItemList();
    }
}

