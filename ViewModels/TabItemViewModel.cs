using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Windows.Controls;

namespace RevitAssist.RevitGuide
{
    public class TabItemViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public Uri Url { get; set; }
        public WebView2 WebView { get; set; }

        public void InitWbv2()
        {
            Debug.WriteLine("initing webv2 model");
            WebView2Model webView2Model = new WebView2Model(this);
            WebView = webView2Model.WebView2;
            Debug.WriteLine("init webv2 model finished");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
