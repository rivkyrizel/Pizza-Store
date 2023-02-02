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

namespace PL.Carts
{

    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        IBl bl;
        public PO.Cart cart { get; set; }
        int updatedAmount;

        private void BoToPo(BO.Cart c)
        {
            cart.CustomerName = c.CustomerName;
            cart.CustomerEmail = c.CustomerEmail;
            cart.CustomerAddress = c.CustomerAddress;
            cart.Items = castBOtoPO(c.Items);
            cart.TotalPrice = c.TotalPrice;
        }

        public CartWindow(IBl Bl,PO.Cart Cart)
        {
            InitializeComponent();
            cart = Cart;
            /* cart_.Items = castBOtoPO(Cart.Items);
            cart_.TotalPrice = Cart.TotalPrice;*/
            DataContext = this;
            //  ProductsItemListview.ItemsSource = cart_.Items;
            //TotalPriceTxt.DataContext = new { TotalPrice = Cart.TotalPrice };
            bl = Bl;
        }

        private ObservableCollection<PO.OrderItem> castBOtoPO(IEnumerable<BO.OrderItem> i)
        {
            ObservableCollection<PO.OrderItem> orderItemList = new();
            foreach (var item in i)
            {
                orderItemList.Add(PLUtils.cast<PO.OrderItem, BO.OrderItem>(item));
            }
            return orderItemList;
        }

        private void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new userDetails(bl, cart).Show();
        }

        private void addProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            //((PO.OrderItem)((Button)sender).DataContext).Amount++;
             int id = ((PO.OrderItem)((Button)sender).DataContext).ProductID;
            PO.OrderItem? p = cart.Items.ToList().Find(p => p.ProductID == id);
            //p.TotalPrice = p.Price * p.Amount;
            //cart_.TotalPrice += p.Price;
            BoToPo(bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), id, (p.Amount + 1)));
        }

        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            //((PO.OrderItem)((Button)sender).DataContext).Amount--;
            int id = ((PO.OrderItem)((Button)sender).DataContext).ProductID;
            PO.OrderItem? p = cart.Items.ToList().Find(p => p.ProductID == id);
            //p.TotalPrice = p.Price * p.Amount;
            //cart_.TotalPrice -= p.Price;
            BoToPo(bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), id, (p.Amount - 1)));
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
         BoToPo(bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), ((PO.OrderItem)((Button)sender).DataContext).ProductID, 0));
        }

    }
}
