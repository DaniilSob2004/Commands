using System;
using System.Collections.Generic;
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

namespace Command
{
    public partial class MainWindow : Window
    {
        TextRange selectedTextRange;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Clear_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // очищаем от форматирования весь текст
            selectedTextRange = new TextRange(textBox.Selection.Start, textBox.Selection.End);
            selectedTextRange.ClearAllProperties();
        }

        private void FontSize_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var fontSize = (string)e.Parameter;  // получаем из параметров размер шрифта
            if (fontSize != null)
            {
                // если ничего не выделенно, то меняем размер шрифта и весь контент
                if (textBox.Selection.Text == "")
                {
                    textBox.FontSize = double.Parse(fontSize);
                    selectedTextRange = new TextRange(textBox.Document.ContentStart, textBox.Document.ContentEnd);
                }
                else  // если выбранный контент есть, то меняем только его размер
                {
                    selectedTextRange = new TextRange(textBox.Selection.Start, textBox.Selection.End);
                }
                selectedTextRange.ApplyPropertyValue(TextElement.FontSizeProperty, double.Parse(fontSize));
            }
        }

        private void Color_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // преобразовываем цвет в hex в объект Brush
            Brush brush = (Brush)new BrushConverter().ConvertFromString(e.Parameter.ToString());
            selectedTextRange = new TextRange(textBox.Selection.Start, textBox.Selection.End);
            selectedTextRange.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
        }
    }

    public class FormatCommand
    {
        static FormatCommand()
        {
            Clear = new RoutedCommand("Clear", typeof(FormatCommand));
            FontSize = new RoutedCommand("FontSize", typeof(FormatCommand));
            Color = new RoutedCommand("Color", typeof(FormatCommand));
        }
        public static RoutedCommand Clear { get; set; }
        public static RoutedCommand FontSize { get; set; }
        public static RoutedCommand Color { get; set; }
    }
}
