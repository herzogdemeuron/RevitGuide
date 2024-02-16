using Autodesk.Revit.DB;
using RevitGuide.ViewModels;
using System.Reflection;
using System;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;

namespace RevitGuide.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow(Document doc)
        {
            _viewModel = new MainViewModel(doc);
            this.DataContext = _viewModel;
            InitializeComponent();
            string directoryDLLs = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Debug.WriteLine("directoryDLLs: " + directoryDLLs);
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

        private void OnOpenClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.HandleOpenClicked();
        }

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.ClearAllTabs();
            this.Close();
        }


    }
}

