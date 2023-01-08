using BlApi;
using System.Reflection;

namespace BlImplementation;

internal static class BlUtils
{
    internal static S cast<S, T>(T t) where S : new()
    {
        object s = new S();
<<<<<<< HEAD
        foreach (PropertyInfo prop in t?.GetType().GetProperties()??throw new BlNoPropertiesInObject())
        {
            PropertyInfo type = s?.GetType().GetProperty(prop.Name)?? throw new BlNoPropertiesInObject();
            if (type == null || type.Name == "Category" )
                continue;
            var value = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null) ??throw new BlNoPropertiesInObject();
=======
        foreach (PropertyInfo prop in t?.GetType().GetProperties()??throw new Exception("the object doesn't have properties"))
        {
            PropertyInfo type = s?.GetType().GetProperty(prop.Name)?? throw new Exception("the object doesn't have properties");
            if (type == null || type.Name == "Category" )
                continue;
            var value = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null) ??throw new Exception("the object doesn't have properties");
>>>>>>> d562df8cb623fd1c9ee18a86f22d3f6618974267
            type.SetValue(s, value);
        }
        return (S)s;
    }
}

