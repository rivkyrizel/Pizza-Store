using System.Reflection;

namespace BlImplementation;

internal static class BlUtils
{
    internal static S cast<S, T>(T t) where S : new()
    {
        object s = new S();
        foreach (PropertyInfo prop in t?.GetType().GetProperties()??throw new Exception("the object doesn't have properties"))
        {
            PropertyInfo type = s?.GetType().GetProperty(prop.Name)?? throw new Exception("the object doesn't have properties");
            if (type == null || type.Name == "Category" )
                continue;
            var value = t?.GetType()?.GetProperty(prop.Name)?.GetValue(t, null) ??throw new Exception("the object doesn't have properties");
            type.SetValue(s, value);
        }
        return (S)s;
    }
}

