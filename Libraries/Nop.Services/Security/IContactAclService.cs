using System;
using System.Collections.Generic;
using System.Text;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;

namespace Nop.Services.Security
{
    public interface IContactAclService
    {
        IList<PermissionRecord> GetContactPermissionRecords(Customer customer);
        IList<ContactRolePermission> GetContactPermissionRecords(List<int> roleIDs);
    }
}
