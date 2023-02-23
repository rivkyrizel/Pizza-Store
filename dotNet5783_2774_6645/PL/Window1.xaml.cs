using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        public Window1()
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(10);

            _timer = new DispatcherTimer(new TimeSpan(0,0 ,1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = string.Format("{0:D2}", _time.Seconds);
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }
    }
}
