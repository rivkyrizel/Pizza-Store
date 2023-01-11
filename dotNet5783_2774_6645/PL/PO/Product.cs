using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace PL.PO
{
    internal class Product : DependencyObject
    {
        public void update(BO.Product p)
        {
            Name = p.Name;
            Price = p.Price;
            InStock = p.InStock;
            Category = p.Category;
        }
        public Product(BO.Product p, BlApi.IBl bl)
        {
            update(p);
            bl.product.updtedObjectAction += update;
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
            set { SetValue(PriceProperty, value); }
        }
        public BO.eCategory? Category
        {
            get { return (BO.eCategory)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }



        public static readonly DependencyProperty NameProperty  = DependencyProperty.Register("Name", typeof(object), typeof(Product), new UIPropertyMetadata(0));
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("Price", typeof(object), typeof(Product), new UIPropertyMetadata(0));
        public static readonly DependencyProperty InStockProperty = DependencyProperty.Register("InStock", typeof(object), typeof(Product), new UIPropertyMetadata(0));
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("Category", typeof(object), typeof(Product), new UIPropertyMetadata(0));
    }
}




