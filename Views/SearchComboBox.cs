using RevitGuide.Commands;
using RevitGuide.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RevitGuide.Views
{
    public class SearchComboBox : ComboBox
    {
        TextBox editableTextBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            editableTextBox = GetTemplateChild("PART_EditableTextBox") as TextBox;

            editableTextBox.TextChanged += EditableTextBox_TextChanged;
        }

        private void EditableTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ItemsSource is ICollectionView ICV)
            {
                if (string.IsNullOrEmpty(editableTextBox.Text.Trim()))
                    ICV.Filter = null;
                else
                    ICV.Filter = new Predicate<object>(i => ((RvtCommand)i).Name.ToLower().Contains(editableTextBox.Text.ToLower()));

                //IsDropDownOpen = true;
            }

        }
    }
}
