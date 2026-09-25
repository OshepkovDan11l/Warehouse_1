using System.Linq;
using System.Windows;
using WarehouseData.Context;
using WarehouseData.Models;

namespace Warehouse.Views
{
    public partial class ProductModWindow : Window
    {
        public Product Result { get; private set; } = new();
        private readonly ApplicationContext _context;

        public ProductModWindow(ApplicationContext context, Product? existing = null)
        {
            InitializeComponent();
            _context = context;

            var categories = context.categories.ToList();
            var manufacturers = context.manufacturers.ToList();
            var suppliers = context.suppliers.ToList();

            CmbCategory.ItemsSource = categories;
            CmbManufacturer.ItemsSource = manufacturers;
            CmbSupplier.ItemsSource = suppliers;

            if (existing != null)
            {
                Result = existing;
                TxtArticle.Text = existing.Article;
                TxtName.Text = existing.Name;
                TxtUnit.Text = existing.Unit;
                TxtPrice.Text = existing.Price.ToString();
                TxtStock.Text = existing.StockQuantity.ToString();
                TxtDiscount.Text = existing.DiscountPercent.ToString();
                CmbCategory.SelectedItem = categories.FirstOrDefault(c => c.Id == existing.CategoryId);
                CmbManufacturer.SelectedItem = manufacturers.FirstOrDefault(m => m.Id == existing.ManufacturerId);
                CmbSupplier.SelectedItem = suppliers.FirstOrDefault(s => s.Id == existing.SupplierId);
            }
            else
            {
                CmbCategory.SelectedIndex = 0;
                CmbManufacturer.SelectedIndex = 0;
                CmbSupplier.SelectedIndex = 0;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtArticle.Text))
            {
                MessageBox.Show("Введите артикул!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Введите наименование!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(TxtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть числом не менее 0!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(TxtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Остаток должен быть числом не менее 0!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(TxtDiscount.Text, out decimal discount))
                discount = 0;
            if (discount < 0 || discount > 100)
            {
                MessageBox.Show("Скидка должна быть от 0 до 100!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Result.Article = TxtArticle.Text;
            Result.Name = TxtName.Text;
            Result.Unit = TxtUnit.Text;
            Result.Price = price;
            Result.StockQuantity = stock;
            Result.DiscountPercent = discount;

            if (CmbCategory.SelectedItem is Category cat)
            {
                Result.Category = cat;
                Result.CategoryId = cat.Id;
            }
            if (CmbManufacturer.SelectedItem is Manufacturer man)
            {
                Result.Manufacturer = man;
                Result.ManufacturerId = man.Id;
            }
            if (CmbSupplier.SelectedItem is Supplier sup)
            {
                Result.Supplier = sup;
                Result.SupplierId = sup.Id;
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}