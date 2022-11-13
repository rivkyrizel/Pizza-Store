using DO;

namespace Dal;

public  class DalProduct
{
    /// <summary>
    /// create product
    /// </summary>
    /// <param name="product">the new product</param>
    /// <returns>id of the product</returns>
    public static int CreateProduct(Product product)
    {
        product.ID = DataSource.Config.ProductID;
        DataSource.ProductList[DataSource.Config.productIdx++] = product;
        return DataSource.Config.productIdx;
    }

    /// <summary>
    /// functiod receives id of product and returns the products details
    /// </summary>
    /// <param name="id">id of specific product</param>
    /// <returns>  product details of given id</returns>
    /// <exception cref="Exception">no product with requested id found</exception>
    public static Product ReadProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (id == DataSource.ProductList[i].ID)
                return DataSource.ProductList[i];

        }
        throw new Exception("error product not found");
    }

    /// <summary>
    /// returns all products
    /// </summary>
    /// <returns> all products in system </returns>
    public static Product[] ReadProductList()
    {
        Product[] productList = new Product[DataSource.Config.productIdx];
        for (int i = 0; i < productList.Length; i++)
            productList[i] = DataSource.ProductList[i];

        return productList;
    }

    /// <summary>
    /// Updates an product
    /// </summary>
    /// <param name="updateProduct"> The updated product </param>
    /// <exception cref="Exception"> No order with the given id found </exception>
    public static void UpdateProduct(Product updateProduct)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (updateProduct.ID == DataSource.ProductList[i].ID)
            {
                DataSource.ProductList[i] = updateProduct;
                return;
            }

        }
        throw new Exception("could not update product");
    }

    /// <summary>
    ///  Deletes product by given id
    /// </summary>
    /// <param name="id"> Id of product to delete </param>
    /// <exception cref="Exception"> No product found with the given id </exception>
    public static void DeleteProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (id == DataSource.ProductList[i].ID)
            {
                for (int j = i; j < DataSource.Config.productIdx; j++)
                {
                    DataSource.ProductList[j] = DataSource.ProductList[j + 1];
                }
                DataSource.Config.productIdx--;
                return;
            }

        }
        throw new Exception("error can't delete product");
    }
}

