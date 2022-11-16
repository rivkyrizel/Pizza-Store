using DO;

namespace Dal;

public static class DataSource
{
    static readonly Random rand = new Random();
    internal static List<Product> ProductList;
    internal static List<Order> OrderList;
    internal static List<OrderItem> OrderItemList;
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

        private static int orderItemID = 500000;
        public static int OrderItemID { get { return orderID++; } }
    }

    private static void createProductList()
    {
     
        (string, eCategory)[] products = { ("  Ravioli  ", (eCategory)1), ("   Pizza   ", (eCategory)0), (" Sandwich  ", (eCategory)3), ("Ice coffee ", (eCategory)4), ("  Coffee   ", (eCategory)4), ("   Baguet  ", (eCategory)3), ("Greek Salad", (eCategory)4), (" XL pizza  ", (eCategory)0), ("Basic Salad", (eCategory)3), (" Coca Cola ", (eCategory)4) };
        for (int i = 0; i < 10; i++)
        {
            Product product = new Product();
            int numberForPrice = (int)rand.NextInt64(10, 100);
            product.Name = products[i].Item1;
            product.Category = products[i].Item2;
            product.Price = numberForPrice;
            product.ID = Config.ProductID;
            product.InStock = (int)rand.NextInt64(1000, 5000);
            ProductList.Add(product);
        }
    }
    private static void createOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };

        for (int i = 0; i < 20; i++)
        {
            Order order = new Order();
            int numberForName = (int)rand.NextInt64(CustomerName.Length);
            int numberForAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int numberForEmail = (int)rand.NextInt64(CustomerEmail.Length);
            order.ID = Config.OrderID;
            order.CustomerName = CustomerName[numberForName];
            order.CustomerAdress = CustomerAdress[numberForAdress];
            order.CustomerEmail = CustomerEmail[numberForEmail];

            Random ran = new Random();
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            order.OrderDate = start.AddDays(ran.Next(range));
            int dateShipExist = (int)rand.NextInt64(0, 5);
            if (dateShipExist > 0)
            {
                TimeSpan spanOrderShip = TimeSpan.FromDays(5);
                order.ShipDate = OrderList[i].OrderDate + spanOrderShip;
                int dateDeliveryExist = (int)rand.NextInt64(0, 5);
                if (dateDeliveryExist > 0)
                {
                    TimeSpan spanShipDelivery = TimeSpan.FromDays(30);
                    order.DeliveryDate = OrderList[i].ShipDate + spanShipDelivery;
                }
                else
                    order.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                order.ShipDate = DateTime.MinValue;
                order.DeliveryDate = DateTime.MinValue;
            }
            OrderList.Add(order);
        }
    }
    private static void createOrderItemList()
    {
        for (int i = 0; i < 40;  i++)
        {
            int orderAmount = (int)rand.NextInt64(1, 20);
            int orderIdx = i % 20;
            for (int j = 0; j < orderAmount; j++)
            {
                int productIdx = i % 10;
                int itemAmount = (int)rand.NextInt64(1, 9);
                OrderItem orderItem = new OrderItem();
                orderItem.ID = Config.OrderItemID;
                orderItem.OrderID = OrderList[orderIdx].ID;
                orderItem.Amount = itemAmount;
                orderItem.Price = ProductList[productIdx].Price;
                orderItem.ProductID = ProductList[productIdx].ID;
                OrderItemList.Add(orderItem);
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

