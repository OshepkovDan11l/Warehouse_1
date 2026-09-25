using System.Collections.ObjectModel;
using WarehouseData.Models;

namespace WarehouseData.Context
{
    public class ApplicationContext
    {
        public ObservableCollection<Organization> orgs;
        public ObservableCollection<Models.Warehouse> whs;
        public ObservableCollection<Category> categories;
        public ObservableCollection<Manufacturer> manufacturers;
        public ObservableCollection<Supplier> suppliers;

        public ApplicationContext()
        {
            orgs = new ObservableCollection<Organization>();
            whs = new ObservableCollection<Models.Warehouse>();
            categories = new ObservableCollection<Category>();
            manufacturers = new ObservableCollection<Manufacturer>();
            suppliers = new ObservableCollection<Supplier>();
            OrgsFill();
        }

        public void OrgsFill()
        {
            var categoryElectronics = new Category { Id = 1, Name = "Электроника" };
            var categoryOfficeSupplies = new Category { Id = 2, Name = "Канцелярские товары" };
            var categoryHomeAppliances = new Category { Id = 3, Name = "Бытовая техника" };
            var categoryFood = new Category { Id = 4, Name = "Продукты питания" };
            categories.Add(categoryElectronics);
            categories.Add(categoryOfficeSupplies);
            categories.Add(categoryHomeAppliances);
            categories.Add(categoryFood);

            var manufacturerLg = new Manufacturer { Id = 1, Name = "LG" };
            var manufacturerXiaomi = new Manufacturer { Id = 2, Name = "Xiaomi" };
            var manufacturerErichKrause = new Manufacturer { Id = 3, Name = "ErichKrause" };
            var manufacturerBosch = new Manufacturer { Id = 4, Name = "Bosch" };
            var manufacturerNestle = new Manufacturer { Id = 5, Name = "Nestlé" };
            manufacturers.Add(manufacturerLg);
            manufacturers.Add(manufacturerXiaomi);
            manufacturers.Add(manufacturerErichKrause);
            manufacturers.Add(manufacturerBosch);
            manufacturers.Add(manufacturerNestle);

            var supplierTechnoImport = new Supplier { Id = 1, Name = "ООО «ТехноИмпорт»" };
            var supplierOfficeLine = new Supplier { Id = 2, Name = "ООО «ОфисЛайн»" };
            var supplierFoodTrade = new Supplier { Id = 3, Name = "ООО «ПродТорг»" };
            suppliers.Add(supplierTechnoImport);
            suppliers.Add(supplierOfficeLine);
            suppliers.Add(supplierFoodTrade);

            Organization organizationSeverstroy = new Organization("Северстрой, ООО");
            Organization organizationGarantTorg = new Organization("Гарант-Торг, ООО");

            Models.Warehouse warehouseSeverstroyCentral = new Models.Warehouse(
                "Северстрой — Центральный склад", "г. Москва, Складская ул., д. 12", organizationSeverstroy.OrgId);
            Models.Warehouse warehouseSeverstroyNorth = new Models.Warehouse(
                "Северстрой — Северный склад", "г. Москва, Логистический пр-д, д. 5", organizationSeverstroy.OrgId);
            Models.Warehouse warehouseGarantTorgMain = new Models.Warehouse(
                "Гарант-Торг — Основной склад", "г. Санкт-Петербург, Промышленная ул., д. 9", organizationGarantTorg.OrgId);
            Models.Warehouse warehouseGarantTorgRetail = new Models.Warehouse(
                "Гарант-Торг — Розничный склад", "г. Санкт-Петербург, Торговый пер., д. 3", organizationGarantTorg.OrgId);

            warehouseSeverstroyCentral.products.Add(new Product { Article = "EL-001", Name = "Телевизор LG 55\" OLED", Unit = "шт", Price = 52000, StockQuantity = 12, DiscountPercent = 5, Category = categoryElectronics, Manufacturer = manufacturerLg, Supplier = supplierTechnoImport, CategoryId = categoryElectronics.Id, ManufacturerId = manufacturerLg.Id, SupplierId = supplierTechnoImport.Id });
            warehouseSeverstroyCentral.products.Add(new Product { Article = "EL-002", Name = "Смартфон Xiaomi Redmi Note", Unit = "шт", Price = 19000, StockQuantity = 30, DiscountPercent = 0, Category = categoryElectronics, Manufacturer = manufacturerXiaomi, Supplier = supplierTechnoImport, CategoryId = categoryElectronics.Id, ManufacturerId = manufacturerXiaomi.Id, SupplierId = supplierTechnoImport.Id });
            warehouseSeverstroyCentral.products.Add(new Product { Article = "EL-003", Name = "Планшет Xiaomi Pad", Unit = "шт", Price = 27000, StockQuantity = 9, DiscountPercent = 3, Category = categoryElectronics, Manufacturer = manufacturerXiaomi, Supplier = supplierTechnoImport, CategoryId = categoryElectronics.Id, ManufacturerId = manufacturerXiaomi.Id, SupplierId = supplierTechnoImport.Id });

            warehouseSeverstroyNorth.products.Add(new Product { Article = "HA-001", Name = "Стиральная машина Bosch", Unit = "шт", Price = 38000, StockQuantity = 6, DiscountPercent = 7, Category = categoryHomeAppliances, Manufacturer = manufacturerBosch, Supplier = supplierTechnoImport, CategoryId = categoryHomeAppliances.Id, ManufacturerId = manufacturerBosch.Id, SupplierId = supplierTechnoImport.Id });
            warehouseSeverstroyNorth.products.Add(new Product { Article = "HA-002", Name = "Холодильник Bosch", Unit = "шт", Price = 54000, StockQuantity = 4, DiscountPercent = 0, Category = categoryHomeAppliances, Manufacturer = manufacturerBosch, Supplier = supplierTechnoImport, CategoryId = categoryHomeAppliances.Id, ManufacturerId = manufacturerBosch.Id, SupplierId = supplierTechnoImport.Id });
            warehouseSeverstroyNorth.products.Add(new Product { Article = "HA-003", Name = "Пылесос Bosch", Unit = "шт", Price = 11000, StockQuantity = 15, DiscountPercent = 5, Category = categoryHomeAppliances, Manufacturer = manufacturerBosch, Supplier = supplierTechnoImport, CategoryId = categoryHomeAppliances.Id, ManufacturerId = manufacturerBosch.Id, SupplierId = supplierTechnoImport.Id });

            warehouseGarantTorgMain.products.Add(new Product { Article = "OF-001", Name = "Ручка шариковая ErichKrause", Unit = "шт", Price = 35, StockQuantity = 500, DiscountPercent = 10, Category = categoryOfficeSupplies, Manufacturer = manufacturerErichKrause, Supplier = supplierOfficeLine, CategoryId = categoryOfficeSupplies.Id, ManufacturerId = manufacturerErichKrause.Id, SupplierId = supplierOfficeLine.Id });
            warehouseGarantTorgMain.products.Add(new Product { Article = "OF-002", Name = "Тетрадь 48 листов ErichKrause", Unit = "шт", Price = 60, StockQuantity = 350, DiscountPercent = 5, Category = categoryOfficeSupplies, Manufacturer = manufacturerErichKrause, Supplier = supplierOfficeLine, CategoryId = categoryOfficeSupplies.Id, ManufacturerId = manufacturerErichKrause.Id, SupplierId = supplierOfficeLine.Id });
            warehouseGarantTorgMain.products.Add(new Product { Article = "OF-003", Name = "Папка-регистратор ErichKrause", Unit = "шт", Price = 210, StockQuantity = 120, DiscountPercent = 0, Category = categoryOfficeSupplies, Manufacturer = manufacturerErichKrause, Supplier = supplierOfficeLine, CategoryId = categoryOfficeSupplies.Id, ManufacturerId = manufacturerErichKrause.Id, SupplierId = supplierOfficeLine.Id });

            warehouseGarantTorgRetail.products.Add(new Product { Article = "FD-001", Name = "Кофе растворимый Nescafé Gold", Unit = "шт", Price = 460, StockQuantity = 180, DiscountPercent = 0, Category = categoryFood, Manufacturer = manufacturerNestle, Supplier = supplierFoodTrade, CategoryId = categoryFood.Id, ManufacturerId = manufacturerNestle.Id, SupplierId = supplierFoodTrade.Id });
            warehouseGarantTorgRetail.products.Add(new Product { Article = "FD-002", Name = "Шоколад Nestlé KitKat", Unit = "шт", Price = 90, StockQuantity = 320, DiscountPercent = 5, Category = categoryFood, Manufacturer = manufacturerNestle, Supplier = supplierFoodTrade, CategoryId = categoryFood.Id, ManufacturerId = manufacturerNestle.Id, SupplierId = supplierFoodTrade.Id });

            organizationSeverstroy.warehouses.Add(warehouseSeverstroyCentral);
            organizationSeverstroy.warehouses.Add(warehouseSeverstroyNorth);
            organizationGarantTorg.warehouses.Add(warehouseGarantTorgMain);
            organizationGarantTorg.warehouses.Add(warehouseGarantTorgRetail);

            orgs.Add(organizationSeverstroy);
            orgs.Add(organizationGarantTorg);
        }
    }
}
