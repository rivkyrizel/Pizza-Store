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
    int productID;
    BO.Cart? cart;
    PO.Product currentProduct;
    private ObservableCollection<PO.Product> products { get; set; }

    private BO.Product cast(PO.Product POp)
    {
        BO.Product b = new();
        b.Category = POp.Category;
        b.ID = POp.ID;
        b.Name = POp.Name;
        b.InStock = POp.InStock;
        b.Price = POp.Price;
        return b;
    }


    private void initializeDataContext()
    {
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
        currentProduct = id == 0 ? new() : new(bl.product.GetProductForManager(productID));
        initializeDataContext();
         if(a=="show") createShowWindow();
    }

    private void createShowWindow()
    {
        addToCartBtn.Visibility = Visibility.Visible;
        TxtAmount.IsReadOnly = true;
        TxtName.IsReadOnly = true;
        TxtPrice.IsReadOnly = true;
    }


    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            products.Remove(products.ToList().Find(po => productID == po.ID));
            currentProduct.ID = bl.product.AddProduct(cast(currentProduct));
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
            int idx = products.ToList().FindIndex(po => productID == po.ID);
            products.RemoveAt(idx);
            products.Insert(idx, currentProduct);
            bl.product.UpdateProduct(cast(currentProduct));
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
            products.Remove(products.ToList().Find(po => productID == po.ID));
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
