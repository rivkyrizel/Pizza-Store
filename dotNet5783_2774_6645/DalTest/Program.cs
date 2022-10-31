// See https://aka.ms/new-console-template for more information
using DalFacade.DO;
using DalList;

namespace DalTest;

public class Program
{
    //private static DalProduct DalP = new DalProduct();
    //private static DalOrder DalO = new DalOrder();
    //private static DalOrderItem DalOItem = new DalOrderItem();
    static void Main()
    {
        string s;
        int val;
        do
        {
            Console.WriteLine("enter 1 for product , 2 for orders, 3 for orders items, 0 to exit");
            s = Console.ReadLine();
            val = Convert.ToInt32(s);
            switch (val)
            {
                case 1:
                    CRUDProduct();
                    break;
                case 2:
                    CRUDOrder();
                    break;
                case 3:
                    CRUDOrderItem();
                    break;
                default:
                    Console.WriteLine("incorrect input");
                    break;
            }
        } while (val != 0);

    }
    private static void CRUDOrder()
    {
        string s = "0";
        do
        {
            Console.WriteLine("enter a to add order , b to display orders by id , c to display list of orders, d to update order , e to erase order from list, 0 to return main menu");
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
            Console.WriteLine("enter a to add order item, b to display order items by order id , c to display list of all ordered items,d to display ordered item by product id and order id, e to update orders item , f to erase order item from list, 0 to return main menu");

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
            Console.WriteLine("enter a to add product , b to display product by id , c to display list of products, d to update product , e to erase product from list, 0 to return main menu");

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
    /// /////////////////
    /// </summary>
    /// <returns></returns>
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
        DalOrder.createOrder(newOrder);
    }

    private static void displayOrder()
    {
        string s;
        Console.WriteLine("enter id:");
        s = Console.ReadLine();
        int val = Convert.ToInt32(s);
        Console.WriteLine(DalOrder.readOrder(val).ToString());
    }

    private static void displayOrderList()
    {
        Order[] orderList = DalOrder.readOrderList();
        for (int i = 0; i < orderList.Length; i++)
        {
            Console.WriteLine(orderList[i].ToString());
        }
    }

    private static void updateOrder()
    {
        Order newOrder = createOrder();
        Console.WriteLine("enter order ID");
        string s = Console.ReadLine();
        newOrder.ID = Convert.ToInt32(s);
        DalOrder.updateOrder(newOrder);
    }

    private static void deleteOrder()
    {
        Console.WriteLine("enter id:");
        string s = Console.ReadLine();
        int id = Convert.ToInt32(s);
        DalOrder.deleteOrder(id);
    }

    /// <summary>
    /// /////////////////
    /// Product
    /// </summary>
    /// <returns></returns>
    private static Product createProduct()
    {
        Product newProduct = new Product();
        Console.WriteLine("enter name:");
        newProduct.Name = Console.ReadLine();
        Console.WriteLine("enter price:");
        newProduct.Price = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter category:");
        newProduct.Category = (eCategory)Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter amount in stock:");
        newProduct.InStock = Convert.ToInt32(Console.ReadLine());
        return newProduct;

    }
    private static void addProduct()
    {
        Product newProduct = createProduct();
        DalProduct.createProduct(newProduct);
    }

    private static void displayProduct()
    {
        string s;
        Console.WriteLine("enter id:");
        s = Console.ReadLine();
        int val = Convert.ToInt32(s);
        Console.WriteLine(DalProduct.readProduct(val).ToString());
    }

    private static void displayProductList()
    {
        Product[] productList = DalProduct.readProductList();
        for (int i = 0; i < productList.Length; i++)
        {
            Console.WriteLine(productList[i].ToString());
        }
    }

    private static void updateProduct()
    {
        Product newProduct = createProduct();
        Console.WriteLine("enter Product ID");
        string s = Console.ReadLine();
        newProduct.ID = Convert.ToInt32(s);
        DalProduct.updateProduct(newProduct);
    }

    private static void deleteProduct()
    {
        Console.WriteLine("enter id:");
        string s = Console.ReadLine();
        int id = Convert.ToInt32(s);
        DalProduct.deleteProduct(id);
    }


    /// <summary>
    /// /////////////////
    /// order item
    /// </summary>
    /// <returns></returns>
    private static OrderItem createOrderItem()
    {
        OrderItem newOrderItem = new OrderItem();
        Console.WriteLine("enter product id:");
        newOrderItem.ProductID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter order id:");
        newOrderItem.OrderID = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter price:");
        newOrderItem.Price = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter amount:");
        newOrderItem.Amount = Convert.ToInt32(Console.ReadLine());
        return newOrderItem;

    }
    private static void addOrderItem()
    {
        OrderItem newOrderItem = createOrderItem();
        DalOrderItem.createOrderItem(newOrderItem);
    }

    private static void displayOrderItems()
    {
        string s;
        Console.WriteLine("enter order id:");
        s = Console.ReadLine();
        int val = Convert.ToInt32(s);
        OrderItem[] orderItems = DalOrderItem.readOrderItems(val);
        for (int i = 0; i < orderItems.Length; i++)
            if (orderItems[i].OrderID != 0)
                Console.WriteLine(orderItems[i].ToString());
       
    }

    private static void displayAllItems()
    {
        OrderItem[] orderList = DalOrderItem.readAllItems();
        for (int i = 0; i < orderList.Length; i++)
        {
            Console.WriteLine(orderList[i].ToString());
        }
    }

    private static void displayItem()
    {
        Console.WriteLine("enter order id:");
        int orderId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine(DalOrderItem.readOrderItem(orderId, productId).ToString());
    }

    private static void updateOrderItem()
    {
        OrderItem newOrderItem = createOrderItem();
        DalOrderItem.updateOrderItem(newOrderItem);
    }

    private static void deleteOrderItem()
    {
        Console.WriteLine("enter product id:");
        int productId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("enter order id:");
        int orderId = Convert.ToInt32(Console.ReadLine());
        DalOrderItem.deleteOrder(orderId, productId);
    }

}

