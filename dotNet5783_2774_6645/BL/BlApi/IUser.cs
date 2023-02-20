﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi;

public interface IUser
{
    public int AddUser(User u);
    public void UpdateUser(User u);
    public bool IsRegistered(string email, string pass);

}