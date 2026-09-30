using System.Windows;
using System.Windows.Controls;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class InvoiceWindow : Window
    {
        private readonly ServiceInvoice _service;
        private readonly ServiceWhs _serviceWhs;
        private Invoice? _current;

        public InvoiceWindow(ServiceInvoice service, ServiceWhs serviceWhs)
        {
            InitializeComponent();
            _service = service;
            _serviceWhs = serviceWhs;
            RefreshInvoices();
        }

        private void RefreshInvoices()
        {
            LbxInvoices.ItemsSource = null;
            LbxInvoices.ItemsSource = _service.GetInvoices();
        }

        private void LbxInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _current = LbxInvoices.SelectedItem as Invoice;
            DgItems.ItemsSource = _current?.Items;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new InvoiceCreateWindow(_serviceWhs);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && dialog.Result != null)
            {
                _service.Add(dialog.Result);
                RefreshInvoices();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_current == null) return;
            if (_current.IsConfirmed)
            {
                MessageBox.Show("Нельзя удалить проведённую накладную!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var confirmation = MessageBox.Show($"Удалить накладную {_current.Number}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                _service.Delete(_current);
                RefreshInvoices();
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (_current == null) return;
            if (_current.IsConfirmed)
            {
                MessageBox.Show("Накладная уже проведена!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var confirmation = MessageBox.Show($"Провести накладную {_current.Number}?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                _service.Confirm(_current);
                RefreshInvoices();
            }
        }

        private void BtnAddItem_Click(object sender, RoutedEventArgs e)
        {
            if (_current == null || _current.IsConfirmed)
            {
                MessageBox.Show("Выберите непроведённую накладную!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new InvoiceItemWindow(_serviceWhs);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true && dialog.Result != null)
            {
                _current.Items.Add(dialog.Result);
                DgItems.ItemsSource = null;
                DgItems.ItemsSource = _current.Items;
            }
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (_current == null || _current.IsConfirmed) return;
            if (DgItems.SelectedItem is InvoiceItem item)
            {
                _current.Items.Remove(item);
                DgItems.ItemsSource = null;
                DgItems.ItemsSource = _current.Items;
            }
        }

        private void DgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}