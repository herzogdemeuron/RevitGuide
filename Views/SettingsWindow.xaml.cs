using RevitGuide.Helpers;
using RevitGuide.Settings;
using RevitGuide.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RevitGuide.Views
{

    public partial class SettingsWindow : Window
    {
        private SettingsViewModel _settingsViewModel;

        private DataGrid _activeDataGrid
        {
            get
            {
                return _settingsViewModel.IsTabSettingsActive ? TabSettingsDataGrid : TriggerSettingsDataGrid;
            }
        }

        public SettingsWindow(SettingsManager settingsManager)
        {
            _settingsViewModel = new SettingsViewModel(settingsManager);
            DataContext = _settingsViewModel;
            InitializeComponent();
            Mediator.Register("UpdateDataGridSelection", itemsettings => UpdateDataGridSelection((List<ItemSetting>)itemsettings));
        }

        private void UpdateDataGridSelection(List<ItemSetting> tabSettings)
        {
            //refresh the datagrid selections
            _activeDataGrid.SelectedItems.Clear();
            foreach (ItemSetting tabSetting in tabSettings)
            {
                _activeDataGrid.SelectedItems.Add(tabSetting);
            }
        }
        private void TabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            if (tabControl.SelectedItem is TabItem selectedTab)
            {
                switch (selectedTab.Name)
                {
                    case "PageTabsTab":
                        _settingsViewModel.IsTabSettingsActive = true;
                        break;
                    case "LiveGuideTab":
                        _settingsViewModel.IsTabSettingsActive = false;
                        break;
                }
            }
        }
        private void DataGridMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            if (!(hit.VisualHit is DataGridRow))
            {
                _activeDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                _activeDataGrid.SelectedItem = null;
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
            foreach (ItemSetting item in _activeDataGrid.SelectedItems)
            {
                selectedIndices.Add(_settingsViewModel.ActiveSettings.IndexOf(item));
            }
            return selectedIndices;
        }
    }
}
