﻿using BlApi;
using BlImplementation;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForList.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        IBl bl;
        public OrderListWindow(IBl Bl)
        {
            bl = Bl;
            InitializeComponent();
            OrderListView.ItemsSource = bl.order.OrderList();
        }
          
        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OrderListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new OrderWindow(bl, ((BO.OrderForList?)OrderListView.SelectedItems[0])?.ID ?? throw new PlNullObjectException()).Show();
        }
    }
}