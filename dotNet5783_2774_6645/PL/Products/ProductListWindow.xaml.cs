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

namespace PL.BoEntities
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        IBl bl;
      
        public ProductListWindow(IBl Bl)
        {
            bl = Bl;
            InitializeComponent();
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            ProductsListview.ItemsSource = bl.product.GetProductList();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object s = AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource = bl.product.GetProductList((BO.eCategory)s);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(bl).Show();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(bl, false, ((BO.ProductForList)ProductsListview.SelectedItems[0]).ID).Show();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ProductsListview.ItemsSource = bl.product.GetProductList();
        }
    }
}
