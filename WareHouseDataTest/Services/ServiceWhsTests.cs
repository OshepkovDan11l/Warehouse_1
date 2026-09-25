using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseData.Context;
using WarehouseData.Models;
using System.Linq;
using WarehouseData.Services;

namespace WareHouseDataTest.Services
{
    [TestClass]
    public class ServiceWhsTests
    {
        private ApplicationContext _context = null!;
        private Organization _org = null!;
        private ServiceWhs _svc = null!;

        [TestInitialize]
        public void Setup()
        {
            _context = new ApplicationContext();
            _org = new Organization("Тестовая организация");
            _svc = new ServiceWhs(_context, _org);
        }

        [TestMethod]
        public void ServiceWhsTest()
        {
            Assert.IsNotNull(_svc);
            Assert.IsInstanceOfType(_svc, typeof(ServiceWhs));
        }

        [TestMethod]
        public void AddWhTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Assert.IsTrue(_svc.AddWh(wh));
            Assert.AreEqual(1, _svc.GetWarehouses().Count);
        }

        [TestMethod]
        public void EditWhTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            _svc.AddWh(wh);
            wh.WhName = "Склад №2";
            Assert.IsTrue(_svc.EditWh(wh));
        }

        [TestMethod]
        public void DelWhTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            _svc.AddWh(wh);
            Assert.IsTrue(_svc.DelWh(wh));
            Assert.AreEqual(0, _svc.GetWarehouses().Count);
        }

        [TestMethod]
        public void GetContextTest()
        {
            Assert.IsInstanceOfType(_svc.GetContext(), typeof(ApplicationContext));
            Assert.AreSame(_context, _svc.GetContext());
        }

        [TestMethod]
        public void GetOrgTest()
        {
            Assert.IsNotNull(_svc.GetOrg());
            Assert.AreSame(_org, _svc.GetOrg());
        }

        [TestMethod]
        public void GetCurrentWarehouseProducts_NoWarehouseSet_ReturnsEmptyList()
        {
            var products = _svc.GetCurrentWarehouseProducts();
            Assert.IsNotNull(products);
            Assert.AreEqual(0, products.Count);
        }

        [TestMethod]
        public void SetCurrentWarehouseAndGetProductsTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "Артикул-1", Name = "Товар А", Price = 100m, StockQuantity = 10 };
            wh.products.Add(product);

            _svc.SetCurrentWarehouse(wh);
            var products = _svc.GetCurrentWarehouseProducts();

            Assert.AreEqual(1, products.Count);
            Assert.AreSame(product, products[0]);
        }

        [TestMethod]
        public void DeleteTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            _svc.AddWh(wh);
            Assert.IsTrue(_svc.Delete(wh));
            Assert.AreEqual(0, _svc.GetWarehouses().Count);
        }

        [TestMethod]
        public void AddProductTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "Артикул-1", Name = "Товар А", Price = 100m, StockQuantity = 10 };

            Assert.IsTrue(_svc.AddProduct(wh, product));
            Assert.AreEqual(1, wh.products.Count);
        }

        [TestMethod]
        public void AddProduct_NullArguments_ReturnsFalse()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "А-1", Name = "Товар", Price = 10m, StockQuantity = 1 };

            Assert.IsFalse(_svc.AddProduct(null!, product));
            Assert.IsFalse(_svc.AddProduct(wh, null!));
        }

        [TestMethod]
        public void EditProductTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product original = new Product { Article = "Артикул-1", Name = "Товар А", Price = 100m, StockQuantity = 10 };
            wh.products.Add(original);

            Product updated = new Product { Article = "Артикул-1", Name = "Товар Б", Price = 200m, StockQuantity = 5 };

            Assert.IsTrue(_svc.EditProduct(wh, updated));
            Assert.AreEqual("Товар Б", original.Name);
            Assert.AreEqual(200m, original.Price);
            Assert.AreEqual(5, original.StockQuantity);
        }

        [TestMethod]
        public void EditProduct_NotFound_ReturnsFalse()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "Несуществующий", Name = "Товар X", Price = 50m, StockQuantity = 3 };

            Assert.IsFalse(_svc.EditProduct(wh, product));
        }

        [TestMethod]
        public void RemoveProductTest()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "Артикул-1", Name = "Товар А", Price = 100m, StockQuantity = 10 };
            wh.products.Add(product);

            Assert.IsTrue(_svc.RemoveProduct(wh, product));
            Assert.AreEqual(0, wh.products.Count);
        }

        [TestMethod]
        public void RemoveProduct_NullArguments_ReturnsFalse()
        {
            Warehouse wh = new Warehouse("Склад №1", "ул. Ленина, 1", _org.OrgId);
            Product product = new Product { Article = "А-1", Name = "Товар", Price = 10m, StockQuantity = 1 };

            Assert.IsFalse(_svc.RemoveProduct(null!, product));
            Assert.IsFalse(_svc.RemoveProduct(wh, null!));
        }
    }
}