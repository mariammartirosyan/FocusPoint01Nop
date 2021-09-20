using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.Security;

namespace Nop.Web.Areas.Admin.Models.Customers
{
    public class CustomersAndRolePermissions
    {
        public CustomerSearchModel CustomerSearchModel { get; set; }
        public CustomerRolePermissionsModel CustomerRolePermissionsModel { get; set; }

    }
    public class CustomerRolePermissionsModel
    {

        public IList<ContactRolePermission> contactRolePermissionsList { get; set; }
    }


}
