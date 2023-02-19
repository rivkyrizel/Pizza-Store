using BlApi;
using BlImplementation;
using BO;
using PL.Carts;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        IBl bl;
        PO.Cart cart_;
        List<string> lst { get; set; }
        private ObservableCollection<PO.Product> products { get; set; } = new();
        public ProductItemWindow(IBl Bl)
        {
            InitializeComponent();
            bl = Bl;
            cart_ = new();
            lst = Enum.GetNames(typeof(BO.eCategory)).ToList();
            lst.Insert(0, "all categories");
            cast(bl.product.GetProductList());
            AttributeSelector.ItemsSource = lst;
            ProductsListview.ItemsSource = products;
        }
        public void cast(IEnumerable<ProductForList?> enumerable)
        {
            products.Clear();
            enumerable.ToList().ForEach(p => products.Add(PLUtils.cast<PO.Product, BO.ProductForList>(p)));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AttributeSelector.SelectedItem.Equals("all categories"))
                cast(bl.product.GetProductList());
            else
                cast(bl.product.GetProductList((BO.eCategory)Enum.Parse(typeof(BO.eCategory), AttributeSelector.SelectedItem.ToString())));
        }


        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(bl, "show", products,  cart_, ((PO.Product?)ProductsListview.SelectedItems[0])?.ID ?? throw new PlNullObjectException()).Show();

        }

        private void viewCartBtn_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow(bl, cart_).Show();
        }
    }
}
