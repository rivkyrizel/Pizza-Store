using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;

namespace BlImplementation;

public class BlUser : BlApi.IUser
{
    private DalApi.IDal dal = DalApi.Factory.Get() ?? throw new BlNullValueException();
    public int AddUser(User u)
    {
        // if (IsRegistered(u.Email, u.Password)) throw new BlUserExistsException();

        return dal.User.Add(BlUtils.cast<DO.User, BO.User>(u));
    }

    public int IsRegistered(string email, string pass)
    {
        try
        {
            DO.User user = dal.User.Get(u => u.Email == email);
            if (user.Password == pass) return user.ID;
        }
        catch (ItemNotFound e)
        {
            throw new BlIdNotFound(e);
        }
        return 0;
    }



    public void UpdateUser(User u)
    {
        dal.User.Update(BlUtils.cast<DO.User, BO.User>(u));
    }
}
