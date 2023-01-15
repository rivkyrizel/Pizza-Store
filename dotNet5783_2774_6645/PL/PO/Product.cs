using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BO;

namespace PL.PO
{
    public class Product : DependencyObject
    {

        public int ID
        {
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }


        public string? Name {
            get { return (string)GetValue(NameProperty); } 
            set { SetValue(NameProperty, value); }        
        }
        public double Price
        {
            get { return (int)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public int InStock
        {
            get { return (int)GetValue(InStockProperty); }
            set { SetValue(InStockProperty, value); }
        }
        public BO.eCategory? Category
        {
            get { return (BO.eCategory)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }


        public static readonly DependencyProperty IDProperty  = DependencyProperty.Register("ID", typeof(int), typeof(Product));
        public static readonly DependencyProperty NameProperty  = DependencyProperty.Register("Name", typeof(string), typeof(Product), new UIPropertyMetadata(""));
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(object), typeof(Product), new UIPropertyMetadata(0));
        public static readonly DependencyProperty InStockProperty = DependencyProperty.Register("InStock", typeof(object), typeof(Product), new UIPropertyMetadata(0));
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(object), typeof(Product), new UIPropertyMetadata(null));
    }
}




