using DO;
using DalApi;

namespace Dal;

internal class DalProduct:IProduct
{

    /// <summary>
    /// create product
    /// </summary>
    /// <param name="product">the new product</param>
    /// <returns>id of the product</returns>
    public int Add(Product p)
    {
        p.ID = DataSource.Config.ProductID;
        DataSource.ProductList.Add(p);
        return p.ID;
    }

    /// <summary>
    ///  Deletes product by given id
    /// </summary>
    /// <param name="id"> Id of product to delete </param>
    /// <exception cref="Exception"> No product found with the given id </exception>
    public void Delete(int id)
    {
        foreach(Product item in DataSource.ProductList)
        {
            if (id == item.ID)
            {
                DataSource.ProductList.Remove(item);
                return;
            }

        }
        throw new ItemNotFound("error can't delete product");
    }

    /// <summary>
    /// Updates an product
    /// </summary>
    /// <param name="updateProduct"> The updated product </param>
    /// <exception cref="Exception"> No order with the given id found </exception>

    public void Update(Product p)
    {
        for (int i = 0; i < DataSource.ProductList.Count; i++)
        {
            if (p.ID == DataSource.ProductList[i].ID)
            {
                DataSource.ProductList[i] = p;
                return;
            }
        }
        throw new ItemNotFound("could not update product");
    }

    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns> all products in system </returns>
    public IEnumerable<Product> GetList()
    {
        return DataSource.ProductList;
    }

    /// <summary>
    /// functiod receives id of product and returns the products details
    /// </summary>
    /// <param name="id">id of specific product</param>
    /// <returns>  product details of given id</returns>
    /// <exception cref="ItemNotFound">no product with requested id found</exception>
    public Product Get(int id)
    {
        foreach (Product item in DataSource.ProductList)
        {
            if (id == item.ID)
                return item;
        }
        throw new ItemNotFound("error product not found");
    }
}

