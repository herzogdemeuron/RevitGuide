using RevitGuide.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RevitGuide.Views
{

    public partial class SettingsWindow : Window
    {
        private SettingsViewModel _settingsViewModel;

        public SettingsWindow()
        {
            _settingsViewModel = new SettingsViewModel();
            DataContext = _settingsViewModel;
            InitializeComponent();
        }

        private void DataGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            if (hit.VisualHit is DataGridRow || hit.VisualHit is TextBlock)
            {
                Debug.WriteLine("A row or cell was clicked, do nothing.");
            }
            else
            {
                Debug.WriteLine("Empty space was clicked, add a new row.");
                _settingsViewModel.HandleNewItem();
               
            }
        }
            private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            _settingsViewModel.HandleConfirm();
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
