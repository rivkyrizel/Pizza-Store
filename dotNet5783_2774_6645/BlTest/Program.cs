using BlApi;
using BlImplementation;

public class Program
{
    public static IBl BL = new Bl();
    static void Main()
    {
        int choice = 0;
        do
        {  
            Console.WriteLine("enter: \n 1 for product \n 2 for orders \n 3 for cart \n 0 to exit");
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice)
            {
                case 1:
                    CRUDProduct();
                    break;
                case 2:
                    CRUDOrder();
                    break;
                case 3:
                    CRUDCart();
                    break;
                default:
                    Console.WriteLine("incorrect input");
                    break;
            }
        } while (choice != 0);
    }
    private static void CRUDOrder()
    {
        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to add order \n b to display orders by id \n c to display list of orders \n d to update order \n e to erase order from list \n 0 to return main menu");
            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    addOrder();
                    break;
                case "b":
                    displayOrder();
                    break;
                case "c":
                    displayOrderList();
                    break;
                case "d":
                    updateOrder();
                    break;
                case "e":
                    deleteOrder();
                    break;
            }
        } while (s != "0");

    }
    private static void CRUDOrderItem()
    {
        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to add order item \n b to display order items by order id \n c to display list of all ordered items \n d to display ordered item by product id and order id \n e to update orders item \n f to erase order item from list \n 0 to return main menu");

            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    addOrderItem();
                    break;
                case "b":
                    displayOrderItems();
                    break;
                case "c":
                    displayAllItems();
                    break;
                case "d":
                    displayItem();
                    break;
                case "e":
                    updateOrderItem();
                    break;
                case "f":
                    deleteOrderItem();
                    break;
            }
        } while (s != "0");

    }
    private static void CRUDProduct()
    {

        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to add product \n b to display product by id \n c to display list of products \n d to update product \n e to erase product from list \n 0 to return main menu");

            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    addProduct();
                    break;
                case "b":
                    displayProduct();
                    break;
                case "c":
                    displayProductList();
                    break;
                case "d":
                    updateProduct();
                    break;
                case "e":
                    deleteProduct();
                    break;
            }
        } while (s != "0");
    }

    /// <summary>
    /// accepts order details from user
    /// </summary>
    /// <returns>order object</returns>
    private static Order createOrder()
    {
        Order newOrder = new Order();
        Console.WriteLine("enter name:");
        newOrder.CustomerName = Console.ReadLine();
        Console.WriteLine("enter email:");
        newOrder.CustomerEmail = Console.ReadLine();
        Console.WriteLine("enter adress:");
        newOrder.CustomerAdress = Console.ReadLine();
        newOrder.OrderDate = DateTime.MinValue;
        TimeSpan ShipDate = TimeSpan.FromDays(2);
        TimeSpan deliveryDate = TimeSpan.FromDays(20);
        newOrder.ShipDate = newOrder.OrderDate + ShipDate;
        newOrder.DeliveryDate = newOrder.OrderDate + deliveryDate;
        return newOrder;

    }
    private static void addOrder()
    {
        Order newOrder = createOrder();
        DalList.Order.Add(newOrder);
    }

    private static void displayOrder()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("|    ID     |   NAME  |  EMAIL   | ADRESS |        ORDER DATE       |        SHIP DATE      |      DELIVERY DATE    |");
        Console.WriteLine("|___________|_________|__________|________|_________________________|_______________________|_______________________|");
        Console.WriteLine("|           |         |          |        |                         |                       |                       |");
        Console.WriteLine(DalList.Order.Get(id));
    }

    private static void displayOrderList()
    {
        IEnumerable<Order> orderList = DalList.Order.GetList();
        Console.WriteLine("|    ID     |   NAME  |  EMAIL   | ADRESS |        ORDER DATE       |        SHIP DATE      |      DELIVERY DATE    |");
        Console.WriteLine("|___________|_________|__________|________|_________________________|_______________________|_______________________|");
        Console.WriteLine("|           |         |          |        |                         |                       |                       |");
        foreach (Order item in orderList)
            Console.WriteLine(item);

    }

    private static void updateOrder()
    {
        int id;
        Order newOrder = createOrder();
        Console.WriteLine("enter order ID:");
        int.TryParse(Console.ReadLine(), out id);
        newOrder.ID = id;
        DalList.Order.Update(newOrder);
    }

    private static void deleteOrder()
    {
        int id;
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out id);
        DalList.Order.Delete(id);
    }

    /// <summary>
    /// accepts product details from user
    /// </summary>
    /// <returns>product object</returns>
    private static Product createProduct()
    {
        Product newProduct = new Product();
        Console.WriteLine("enter name:");
        newProduct.Name = Console.ReadLine();
        Console.WriteLine("enter price:");
        int.TryParse(Console.ReadLine(), out int price);
        newProduct.Price = price;
        Console.WriteLine("enter category:");
        int.TryParse(Console.ReadLine(), out int category);
        newProduct.Category = (eCategory)category;
        Console.WriteLine("enter amount in stock:");
        int.TryParse(Console.ReadLine(), out int inStock);
        newProduct.InStock = inStock;
        return newProduct;

    }
    private static void addProduct()
    {
        Product newProduct = createProduct();
        DalList.Product.Add(newProduct);
    }

    private static void displayProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE | IN STOCK |");
        Console.WriteLine("|__________|__________________|__________|_______|__________|");
        Console.WriteLine("|          |                  |          |       |          |");
        Console.WriteLine(DalList.Product.Get(id));
    }

    private static void displayProductList()
    {
        IEnumerable<Product> productList = DalList.Product.GetList();
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE | IN STOCK |");
        Console.WriteLine("|__________|__________________|__________|_______|__________|");
        Console.WriteLine("|          |                  |          |       |          |");
        foreach (Product item in productList)
            Console.WriteLine(item);
    }

    private static void updateProduct()
    {
        Product newProduct = createProduct();
        Console.WriteLine("enter Product ID");
        int.TryParse(Console.ReadLine(), out int id);
        newProduct.ID = id;
        DalList.Product.Update(newProduct);
    }

    private static void deleteProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        DalList.Product.Delete(id);
    }


    /// <summary>
    /// accepts order item details from user
    /// </summary>
    /// <returns>order item object</returns>
    private static OrderItem createOrderItem()
    {
        OrderItem newOrderItem = new OrderItem();
        Console.WriteLine("enter product id:");
        int.TryParse(Console.ReadLine(), out int productId);
        newOrderItem.ProductID = productId;
        Console.WriteLine("enter order id:");
        int.TryParse(Console.ReadLine(), out int orderId);
        newOrderItem.OrderID = orderId;
        Console.WriteLine("enter price:");
        int.TryParse(Console.ReadLine(), out int price);
        newOrderItem.Price = price;
        Console.WriteLine("enter amount:");
        int.TryParse(Console.ReadLine(), out int amount);
        newOrderItem.Amount = amount;
        return newOrderItem;

    }
    private static void addOrderItem()
    {
        OrderItem newOrderItem = createOrderItem();
        DalList.OrderItem.Add(newOrderItem);
    }

    private static void displayOrderItems()
    {
        Console.WriteLine("enter order id:");
        int.TryParse(Console.ReadLine(), out int id);
        IEnumerable<OrderItem> orderItems = DalList.OrderItem.GetOrderItems(id);
        Console.WriteLine("| PRODUCT ID |  ORDER ID  |   PRICE   |  AMOUNT  |");
        Console.WriteLine("|____________|____________|___________|__________|");
        Console.WriteLine("|            |            |           |          |");
        foreach (OrderItem item in orderItems)
            Console.WriteLine(item);

    }

    private static void displayAllItems()
    {
        IEnumerable<OrderItem> orderList = DalList.OrderItem.GetList();
        Console.WriteLine("| PRODUCT ID |  ORDER ID  |   PRICE   |  AMOUNT  |");
        Console.WriteLine("|____________|____________|___________|__________|");
        Console.WriteLine("|            |            |           |          |");
        foreach (OrderItem item in orderList)
            Console.WriteLine(item);
    }

    private static void displayItem()
    {
        Console.WriteLine("enter  id:");
        int.TryParse(Console.ReadLine(), out int Id);
        Console.WriteLine("| PRODUCT ID |  ORDER ID  |   PRICE   |  AMOUNT  |");
        Console.WriteLine("|____________|____________|___________|__________|");
        Console.WriteLine("|            |            |           |          |");
        Console.WriteLine(DalList.OrderItem.Get(Id));
    }

    private static void updateOrderItem()
    {
        OrderItem newOrderItem = createOrderItem();
        DalList.OrderItem.Update(newOrderItem);
    }

    private static void deleteOrderItem()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int Id);
        DalList.OrderItem.Delete(Id);
    }
}
}
