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
using System.Windows.Navigation;
using System.Windows.Media.Animation;
using PL.Carts;
namespace PL.Products;


/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    IBl? bl;
    bool admin;
    bool add;
    BO.Cart? cart;

    public ProductListWindow(IBl? Bl, bool Admin = true, bool Add = false, BO.Cart? Cart = null)
    {
        bl = Bl;
        cart = Cart;
        admin = Admin;
        add = Add;
        InitializeComponent();
        List<string> list = Enum.GetNames(typeof(BO.eCategory)).ToList();
        list.Insert(0, "all categories");
        AttributeSelector.ItemsSource = list;
        ProductsListview.ItemsSource = bl?.product.GetProductList();
        if (!admin)
        {
            BtnAddProduct.Visibility = Visibility.Hidden;
            viewCartBtn.Visibility = Visibility.Visible;
        }
    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        object s = AttributeSelector.SelectedItem;
        if (s.Equals("all categories")) ProductsListview.ItemsSource = bl.product.GetProductList();
        else ProductsListview.ItemsSource = bl.product.GetProductList((BO.eCategory)Enum.Parse(typeof(BO.eCategory), s.ToString()));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, "add").Show();
    }
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        string state = admin ? "update" : "show";
        new ProductWindow(bl, state, ((BO.ProductForList?)ProductsListview.SelectedItems[0])?.ID ?? throw new PlNullObjectException(),cart).Show();
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.product.GetProductList();
    }

    private void viewCartBtn_Click(object sender, RoutedEventArgs e)
    {
       new CartWindow(bl,cart).Show();
    }

}
