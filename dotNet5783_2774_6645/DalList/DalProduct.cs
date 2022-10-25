using DalFacade.DO;

namespace DalList;

public class DalProduct
{
    public int createProduct(Product product)
    {
        DataSource.ProductList[DataSource.Config.productIdx++] = product;
        return DataSource.Config.productIdx;
    }

    public Product readProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (id == DataSource.ProductList[i].ID)
                return DataSource.ProductList[i];

        }
        throw new Exception("error");
    }

    public Product[] readProducts()
    {
        return DataSource.ProductList;
    }

    public void updateProduct(Product updateProduct)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (updateProduct.ID == DataSource.ProductList[i].ID)
            {
                DataSource.ProductList[i] = updateProduct;
                return;
            }

        }
        throw new Exception("error");
    }

    public void deleteProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (id == DataSource.ProductList[i].ID)
            {
                DataSource.ProductList[i] = DataSource.ProductList[DataSource.Config.productIdx--];
                return;
            }

        }
        throw new Exception("error");
    }
}

