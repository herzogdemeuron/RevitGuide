using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace RevitAssist
{
    public class WebView2Model : INotifyPropertyChanged
    {
      
        public TabItemViewModel TabItem { get; set; }

        public async Task InitializeWebView2Async(WebView2 webView2, TabItemViewModel viewModel)
        {
            TabItem = viewModel;
            TabItem.WebView = webView2;
            
            string userDataFolderPath = @"C:\CustomUserDataFolder\" + TabItem.Title;
            CoreWebView2EnvironmentOptions options = new CoreWebView2EnvironmentOptions(userDataFolderPath);
            CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(null, userDataFolderPath, options);
            await webView2.EnsureCoreWebView2Async(environment);
            webView2.ZoomFactor = 0.5f;
            if(viewModel.Title == "Live")
            {
              CommandBinder.WebView = webView2;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}