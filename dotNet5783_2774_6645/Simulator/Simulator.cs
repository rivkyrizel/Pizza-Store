using BlApi;
using BlImplementation;
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
    //static BO.Order order;
    static IBl bl = BlApi.Factory.Get;

    static Thread? myThread { get; set; }

    static Stopwatch? myStopWatch { get; set; }

   public static event EventHandler stop;
    public static event EventHandler propsChanged;

    public static void Run()
    {
        myThread = new Thread(new ThreadStart(Simulation));
        myThread.Start();
    }

    public static void StopSimulator()
    {
    }

    private static void Simulation()
    {
        int? orderID = bl.order.SelectOrder();
        if (orderID == null)
        {
            stop(orderID, EventArgs.Empty);//??????????????????
            StopSimulator();
        }
        else
            propsChanged(orderID, EventArgs.Empty);  
    }
}
