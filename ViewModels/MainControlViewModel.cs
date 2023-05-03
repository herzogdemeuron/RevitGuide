using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitAssist
{
    public class MainControlViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItemViewModel> Tabs { get; set; }

        public MainControlViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>();
            AddTab("https://climbing-college-eff.notion.site/DT-BIM-Manual-WIP-fd16016d4aa143599422cd3409b3f7a8", "HdM Manual");
            AddTab("https://climbing-college-eff.notion.site/537-BAP-light-c3a4f474d3834062943a1e6b4510c335", "537");
            AddTab("https://www.google.com", "Live");
        }

        private void AddTab(string url, string header)
        {
            TabItemViewModel tab = new TabItemViewModel
            {
                Title = header,
                Url = new Uri(url),
            };

            Tabs.Add(tab);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
