﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class User
{
    public int ID { get; set; }
    public int? UserID { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public IEnumerable<Order>? Orders { get; set; }
    public IEnumerable<OrderItem>? cart { get; set; }
}
