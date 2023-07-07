using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace RevitGuide.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItemViewModel> Tabs { get; set; }
        private TabItemViewModel _selectedTab;
        public TabItemViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>();
            AddTab("https://climbing-college-eff.notion.site/HdM-Revit-Guide-571251e164de4fc9b12728bf698d474a?pvs=4", "Workflows");
            AddTab(@"file://///hersrv01/Benutzer/Kejun_L/_934_DT/230426_RevitAssist/pdfs/live_assist_default.html", "Live Guide");

            SelectedTab = Tabs.FirstOrDefault();
        }

        private void AddTab(string url, string header)
        {
            TabItemViewModel tab = new TabItemViewModel
            {
                Title = header,
                Url = new Uri(url),
            };
            tab.InitWbv2();
            Tabs.Add(tab);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
