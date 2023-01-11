using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BO;

namespace PL.PO
{
    internal class Product :DependencyObject
    {
        public void update(BO.Product p)
        {
            Name = p.Name;
            //for test:
            Name = "NewName____";
        }
        public Product(BO.Product p, BlApi.IBl bl)
        {
            Name = p.Name;
            bl.product.updtedObjectAction += update;
        }

     

        public string Name {
            get { return (string)GetValue(NameProperty); } 
            set { SetValue(NameProperty, value); }        
        }


        public static readonly DependencyProperty NameProperty  = DependencyProperty.Register("Name", typeof(object), typeof(Product), new UIPropertyMetadata(0));
    }
}




