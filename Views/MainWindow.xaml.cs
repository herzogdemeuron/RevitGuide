using RevitGuide.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace RevitGuide.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel();
            this.DataContext = _viewModel;
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void OnConfigClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleConfigClicked();
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearAllTabs();
            this.Close();
        }


    }
}

