using BO;

namespace BlApi;
public interface IProduct
{
    public IEnumerable<ProductForList> GetProductList();
    public IEnumerable<ProductItem> GetProductItem();
    public Product GetProductForCustomer(int id);
    public Product GetProductForMenager(int id);
    public void AddProduct(Product p);
    public void DeleteProduct(int id);
    public void UpdateProduct(Product p);

}

