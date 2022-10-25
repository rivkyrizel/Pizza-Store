﻿using DalFacade.DO;

namespace DalList;

internal static class DataSource
{
    static readonly Random rand = new Random();
    internal static Product[] ProductList = new Product[50];
    internal static Order[] OrderList = new Order[100];
    internal static OrderItem[] OrderItem = new OrderItem[200];
    static DataSource()
    {
        SInitialize();
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

    private static void CreateProductList()
    {
        string[] productNames = { "כסא", "שולחן" };

        for (int i = 0; i < 10; Config.productIdx++, i++)
        {
            ProductList[i] = new Product();
            int numberForName = (int)rand.NextInt64(productNames.Length);
            int numberForPrice = (int)rand.NextInt64(10, 100);
            ProductList[i].Name = productNames[numberForName];
            ProductList[i].Price = numberForPrice;
            ProductList[i].ID = Config.ProductID;
            ProductList[i].InStock = (int)rand.NextInt64(10, 5000);
            int x = 1;
            ProductList[i].Category = (eCategory)x;
        }
    }
    private static void CreateOrderList()
    {
        string[] CustomerName = { "aaa", "bbb", "ccc" };
        string[] CustomerAdress = { "ddd", "eee", "fff" };
        string[] CustomerEmail = { "ggg", "hhh", "iii" };
        for (int i = 0; i < 20; Config.orderIdx++, i++)
        {
            OrderList[i] = new Order();
            int numberForName = (int)rand.NextInt64(CustomerName.Length);
            int numberForAdress = (int)rand.NextInt64(CustomerAdress.Length);
            int numberForEmail = (int)rand.NextInt64(CustomerEmail.Length);
            TimeSpan ShipDate = TimeSpan.FromDays(2);
            TimeSpan deliveryDate = TimeSpan.FromDays(20);
            OrderList[i].ID = Config.OrderID;
            OrderList[i].CustomerName = CustomerName[numberForName];
            OrderList[i].CustomerAdress = CustomerAdress[numberForAdress];
            OrderList[i].CustomerEmail = CustomerEmail[numberForEmail];
            OrderList[i].OrderDate = DateTime.MinValue;
            OrderList[i].ShipDate = OrderList[i].OrderDate + ShipDate;
            OrderList[i].DeliveryDate = OrderList[i].OrderDate + deliveryDate;

        }
    }
    private static void CreateOrderItemList()
    {
        string[] productNames = { "כסא", "שולחן" };
        for (int i = 0; i < 40; Config.orderItemIdx++, i++)
        {
            int orderAmount = (int)rand.NextInt64(1, 4);
            int orderIdx = i % 20;
            for (int j = 0; j < orderAmount; j++)
            {
                int productIdx = i % 10;
                int itemAmount = (int)rand.NextInt64(1, 15);
                OrderItem[i] = new OrderItem();
                OrderItem[i].OrderID = OrderList[orderIdx].ID;
                OrderItem[i].Amount = itemAmount;
                OrderItem[i].Price = ProductList[productIdx].Price * itemAmount;
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
