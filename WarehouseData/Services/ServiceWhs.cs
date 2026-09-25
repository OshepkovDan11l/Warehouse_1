using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services
{
    public class ServiceWhs
    {
        private ApplicationContext _context;
        private Organization _org;
        private WarehouseData.Models.Warehouse? _currentWarehouse;

        public ServiceWhs(ApplicationContext context, Organization org)
        {
            _context = context;
            _org = org;
        }

        public ApplicationContext Context => _context;
        public ApplicationContext GetContext() => _context;
        public Organization GetOrg() => _org;

        public void SetCurrentWarehouse(WarehouseData.Models.Warehouse wh) => _currentWarehouse = wh;

        public List<Product> GetCurrentWarehouseProducts()
        {
            return _currentWarehouse?.products ?? new List<Product>();
        }

        public List<WarehouseData.Models.Warehouse> GetWarehouses()
        {
            return _org.warehouses.ToList();
        }

        public bool Delete(WarehouseData.Models.Warehouse wh) => DelWh(wh);

        public bool AddWh(WarehouseData.Models.Warehouse wh)
        {
            if (wh == null || string.IsNullOrWhiteSpace(wh.WhName)) return false;
            _org.warehouses.Add(wh);
            return true;
        }

        public bool EditWh(WarehouseData.Models.Warehouse wh)
        {
            if (wh == null || string.IsNullOrWhiteSpace(wh.WhName)) return false;
            var existing = _org.warehouses.FirstOrDefault(w => w.WhId == wh.WhId);
            if (existing == null) return false;
            existing.WhName = wh.WhName;
            existing.WhAddress = wh.WhAddress;
            return true;
        }

        public bool DelWh(WarehouseData.Models.Warehouse wh)
        {
            if (wh == null) return false;
            return _org.warehouses.Remove(wh);
        }

        public bool AddProduct(WarehouseData.Models.Warehouse wh, Product product)
        {
            if (wh == null || product == null) return false;
            wh.products.Add(product);
            return true;
        }

        public bool EditProduct(WarehouseData.Models.Warehouse wh, Product product)
        {
            if (wh == null || product == null) return false;
            var existing = wh.products.FirstOrDefault(p => p.Article == product.Article);
            if (existing == null) return false;
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
            existing.DiscountPercent = product.DiscountPercent;
            existing.Category = product.Category;
            existing.Manufacturer = product.Manufacturer;
            existing.Supplier = product.Supplier;
            return true;
        }

        public bool RemoveProduct(WarehouseData.Models.Warehouse wh, Product product)
        {
            if (wh == null || product == null) return false;
            return wh.products.Remove(product);
        }
    }
}