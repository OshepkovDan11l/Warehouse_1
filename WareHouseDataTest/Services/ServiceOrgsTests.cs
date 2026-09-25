using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseData.Context;
using WarehouseData.Models;

namespace WareHouseDataTest.Services
{
    [TestClass]
    public class ServiceOrgsTests
    {
        [TestMethod]
        public void ServiceOrgsTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.IsNotNull(svc);
            Assert.IsInstanceOfType(svc, typeof(ServiceOrgs));
        }

        [TestMethod]
        public void AddOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Organization org = new Organization("Всякая всячина, ООО");
            Assert.IsNotNull(org);
            Assert.IsTrue(svc.AddOrg(org));
            Assert.AreEqual(3, svc.GetContext().orgs.Count());
        }

        [TestMethod]
        public void AddOrg_Null_ReturnsFalse()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.IsFalse(svc.AddOrg(null!));
        }

        [TestMethod]
        public void AddOrg_EmptyName_ReturnsFalse()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Organization org = new Organization(" ");
            Assert.IsFalse(svc.AddOrg(org));
        }

        [TestMethod]
        public void EditOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Organization org = new Organization("Всякая всячина, ООО");
            svc.AddOrg(org);
            Organization? org1 = svc.GetContext()
                .orgs.FirstOrDefault(o => o.OrgName == "Всякая всячина, ООО");
            Assert.IsNotNull(org1);
            org1.OrgName = "Разные разности";
            Assert.IsTrue(svc.EditOrg(org1));
        }

        [TestMethod]
        public void EditOrg_Null_ReturnsFalse()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.IsFalse(svc.EditOrg(null!));
        }

        [TestMethod]
        public void EditOrg_NotFound_ReturnsFalse()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Organization org = new Organization("Несуществующая");
            Assert.IsFalse(svc.EditOrg(org));
        }

        [TestMethod]
        public void DelOrgTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Organization org = new Organization("Всякая всячина, ООО");
            svc.AddOrg(org);
            Organization org1 = svc.GetContext().orgs.First();
            Assert.IsNotNull(org1);
            Assert.IsTrue(svc.DelOrg(org1));
            Assert.AreEqual(2, svc.GetContext().orgs.Count());
        }

        [TestMethod]
        public void DelOrg_Null_ReturnsFalse()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.IsFalse(svc.DelOrg(null!));
        }

        [TestMethod]
        public void GetContextTest()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.IsInstanceOfType(svc.GetContext(), typeof(ApplicationContext));
        }

        [TestMethod]
        public void GetContext_ReturnsSameInstance()
        {
            ApplicationContext context = new ApplicationContext();
            ServiceOrgs svc = new ServiceOrgs(context);
            Assert.AreSame(context, svc.GetContext());
        }
    }
}