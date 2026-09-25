using System.Windows;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class OrgModWindow : Window
    {
        private int _mode;
        private Organization? _org;
        private ServiceOrgs _service;

        public OrgModWindow(string title, string name, int mode, ServiceOrgs service)
        {
            InitializeComponent();
            Title = title;
            tbName.Text = name;
            _mode = mode;
            _service = service;
        }

        public OrgModWindow(string title, string name, int mode, Organization org, ServiceOrgs service)
        {
            InitializeComponent();
            Title = title;
            _mode = mode;
            _org = org;
            _service = service;

            if (mode == 3)
            {
                tbName.Text = "";
                tbHint.Text = $"Введите «{name}» для подтверждения удаления";
                tbHint.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                tbName.Text = name;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Введите название!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_mode == 1)
                _service.AddOrg(new Organization(tbName.Text));
            else if (_mode == 2 && _org != null)
            {
                _org.OrgName = tbName.Text;
                _service.EditOrg(_org);
            }
            else if (_mode == 3 && _org != null)
            {
                if (tbName.Text != _org.OrgName)
                {
                    MessageBox.Show($"Название не совпадает!\nВведите точно: «{_org.OrgName}»",
                        "Ошибка подтверждения", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                _service.DelOrg(_org);
            }

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}