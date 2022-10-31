// See https://aka.ms/new-console-template for more information
using DalFacade.DO;
using DalList;

namespace DalTest;

public class Program
{
    private DalProduct DalP = new DalProduct();
    private DalOrder DalO = new DalOrder();
    private DalOrderItem DalOItem = new DalOrderItem();
    public void Main()
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
    public void CRUDOrder()
    {

    }
    public void CRUDOrderItem()
    {

    }
    public void CRUDProduct()
    {

    }
}

