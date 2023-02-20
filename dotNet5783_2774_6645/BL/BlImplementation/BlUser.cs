﻿using System;
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
    
        return dal.User.Add(BlUtils.cast<DO.User, BO.User>(u));
    }

    public bool IsRegistered(string email, string pass)
    {
        try
        {
            return dal.User.Get(u => u.Email == email).Password == pass;
        }
        catch (ItemNotFound e)
        {
            throw new BlNullValueException();
        }
    }

    public void UpdateUser(User u)
    {
        dal.User.Update(BlUtils.cast<DO.User, BO.User>(u));
    }
}