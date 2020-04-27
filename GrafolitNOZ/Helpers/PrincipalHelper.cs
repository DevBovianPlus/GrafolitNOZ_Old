using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers
{
    public class PrincipalHelper
    {
        public static UserPrincipal GetUserPrincipal()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return (UserPrincipal)HttpContext.Current.User;
            }

            return null;
        }

        public static bool IsUserSuperAdmin()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.SuperAdmin.ToString());
        }

        public static bool IsUserAdmin()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.Admin.ToString());
        }

        public static bool IsUserLeader()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.Leader.ToString());
        }

        public static bool IsUserWarehouseKeeper()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.Warehouse.ToString());
        }

        public static bool IsUserUser()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.User.ToString());
        }

        public static bool IsUserLogistics()
        {
            return GetUserPrincipal().IsInRole(Enums.UserRole.Logistics.ToString());
        }
    }
}