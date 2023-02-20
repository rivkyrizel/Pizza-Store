using DalApi;
using DO;
using System.Xml.Serialization;

namespace Dal;

public class User : IUser
{
    static string userSrc = @"..\..\xml\User.xml";
    public XmlRootAttribute xRoot()
    {
        XmlRootAttribute xRoot = new XmlRootAttribute();
        xRoot.ElementName = "ArrayOfUser";
        xRoot.IsNullable = true;
        return xRoot;
    }

    public int Add(DO.User user)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.User>), xRoot());
        StreamReader r = new(userSrc);
        List<DO.User>? lst = (List<DO.User>?)ser.Deserialize(r);
        user.ID = lst?.Last().ID + 1 ?? throw new XMLFileNullExeption();
        lst?.Add(user);
        r.Close();
        StreamWriter w = new(userSrc);
        ser.Serialize(w, lst);
        w.Close();
        return user.ID;
    }

    public void Delete(int id)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.User>), xRoot());
        StreamReader r = new(userSrc);
        List<DO.User>? lst = (List<DO.User>?)ser.Deserialize(r);
        lst?.Remove(lst.Where(p => p.ID == id).FirstOrDefault());
        r.Close();
        StreamWriter w = new(userSrc);
        ser.Serialize(w, lst);
        w.Close();
    }

    public DO.User Get(Func<DO.User, bool> func)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.User>), xRoot());
        StreamReader r = new(userSrc);
        List<DO.User>? lst = (List<DO.User>?)ser.Deserialize(r);
        r.Close();
        return lst?.Where(func) != null ? lst.Where(func).First() : throw new ItemNotFound("");
    }

    public IEnumerable<DO.User>? GetList(Func<DO.User, bool>? func = null)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.User>), xRoot());
        StreamReader r = new(userSrc);
        List<DO.User>? lst = (List<DO.User>?)ser.Deserialize(r);
        r.Close();
        return (func == null ? lst : lst?.Where(func));
    }

    public void Update(DO.User order)
    {
        XmlSerializer ser = new XmlSerializer(typeof(List<DO.User>), xRoot());
        StreamReader readFile = new(userSrc);
        List<DO.User>? lst = (List<DO.User>?)ser.Deserialize(readFile) ?? throw new XMLFileNullExeption();
        int idx = lst.FindIndex(pr => pr.ID == order.ID);
        if (idx >= 0) lst[idx] = order;
        else
            throw new ItemNotFound("could not update product");
        readFile.Close();
        StreamWriter writeFile = new(userSrc);
        ser.Serialize(writeFile, lst);
        writeFile.Close();
    }
}
