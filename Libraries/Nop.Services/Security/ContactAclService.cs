using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;

namespace Nop.Services.Security
{
    public class ContactAclService : IContactAclService
    {
        private readonly StandardPermissionProvider _standardPermissionProvider = new StandardPermissionProvider();
        private readonly IRepository<CustomerCustomerRoleMapping> _customerCustomerRoleMappingRepository;
        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly IRepository<PermissionRecordCustomerRoleMapping> _permissionRecordCustomerRoleMappingRepository;
        private readonly IWorkContext _workContext;

        public ContactAclService(
            IRepository<CustomerCustomerRoleMapping> customerCustomerRoleMappingRepository,
            IRepository<PermissionRecord> permissionRecordRepository,
            IRepository<PermissionRecordCustomerRoleMapping> permissionRecordCustomerRoleMappingRepository,
             IWorkContext workContext)
        {
            _customerCustomerRoleMappingRepository = customerCustomerRoleMappingRepository;
            _permissionRecordRepository = permissionRecordRepository;
            _permissionRecordCustomerRoleMappingRepository = permissionRecordCustomerRoleMappingRepository;
            _workContext = workContext;
        }
        public IList<PermissionRecord> GetContactPermissionRecords(Customer customer)
        {
            var query = from customerRole in _customerCustomerRoleMappingRepository.Table
                        join permissionRole in _permissionRecordCustomerRoleMappingRepository.Table on customerRole.CustomerRoleId equals permissionRole.CustomerRoleId
                        join permissionRecord in _permissionRecordRepository.Table on permissionRole.PermissionRecordId equals permissionRecord.Id
                        where customerRole.CustomerId == (customer ?? _workContext.CurrentCustomer).Id
                        select permissionRecord;
            return query.ToList();
        }

        public IList<ContactRolePermission> GetContactPermissionRecords(List<int> roleIDs)
        {
            var query = from permissionRole in _permissionRecordCustomerRoleMappingRepository.Table
                        join permissionRecord in _permissionRecordRepository.Table on permissionRole.PermissionRecordId equals permissionRecord.Id
                        where roleIDs.Contains(permissionRole.CustomerRoleId)
                        select new ContactRolePermission { RoleName = permissionRole.CustomerRole.SystemName, PermissionName = permissionRecord.SystemName };
            return query.ToList();
        }
    }
    public class ContactRolePermission
    {
        public string RoleName { get; set; }
        public string PermissionName { get; set; }
    }
}
