using BlApi;
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
        }

        private void updateDliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateDeliveryOrder(orderId);
        }

        private void updateShipedBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateShipedOrder(orderId);
        }
    }
}
