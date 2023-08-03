using RevitGuide.Commands;
using RevitGuide.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Check if the entire text is selected
            if (editableTextBox.SelectionLength == editableTextBox.Text.Length)
            {
                // Replace the text
                editableTextBox.Text = e.Text;
            }
            else
            {
                // Append the new text to the existing text
                editableTextBox.Text += e.Text;
            }

            // Set the caret to the end of the text
            editableTextBox.CaretIndex = editableTextBox.Text.Length;
            // deselect all text
            IsDropDownOpen = true;
            //editableTextBox.SelectionLength = 0;
            //editableTextBox.SelectionBrush = null;
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
