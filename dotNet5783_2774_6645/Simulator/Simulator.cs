﻿using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Simulator;
public static class Simulator
{
    static IBl bl = BlApi.Factory.Get;

    static BO.Order order = new();
    static Thread? myThread { get; set; }

  public static bool doWork=true;

    static Stopwatch? myStopWatch { get; set; }

    public class OrderEventArgs : EventArgs
    {
        public BO.Order order;
        public int seconds;
        public OrderEventArgs(int newSecond, BO.Order newOrder)
        {
            order = newOrder;
            seconds = newSecond;
        }
    }

    public static event EventHandler stop;
    public static event EventHandler<OrderEventArgs> propsChanged;

    public static void Run()
    {
        myThread = new Thread(new ThreadStart(Simulation));
        myThread.Start();
    }


    private static void Simulation()
    {
        while (doWork)
        {
            int? orderID = bl.order.SelectOrder();
            if (orderID == null)
            {
                stop("", EventArgs.Empty);
                break;
            }
            Random rnd = new Random();
            int seconds = rnd.Next(1000, 5000);
         
            order = bl.order.GetOrder((int)orderID);
            if (order.Status == BO.OrderStatus.Confirmed)
                bl.order.UpdateShipedOrder(order.ID);
            else
                bl.order.UpdateDeliveryOrder(order.ID);
            propsChanged("", new OrderEventArgs(seconds, order));

            Thread.Sleep(seconds);

        }
    }
}