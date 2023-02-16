using BlApi;
using PL.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        IBl bl;
        int orderId;
        PO.Order pOrder;
        ObservableCollection<PO.OrderForList> orders;

        public OrderWindow(IBl Bl, int id, ObservableCollection<PO.OrderForList> o)
        {
            orderId = id;
            bl = Bl;
            pOrder = new(bl.order.GetOrder(id));
            DataContext = pOrder;
            orders = o;
            InitializeComponent();
            listViewOrderItems.ItemsSource = pOrder?.Items;
        }

        private void updateDliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateDeliveryOrder(orderId);
            pOrder.DeliveryDate = bl.order.GetOrder(orderId).DeliveryDate;
            int idx = orders.ToList().FindIndex(o => orderId == o.ID);
            PO.OrderForList? order = orders.ToList().Find(o => orderId == o.ID);
            orders.Remove(order);
            order.Status = BO.OrderStatus.DeliveredToCustomer;
            orders.Insert(idx, order);
            pOrder.Status = BO.OrderStatus.DeliveredToCustomer;
            updateShipedBtn.Visibility = Visibility.Hidden;
            updateDliveryBtn.Visibility = Visibility.Hidden;
        }

        private void updateShipedBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateShipedOrder(orderId);
            int idx = orders.ToList().FindIndex(o => orderId == o.ID);
            PO.OrderForList? order = orders.ToList().Find(o => orderId == o.ID);
            orders.Remove(order);
            order.Status = BO.OrderStatus._______Sent________;
            orders.Insert(idx, order);
            pOrder.ShipDate = bl.order.GetOrder(orderId).ShipDate;
            pOrder.Status = BO.OrderStatus._______Sent________;
            updateShipedBtn.Visibility = Visibility.Hidden;
            updateDliveryBtn.Visibility = Visibility.Visible;
        }
    }
}
