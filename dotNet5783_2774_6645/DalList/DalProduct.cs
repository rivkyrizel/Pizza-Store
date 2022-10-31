using DalFacade.DO;

namespace DalList;

public static class DalProduct
{
    public static int createProduct(Product product)
    {
        product.ID = DataSource.Config.ProductID;
        DataSource.ProductList[DataSource.Config.productIdx++] = product;
        return DataSource.Config.productIdx;
    }

    public static Product readProduct(int id)
    {
        for (int i = 0; i < DataSource.Config.productIdx; i++)
        {
            if (id == DataSource.ProductList[i].ID)
                return DataSource.ProductList[i];

        }
        throw new Exception("error product not found");
    }

    public static Product[] readProductList()
    {
        Product[] productList = new Product[DataSource.Config.productIdx];
        for (int i = 0; i < productList.Length; i++)
            productList[i] = DataSource.ProductList[i];

        return productList;
    }

    public static void updateProduct(Product updateProduct)
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

    public static void deleteProduct(int id)
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

