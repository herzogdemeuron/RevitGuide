using Microsoft.Web.WebView2.Wpf;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace RevitGuide.ViewModels
{
    public class TabItemViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public Uri Uri { get; set; }
        public WebView2 WebView { get; set; }
        public bool IsLive { get; set; } = false;
        public bool IsSpacer { get; set; } = false;

        private string _folderPath;
        public string FolderPath
        {
            get => Path.Combine(_folderPath, Title);
            set
            {
                _folderPath = value;
                OnPropertyChanged();
            }
        }
        public void InitWbv2()
        {
            WebView2Model webView2Model = new WebView2Model(this);
            WebView = webView2Model.WebView2;
        }

        public Uri GetCurrentUri()
        {
            return WebView.Source??null;
        }

        public void GoHome()
        {
            WebView.Source = Uri;
        }

        public void GoBack()
        {
            if (WebView.CanGoBack)
            {
                WebView.GoBack();
            }
        }

        public void Dispose()
        {
            WebView.Dispose();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
