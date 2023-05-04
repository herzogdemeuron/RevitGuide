using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitAssist
{
    public class MainControlViewModel : INotifyPropertyChanged
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

        public MainControlViewModel()
        {
            Debug.WriteLine("Main Control: creating, start adding tabs");
            Tabs = new ObservableCollection<TabItemViewModel>();
            AddTab("https://climbing-college-eff.notion.site/DT-BIM-Manual-WIP-fd16016d4aa143599422cd3409b3f7a8", "HdM Manual");
            AddTab("https://climbing-college-eff.notion.site/537-BAP-light-c3a4f474d3834062943a1e6b4510c335", "537");
            AddTab("https://www.google.com", "Live");
            Debug.WriteLine("Main Control: all tabs added");

            SelectedTab = Tabs.FirstOrDefault();
        }

        private void AddTab(string url, string header)
        {
            Debug.WriteLine("adding tab");
            Debug.WriteLine(header);
            TabItemViewModel tab = new TabItemViewModel
            {
                Title = header,
                Url = new Uri(url),
            };
            tab.InitWbv2();

            Tabs.Add(tab);
            Debug.WriteLine("tab added");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
