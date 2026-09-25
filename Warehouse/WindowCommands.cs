using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Warehouse
{
    public class WindowCommands
    {
        static WindowCommands()
        {
            OrgCancel = new RoutedCommand("OrgCancel", typeof(Window));
            OrgOk = new RoutedCommand("OrgOk", typeof(Window));
            OrgAdd = new RoutedCommand("OrgAdd", typeof(Window));
            OrgEdit = new RoutedCommand("OrgEdit", typeof(Window));
            OrgDelete = new RoutedCommand("OrgDelete", typeof(Window));
            WhAdd = new RoutedCommand("WhAdd", typeof(UserControl));
            WhEdit = new RoutedCommand("WhEdit", typeof(UserControl));
            WhDelete = new RoutedCommand("WhDelete", typeof(UserControl));
            GetWarehouses = new RoutedCommand("GetWarehouses", typeof(Window));
            Exit = new RoutedCommand("Exit", typeof(Window));
            GetProducts = new RoutedCommand("GetProducts", typeof(Window));
            AddProduct = new RoutedCommand("AddProduct", typeof(Window));
            EditProduct = new RoutedCommand("EditProduct", typeof(Window));
            RemoveProduct = new RoutedCommand("RemoveProduct", typeof(Window));
            GetOrders = new RoutedCommand("GetOrders", typeof(Window));
            AddOrder = new RoutedCommand("AddOrder", typeof(Window));
            EditOrder = new RoutedCommand("EditOrder", typeof(Window));
            RemoveOrder = new RoutedCommand("RemoveOrder", typeof(Window));
            ShowCategories = new RoutedCommand("ShowCategories", typeof(Window));
            ShowSuppliers = new RoutedCommand("ShowSuppliers", typeof(Window));
            ShowManufacturers = new RoutedCommand("ShowManufacturers", typeof(Window));
            Open = new RoutedCommand("Open", typeof(Window));
            Save = new RoutedCommand("Save", typeof(Window));
            ImportProducts = new RoutedCommand("ImportProducts", typeof(Window));
        }
        public static RoutedCommand OrgCancel { get; set; }
        public static RoutedCommand OrgOk { get; set; }
        public static RoutedCommand OrgAdd { get; set; }
        public static RoutedCommand OrgEdit { get; set; }
        public static RoutedCommand OrgDelete { get; set; }
        public static RoutedCommand WhAdd { get; set; }
        public static RoutedCommand WhEdit { get; set; }
        public static RoutedCommand WhDelete { get; set; }
        public static RoutedCommand GetWarehouses { get; set; }
        public static RoutedCommand Exit { get; set; }
        public static RoutedCommand GetProducts { get; set; }
        public static RoutedCommand AddProduct { get; set; }
        public static RoutedCommand EditProduct { get; set; }
        public static RoutedCommand RemoveProduct { get; set; }
        public static RoutedCommand GetOrders { get; set; }
        public static RoutedCommand AddOrder { get; set; }
        public static RoutedCommand EditOrder { get; set; }
        public static RoutedCommand RemoveOrder { get; set; }
        public static RoutedCommand ShowCategories { get; set; }
        public static RoutedCommand ShowSuppliers { get; set; }
        public static RoutedCommand ShowManufacturers { get; set; }
        public static RoutedCommand Open { get; set; }
        public static RoutedCommand Save { get; set; }
        public static RoutedCommand ImportProducts { get; set; }
    }
}