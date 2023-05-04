using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
        public WebView2 WebView2 { get; set; }

        public WebView2Model(TabItemViewModel tabItemModel)
        {
            Debug.WriteLine("creating webv2model instance");
            TabItem = tabItemModel;
            InitializeWebView2Async();
        }

        public async void InitializeWebView2Async()
        {
            Debug.WriteLine("initing webv2 in the model");
            string userDataFolderPath = @"C:\CustomUserDataFolder\" + TabItem.Title;

            WebView2 = new WebView2()
            {
                CreationProperties = new CoreWebView2CreationProperties()
                {
                    UserDataFolder = userDataFolderPath
                }
            };
            Debug.WriteLine(" webv2 in the model created");
            WebView2.CoreWebView2InitializationCompleted += WebView2_CoreWebView2InitializationCompleted;

            // Ensure WebView2 is initialized asynchronously
            await WebView2.EnsureCoreWebView2Async(null);
        }

        private void WebView2_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            Debug.WriteLine("CoreWebView2InitializationCompleted triggered");
            if (!e.IsSuccess)
            {
                Debug.WriteLine($"Error initializing WebView2: {e.InitializationException}");
            }
            else
            {
                WebView2.Source = TabItem.Url;
                WebView2.ZoomFactor = 0.5f;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}