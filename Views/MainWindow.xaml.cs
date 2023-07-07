using RevitGuide.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace RevitGuide.Views
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void OnConfigClicked(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }


    }
}

