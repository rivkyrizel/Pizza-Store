using BlApi;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        IBl bl;
        int orderId;
        public PO.Order? pOrder { get; set; }
        ObservableCollection<PO.OrderForList> orders;

        public OrderWindow(IBl Bl, int id, bool admin = true, ObservableCollection<PO.OrderForList?>? o = null)
        {
            InitializeComponent();
            orderId = id;
            bl = Bl;
            pOrder = new(bl.order.GetOrder(id));
            orders = o;
            listViewOrderItems.ItemsSource = pOrder?.Items;
            pOrder.Status = admin ? pOrder.Status : BO.OrderStatus.DeliveredToCustomer;
            DataContext = pOrder;
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
        }

        private void updateShipedBtn_Click(object sender, RoutedEventArgs e)
        {
            bl.order.UpdateShipedOrder(orderId);
            int idx = orders.ToList().FindIndex(o => orderId == o.ID);
            PO.OrderForList? order = orders.ToList().Find(o => orderId == o.ID);
            orders.Remove(order);
            order.Status = BO.OrderStatus.Sent;
            orders.Insert(idx, order);
            pOrder.Status = BO.OrderStatus.Sent;
            pOrder.ShipDate = bl.order.GetOrder(orderId).ShipDate;
        }
    }
}
