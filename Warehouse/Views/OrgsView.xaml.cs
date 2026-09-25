using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WarehouseData.Context;
using WarehouseData.Models;
using WarehouseData.Services;

namespace Warehouse.Views
{
    public partial class OrgsView : Window
    {
        private ServiceOrgs _service;
        private Organization? _org { get; set; }

        public OrgsView(ServiceOrgs svcOrgs)
        {
            InitializeComponent();
            _service = svcOrgs;
            FillCompaniesCollection();
            lbxOrgsList.Focus();
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e) => Close();

        private void Ok_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_org != null && !string.IsNullOrEmpty(_org.OrgName))
            {
                MainWindow mainWindow = new MainWindow(_service.GetContext(), _org);
                mainWindow.Show();
            }
        }

        private void FillCompaniesCollection()
        {
            int selectedIndex = lbxOrgsList.SelectedIndex > 0 ? lbxOrgsList.SelectedIndex : 0;
            lbxOrgsList.ItemsSource = null;
            lbxOrgsList.Items.Clear();
            lbxOrgsList.ItemsSource = _service.GetContext().orgs;
            if (lbxOrgsList.Items.Count > 0)
            {
                try { lbxOrgsList.SelectedIndex = selectedIndex; }
                catch { lbxOrgsList.SelectedIndex = -1; }
            }
        }

        private void OrgAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OrgModWindow dialog = new OrgModWindow("Новая компания", "", 1, _service);
            dialog.Owner = this;
            if (dialog.ShowDialog() == true) FillCompaniesCollection();
        }

        private void OrgEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_org != null)
            {
                OrgModWindow dialog = new OrgModWindow("Изменить компанию", _org.OrgName, 2, _org, _service);
                dialog.Owner = this;
                if (dialog.ShowDialog() == true) FillCompaniesCollection();
            }
        }

        private void OrgDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_org != null)
            {
                OrgModWindow dialog = new OrgModWindow("Удалить компанию", _org.OrgName, 3, _org, _service);
                dialog.Owner = this;
                if (dialog.ShowDialog() == true) FillCompaniesCollection();
            }
        }

        private void lbxOrgsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _org = (Organization?)lbxOrgsList.SelectedItem;
            if (_org != null && !string.IsNullOrEmpty(_org.OrgName))
                tbStatus.Text = _org.OrgName;
        }
    }
}