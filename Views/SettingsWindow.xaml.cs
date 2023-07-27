using RevitGuide.Helpers;
using RevitGuide.Settings;
using RevitGuide.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RevitGuide.Views
{

    public partial class SettingsWindow : Window
    {
        private SettingsViewModel _settingsViewModel;

        public SettingsWindow(SettingsManager settingsManager)
        {
            _settingsViewModel = new SettingsViewModel(settingsManager);
            DataContext = _settingsViewModel;
            InitializeComponent();
            Mediator.Register("UpdateDataGridSelection", tabsettings => UpdateDataGridSelection((List<TabSetting>)tabsettings));
        }

        private void UpdateDataGridSelection(List<TabSetting> tabSettings)
        {
            TabSettingsDataGrid.SelectedItems.Clear();
            foreach (TabSetting tabSetting in tabSettings)
            {
                TabSettingsDataGrid.SelectedItems.Add(tabSetting);
            }
        }

        private void DataGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            if (!(hit.VisualHit is DataGridRow))
            {
                TabSettingsDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                TabSettingsDataGrid.SelectedItem = null;
            }
        }

            private void OnConfirmClicked(object sender, RoutedEventArgs e)
        {
            _settingsViewModel.HandleConfirm();
            this.DialogResult = true;
            Close();
        }

        private void AddItemClicked(object sender, RoutedEventArgs e)
        {
            _settingsViewModel.HandleNewItem(GetDatagridSelectedIndices());
        }

        private void DeleteItemClicked (object sender, RoutedEventArgs e)
        {

            _settingsViewModel.HandleRemoveItems(GetDatagridSelectedIndices());
        }

        private void MoveupClicked (object sender, RoutedEventArgs e)
        {
            _settingsViewModel.HandleMoveItems(GetDatagridSelectedIndices(), true);
        }
        private void MovedownClicked(object sender, RoutedEventArgs e)
        {
            _settingsViewModel.HandleMoveItems(GetDatagridSelectedIndices(), false);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private List<int> GetDatagridSelectedIndices()
        {
            List<int> selectedIndices = new List<int>();
            foreach (TabSetting item in TabSettingsDataGrid.SelectedItems)
            {
                selectedIndices.Add(_settingsViewModel.TabSettings.IndexOf(item));
            }
            return selectedIndices;
        }
    }
}
