using BlApi;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using PL.Carts;
using PL.General;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        IBl bl;
        int orderId;
        public PO.Order pOrder { get; set; }
        public bool isAdmin { get; set; }
        ObservableCollection<PO.OrderForList>? orders;

        public OrderWindow(IBl Bl, int id, bool admin = true, ObservableCollection<PO.OrderForList>? o = null)
        {
            InitializeComponent();
            orderId = id;
            bl = Bl;
            try
            {
                pOrder = new(bl.order.GetOrder(id));
            }
            catch (BlIdNotFound e)
            {
                MessageBox.Show(e.Message + e.InnerException);
            }
            catch (BlInvalideData e)
            {
                MessageBox.Show(e.Message);
            }
            orders = o;
            listViewOrderItems.ItemsSource = pOrder?.Items;
            isAdmin = admin;
            DataContext = this;
        }

        private void updateDliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.order.UpdateDeliveryOrder(orderId);

                pOrder.DeliveryDate = bl.order.GetOrder(orderId).DeliveryDate;
                int idx = orders.ToList().FindIndex(o => orderId == o.ID);

                PO.OrderForList? order = orders.ToList().Find(o => orderId == o.ID);

                orders.Remove(order);
                order.Status = BO.OrderStatus.DeliveredToCustomer;
                orders.Insert(idx, order);
                pOrder.Status = BO.OrderStatus.DeliveredToCustomer;
                Close();
            }
            catch (BlInvalidStatusException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (PlNullObjectException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlIdNotFound ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }
        }

        private void updateShipedBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.order.UpdateShipedOrder(orderId);
                int idx = orders.ToList().FindIndex(o => orderId == o.ID);
                PO.OrderForList order = orders.ToList().Find(o => orderId == o.ID) ?? throw new PlNullObjectException();
                orders.Remove(order);
                order.Status = BO.OrderStatus.Sent;
                orders.Insert(idx, order);
                pOrder.Status = BO.OrderStatus.Sent;
                pOrder.ShipDate = bl.order.GetOrder(orderId).ShipDate;
                Close();
            }
            catch (BlInvalidStatusException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (PlNullObjectException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BlIdNotFound ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException);
            }

        }

        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new UpdateOrderWindow(bl, pOrder, orders).Show();
            Close();
        }
    }
}
