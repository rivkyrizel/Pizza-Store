using BlApi;
using BlImplementation;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PL.PO;
using BO;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        IBl bl;
        public PO.Order poOrder { get; set; }
        public UpdateOrderWindow(IBl Bl, PO.Order o)
        {
            InitializeComponent();
            bl = Bl;
            poOrder = o;
            DataContext = this;
        }



        private void changeProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> lst = new List<BO.OrderItem>(poOrder.Items.ToList());
            BO.OrderItem product = (BO.OrderItem)((Button)sender).DataContext;
            int newAmount = (((Button)sender).Name == "addProductAmountBtn") ? product.Amount + 1 : product.Amount - 1;
            poOrder.TotalPrice = (poOrder.TotalPrice - product.Price * product.Amount) + product.Price * newAmount;
            product.Amount = newAmount;
            lst[lst.FindIndex(i => i.ProductID == product.ProductID)] = product;
            poOrder.Items = lst;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            List<BO.OrderItem> lst = new List<BO.OrderItem>(poOrder.Items.ToList());
            BO.OrderItem product = (BO.OrderItem)((Button)sender).DataContext;
            poOrder.TotalPrice -= product.Price * product.Amount;
            product.Amount = 0;
            lst[lst.FindIndex(i => i.ProductID == product.ProductID)] = product;
            poOrder.Items = lst;
        }


        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.Order o = new();
            o.Status = poOrder.Status;
            o.OrderDate = poOrder.OrderDate;
            o.ShipDate = poOrder.ShipDate;
            o.CustomerName = poOrder.CustomerName;
            o.CustomerAddress = poOrder.CustomerAddress;
            o.CustomerEmail = poOrder.CustomerEmail;
            o.DeliveryDate = poOrder.DeliveryDate;
            o.Items = poOrder.Items;
            o.ID = poOrder.ID;
            bl.order.UpdateOrder(o);
            Close();
        }
    }
}
