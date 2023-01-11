using BlApi;
using System.Reflection;

namespace BlImplementation;

internal class BlProduct : IProduct
{

    public Action<BO.Product> updtedObjectAction { get; set; }  
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new BlNullValueException();


    
    private DO.Product castBOToDO(BO.Product pBO)
    {
       
        DO.Product pDO = BlUtils.cast<DO.Product, BO.Product>(pBO);
        pDO.Category = (DO.eCategory?)pBO.Category;
        pDO.Amount = (int)pBO.InStock;
        return pDO;
    }
    private void func(BO.Product b)
    {
        if (1 == 1) ;
    }

    private S castProduct<S, T>(T t) where S : new()
    {
        S s = BlUtils.cast<S, T>(t);
        var value = t?.GetType().GetProperty("Category")?.GetValue(t, null) ?? throw new BlNullValueException();
        s?.GetType().GetProperty("Category")?.SetValue(s, (BO.eCategory?)(int)value);

        switch (s?.GetType().Name)
        {
            case "Product":
                var val1 = t?.GetType()?.GetProperty("Amount")?.GetValue(t, null);
                s?.GetType().GetProperty("InStock")?.SetValue(s, val1);
                break;
            case "ProductItem":
                var val2 = t?.GetType()?.GetProperty("Amount")?.GetValue(t, null) ?? throw new BlNullValueException();
                s?.GetType().GetProperty("InStock")?.SetValue(s, ((int)val2 > 0) ? true : false);
                break;
            default:
                break;
        }
        return s;
    }


    /// <summary>
    /// Adds new product
    /// </summary>
    /// <param name="p"> new product </param>
    public void AddProduct(BO.Product p)
    {
        if (p.Name != "" && p.Price > 0 && p.InStock > 0 && p.Category != null)
            dal.Product.Add(castBOToDO(p));
        else
            throw new BlNullValueException();
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id"> id of product to delete </param>
    /// <exception cref="BlProductFoundInOrders"> the product exists in users orders </exception>
    /// <exception cref="BlIdNotFound">  no product with id found </exception>
    public void DeleteProduct(int id)
    {
        try
        {
            IEnumerable<DO.OrderItem> orderItems = dal.OrderItem.GetList() ?? throw new BlNullValueException();

            if (orderItems.ToList().Exists(i => i.ProductID == id)) throw new BlProductFoundInOrders();
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
                return castProduct<BO.Product, DO.Product>(dal.Product.Get(p => p.ID == id));

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
    public IEnumerable<BO.ProductForList> GetProductList(BO.eCategory? e = null)
    {
        IEnumerable<DO.Product> DOlist;
        if (e != null) DOlist = dal.Product.GetList(p => (int)(object)p.Category == (int)(object)e) ?? throw new BlNullValueException();
        else DOlist = dal.Product.GetList() ?? throw new BlNullValueException();

        IEnumerable<BO.ProductForList> BOlist = from item in DOlist
                                                select castProduct<BO.ProductForList, DO.Product>(item);
        return BOlist;
    }

    /// <summary>
    /// gets list of product for user
    /// </summary>
    /// <returns> list of product, type:productitem</returns>
    public IEnumerable<BO.ProductItem> GetProductItem()
    {
        IEnumerable<DO.Product> DOlist = dal.Product.GetList() ?? throw new BlNullValueException();
        IEnumerable<BO.ProductItem> BOlist = from item in DOlist
                                             select castProduct<BO.ProductItem, DO.Product>(item);
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
                updtedObjectAction += func;
                updtedObjectAction?.Invoke(p);
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


