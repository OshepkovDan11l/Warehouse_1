using System;
using System.Windows;
using System.Windows.Input;
using Warehouse.Controls;
using Warehouse.Views;
using WarehouseData.Context;
using WarehouseData.Models;
using WarehouseData.Services;
namespace Warehouse
{
    public partial class MainWindow : Window
    {
        private ApplicationContext _context;
        private Organization? _org;
        public ServiceWhs? serviceWhs { get; private set; }
        public MainWindow(ApplicationContext applicationContext, Organization org)
        {
            InitializeComponent();
            _context = applicationContext;
            _org = org;
            if (_org != null && !String.IsNullOrEmpty(_org.OrgName))
            {
                tbStatus.Text = _org.OrgName;
                Title = _org.OrgName;
            }
            serviceWhs = new ServiceWhs(applicationContext, org);
            MainFrame.Navigate(new UserControlWarehouses(serviceWhs));
        }
        private UserControlWarehouses? GetUCW() => MainFrame.Content as UserControlWarehouses;
        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e) => Close();
        private void GetProducts_Executed(object sender, ExecutedRoutedEventArgs e) { }
        private void GetWarehouses_Executed(object sender, ExecutedRoutedEventArgs e) { }
        private void WhAdd_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteWhAdd();
        private void WhEdit_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteWhEdit();
        private void WhDelete_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteWhDelete();
        private void AddProduct_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteAddProduct();
        private void EditProduct_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteEditProduct();
        private void RemoveProduct_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteRemoveProduct();
        private void GetOrders_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteGetOrders();
        private void AddOrder_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteAddOrder();
        private void EditOrder_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteEditOrder();
        private void RemoveOrder_Executed(object sender, ExecutedRoutedEventArgs e) => GetUCW()?.ExecuteRemoveOrder();
        private void ImportProducts_Executed(object sender, ExecutedRoutedEventArgs e)
            => GetUCW()?.ExecuteImportProducts();
        private void ShowCategories_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new DictionaryWindow("Категории товаров", serviceWhs!.GetContext().categories);
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void ShowSuppliers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new DictionaryWindow("Поставщики", serviceWhs!.GetContext().suppliers);
            dialog.Owner = this;
            dialog.ShowDialog();
        }
        private void ShowManufacturers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new DictionaryWindow("Производители", serviceWhs!.GetContext().manufacturers);
            dialog.Owner = this;
            dialog.ShowDialog();
        }
    }
}