using BlApi;

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
        pDO.Price = pBO.Price;
        pDO.Category = (DO.eCategory)pBO.Category;
        pDO.InStock = pBO.InStock;
        return pDO;
    }
    public void AddProduct(BO.Product p)
    {
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
            dal.Product.Add(castBOToDO(p));
    }

    public void DeleteProduct(int id)
    {
        IEnumerable<BO.OrderItem> orderItems = (IEnumerable<BO.OrderItem>)dal.OrderItem.GetList();

        foreach (BO.OrderItem item in orderItems)
            if (item.ProductId == id)
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

    public BO.Product GetProductForMenager(int id)
    {
        return Get(id);
    }

    public BO.Product GetProductForCustomer(int id)
    {
        return Get(id);
    }
    private BO.Product Get(int id)
    {
        try
        {
            if (id > 0)
                return castDOToBO(dal.Product.Get(id));

            throw new BlInvalideData();
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }

    }

    public IEnumerable<BO.ProductForList> GetProductList()
    {
        return (IEnumerable<BO.ProductForList>)dal.Product.GetList();
    }

    public IEnumerable<BO.ProductItem> GetProductItem()
    {
        return (IEnumerable<BO.ProductItem>)dal.Product.GetList();
    }

    public void UpdateProduct(BO.Product p)
    {
        try
        {

            if (p.ID > 0) throw new BlInvalideData();

            if (p.Name != "" && p.Price > 0 && p.InStock > 0)
            {
                dal.Product.Update(castBOToDO(p));
            }

            throw new BlNullValueException();
        }
        catch (DalApi.ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
    }
}

