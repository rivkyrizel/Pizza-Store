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

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    IBl bl;
    bool admin;
    BO.Cart? cart;

    public ProductListWindow(IBl Bl, bool Admin=true, BO.Cart? Cart=null)
    {
        bl = Bl;
        cart=Cart;
        admin = Admin;
        InitializeComponent();
        List<string> list = Enum.GetNames(typeof(BO.eCategory)).ToList();
        list.Insert(0, "all categories");
        AttributeSelector.ItemsSource = list;
        ProductsListview.ItemsSource = bl.product.GetProductList();
        if (!admin)
        {
            BtnAddProduct.Visibility = Visibility.Hidden;
            confirmOrderBtn.Visibility = Visibility.Visible;
        }
    }

    private void AttributeSelector_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        object s = AttributeSelector.SelectedItem;
        if (s== "all categories") ProductsListview.ItemsSource = bl.product.GetProductList();
        else ProductsListview.ItemsSource = bl.product.GetProductList((BO.eCategory)Enum.Parse(typeof(BO.eCategory),s.ToString()));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl,"add").Show();
    }
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (admin)
            new ProductWindow(bl, "update", ((BO.ProductForList?)ProductsListview.SelectedItems[0])?.ID ?? throw new PlNullObjectException()).Show();
        else
            new ProductWindow(bl, "show", ((BO.ProductForList?)ProductsListview.SelectedItems[0])?.ID ?? throw new PlNullObjectException(), cart).Show();
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.product.GetProductList();
    }

    private void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
    {
        new userDetails(bl, cart).Show();
        Close();
    }
}
