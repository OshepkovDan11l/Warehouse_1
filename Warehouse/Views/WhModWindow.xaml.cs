using System.Windows;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class WhModWindow : Window
    {
        public string ResultName { get; private set; } = string.Empty;
        public string ResultAddress { get; private set; } = string.Empty;

        private readonly WarehouseData.Models.Warehouse? _wh;
        private readonly ServiceWhs _serviceWhs;
        private readonly int _mode;

        public WhModWindow(string title, int mode, WarehouseData.Models.Warehouse? newWarehouse, ServiceWhs serviceWhs)
        {
            InitializeComponent();
            Title = title;
            _mode = mode;
            _wh = newWarehouse;
            _serviceWhs = serviceWhs;
            if (newWarehouse != null)
            {
                TxtName.Text = newWarehouse.WhName;
                TxtAddress.Text = newWarehouse.WhAddress;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Введите название склада!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ResultName = TxtName.Text;
            ResultAddress = TxtAddress.Text;

            if (_mode == 1)
            {
                var newWarehouse = new WarehouseData.Models.Warehouse(
                    ResultName, ResultAddress, _serviceWhs.GetOrg().OrgId);
                _serviceWhs.AddWh(newWarehouse);
            }
            else if (_mode == 2 && _wh != null)
            {
                _wh.WhName = ResultName;
                _wh.WhAddress = ResultAddress;
                _serviceWhs.EditWh(_wh);
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}