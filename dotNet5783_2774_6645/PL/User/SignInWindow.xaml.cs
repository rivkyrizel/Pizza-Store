using BlApi;
using PL.Products;
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

namespace PL.User
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public class Login : DependencyObject
        {
            public bool isLogin {
                get { return (bool)GetValue(loginProperty); }
                set { SetValue(loginProperty, value); }
            }

            public static readonly DependencyProperty loginProperty = DependencyProperty.Register("isLogin", typeof(bool), typeof(Login), new UIPropertyMetadata(false));
        }
        public Login l { get; set; } = new();
        public PO.User user { get; set; } = new();

        IBl bl;
        public SignInWindow(IBl b)
        {
            l.isLogin = true;
            bl = b;
            InitializeComponent();
            DataContext = this;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            l.isLogin = true;

        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            l.isLogin = false;
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (l.isLogin)
                {
                    if (!bl.user.IsRegistered(user.Email, user.Password))
                    {
                        MessageBox.Show(" your password is incorrect");
                        return;
                    }
                }
                else
                    bl.user.AddUser(PLUtils.cast<BO.User, PO.User>(user));

                new ProductItemWindow(bl, new PO.Cart()).Show();
                Close();
            }
            catch(BlNullValueException ex)
            {
                MessageBox.Show("your email is incorrect");
            }
        }
    }
}
