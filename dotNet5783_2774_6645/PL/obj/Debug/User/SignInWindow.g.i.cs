// Updated by XamlIntelliSenseFileGenerator 21/02/2023 15:16:10
#pragma checksum "..\..\..\User\SignInWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53F2F6949B1AB431E574A5AB2CD9C0015B5AABC5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL.User;
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


namespace PL.User
{


    /// <summary>
    /// SignInWindow
    /// </summary>
    public partial class SignInWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 10 "..\..\..\User\SignInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SignUpBtn;

#line default
#line hidden


#line 11 "..\..\..\User\SignInWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginBtn;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;V1.0.0.0;component/user/signinwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\User\SignInWindow.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.SignUpBtn = ((System.Windows.Controls.Button)(target));

#line 10 "..\..\..\User\SignInWindow.xaml"
                    this.SignUpBtn.Click += new System.Windows.RoutedEventHandler(this.SignUpBtn_Click);

#line default
#line hidden
                    return;
                case 2:
                    this.LoginBtn = ((System.Windows.Controls.Button)(target));

#line 11 "..\..\..\User\SignInWindow.xaml"
                    this.LoginBtn.Click += new System.Windows.RoutedEventHandler(this.LoginBtn_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.Sign = ((System.Windows.Controls.Button)(target));

#line 22 "..\..\..\User\SignInWindow.xaml"
                    this.Sign.Click += new System.Windows.RoutedEventHandler(this.Sign_Click);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button signUpBtn;
        internal System.Windows.Controls.Button loginBtn;
    }
}

