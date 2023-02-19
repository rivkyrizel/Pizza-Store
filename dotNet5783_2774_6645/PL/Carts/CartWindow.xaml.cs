using BlApi;
using BlImplementation;
using BO;
using PL.PO;
using PL;
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
using DO;

namespace PL.Carts
{

    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        public PO.Cart cart { get; set; }


        public CartWindow(IBl Bl, PO.Cart Cart)
        {
            bl = Bl;
            InitializeComponent();
            cart = Cart;
            DataContext = this;
        }

        private void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new userDetails(bl, cart).Show();
            Close();
        }

        private void changeProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            PO.OrderItem product = (PO.OrderItem)((Button)sender).DataContext;
            int newAmount = (((Button)sender).Name == "addProductAmountBtn") ? product.Amount + 1 : product.Amount - 1;
            PLUtils.castCart(bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), product.ProductID, newAmount),cart);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            PLUtils.castCart(bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), ((PO.OrderItem)((Button)sender).DataContext).ProductID, 0),cart);
        }

    }
}
