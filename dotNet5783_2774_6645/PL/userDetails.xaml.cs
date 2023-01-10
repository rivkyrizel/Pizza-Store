﻿using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for userDetails.xaml
    /// </summary>
    public partial class userDetails : Window
    {
        BO.Cart cart;
        IBl bl;
        public userDetails(IBl Bl,BO.Cart Cart)
        {
            bl = Bl;
            InitializeComponent();
            cart = Cart;
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
           
            cart.CustomerName = NameTxt.Text;
            cart.CustomerEmail = EmailTxt.Text;
            cart.CustomerAddress = AddressTxt.Text;
            bl.Cart.confirmOrder(cart);
            Close();
        }
    }
}
