﻿#pragma checksum "..\..\..\Orders\OrderTrackingWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D43ECC0F825D424B73D2CA219C24227541977EC2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL.Orders;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PL.Orders {
    
    
    /// <summary>
    /// OrderTrackingWindow
    /// </summary>
    public partial class OrderTrackingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Orders\OrderTrackingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button searchBtn;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Orders\OrderTrackingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox orderIdBtn;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Orders\OrderTrackingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox orderTrackingTxt;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Orders\OrderTrackingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button orderDetailsBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;V1.0.0.0;component/orders/ordertrackingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Orders\OrderTrackingWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.searchBtn = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\Orders\OrderTrackingWindow.xaml"
            this.searchBtn.Click += new System.Windows.RoutedEventHandler(this.searchBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.orderIdBtn = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.orderTrackingTxt = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.orderDetailsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Orders\OrderTrackingWindow.xaml"
            this.orderDetailsBtn.Click += new System.Windows.RoutedEventHandler(this.orderDetailsBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

