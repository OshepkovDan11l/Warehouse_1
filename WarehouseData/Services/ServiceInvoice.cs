using System.Collections.ObjectModel;
using WarehouseData.Models;
using WarehouseData.Services;

namespace WarehouseData.Services
{
    public class ServiceInvoice
    {
        private readonly ServiceWhs _serviceWhs;
        private readonly ObservableCollection<Invoice> _invoices = new();

        public ServiceInvoice(ServiceWhs serviceWhs)
        {
            _serviceWhs = serviceWhs;
        }

        public IEnumerable<Invoice> GetInvoices() => _invoices;

        public bool Add(Invoice invoice)
        {
            if (invoice == null) return false;
            _invoices.Add(invoice);
            return true;
        }

        public bool Delete(Invoice invoice)
        {
            if (invoice == null || invoice.IsConfirmed) return false;
            return _invoices.Remove(invoice);
        }

        public bool Confirm(Invoice invoice)
        {
            if (invoice == null || invoice.IsConfirmed) return false;

            var products = _serviceWhs.GetCurrentWarehouseProducts();

            foreach (var item in invoice.Items)
            {
                if (item.Product == null) continue;

                var warehouseProduct = products
                    .FirstOrDefault(p => p.Article == item.Product.Article);

                if (warehouseProduct == null) continue;

                if (invoice.Type == InvoiceType.Incoming)
                    warehouseProduct.StockQuantity += item.Quantity;
                else if (invoice.Type == InvoiceType.Outgoing)
                    warehouseProduct.StockQuantity -= item.Quantity;
            }

            invoice.IsConfirmed = true;
            return true;
        }
    }
}