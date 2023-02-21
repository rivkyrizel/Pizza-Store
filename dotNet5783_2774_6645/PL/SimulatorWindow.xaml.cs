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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        public event PropertyChangedEventHandler? PropertyChanged;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        void ToolWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Code to remove close box from window
            var hwnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        Stopwatch stopWatch;
        bool isTimerRun;
        bool CancellationPending = true;
        BackgroundWorker timerWorker;
        BackgroundWorker backgroundWorker;


        public SimulatorWindow()
        {
            //InitializeComponent();
            Loaded += ToolWindow_Loaded;
            stopWatch = new Stopwatch();
            timerWorker = new BackgroundWorker();

            timerWorker.DoWork += timerWorker_DoWork;
            timerWorker.ProgressChanged += timerWorker_ProgressChanged;
            timerWorker.WorkerReportsProgress = true;
            stopWatch.Restart();
            isTimerRun = true;
            timerWorker.RunWorkerAsync();
        }

        private void timerWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
           // this.timerTextBlock.Text = timerText;
        }
        private void timerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (CancellationPending)
            {
                backgroundWorker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
    }
}

