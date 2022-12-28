using System.Reflection;

namespace BlImplementation;

internal static class BlUtils
{
    internal static S castDoToBo<S, T>(T t) where S : new()

    {
       // S s = new();
        object s = new S();
        foreach (PropertyInfo prop in t.GetType().GetProperties())
        {
            PropertyInfo type = s.GetType().GetProperty(prop.Name);
            if (type == null || type.Name == "Category" ) continue;
            
            var value = t.GetType().GetProperty(prop.Name).GetValue(t, null);
            type.SetValue(s, value);

        }
        return (S)s;
    }
}

