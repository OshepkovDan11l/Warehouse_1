using System.Windows;

namespace Warehouse.Views
{
    public partial class DictionaryItemWindow : Window
    {
        public string ResultName { get; private set; } = string.Empty;

        public DictionaryItemWindow(string title, string current)
        {
            InitializeComponent();
            Title = title;
            TxtName.Text = current;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Введите название!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ResultName = TxtName.Text;
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}