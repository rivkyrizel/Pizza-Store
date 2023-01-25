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
using System.Collections;
using BO;
using System.Collections.ObjectModel;

namespace PL.Products;


/// <summary>
/// Interaction logic for ProductListWindow.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    IBl bl;
    bool admin;
    bool add;
    BO.Cart? cart;
    List<string> lst { get; set; }  
   private ObservableCollection<PO.Product> products { get; set; }

    public ProductListWindow(IBl Bl, bool Admin = true, bool Add = false)
    {
        bl = Bl;
        cart = new();
        admin = Admin;
        add = Add;
        InitializeComponent();
        products= new ObservableCollection<PO.Product>();
        List<string> list = Enum.GetNames(typeof(BO.eCategory)).ToList();
        list.Insert(0, "all categories");
        lst = list;
        cast(bl.product.GetProductList());
        ProductsListview.ItemsSource = products;
        prodListbtns.DataContext = new {admin = Admin};
    }

    public void cast(IEnumerable<ProductForList?> enumerable)
    {
        products.Clear();
        foreach (var i in enumerable)
        {
            PO.Product p = new();
            p.ID = i.ID;
            p.Category = i.Category;
            p.Price = i.Price;
            p.Name = i.Name;
            products.Add(p);
        }
    }

    private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        object s = AttributeSelector.SelectedItem;
        if (s.Equals("all categories")) cast(bl.product.GetProductList());
        else cast(bl.product.GetProductList((BO.eCategory)Enum.Parse(typeof(BO.eCategory), s.ToString())));
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new ProductWindow(bl, "add", products).Show();
    }
    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        string state = admin ? "update" : "show";
        new ProductWindow(bl, state, products,((PO.Product?)ProductsListview.SelectedItems[0])?.ID ?? throw new PlNullObjectException(), cart).Show();

    }

    private void viewCartBtn_Click(object sender, RoutedEventArgs e)
    {
        new CartWindow(bl, cart).Show();
    }

}
