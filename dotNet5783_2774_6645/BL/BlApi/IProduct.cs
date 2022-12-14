using BO;

namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList?> GetProductList(BO.eCategory? e=null);
    public IEnumerable<ProductItem?> GetProductItem();
    public Product GetProductForCustomer(int id);
    public Product GetProductForManager(int id);
    public void AddProduct(Product p);
    public void DeleteProduct(int id);
    public void UpdateProduct(Product p);
    public IEnumerable<ProductItem?> GetListProductByCategory(eCategory e);

}

