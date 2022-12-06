using BlApi;
using BlImplementation;
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

namespace PL.BoEntities
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl bl;

        int productID;

        public ProductWindow(IBl Bl , bool add = true, int id=0)
        {
            InitializeComponent();
            bl = Bl;
            SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
            productID = id;

            if (add) createAddWindow();
            else createUpdateWindow();
            
        }

        private void createAddWindow()
        {
            BtnDelete.Visibility = Visibility.Hidden;
            BtnUpdate.Visibility = Visibility.Hidden;
        }

        private void createUpdateWindow()
        {
            BO.Product p = bl.product.GetProductForManager(productID);
            TxtAmount.Text = p.InStock.ToString();
            TxtName.Text = p.Name;
            TxtPrice.Text = p.Price.ToString();
            SelectCategory.Text = p.Category.ToString();
            BtnAdd.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p = new();
            p.Name = TxtName.Text;
            int.TryParse(TxtAmount.Text, out int a);
            p.InStock = a;
            int.TryParse(TxtPrice.Text, out int b);
            p.Price = b;
            object s = SelectCategory.SelectedItem;
            p.Category = (BO.eCategory)s;
            bl.product.AddProduct(p);
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p = new();
            p.Name = TxtName.Text;
            int.TryParse(TxtAmount.Text, out int a);
            p.InStock = a;
            int.TryParse(TxtPrice.Text, out int b);
            p.Price = b;
            p.ID = productID;
            object s = SelectCategory.SelectedItem;
            p.Category = (BO.eCategory)s;
            bl.product.UpdateProduct(p);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            bl.product.DeleteProduct(productID);
        }
    }
}
