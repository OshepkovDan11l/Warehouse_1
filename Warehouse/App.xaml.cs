using System.Configuration;
using System.Data;
using System.Windows;
using WarehouseData.Models;
using System.Collections.ObjectModel;
using Warehouse.Views;
using WarehouseData.Context;
using WarehouseData.Services;

namespace Warehouse
{
    public partial class App : Application
    {
        private ApplicationContext _context;

        public ServiceOrgs svcOrgs { get; set; }

        public App()
        {
            _context = new ApplicationContext();
            svcOrgs = new ServiceOrgs(_context);

            OrgsView orgsView = new OrgsView(svcOrgs);
            orgsView.Show();
        }
    }
}