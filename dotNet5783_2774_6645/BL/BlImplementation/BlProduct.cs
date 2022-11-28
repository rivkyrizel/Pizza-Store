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

    public void AddProduct(BO.Product p)
    {
        if (p.ID > 0 && p.Name != "" && p.Price > 0 && p.InStock > 0)
            dal.Product.Add(castBOToDO(p));
    }

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

    public BO.Product GetProductForManager(int id)
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
        IEnumerable<DO.Product> DOlist = dal.Product.GetList();
        List<BO.ProductForList> BOlist = new();
        foreach (DO.Product item in DOlist)
        {
            BOlist.Add(castDOtoBOpForList(item));
        }
        return BOlist;
    }

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
}

