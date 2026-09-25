using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Warehouse.Views;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Controls
{
    public partial class UserControlWarehouses : UserControl
    {
        private ServiceWhs _serviceWhs;
        private List<Product> _allProducts = new();

        public UserControlWarehouses(ServiceWhs serviceWhs)
        {
            InitializeComponent();
            _serviceWhs = serviceWhs;
            FillWarehousesCollection();
        }

        private void FillWarehousesCollection()
        {
            lbxWhList.ItemsSource = null;
            lbxWhList.Items.Clear();
            lbxWhList.ItemsSource = _serviceWhs.GetWarehouses();
        }

        private void lbxWhList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbxWhList.SelectedItem is WarehouseData.Models.Warehouse selectedWarehouse)
            {
                _serviceWhs.SetCurrentWarehouse(selectedWarehouse);
                _allProducts = selectedWarehouse.products ?? new List<Product>();
                FillSupplierFilter();
                ApplyFilters();
            }
        }

        private void FillSupplierFilter()
        {
            var suppliers = _allProducts
                .Select(p => p.Supplier?.Name ?? "")
                .Distinct()
                .ToList();
            suppliers.Insert(0, "Все");
            CmbSupplier.ItemsSource = suppliers;
            CmbSupplier.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            var result = _allProducts.AsEnumerable();

            if (!string.IsNullOrEmpty(TxtSearch.Text))
                result = result.Where(p =>
                    p.Name.Contains(TxtSearch.Text, System.StringComparison.OrdinalIgnoreCase));

            if (CmbSupplier.SelectedItem is string sup && sup != "Все")
                result = result.Where(p => p.Supplier?.Name == sup);

            if (CmbSort.SelectedItem is ComboBoxItem sort)
            {
                if (sort.Content?.ToString() == "Остаток ↑")
                    result = result.OrderBy(p => p.StockQuantity);
                else if (sort.Content?.ToString() == "Остаток ↓")
                    result = result.OrderByDescending(p => p.StockQuantity);
            }

            DgProducts.ItemsSource = result.ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
        private void CmbSupplier_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => ExecuteAddProduct();
        private void DgProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e) => ExecuteEditProduct();

        private void WhAdd_Executed(object sender, ExecutedRoutedEventArgs e) => ExecuteWhAdd();
        private void WhEdit_Executed(object sender, ExecutedRoutedEventArgs e) => ExecuteWhEdit();
        private void WhDelete_Executed(object sender, ExecutedRoutedEventArgs e) => ExecuteWhDelete();
        private void ImportProducts_Executed(object sender, ExecutedRoutedEventArgs e) => ExecuteImportProducts();

        public void ExecuteWhAdd()
        {
            var dialog = new WhModWindow("Новый склад", 1, null, _serviceWhs);
            if (dialog.ShowDialog() == true)
                FillWarehousesCollection();
        }

        public void ExecuteWhEdit()
        {
            if (lbxWhList.SelectedItem is WarehouseData.Models.Warehouse selectedWarehouse)
            {
                var dialog = new WhModWindow("Изменить склад", 2, selectedWarehouse, _serviceWhs);
                if (dialog.ShowDialog() == true)
                    FillWarehousesCollection();
            }
            else
                MessageBox.Show("Выберите склад!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ExecuteWhDelete()
        {
            if (lbxWhList.SelectedItem is WarehouseData.Models.Warehouse selectedWarehouse)
            {
                var result = MessageBox.Show($"Удалить склад {selectedWarehouse.WhName}?", "Подтверждение",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _serviceWhs.Delete(selectedWarehouse);
                    FillWarehousesCollection();
                }
            }
            else
                MessageBox.Show("Выберите склад!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void ExecuteAddProduct()
        {
            if (lbxWhList.SelectedItem is not WarehouseData.Models.Warehouse selectedWarehouse)
            {
                MessageBox.Show("Выберите склад!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dialog = new ProductModWindow(_serviceWhs.Context!);
            dialog.Owner = Window.GetWindow(this);
            if (dialog.ShowDialog() == true)
            {
                selectedWarehouse.products.Add(dialog.Result);
                _allProducts = selectedWarehouse.products;
                FillSupplierFilter();
                ApplyFilters();
            }
        }

        public void ExecuteEditProduct()
        {
            if (DgProducts.SelectedItem is not Product product) return;
            if (lbxWhList.SelectedItem is not WarehouseData.Models.Warehouse selectedWarehouse) return;

            var dialog = new ProductModWindow(_serviceWhs.Context!, product);
            dialog.Owner = Window.GetWindow(this);
            if (dialog.ShowDialog() == true)
            {
                int productIndex = selectedWarehouse.products.IndexOf(product);
                if (productIndex >= 0) selectedWarehouse.products[productIndex] = dialog.Result;
                _allProducts = selectedWarehouse.products;
                FillSupplierFilter();
                ApplyFilters();
            }
        }

        public void ExecuteRemoveProduct()
        {
            if (DgProducts.SelectedItem is not Product product) return;
            if (lbxWhList.SelectedItem is not WarehouseData.Models.Warehouse selectedWarehouse) return;

            var confirmation = MessageBox.Show($"Удалить товар '{product.Name}'?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmation == MessageBoxResult.Yes)
            {
                selectedWarehouse.products.Remove(product);
                _allProducts = selectedWarehouse.products;
                FillSupplierFilter();
                ApplyFilters();
            }
        }

        private void OpenInvoiceWindow()
        {
            if (lbxWhList.SelectedItem is not WarehouseData.Models.Warehouse selectedWarehouse)
            {
                MessageBox.Show("Выберите склад!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
      
            var svcInvoice = new ServiceInvoice(_serviceWhs);
            var dialog = new InvoiceWindow(svcInvoice, _serviceWhs);
            dialog.Owner = Window.GetWindow(this);
            dialog.ShowDialog();
           
            _allProducts = selectedWarehouse.products;
            ApplyFilters();
        }

        public void ExecuteGetOrders() => OpenInvoiceWindow();
        public void ExecuteAddOrder() => OpenInvoiceWindow();
        public void ExecuteEditOrder() => OpenInvoiceWindow();
        public void ExecuteRemoveOrder() => OpenInvoiceWindow();

        public void ExecuteImportProducts()
        {
            if (lbxWhList.SelectedItem is not WarehouseData.Models.Warehouse selectedWarehouse)
            {
                MessageBox.Show("Выберите склад!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV файлы (*.csv)|*.csv",
                Title = "Импорт товаров"
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                var lines = System.IO.File.ReadAllLines(dialog.FileName);
                int imported = 0;
                var context = _serviceWhs.GetContext();

                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(';');
                    if (parts.Length < 4) continue;

                    var categoryName = parts.Length > 5 ? parts[5].Trim() : "";
                    var manufacturerName = parts.Length > 6 ? parts[6].Trim() : "";
                    var supplierName = parts.Length > 7 ? parts[7].Trim() : "";

                    var category = context.categories.FirstOrDefault(c => c.Name == categoryName);
                    var manufacturer = context.manufacturers.FirstOrDefault(m => m.Name == manufacturerName);
                    var supplier = context.suppliers.FirstOrDefault(s => s.Name == supplierName);

                    var product = new Product
                    {
                        Article = parts[0].Trim(),
                        Name = parts[1].Trim(),
                        Price = decimal.TryParse(parts[2].Trim(), out var price) ? price : 0,
                        StockQuantity = int.TryParse(parts[3].Trim(), out var qty) ? qty : 0,
                        DiscountPercent = parts.Length > 4 && decimal.TryParse(parts[4].Trim(), out var disc) ? disc : 0,
                        Category = category!,
                        CategoryId = category?.Id ?? 0,
                        Manufacturer = manufacturer!,
                        ManufacturerId = manufacturer?.Id ?? 0,
                        Supplier = supplier!,
                        SupplierId = supplier?.Id ?? 0,
                    };

                    if (string.IsNullOrWhiteSpace(product.Article) ||
                        string.IsNullOrWhiteSpace(product.Name)) continue;

                    selectedWarehouse.products.Add(product);
                    imported++;
                }

                _allProducts = selectedWarehouse.products;
                FillSupplierFilter();
                ApplyFilters();
                MessageBox.Show($"Импортировано товаров: {imported}", "Готово",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}