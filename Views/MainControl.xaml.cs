using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitAssist.RevitGuide
{
    public partial class MainControl : Window
    {
        
        public MainControl()
        {
            Debug.WriteLine("Main Control called");
            DataContext = new MainControlViewModel();
            InitializeComponent();
            
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }


    }
}

