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



namespace RevitAssist.RevitGuide
{
    public class WebView2Model : INotifyPropertyChanged
    {
        public TabItemViewModel TabItemModel { get; set; }
        public WebView2 WebView2 { get; set; }

        public WebView2Model(TabItemViewModel tabItemModel)
        {
            Debug.WriteLine("creating webv2model instance");
            TabItemModel = tabItemModel;
            InitializeWebView2Async();
        } 

        public async void InitializeWebView2Async()
        {
            Debug.WriteLine("initing webv2 in the model");
            string userDataFolderPath = @"C:\CustomUserDataFolder\" + TabItemModel.Title;

            WebView2 = new WebView2()
            {
                //Tag = this,
                CreationProperties = new CoreWebView2CreationProperties()
                {
                    UserDataFolder = userDataFolderPath
                }
            };
            Debug.WriteLine(" webv2 in the model created");
            var environment = await CoreWebView2Environment.CreateAsync(null, userDataFolderPath, new CoreWebView2EnvironmentOptions("--kiosk-printing"));
            await WebView2.EnsureCoreWebView2Async(environment);
            Debug.WriteLine(" webv2 init ensured");
            WebView2.Source = TabItemModel.Url;
            WebView2.ZoomFactor = 0.7f;

            if (TabItemModel.Title == "Live Guide")
            {
                CommandBinder.WebView = WebView2;
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}