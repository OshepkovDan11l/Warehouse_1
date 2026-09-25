using System.Windows;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class InvoiceItemWindow : Window
    {
        public InvoiceItem? Result { get; private set; }

        public InvoiceItemWindow(ServiceWhs serviceWhs)
        {
            InitializeComponent();
            CmbProduct.ItemsSource = serviceWhs.GetCurrentWarehouseProducts();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProduct.SelectedItem is not Product product)
            {
                MessageBox.Show("Выберите товар!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtQty.Text, out int qty) || qty <= 0)
            {
                MessageBox.Show("Введите корректное количество!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Result = new InvoiceItem { Product = product, Quantity = qty };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}