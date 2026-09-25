using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseData.Models;
using WarehouseData.Services;
using System.Numerics;

namespace WareHouseDataTest.Services
{
    [TestClass]
    public class ServiceInvoiceTests
    {
        private Warehouse _warehouse = null!;
        private ServiceInvoice _svc = null!;
        private Product _product = null!;
        private ServiceWhs _serviceWhs = null!;

        [TestInitialize]
        public void Setup()
        {
            _warehouse = new Warehouse("Склад №1", "ул. Ленина, 1", BigInteger.One);
            _product = new Product { Article = "АРТ-001", Name = "Товар 1", StockQuantity = 10 };
            _warehouse.products.Add(_product);

            var org = new Organization("Тест");
            org.warehouses.Add(_warehouse);

            var context = new WarehouseData.Context.ApplicationContext();
            _serviceWhs = new ServiceWhs(context, org);
            _serviceWhs.SetCurrentWarehouse(_warehouse);

            _svc = new ServiceInvoice(_serviceWhs);
        }

        [TestMethod]
        public void AddTest()
        {
            Invoice invoice = new Invoice { Number = "№1" };
            Assert.IsTrue(_svc.Add(invoice));
            Assert.AreEqual(1, _svc.GetInvoices().Count());
        }

        [TestMethod]
        public void Add_Null_ReturnsFalse()
        {
            Assert.IsFalse(_svc.Add(null!));
        }

        [TestMethod]
        public void DeleteTest()
        {
            Invoice invoice = new Invoice { Number = "№1" };
            _svc.Add(invoice);
            Assert.IsTrue(_svc.Delete(invoice));
            Assert.AreEqual(0, _svc.GetInvoices().Count());
        }

        [TestMethod]
        public void Delete_Confirmed_ReturnsFalse()
        {
            Invoice invoice = new Invoice { Number = "№1", IsConfirmed = true };
            _svc.Add(invoice);
            Assert.IsFalse(_svc.Delete(invoice));
        }

        [TestMethod]
        public void Delete_Null_ReturnsFalse()
        {
            Assert.IsFalse(_svc.Delete(null!));
        }

        [TestMethod]
        public void Confirm_Incoming_IncreasesStock()
        {
            Invoice invoice = new Invoice
            {
                Number = "№1",
                Type = InvoiceType.Incoming,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = _product, Quantity = 5 }
                }
            };
            _svc.Add(invoice);
            Assert.IsTrue(_svc.Confirm(invoice));
            Assert.IsTrue(invoice.IsConfirmed);
            Assert.AreEqual(15, _product.StockQuantity);
        }

        [TestMethod]
        public void Confirm_Outgoing_DecreasesStock()
        {
            Invoice invoice = new Invoice
            {
                Number = "№1",
                Type = InvoiceType.Outgoing,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = _product, Quantity = 3 }
                }
            };
            _svc.Add(invoice);
            Assert.IsTrue(_svc.Confirm(invoice));
            Assert.AreEqual(7, _product.StockQuantity);
        }

        [TestMethod]
        public void Confirm_AlreadyConfirmed_ReturnsFalse()
        {
            Invoice invoice = new Invoice { Number = "№1", IsConfirmed = true };
            _svc.Add(invoice);
            Assert.IsFalse(_svc.Confirm(invoice));
        }

        [TestMethod]
        public void Confirm_Null_ReturnsFalse()
        {
            Assert.IsFalse(_svc.Confirm(null!));
        }

        [TestMethod]
        public void Confirm_ItemWithNullProduct_SkipsAndReturnsTrue()
        {
            Invoice invoice = new Invoice
            {
                Number = "№1",
                Type = InvoiceType.Incoming,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = null!, Quantity = 5 }
                }
            };
            _svc.Add(invoice);
            Assert.IsTrue(_svc.Confirm(invoice));
            Assert.AreEqual(10, _product.StockQuantity);
        }

        [TestMethod]
        public void Confirm_ItemProductNotInWarehouse_SkipsAndReturnsTrue()
        {
            Product unknown = new Product { Article = "НЕТ-999", Name = "Чужой товар", StockQuantity = 0 };
            Invoice invoice = new Invoice
            {
                Number = "№1",
                Type = InvoiceType.Incoming,
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Product = unknown, Quantity = 5 }
                }
            };
            _svc.Add(invoice);
            Assert.IsTrue(_svc.Confirm(invoice));
            Assert.AreEqual(10, _product.StockQuantity);
        }
    }
}