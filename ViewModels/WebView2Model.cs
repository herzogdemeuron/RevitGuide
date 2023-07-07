using Autodesk.Revit.UI;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;



namespace RevitGuide.ViewModels
{
    public class WebView2Model : INotifyPropertyChanged
    {
        public TabItemViewModel TabItemModel { get; set; }
        public WebView2 WebView2 { get; set; }
        private CommandManager CommandManager { get; set; } = new CommandManager();

        public WebView2Model(TabItemViewModel tabItemModel)
        {
            TabItemModel = tabItemModel;
            InitializeWebView2Async();
        } 

        public async void InitializeWebView2Async()
        {
            string userDataFolderPath = @"C:\CustomUserDataFolder\" + TabItemModel.Title;

            WebView2 = new WebView2()
            {
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
            WebView2.CoreWebView2.NewWindowRequested += NewWindowRequested;

            if (TabItemModel.Title == "Live Guide")
            {
                CommandBinder.WebView = WebView2;
            }

        }

        private void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {

        }

        void NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            string uri = e.Uri;
            //TaskDialog.Show("Navigation Starting", uri);
            if (uri.StartsWith("http://revitcommand"))
            {
                e.Handled = true;
                CommandManager.Execute(uri);
            }
            
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}