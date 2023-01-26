using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PL;

internal class PLUtils
{
    internal static S cast<S, T>(T t) where S : new()
    {
        object s = new S();
        foreach (PropertyInfo prop in t?.GetType().GetProperties() ?? throw new BlNoPropertiesInObject())
        {
            PropertyInfo? type = s?.GetType().GetProperty(prop.Name);
            if (type == null || type.Name == "Category")
                continue;
            if (type.Name == "Items") {
                List<BO.OrderItem> list= new List<BO.OrderItem>();
                var val = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null)??throw new PlNullObjectException();
                foreach (var item in (IEnumerable<PO.OrderItem>)val)
                {
                    list.Add(cast<BO.OrderItem, PO.OrderItem>(item));
                }
                type.SetValue(s, list);
                continue;
            }            
            var value = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null);
            type.SetValue(s, value);
        }
        return (S)s;
    }
}
