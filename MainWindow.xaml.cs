using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush DefaultColor = new SolidColorBrush(Color.FromArgb(100, 221, 221, 221));

        public MainWindow()
        {
            InitializeComponent();
            rtbText.Document.Blocks.Clear();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbText.Document.ContentStart, rtbText.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "RTF文件 (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbText.Document.ContentStart, rtbText.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
                rtbText.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);
        }

        private void cmbFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontSize.SelectedItem != null)
                rtbText.Selection.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.SelectedItem);
        }

        private void btnBold_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.FontWeightProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
                rtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Normal);
            else
                rtbText.Selection.ApplyPropertyValue(FontWeightProperty, FontWeights.Bold);
        }

        private void btnItalic_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.FontStyleProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
                rtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Normal);
            else
                rtbText.Selection.ApplyPropertyValue(FontStyleProperty, FontStyles.Italic);
        }

        private void btnUnderline_Click(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
                rtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            else
                rtbText.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        private void rtbText_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbText.Selection.GetPropertyValue(Inline.FontWeightProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold)))
                btnBold.Background = Brushes.Gray;
            else
                btnBold.Background = DefaultColor; 

            temp = rtbText.Selection.GetPropertyValue(Inline.FontStyleProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic)))
                btnItalic.Background = Brushes.Gray; 
            else
                btnItalic.Background = DefaultColor; 

            temp = rtbText.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            if ((temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline)))
                btnUnderline.Background = Brushes.Gray;
            else
                btnUnderline.Background = DefaultColor;

            temp = rtbText.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp; 
            temp = rtbText.Selection.GetPropertyValue(Inline.FontSizeProperty);
            cmbFontSize.SelectedItem = temp; 
        }

        private void rtbText_LostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
