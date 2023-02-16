using BlApi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PL;

public class PLUtils
{
    public static S cast<S, T>(T t) where S : new()
    {
        object s = new S();
        foreach (PropertyInfo prop in t?.GetType().GetProperties() ?? throw new BlNoPropertiesInObject())
        {
            PropertyInfo type = s.GetType().GetProperty(prop.Name);
                if (type == null) 
                continue;
            var value = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null);
       /*     if (type.Name == "Category") {
                if (type.ReflectedType.FullName.StartsWith("BO"))
                    type.SetValue(s, (BO.eCategory?)value);
                else  type.SetValue(s, (DO.eCategory?)value);
                continue;
            }*/

            if (type.Name == "Items") {

                if (type.ReflectedType.FullName.StartsWith("BO"))
                    type.SetValue(s, castPOItemsToBOItems(value)); 
                   else  type.SetValue(s, castBOItemsToPOItems(value));
           
                continue;
            }            
            type.SetValue(s, value);
        }
        return (S)s;
    }

    public static ObservableCollection<PO.OrderItem> castBOItemsToPOItems(object l)
    {
        List<PO.OrderItem> list = new();
        foreach (var item in (IEnumerable<BO.OrderItem>)l)
        {
            list.Add(cast<PO.OrderItem, BO.OrderItem>(item));
        }
        return new ObservableCollection<PO.OrderItem>(list);
    }

    public static List<BO.OrderItem> castPOItemsToBOItems(object l)
    {
        List<BO.OrderItem> list = new();
        foreach (var item in (ObservableCollection<PO.OrderItem>)l)
        {
            list.Add(cast<BO.OrderItem, PO.OrderItem>(item));
        }
        return list;
    }


}
