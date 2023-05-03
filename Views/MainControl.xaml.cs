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


namespace RevitAssist
{
    public partial class MainControl : Window
    {
        private readonly WebView2Model _webView2Model = new WebView2Model();
        public MainControl()
        {
            InitializeComponent();
            DataContext = new MainControlViewModel();
        }

        private async void WebView2Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("WebView2Loaded called");
            WebView2 webView2 = sender as WebView2;
            TabItemViewModel viewModel = webView2.Tag as TabItemViewModel;
            Debug.WriteLine(viewModel.Title);
            await _webView2Model.InitializeWebView2Async(webView2, viewModel);
        }
    }
}

