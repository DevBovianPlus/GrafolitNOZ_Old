using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace GrafolitNOZ.Helpers
{
    public static class WebServiceHelper
    {
        private static string BaseWebServiceURI
        {
            get
            {
                return WebConfigurationManager.AppSettings["BaseWebService"].ToString();
            }
        }

        private static string WebServiceSignInURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["ValuesController"].ToString();
            }
        }

        private static string WebServiceClientURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["ClientController"].ToString();
            }
        }

        private static string WebServiceEmployeeURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["EmployeeController"].ToString();
            }
        }

        private static string WebServiceDashboardURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["DashboardController"].ToString();
            }
        }

        private static string WebServiceOptimalStockOrderURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["OptimalStockOrderController"].ToString();
            }
        }

        private static string WebServiceSettingsURL
        {
            get
            {
                return BaseWebServiceURI + WebConfigurationManager.AppSettings["SettingsController"].ToString();
            }
        }

        public static string SignIn(string username, string pass)
        {
            return WebServiceSignInURL + "SignInNOZ?username=" + username + "&password=" + pass;
        }

        public static string GetWebServiceLogFile()
        {
            return WebServiceSignInURL + "GetWebServiceLogFile";
        }

        public static string GetUtilityServiceLogFile()
        {
            return WebServiceSignInURL + "GetUtilityServiceLogFile";
        }


        #region Client

        public static string GetClientsFromDb()
        {
            return WebServiceClientURL + "GetAllClients";
        }
        public static string GetClientsFromDb(int employeeID)
        {
            return WebServiceClientURL + "GetAllClients?employeeID=" + employeeID.ToString();
        }

        public static string GetClientsFromDb(string typeCode)
        {
            return WebServiceClientURL + "GetAllClients?employeeID=0&typeCode=" + typeCode;
        }

        public static string GetClientsFromDb(int employeeID, string typeCode)
        {
            return WebServiceClientURL + "GetAllClients?employeeID=" + employeeID.ToString() + "&typeCode=" + typeCode;
        }

        public static string GetClientByID(int id)
        {
            return WebServiceClientURL + "GetClientByID?clientID=" + id.ToString();
        }

        public static string GetClientByID(int id, int employeeID)
        {
            return WebServiceClientURL + "GetClientByID?clientID=" + id.ToString() + "&employeeID=" + employeeID.ToString();
        }

        public static string SaveClientDataChanges()
        {
            return WebServiceClientURL + "SaveClientData";
        }

        public static string DeleteClient(int id)
        {
            return WebServiceClientURL + "DeleteClient?clientID=" + id;
        }


        public static string SaveContactPersonChanges()
        {
            return WebServiceClientURL + "SaveContactPersonToClient";
        }

        public static string DeleteContactPerson(int contactPersonID, int clientID)
        {
            return WebServiceClientURL + "DeleteContactPerson?contactPersonID=" + contactPersonID + "&clientID=" + clientID;
        }

        public static string SaveClientEmployeeChanges()
        {
            return WebServiceClientURL + "SaveClientEmployee";
        }

        public static string DeleteClientEmployee(int clientID, int employeeID)
        {
            return WebServiceClientURL + "DeleteClientEmployee?clientID=" + clientID + "&employeeID=" + employeeID;
        }

        public static string ClientEmployeeExist(int clientID, int employeeID)
        {
            return WebServiceClientURL + "ClientEmployeeExist?clientID=" + clientID + "&employeeID=" + employeeID;
        }

        public static string GetClientTypeByID(int id)
        {
            return WebServiceClientURL + "GetClientTypeByCode?id=" + id;
        }

        public static string GetClientTypeByCode(string typeCode)
        {
            return WebServiceClientURL + "GetAllClients?typeCode=" + typeCode;
        }

        public static string GetClientTypes()
        {
            return WebServiceClientURL + "GetClientTypes";
        }

        public static string GetLanguages()
        {
            return WebServiceClientURL + "GetLanguages";
        }

        public static string GetDepartments()
        {
            return WebServiceClientURL + "GetDepartments";
        }


        public static string GetClientTransportTypes()
        {
            return WebServiceClientURL + "GetAllTransportTypes";
        }
        public static string GetClientTransportTypeByID(int id)
        {
            return WebServiceClientURL + "GetTransportTypeByID?transportTypeID=" + id;
        }
        public static string SaveClientTransportType()
        {
            return WebServiceClientURL + "SaveTransportTypeData";
        }
        public static string DeleteClientTransportType(int transportTypeID)
        {
            return WebServiceClientURL + "DeleteTransportType?transportTypeID=" + transportTypeID;
        }

        public static string GetClientByNameOrInsert(string clientName)
        {
            return WebServiceClientURL + "GetClientByNameOrInsert?clientName=" + clientName;
        }

        public static string GetClientByName(string clientName)
        {
            return WebServiceClientURL + "GetClientByName?clientName=" + clientName;
        }

        public static string GetSupplierList()
        {
            return WebServiceClientURL + "GetSupplierList";
        }
        #endregion

        #region Employee

        public static string GetAllEmployees()
        {
            return WebServiceEmployeeURL + "GetAllEmployees";
        }

        public static string GetAllEmployeesByRoleID(int roleID)
        {
            return WebServiceEmployeeURL + "GetAllEmployeesByRoleID?roleID=" + roleID;
        }

        public static string GetEmployeeByID(int employeeId)
        {
            return WebServiceEmployeeURL + "GetEmployeeByID?employeeId=" + employeeId;
        }

        public static string SaveEmployee()
        {
            return WebServiceEmployeeURL + "SaveEmployee";
        }

        public static string GetPantheonUsers()
        {
            return WebServiceEmployeeURL + "GetPantheonUsers";
        }

        public static string DeleteEmployee(int employeeID)
        {
            return WebServiceEmployeeURL + "DeleteEmployee?employeeID=" + employeeID;
        }

        public static string GetRoles()
        {
            return WebServiceEmployeeURL + "GetRoles";
        }
        #endregion

        #region Dashboard

        public static string GetDashboardNOZData()
        {
            return WebServiceDashboardURL + "GetDashboardNOZData";
        }

        #endregion

        #region OptimalStockOrder

        public static string GetOptimalStockOrders()
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockOrders";
        }

        public static string GetOptimalStockOrderByID(int ID)
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockOrderByID?ID=" + ID;
        }

        public static string SaveOptimalStockOrder(bool submitCopiedOrder)
        {
            return WebServiceOptimalStockOrderURL + "SaveOptimalStockOrder?submitCopiedOrder=" + submitCopiedOrder;
        }

        public static string DeleteOptimalStockOrder(int ID)
        {
            return WebServiceOptimalStockOrderURL + "DeleteOptimalStockOrder?ID=" + ID;
        }

        public static string GetOptimalStockOrderPositionsByOrderID(int orderID)
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockOrderPositionsByOrderID?orderID=" + orderID;
        }


        public static string GetOptimalStockOrderPositionByID(int ID)
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockOrderPositionByID?ID=" + ID;
        }


        public static string SaveOptimalStockPositionOrder()
        {
            return WebServiceOptimalStockOrderURL + "SaveOptimalStockPositionOrder";
        }

        public static string DeleteOptimalStockPosition(int ID)
        {
            return WebServiceOptimalStockOrderURL + "DeleteOptimalStockPosition?ID=" + ID;
        }

        public static string GetOptimalStockTree(string productCategory, string color)
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockTree?productCategory=" + productCategory + "&color=" + color;
        }

        public static string GetCategoryList()
        {
            return WebServiceOptimalStockOrderURL + "GetCategoryList";
        }

        public static string GetColorListByCategory(string category)
        {
            return WebServiceOptimalStockOrderURL + "GetColorListByCategory?category=" + category;
        }

        public static string GetProductsForSelectedOptimalStock(string color)
        {
            return WebServiceOptimalStockOrderURL + "GetProductsForSelectedOptimalStock?color=" + color;
        }


        public static string UpdateSubCategoriesWithProductsForSelectedNodes(string color)
        {
            return WebServiceOptimalStockOrderURL + "UpdateSubCategoriesWithProductsForSelectedNodes?color=" + color;
        }



        public static string GetOptimalStockStatuses()
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockStatuses";
        }

        public static string GetOptimalStockStatusByID(int statusID)
        {
            return WebServiceOptimalStockOrderURL + "GetOptimalStockStatusByID?ID=" + statusID;
        }

        public static string CopyOptimalStockOrderByID(int optimalStockOrderID)
        {
            return WebServiceOptimalStockOrderURL + "CopyOptimalStockOrderByID?optimalStockOrderID=" + optimalStockOrderID;
        }

        public static string CreateEmailForUserCreateNewCodeForProduct()
        {
            return WebServiceOptimalStockOrderURL + "CreateEmailForUserCreateNewCodeForProduct";
        }
        #endregion

        #region Settings

        public static string GetSettings()
        {
            return WebServiceSettingsURL + "GetAppSettings";
        }

        public static string GetAllEmailsNOZ()
        {
            return WebServiceSettingsURL + "GetAllEmailsNOZ";
        }

        public static string SaveSettings()
        {
            return WebServiceSettingsURL + "SaveSettings";
        }

        #endregion

        /*#region "Admin"
        public static string CreatePDFAndSendPDOOrdersMultiple()
        {
            return WebServiceOrderPDOURL + "CreatePDFAndSendPDOOrdersMultiple";
        }

        public static string RunPantheon(string sFile, string sArgs)
        {
            return WebServiceOrderPDOURL + "RunPantheon?sFile=" + sFile + "&sArgs=" + sArgs;
        }

        public static string ChangeConfigValue(string sConfigName, string sConfigValue)
        {
            return WebServiceOrderPDOURL + "ChangeConfigValue?sConfigName=" + sConfigName + "&sConfigValue=" + sConfigValue;
        }

        public static string GetConfigValue(string sConfigName)
        {
            return WebServiceOrderPDOURL + "GetConfigValue?sConfigName=" + sConfigName;
        }
        #endregion*/
    }
}