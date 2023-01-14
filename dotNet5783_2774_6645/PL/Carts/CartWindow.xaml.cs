using BlApi;
using BlImplementation;
using BO;
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

namespace PL.Carts
{

    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        BO.Cart cart;
        int updatedAmount;
        public CartWindow(IBl Bl,BO.Cart? Cart)
        {
            InitializeComponent();
            ProductsItemListview.ItemsSource = Cart?.Items;
            bl = Bl;
            cart = Cart??throw new PlNullObjectException();
        }

        private void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new userDetails(bl, cart).Show();
        }

        private void addProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
           updatedAmount = cart.Items.ToList().Find(p => p.ProductID == ((BO.ProductItem?)ProductsItemListview.SelectedItems[0]).ID).Amount;
            //updateAmountTxt.Text = updatedAmount.ToString();
        }
        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
