using RevitGuide.Commands;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace RevitGuide.Views
{
    public class SearchComboBox : ComboBox
    {
        TextBox editableTextBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.IsTextSearchEnabled = false;
            this.IsEditable = true;
            editableTextBox = GetTemplateChild("PART_EditableTextBox") as TextBox;
            if (editableTextBox != null)
            {
                editableTextBox.TextChanged += EditableTextBox_TextChanged;
                editableTextBox.PreviewTextInput += EditableTextBox_PreviewTextInput;
            }
        }

        private void EditableTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            if (editableTextBox.SelectionLength == editableTextBox.Text.Length)
            {
                editableTextBox.Text = e.Text;
            }
            else
            {
                editableTextBox.Text += e.Text;
            }
            editableTextBox.CaretIndex = editableTextBox.Text.Length;
            IsDropDownOpen = true;
        }

        private void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ItemsSource is ICollectionView ICV)
            {
                if (string.IsNullOrEmpty(editableTextBox.Text.Trim()))
                {
                    ICV.Filter = null;
                }
                else
                {
                    ICV.Filter = new Predicate<object>(i => ((RvtCommand)i).Name.ToLower().Contains(editableTextBox.Text.ToLower()));
                }
            }
        }
    }
}
