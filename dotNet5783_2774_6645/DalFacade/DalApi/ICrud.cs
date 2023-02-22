using System.Runtime.CompilerServices;

namespace DalApi;

public interface ICrud<T>
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(T t);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(T t);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<T>? GetList(Func<T?,bool>? func=null);

    [MethodImpl(MethodImplOptions.Synchronized)]
    public T? Get(Func<T?, bool> func);

}

