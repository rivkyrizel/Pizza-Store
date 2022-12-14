using BlApi;
using BO;

namespace BlImplementation;

internal class BlProduct : IProduct

{
    private DalApi.IDal dal = new Dal.DalList();

    private BO.Product castDOToBO(DO.Product pDO)
    {
        BO.Product pBO = new BO.Product();
        pBO.ID = pDO.ID;
        pBO.Name = pDO.Name;
        pBO.Price = pDO.Price;
        pBO.Category = (BO.eCategory)pDO.Category;
        pBO.InStock = pDO.InStock;
        return pBO;
    }

    private DO.Product castBOToDO(BO.Product pBO)
    {
        DO.Product pDO = new DO.Product();
        pDO.ID = pBO.ID;
        pDO.Name = pBO.Name;
        pDO.Price = (double)pBO.Price;
        pDO.Category = (DO.eCategory)pBO.Category;
        pDO.InStock = (int)pBO.InStock;
        return pDO;
    }

    private BO.ProductForList castDOtoBOpForList(DO.Product pDO)
    {
        BO.ProductForList pBO = new();
        pBO.ID = pDO.ID;
        pBO.Name = pDO.Name;
        pBO.Price = pDO.Price;
        pBO.Category = (BO.eCategory)pDO.Category;
        return pBO;
    }

    private BO.ProductItem castDOtoBOpItem(DO.Product pDO)
    {
        BO.ProductItem pBO = new();
        pBO.ID = pDO.ID;
        pBO.Name = pDO.Name;
        pBO.Price = pDO.Price;
        pBO.Category = (BO.eCategory)pDO.Category;
        pBO.Amount = pDO.InStock;
        pBO.InStock = Convert.ToBoolean(pDO.InStock);
        return pBO;
    }

    /// <summary>
    /// Adds new product
    /// </summary>
    /// <param name="p"> new product </param>
    public void AddProduct(BO.Product p)
    {
        if (p.Name != "" && p.Price > 0 && p.InStock > 0)
            dal.Product.Add(castBOToDO(p));
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id"> id of product to delete </param>
    /// <exception cref="BlProductFoundInOrders"> the product exists in users orders </exception>
    /// <exception cref="BlIdNotFound">  no product with id found </exception>
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetList();

        foreach (DO.OrderItem item in orderItems)
            if (item.ProductID == id)
                throw new BlProductFoundInOrders();
        try
        {
            dal.Product.Delete(id);
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }

    }

    /// <summary>
    /// gets product by id
    /// </summary>
    /// <param name="id"> id of requested product </param>
    /// <returns></returns>
    public BO.Product GetProductForManager(int id)
    {
        return Get(id);
    }

    /// <summary>
    /// gets product by id
    /// </summary>
    /// <param name="id"> id of requested product </param>
    /// <returns></returns>
    public BO.Product GetProductForCustomer(int id)
    {
        return Get(id);
    }
    private BO.Product Get(int id)
    {
        try
        {
            if (id > 0)
                return castDOToBO(dal.Product.Get(p=>p.ID==id));

            throw new BlInvalideData();
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }

    }

    /// <summary>
    /// gets list of products for manager 
    /// </summary>
    /// <returns> list of products </returns>
    public IEnumerable<BO.ProductForList> GetProductList()
    {
        IEnumerable<DO.Product> DOlist = dal.Product.GetList();
        List<BO.ProductForList> BOlist = new();
        foreach (DO.Product item in DOlist)
        {
            BOlist.Add(castDOtoBOpForList(item));
        }
        return BOlist;
    }

    /// <summary>
    /// gets list of product for user
    /// </summary>
    /// <returns> list of product, type:productitem</returns>
    public IEnumerable<BO.ProductItem> GetProductItem()
    {
        IEnumerable<DO.Product> DOlist = dal.Product.GetList();
        List<BO.ProductItem> BOlist = new();
        foreach (DO.Product item in DOlist)
        {
            BOlist.Add(castDOtoBOpItem(item));
        }
        return BOlist;
    }

    /// <summary>
    /// updates product details
    /// </summary>
    /// <param name="p"> product object to update </param>
    /// <exception cref="BlInvalideData"> id is invalid  </exception>
    /// <exception cref="BlNullValueException"> product details missing </exception>
    /// <exception cref="BlIdNotFound"> id of product does not exist </exception>
    public void UpdateProduct(BO.Product p)
    {
        try
        {

            if (p.ID < 0) throw new BlInvalideData();

            if (p.Name != "" && p.Price > 0 && p.InStock > 0)
            {
                dal.Product.Update(castBOToDO(p));
                return;
            }

            throw new BlNullValueException();
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }

    public IEnumerable<ProductItem> GetListProductByCategory(BO.eCategory e)
    {
        IEnumerable<DO.Product> ls = (IEnumerable<DO.Product>)dal.Product.GetList(p=> (BO.eCategory)p.Category == e);
        List<ProductItem> l = new();
        foreach (DO.Product product in ls)
        {
            ProductItem item = castDOtoBOpItem(product);
            l.Add(item);
        }
        return l;
    }
}

