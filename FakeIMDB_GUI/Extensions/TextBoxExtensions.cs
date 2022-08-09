using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FakeIMDB_GUI.Extensions
{
    public class TextBoxExtensions
    {
        public static string GetTextBoxLabel(DependencyObject obj)
        {
            return (string)obj.GetValue(TextBoxLabelProperty);
        }

        public static void SetTextBoxLabel(DependencyObject obj, string value)
        {
            obj.SetValue(TextBoxLabelProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextBoxLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxLabelProperty =
            DependencyProperty.RegisterAttached("TextBoxLabel", typeof(string), typeof(TextBoxExtensions), new PropertyMetadata(string.Empty));
    }
}
