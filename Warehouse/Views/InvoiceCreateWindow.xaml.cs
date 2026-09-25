using System;
using System.Windows;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class InvoiceCreateWindow : Window
    {
        public Invoice? Result { get; private set; }

        public InvoiceCreateWindow(ServiceWhs serviceWhs)
        {
            InitializeComponent();
            DpDate.SelectedDate = DateTime.Today;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Result = new Invoice
            {
                Type = CmbType.SelectedIndex == 0 ? InvoiceType.Incoming : InvoiceType.Outgoing,
                Date = DpDate.SelectedDate ?? DateTime.Today
            };
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}