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
        public OrderWindow(IBl Bl, int id, ObservableCollection<PO.OrderForList>? orders)
        {
            orderId = id;
            bl = Bl;
            PO.Order pOrder = new(bl.order.GetOrder(id));
            DataContext = pOrder;
            InitializeComponent();
            listViewOrderItems.ItemsSource = pOrder?.Items;
        }

        private void updateDliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateDeliveryOrder(orderId);
            updateDliveryBtn.Visibility = Visibility.Hidden;
            DeliverdLbl.Visibility = Visibility.Visible;
        }

        private void updateShipedBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateShipedOrder(orderId);
            updateShipedBtn.Visibility = Visibility.Hidden;
            updateDliveryBtn.Visibility = Visibility.Visible;
        }
    }
}
