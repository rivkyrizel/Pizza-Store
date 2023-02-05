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
using System.Collections.ObjectModel;
namespace PL.Products;

/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    IBl bl;
    int productID;
    PO.Cart cart;
    PO.Product currentProduct;
    public bool isShow { get; set; } = false;
    private ObservableCollection<PO.Product> products { get; set; }



    private void initializeDataContext()
    {
        GridData.DataContext = currentProduct;
    }

    public ProductWindow(IBl Bl, string active, ObservableCollection<PO.Product> Products, ref PO.Cart Cart, int id = 0)
    {
        InitializeComponent();
        gridBtn.DataContext = new { act = active };

        bl = Bl;
        cart = Cart;
        products = Products;
        SelectCategory.ItemsSource = Enum.GetValues(typeof(BO.eCategory));
        productID = id;
        currentProduct = id == 0 ? new() : new(bl.product.GetProductForManager(productID));
        initializeDataContext();

        if (active == "show") isShow = true;
    }



    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            products.Remove(products.ToList().Find(po => productID == po.ID));
            currentProduct.ID = bl.product.AddProduct(PLUtils.cast<BO.Product, PO.Product>(currentProduct));
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
            bl.product.UpdateProduct(PLUtils.cast<BO.Product, PO.Product>(currentProduct));
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
        BO.Cart b = PLUtils.cast<BO.Cart, PO.Cart>(cart);
        PO.Cart newCart = PLUtils.cast<PO.Cart, BO.Cart>(bl.Cart.AddToCart(b, productID));
        cart.Items = newCart.Items;
        cart.TotalPrice= newCart.TotalPrice;
        Close();
    }

}



