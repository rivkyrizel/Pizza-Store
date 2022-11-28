using BlApi;
using BlImplementation;
using BO;

public class Program
{
    public static IBl BL = new Bl();
    public static Cart cart = new();

    //=========================================== MAIN ===================================================
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


    //=========================================== ORDER ===================================================

    private static void CRUDOrder()
    {
        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to get order for manager \n b to display orders by id \n c to update shiped date \n d to update dalivery date \n e to update order \n 0 to return main menu");
            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    displayOrderList();
                    break;
                case "b":
                    displayOrder();
                    break;
                case "c":
                    updateShipedDate();
                    break;
                case "d":
                    updateDeliveryDate();
                    break;
                case "e":
                    updateOrder();
                    break;
            }
        } while (s != "0");

    }







    //=========================================== CART ===================================================

    private static void CRUDCart()
    {
        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to add to cart \n b to update amount of item \n c to confirm order");

            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    addToCart();
                    break;
                case "b":
                    updateAmount();
                    break;
                case "c":
                    confirmOrder();
                    break;
            }
        } while (s != "0");

    }


    private static void addToCart()
    {
        Console.WriteLine("enter product ID:");
        int.TryParse(Console.ReadLine(), out int pId);
        cart = BL.Cart.AddToCart(cart, pId);
    }

    private static void confirmOrder()
    {
        Console.WriteLine("enter name:");
        string name = Console.ReadLine();
        Console.WriteLine("enter email:");
        string email = Console.ReadLine();
        Console.WriteLine("enter address:");
        string address = Console.ReadLine();

        BL.Cart.confirmOrder(cart, name, email, address);
    }

    private static void updateAmount()
    {
        Console.WriteLine("enter product ID:");
        int.TryParse(Console.ReadLine(), out int pId);
        Console.WriteLine("enter new amount:");
        int.TryParse(Console.ReadLine(), out int amount);
        cart = BL.Cart.updateAmount(cart, pId, amount);
    }



    //=========================================== PRODUCT ===================================================

    private static void CRUDProduct()
    {

        string s = "0";
        do
        {
            Console.WriteLine("enter: \n a to get product for manager \n b to get product to customer \n c to get product by id to customer  \n d to get product by id to manager  \n e to add product \n f to erase product from list \n g  to update product\n  0 to return main menu");

            s = Console.ReadLine();
            switch (s)
            {
                case "a":
                    getProductList();
                    break;
                case "b":
                    getProductItem();
                    break;
                case "c":
                    getProductForCustomer();
                    break;
                case "d":
                    getProductForManager();
                    break;
                case "e":
                    addProduct();
                    break;
                case "f":
                    deleteProduct();
                    break;
                case "g":
                    updateProduct();
                    break;
            }
        } while (s != "0");
    }

    ///  <summary>
    ///  accepts order details from user
    ///  </summary>
    ///  <returns>order object</returns>

    private static void getProductList()
    {
        IEnumerable<ProductForList> productList = BL.product.GetProductList();
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE |");
        Console.WriteLine("|__________|__________________|__________|_______|");
        Console.WriteLine("|          |                  |          |       |");
        foreach (ProductForList item in productList)
           Console.WriteLine(item);
    }

    private static void getProductItem()
    {
        IEnumerable<ProductItem> productList = BL.product.GetProductItem();
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE |   AMOUNT  |   IN STOCK |");
        Console.WriteLine("|__________|__________________|__________|_______|___________|____________|");
        Console.WriteLine("|          |                  |          |       |           |            |");
        foreach (ProductItem item in productList)
            Console.WriteLine(item);
    }

    private static void getProductForCustomer()
    {
       int id= displayProduct();
        Product p = BL.product.GetProductForCustomer(id);
        Console.WriteLine(p);
    }

    private static void getProductForManager()
    {
        int id=displayProduct();
        Product p = BL.product.GetProductForManager(id);
        Console.WriteLine(p);
    }

    private static void displayOrder()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("|    ID     |   NAME  |  EMAIL   | ADRESS |        ORDER DATE       |        SHIP DATE      |      DELIVERY DATE    |       STATUS      |  TOTAL PRICE  |");
        Console.WriteLine("|___________|_________|__________|________|_________________________|_______________________|_______________________|___________________|_______________|");
        Console.WriteLine("|           |         |          |        |                         |                       |                       |                   |               |");
        Console.WriteLine(BL.order.GetOrder(id));
    }

    private static void displayOrderList()
    {
        IEnumerable<OrderForList> orderList = BL.order.OrderList();
        Console.WriteLine("|    ID     |   NAME  |           STATUS         | AMOUNT |TOTAL PRICE|");
        Console.WriteLine("|___________|_________|__________________________|________|___________|");
        Console.WriteLine("|           |         |                          |        |           |");
        foreach (OrderForList item in orderList)
            Console.WriteLine(item);

    }

    private static void updateShipedDate()
    {
        try
        {
            int id;
            Console.WriteLine("enter order ID:");
            int.TryParse(Console.ReadLine(), out id);
            BL.order.UpdateShipedOrder(id);
        }
        catch(BlInvalidStatusException e)
        {
            Console.WriteLine(e.Message);
        }
        catch(BlIdNotFound e)
        {
            Console.WriteLine(e);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void updateDeliveryDate()
    {
        try
        {
            int id;
            Console.WriteLine("enter order ID:");
            int.TryParse(Console.ReadLine(), out id);
            BL.order.UpdateDeliveryOrder(id);
        }
        catch (BlInvalidStatusException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (BlIdNotFound e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void updateOrder()
    {
    }


    /// <summary>
    /// accepts product details from user
    /// </summary>
    /// <returns>product object</returns>
    private static Product createProduct()
    {
        Product newProduct = new();
        Console.WriteLine("enter name:");
        newProduct.Name = Console.ReadLine();
        newProduct.ID = Dal.DataSource.Config.ProductID;
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
        BL.product.AddProduct(newProduct);
    }

    private static int displayProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        Console.WriteLine("|    ID    |       NAME       | CATEGORY | PRICE | IN STOCK |");
        Console.WriteLine("|__________|__________________|__________|_______|__________|");
        Console.WriteLine("|          |                  |          |       |          |");
        return id;
    }



    private static void updateProduct()
    {
        Product newProduct = createProduct();
        Console.WriteLine("enter Product ID");
        int.TryParse(Console.ReadLine(), out int id);
        newProduct.ID = id;
        BL.product.UpdateProduct(newProduct);
    }

    private static void deleteProduct()
    {
        Console.WriteLine("enter id:");
        int.TryParse(Console.ReadLine(), out int id);
        BL.product.DeleteProduct(id);
    }


    /// <summary>
    /// accepts order item details from user
    /// </summary>
    /// <returns> cart object</returns>


}

