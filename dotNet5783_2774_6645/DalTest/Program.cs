// See https://aka.ms/new-console-template for more information
using DalFacade.DO;
using DalList;

namespace DalTest;

public class Program
{
    static void Main()
    {
        int choice;
        do
        {
            Console.WriteLine("enter 1 for product , 2 for orders, 3 for orders items, 0 to exit");
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
                    CRUDOrderItem();
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
        DalOrder.CreateOrder(newOrder);
    }

    private static void displayOrder()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine(DalOrder.ReadOrder(id));
    }

    private static void displayOrderList()
    {
        Order[] orderList = DalOrder.ReadOrderList();
        for (int i = 0; i < orderList.Length; i++)
            Console.WriteLine(orderList[i]);
    }

    private static void updateOrder()
    {
        int id;
        Order newOrder = createOrder();
        Console.WriteLine("enter order ID");
        int.TryParse(Console.ReadLine(), out id);
        newOrder.ID = id;
        DalOrder.UpdateOrder(newOrder);
    }

    private static void deleteOrder()
    {
        int id;
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out id);
        DalOrder.DeleteOrder(id);
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
        DalProduct.CreateProduct(newProduct);
    }

    private static void displayProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine(DalProduct.ReadProduct(id));
    }

    private static void displayProductList()
    {
        Product[] productList = DalProduct.ReadProductList();
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE | IN STOCK |");
        Console.WriteLine("|__________|__________________|__________|_______|__________|");
        Console.WriteLine("|          |                  |          |       |          |");
        for (int i = 0; i < productList.Length; i++)
        {
            Console.WriteLine(productList[i]);
        }
    }

    private static void updateProduct()
    {
        Product newProduct = createProduct();
        Console.WriteLine("enter Product ID");
        int.TryParse(Console.ReadLine(), out int id);
        newProduct.ID = id;
        DalProduct.UpdateProduct(newProduct);
    }

    private static void deleteProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        DalProduct.DeleteProduct(id);
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
        DalOrderItem.CreateOrderItem(newOrderItem);
    }

    private static void displayOrderItems()
    {
        Console.WriteLine("enter order id:");
        int.TryParse(Console.ReadLine(), out int id);
        OrderItem[] orderItems = DalOrderItem.ReadOrderItems(id);
        for (int i = 0; i < orderItems.Length; i++)
            if (orderItems[i].OrderID != 0)
                Console.WriteLine(orderItems[i]);

    }

    private static void displayAllItems()
    {
        OrderItem[] orderList = DalOrderItem.ReadAllItems();
        for (int i = 0; i < orderList.Length; i++)
        {
            Console.WriteLine(orderList[i]);
        }
    }

    private static void displayItem()
    {
        Console.WriteLine("enter order id:");
        int.TryParse(Console.ReadLine(), out int orderId);
        Console.WriteLine("enter product id:");
        int.TryParse(Console.ReadLine(), out int productId);
        Console.WriteLine(DalOrderItem.ReadOrderItem(orderId, productId));
    }

    private static void updateOrderItem()
    {
        OrderItem newOrderItem = createOrderItem();
        DalOrderItem.UpdateOrderItem(newOrderItem);
    }

    private static void deleteOrderItem()
    {
        Console.WriteLine("enter product id:");
        int.TryParse(Console.ReadLine(), out int productId);
        Console.WriteLine("enter order id:");
        int.TryParse(Console.ReadLine(), out int orderId);
        DalOrderItem.DeleteOrder(orderId, productId);
    }

}

