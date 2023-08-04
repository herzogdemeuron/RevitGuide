using RevitGuide.Commands;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitGuide.Views
{
    public class SearchComboBox : ComboBox
    {
        TextBox _editableTextBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _editableTextBox = GetTemplateChild("PART_EditableTextBox") as TextBox;
            this.IsTextSearchEnabled = false;
            this.IsEditable = true;

            if (_editableTextBox != null)
            {
                _editableTextBox.TextChanged += EditableTextBox_TextChanged;
                _editableTextBox.GotFocus += EditableTextBox_GotFocus;
            }
        }

        private void EditableTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            string text = _editableTextBox.Text;
            this.SelectedIndex = -1;
            _editableTextBox.Text = text;
            IsDropDownOpen = true;
        }

        private void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ItemsSource is ICollectionView ICV)
            {
                if (string.IsNullOrEmpty(_editableTextBox.Text.Trim()))
                {
                    ICV.Filter = null;
                }
                else
                {
                    ICV.Filter = new Predicate<object>(i => ((RvtCommand)i).Name.ToLower().Contains(_editableTextBox.Text.ToLower()));
                }
            }
        }
    }
}
