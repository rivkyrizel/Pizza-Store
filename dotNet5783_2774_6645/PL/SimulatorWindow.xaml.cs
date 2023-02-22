using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Simulator.Simulator;

namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    IBl bl;
    BackgroundWorker worker;

    #region the closing button
    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;

    public event PropertyChangedEventHandler? PropertyChanged;

    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    void ToolWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
    }
    #endregion

    bool isTimerRun;
    Duration duration;
    DoubleAnimation doubleanimation;
    ProgressBar ProgressBar;
    private int seconds;

    public SimulatorWindow(IBl Bl)
    {
        InitializeComponent();
        bl = Bl;
        Loaded += ToolWindow_Loaded;
        workerStart();
        ProgressBarStart();
    }
    void ProgressBarStart()
    {
        ProgressBar = new ProgressBar();
        ProgressBar.IsIndeterminate = false;
        ProgressBar.Orientation = Orientation.Horizontal;
        ProgressBar.Width = 500;
        ProgressBar.Height = 30;
        duration = new Duration(TimeSpan.FromSeconds(2));
        doubleanimation = new DoubleAnimation(200.0, duration);
        ProgressBar.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
        SBar.Items.Add(ProgressBar);
    }

    void workerStart()
    {
        isTimerRun = true;
        worker = new BackgroundWorker();
        worker.DoWork += WorkerDoWork;
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;
        worker.ProgressChanged += workerProgressChanged;
        worker.RunWorkerCompleted += RunWorkerCompleted;
        worker.RunWorkerAsync();
    }

    void WorkerDoWork(object sender, DoWorkEventArgs e)
    {
        propsChanged += progressChanged;
        Simulator.Simulator.stop += stop;
        Run();
        while (!worker.CancellationPending&&isTimerRun)
        {
            worker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }


    void workerProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        simulatorTxt.Text = DateTime.Now.ToString("h:mm:ss");
    }

    private void stopSimulatorBtn_Click(object sender, RoutedEventArgs e)
    {
        if (worker.WorkerSupportsCancellation == true)
            worker.CancelAsync();
        if (isTimerRun)
        {
            isTimerRun = false;
        }
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(stopSimulatorBtn_Click, sender, e);
        }
        else
        {
            this.Close();
        }

    }

    void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        this.Close();
    }

    void stop(object sender, EventArgs e)
    {
        stopSimulatorBtn_Click(sender,new RoutedEventArgs());
        doWork = false;
        propsChanged -= progressChanged;
        Simulator.Simulator.stop -= stop;
    }


    void progressChanged(object sender, EventArgs e)
    {
      OrderEventArgs orderEventArgs = (OrderEventArgs)e;
        seconds = orderEventArgs.seconds;
        if (!CheckAccess())
        {
            Dispatcher.BeginInvoke(progressChanged, sender, e);
        }
        else
        {
            orderIDTxt.Text= orderEventArgs.order.ID.ToString();
            timeTxt.Text=orderEventArgs.seconds.ToString();
            statusBtn.Text=orderEventArgs.order.Status.ToString();
        }
    }
}
