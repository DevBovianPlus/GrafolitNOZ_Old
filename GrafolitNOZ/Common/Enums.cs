using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Common
{
    public class Enums
    {
        public enum UserAction : int
        {
            Add = 1,
            Edit = 2,
            Delete = 3
        }

        public enum UserRole
        {
            SuperAdmin,
            Admin,
            Leader,
            Warehouse,
            User,
            Logistics,
            None
        }

        public enum CommonSession
        {
            ShowWarning,
            ShowWarningMessage,
            UserActionPopUp,
            UserActionNestedPopUp,
            activeTab,
            PrintModel,
            PreviousPageName,
            PreviousPageSessions,
            DecodedQueryString,
            StayOnFormAndOpenPopup,
            SearchString
        }

        public enum QueryStringName
        {
            action,
            recordId,
            printReport,
            printId,
            showPreviewReport,
            filter,
            id,
            submitInquiry,
            submitOrder
        }

        public enum PreviousPage
        {
            Orders,
            Recalls,
            Tender,
            SendTender
        }

        public enum Cookies
        {
            UserLastRequest,
            SessionExpires
        }

        public enum CookieCommonValue
        {
            STOP
        }

        public enum CustomDisplayText
        {
            DA,
            NE
        }

        public enum UnitsFromOrder
        {
            KOS,
            KG
        }

        public enum TicketSession
        {
            TicketModel
        }

        public enum ClientSession
        {
            ClientID,
            ClientFullModel,
            ContactPersonID,
            ContactPersonModel
        }
       
        public enum EmployeeSession
        {
            EmployeeID,
            EmployeeModel
        }

        public enum ReportContentType
        {
            GREETINGS = 1,
            REGARDS = 2,
            INQUIRY = 3,
            MATERIAL = 4,
            QUANTITY = 5,
            NOTES
        }

        public enum OptimalStockOrderSession
        {
            OptimalStockTree,
            OptimalStockTreeWithProducts,
            OptimalStockTreeWithProductsNoSupplier,
            SupplierListModel,
            OptimalStockTreeWithProductsFilterBySupplier,
            OptimalStockOrderModel,
            OptimalStockStatuses,
            MainProductPopup,
            SelectedChildProducts,
        }
    }
}