using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TextEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "Текстовий редактор";

            Uri iconUri = new Uri("https://icons.iconarchive.com/icons/hopstarter/soft-scraps/256/Text-Edit-icon.png", UriKind.Absolute);
            BitmapImage bitmapImage = new BitmapImage(iconUri);
            this.Icon = bitmapImage;

            AddNewTab(null, null);
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string content = File.ReadAllText(filePath);
                GetCurrentTextBox().Text = content;
                this.Title = Path.GetFileName(filePath) + " - Text Editor";
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, GetCurrentTextBox().Text);
                this.Title = Path.GetFileName(filePath) + " - Text Editor";
            }
        }

        private void CopyText(object sender, RoutedEventArgs e)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                Clipboard.SetText(GetCurrentTextBox().SelectedText);
            }
        }

        private void PasteText(object sender, RoutedEventArgs e)
        {
            GetCurrentTextBox().Paste();
        }

        private void CutText(object sender, RoutedEventArgs e)
        {
            if (GetCurrentTextBox().SelectedText.Length > 0)
            {
                Clipboard.SetText(GetCurrentTextBox().SelectedText);
                GetCurrentTextBox().SelectedText = string.Empty;
            }
        }

        private void UndoAction(object sender, RoutedEventArgs e)
        {
            if (GetCurrentTextBox().CanUndo)
            {
                GetCurrentTextBox().Undo();
            }
        }

        private void AboutApp(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Текстовий редактор\nВерсія 1.0\nЛабораторна робота 1\nЯрош Володимир ЗПІЗ-22-1", "Справка");
        }

        private TextBox GetCurrentTextBox()
        {
            TabItem activeTab = (TabItem)TabControlTextEditors.SelectedItem;
            return (TextBox)activeTab.Content;
        }

        private void AddNewTab(object sender, RoutedEventArgs e)
        {
            TabItem newTab = new TabItem
            {
                Header = "Нова вкладка"
            };

            TextBox newEditor = new TextBox
            {
                AcceptsReturn = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                TextWrapping = TextWrapping.Wrap,
                Height = 300,
                Margin = new Thickness(10)
            };

            Button closeButton = new Button
            {
                Content = "X",
                Width = 20, 
                Height = 20, 
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Padding = new Thickness(0),
                Margin = new Thickness(5, 0, 0, 0)
            };

            closeButton.Click += (s, args) =>
            {
                TabControlTextEditors.Items.Remove(newTab);
            };

            StackPanel headerPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            headerPanel.Children.Add(new TextBlock { Text = "Нова вкладка" });
            headerPanel.Children.Add(closeButton);
            newTab.Header = headerPanel;

            newTab.Content = newEditor;

            TabControlTextEditors.Items.Add(newTab);

            TabControlTextEditors.SelectedItem = newTab;
        }

        private void InsertGreekAlpha(object sender, RoutedEventArgs e)
        {
            TextBox activeTextBox = GetCurrentTextBox();
            activeTextBox.SelectedText += "α";  
        }

        private void InsertGreekBeta(object sender, RoutedEventArgs e)
        {
            TextBox activeTextBox = GetCurrentTextBox();
            activeTextBox.SelectedText += "β";  
        }

        private void InsertGreekMu(object sender, RoutedEventArgs e)
        {
            TextBox activeTextBox = GetCurrentTextBox();
            activeTextBox.SelectedText += "µ"; 
        }
    }
}
