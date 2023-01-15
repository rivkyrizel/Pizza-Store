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
        BO.Cart BOcart;
        PO.Cart cart;
        int updatedAmount;
       
        public CartWindow(IBl Bl,BO.Cart? Cart)
        {
            InitializeComponent();
            cart=new();
            cart.Items = castBOtoPO(Cart.Items);
            ProductsItemListview.ItemsSource = cart.Items;
            bl = Bl;
            BOcart = Cart??throw new PlNullObjectException();
        }

        private ObservableCollection<PO.OrderItem> castBOtoPO(IEnumerable<BO.OrderItem> i)
        {
            ObservableCollection<PO.OrderItem> orderItemList = new();
            foreach(var item in i)
            {
                orderItemList.Add(PLUtils.cast<PO.OrderItem, BO.OrderItem>(item));
            }
            return orderItemList;
        }

  


        private void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new userDetails(bl, BOcart).Show();
        }

        private void addProductAmountBtn_Click(object sender, RoutedEventArgs e)
        {
           ((PO.OrderItem)((Button)sender).DataContext).Amount++;
            int id = ((PO.OrderItem)((Button)sender).DataContext).ProductID;
            PO.OrderItem? p = cart.Items.ToList().Find(p => p.ProductID == id);
            p.TotalPrice = p.Price * p.Amount;

        }
        private void decreaseProductBtn_Click(object sender, RoutedEventArgs e)
        {
            ((PO.OrderItem)((Button)sender).DataContext).Amount--;
            int id = ((PO.OrderItem)((Button)sender).DataContext).ProductID;
            PO.OrderItem? p = cart.Items.ToList().Find(p => p.ProductID == id);
            p.TotalPrice = p.Price * p.Amount;
        }

        private void updateAmountTxt_Click(object sender, RoutedEventArgs e)
        {
            bl.Cart.updateAmount(PLUtils.cast<BO.Cart, PO.Cart>(cart), ((PO.OrderItem)((Button)sender).DataContext).ProductID, ((PO.OrderItem)((Button)sender).DataContext).Amount);
        }
    }
}
