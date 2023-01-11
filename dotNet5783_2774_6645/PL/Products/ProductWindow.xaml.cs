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
using PL.PO;
namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl;
    BO.Product p;
    int productID;
    BO.Cart? cart;
    private Product currentProduct;  


    private void initializeDataContext()
    {
        currentProduct = new Product(p,bl);
        DataContext = currentProduct;
    }
    public ProductWindow(IBl Bl, string a, int id = 0, BO.Cart? Cart=null)
    {
        InitializeComponent();
        bl = Bl;
        cart = Cart;
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        productID = id;

        if (a == "add") createAddWindow();
        else if (a == "update") createUpdateWindow();
        else createShowWindow();
        initializeDataContext();
    }

    private void createShowWindow()
    {
        BO.Product p = bl.product.GetProductForManager(productID);
       // DataContext = p;
        BtnAdd.Visibility = Visibility.Hidden;
        BtnDelete.Visibility = Visibility.Hidden;
        BtnUpdate.Visibility = Visibility.Hidden;
        addToCartBtn.Visibility = Visibility.Visible;
        TxtAmount.IsReadOnly = true;
        TxtName.IsReadOnly = true;
        TxtPrice.IsReadOnly = true;
    }

    private void createAddWindow()
    {
        BtnDelete.Visibility = Visibility.Hidden;
        BtnUpdate.Visibility = Visibility.Hidden;
    }

    private void createUpdateWindow()
    {
         p = bl.product.GetProductForManager(productID);
       // DataContext = p;
        BtnAdd.Visibility = Visibility.Hidden;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Product p = new();
            p.Name = TxtName.Text;
            int.TryParse(TxtAmount.Text, out int a);
            p.InStock = a;
            int.TryParse(TxtPrice.Text, out int b);
            p.Price = b;
            object s = SelectCategory.SelectedItem;
            if (s == null) p.Category = null;
            else p.Category = (BO.eCategory)s;
            bl.product.AddProduct(p);
            Close();
        }
        catch (BlIdNotFound ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException);
        }
        catch (BlNullValueException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlInvalideData ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
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
            currentProduct.update(p);
            Close();
        }
        catch (BlIdNotFound ex)
        {
            MessageBox.Show(ex.Message + ex.InnerException);
        }
        catch (BlNullValueException ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlInvalideData ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.product.DeleteProduct(productID);
            Close();
        }
        catch (BlProductFoundInOrders ex)
        {
            MessageBox.Show(ex.Message);
        }
        catch (BlIdNotFound ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BtnReturn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void addToCartBtn_Click(object sender, RoutedEventArgs e)
    {
        cart = bl.Cart.AddToCart(cart, productID);
        Close();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        currentProduct.Name = "newName";
    }
}
