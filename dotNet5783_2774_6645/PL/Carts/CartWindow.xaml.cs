using BlApi;
using System.Windows;
using System.Windows.Controls;
using PL.Products;

namespace PL.Carts
{

    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        public PO.Cart cart { get; set; }
        public bool isAdmin { get; set; }  

        public CartWindow(IBl Bl, PO.Cart Cart , bool admin = false)
        {
            bl = Bl;
            InitializeComponent();
            cart = Cart;
            isAdmin = admin;
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

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductItemWindow(bl,cart).Show();
            Close();
        }

        private void updateOrderBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
