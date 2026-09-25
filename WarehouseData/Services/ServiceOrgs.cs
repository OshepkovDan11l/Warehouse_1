using WarehouseData.Context;
using WarehouseData.Models;

namespace WarehouseData.Services
{
    public class ServiceOrgs
    {
        private ApplicationContext _context;

        public ServiceOrgs(ApplicationContext context)
        {
            _context = context;
        }

        public ApplicationContext GetContext() => _context;

        public bool AddOrg(Organization org)
        {
            if (org == null || string.IsNullOrWhiteSpace(org.OrgName)) return false;
            _context.orgs.Add(org);
            return true;
        }

        public bool EditOrg(Organization org)
        {
            if (org == null || string.IsNullOrWhiteSpace(org.OrgName)) return false;
            var existing = _context.orgs.FirstOrDefault(o => o.OrgId == org.OrgId);
            if (existing == null) return false;
            existing.OrgName = org.OrgName;
            return true;
        }

        public bool DelOrg(Organization org)
        {
            if (org == null) return false;
            return _context.orgs.Remove(org);
        }
    }
}