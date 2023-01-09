using BlApi;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        IBl bl;
        int orderId;
        public OrderWindow(IBl Bl, int id = 0)
        {
            orderId = id;
            bl = Bl;
            BO.Order bOrder = bl.order.GetOrder(id);
            DataContext = bOrder;
            InitializeComponent();
            listViewOrderItems.ItemsSource = bOrder?.Items;
            if (bOrder.Status == 0)
            {
                updateShipedBtn.Visibility = Visibility.Visible;
            }
            else if (bOrder.Status == (BO.OrderStatus)1)
            {
                updateDliveryBtn.Visibility = Visibility.Visible;
            }
            else
            {
                DeliverdLbl.Visibility = Visibility.Visible;
            }
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
