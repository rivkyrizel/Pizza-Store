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
using System.Collections.ObjectModel;

namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl;
    BO.Product? p;
    int productID;
    BO.Cart? cart;
    PO.Product currentProduct;
    private ObservableCollection<PO.Product> products { get; set; }

    private void cast(BO.Product p)
    {
        currentProduct.Name = p.Name;
        currentProduct.Price = p.Price;
        currentProduct.Category = p.Category;
        currentProduct.InStock = p.InStock;
        currentProduct.ID = p.ID;
    }


    private void initializeDataContext()
    {
        currentProduct = new Product();
        GridData.DataContext = currentProduct;
    }
    public ProductWindow(IBl Bl, string a, ObservableCollection<PO.Product> Products, int id = 0, BO.Cart? Cart = null)
    {
        InitializeComponent();
        bl = Bl;
        cart = Cart;
        products = Products;
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        productID = id;
        currentProduct = new();
        initializeDataContext();

        if (a == "add") createAddWindow();
        else if (a == "update")
            createUpdateWindow();
        else createShowWindow();
    }

    private void createShowWindow()
    {
        cast(bl.product.GetProductForManager(productID));
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
        cast(p);
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
            int id = bl.product.AddProduct(p);
            cast(p);
            products.Remove(products.ToList().Find(po => p.ID == po.ID));
            currentProduct.ID = id;
            products.Add(currentProduct);
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
            cast(p);
            int idx = products.ToList().FindIndex(po => p.ID == po.ID);
            products.RemoveAt(idx);
            products.Insert(idx, currentProduct);
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
            bl?.product.DeleteProduct(productID);
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
        cart = bl?.Cart.AddToCart(cart, productID);
        Close();
    }

}
