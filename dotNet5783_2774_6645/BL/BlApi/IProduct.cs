using BO;
using System.Runtime.CompilerServices;

namespace BlApi;
public interface IProduct
{
    // public Action<BO.Product> updtedObjectAction { get; set; }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<ProductForList?> GetProductList(BO.eCategory? e=null);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<ProductItem?> GetProductItem(BO.eCategory? e = null);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product GetProductForCustomer(int id);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product GetProductForManager(int id);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public int AddProduct(Product p);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DeleteProduct(int id);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void UpdateProduct(Product p);

}

