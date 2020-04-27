using DatabaseWebService.Models;
using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsOTP;
using DatabaseWebService.ModelsOTP.Client;
using DatabaseWebService.ModelsOTP.Order;
using DatabaseWebService.ModelsOTP.Recall;
using DatabaseWebService.ModelsOTP.Route;
using DatabaseWebService.ModelsOTP.Tender;
using Newtonsoft.Json;
using GrafolitNOZ.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using DatabaseWebService.ModelsPDO.Inquiry;
using DatabaseWebService.ModelsPDO.Order;
using DatabaseWebService.ModelsPDO;
using DatabaseWebService.ModelsPDO.Settings;
using DatabaseWebService.Models.Employee;
using DatabaseWebService.ModelsNOZ.OptimalStockOrder;
using DatabaseWebService.ModelsNOZ;

namespace GrafolitNOZ.Infrastructure
{
    public class DatabaseConnection
    {
        public WebResponseContentModel<UserModel> SignIn(string username, string password)
        {
            WebResponseContentModel<UserModel> user = new WebResponseContentModel<UserModel>();
            try
            {
                user = GetResponseFromWebRequest<WebResponseContentModel<UserModel>>(WebServiceHelper.SignIn(username, password), "get");
            }
            catch (Exception ex)
            {
                user.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }
            return user;
        }

        public WebResponseContentModel<byte[]> GetWebServiceLogFile()
        {
            WebResponseContentModel<byte[]> user = new WebResponseContentModel<byte[]>();
            try
            {
                user = GetResponseFromWebRequest<WebResponseContentModel<byte[]>>(WebServiceHelper.GetWebServiceLogFile(), "get");
            }
            catch (Exception ex)
            {
                user.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }
            return user;
        }

        public WebResponseContentModel<byte[]> GetUtilityServiceLogFile()
        {
            WebResponseContentModel<byte[]> user = new WebResponseContentModel<byte[]>();
            try
            {
                user = GetResponseFromWebRequest<WebResponseContentModel<byte[]>>(WebServiceHelper.GetUtilityServiceLogFile(), "get");
            }
            catch (Exception ex)
            {
                user.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }
            return user;
        }


        #region Client

        public WebResponseContentModel<List<ClientSimpleModel>> GetAllClients()
        {
            WebResponseContentModel<List<ClientSimpleModel>> dt = new WebResponseContentModel<List<ClientSimpleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientSimpleModel>>>(WebServiceHelper.GetClientsFromDb(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<ClientSimpleModel>> GetAllClients(string typeCode)
        {
            WebResponseContentModel<List<ClientSimpleModel>> dt = new WebResponseContentModel<List<ClientSimpleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientSimpleModel>>>(WebServiceHelper.GetClientsFromDb(typeCode), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<ClientSimpleModel>> GetAllClients(int employeeID)
        {
            WebResponseContentModel<List<ClientSimpleModel>> dt = new WebResponseContentModel<List<ClientSimpleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientSimpleModel>>>(WebServiceHelper.GetClientsFromDb(employeeID), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<ClientSimpleModel>> GetAllClients(int employeeID, string typeCode)
        {
            WebResponseContentModel<List<ClientSimpleModel>> dt = new WebResponseContentModel<List<ClientSimpleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientSimpleModel>>>(WebServiceHelper.GetClientsFromDb(employeeID, typeCode), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<ClientFullModel> GetClient(int clientID)
        {
            WebResponseContentModel<ClientFullModel> client = new WebResponseContentModel<ClientFullModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<ClientFullModel>>(WebServiceHelper.GetClientByID(clientID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<ClientFullModel> GetClient(int clientID, int employeeID)
        {
            WebResponseContentModel<ClientFullModel> client = new WebResponseContentModel<ClientFullModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<ClientFullModel>>(WebServiceHelper.GetClientByID(clientID, employeeID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<ClientFullModel> SaveClientChanges(ClientFullModel newData)
        {
            WebResponseContentModel<ClientFullModel> model = new WebResponseContentModel<ClientFullModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<ClientFullModel>>(WebServiceHelper.SaveClientDataChanges(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteClient(int clientID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteClient(clientID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<ContactPersonModel> SaveContactPersonChanges(ContactPersonModel newData)
        {
            WebResponseContentModel<ContactPersonModel> model = new WebResponseContentModel<ContactPersonModel>();
            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<ContactPersonModel>>(WebServiceHelper.SaveContactPersonChanges(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteContactPerson(int contactPersonID, int clientID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteContactPerson(contactPersonID, clientID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<ClientEmployeeModel> SaveClientEmployeeChanges(ClientEmployeeModel newData)
        {
            WebResponseContentModel<ClientEmployeeModel> model = new WebResponseContentModel<ClientEmployeeModel>();
            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<ClientEmployeeModel>>(WebServiceHelper.SaveClientEmployeeChanges(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteClientEmployee(int clientID, int employeeID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteClientEmployee(clientID, employeeID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> ClientEmployeeExist(int clientID, int employeeID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.ClientEmployeeExist(clientID, employeeID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<ClientType> GetClientTypeByCode(string typeCode)
        {
            WebResponseContentModel<ClientType> dt = new WebResponseContentModel<ClientType>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<ClientType>>(WebServiceHelper.GetClientTypeByCode(typeCode), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<ClientType> GetClientTypeById(int id)
        {
            WebResponseContentModel<ClientType> dt = new WebResponseContentModel<ClientType>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<ClientType>>(WebServiceHelper.GetClientTypeByID(id), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<ClientType>> GetClientTypes()
        {
            WebResponseContentModel<List<ClientType>> dt = new WebResponseContentModel<List<ClientType>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientType>>>(WebServiceHelper.GetClientTypes(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<LanguageModel>> GetLanguages()
        {
            WebResponseContentModel<List<LanguageModel>> dt = new WebResponseContentModel<List<LanguageModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<LanguageModel>>>(WebServiceHelper.GetLanguages(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<DepartmentModel>> GetDepartments()
        {
            WebResponseContentModel<List<DepartmentModel>> dt = new WebResponseContentModel<List<DepartmentModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<DepartmentModel>>>(WebServiceHelper.GetDepartments(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<ClientTransportType>> GetAllTransportTypes()
        {
            WebResponseContentModel<List<ClientTransportType>> dt = new WebResponseContentModel<List<ClientTransportType>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientTransportType>>>(WebServiceHelper.GetClientTransportTypes(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<ClientTransportType> GetTransportTypeByID(int transportTypeID)
        {
            WebResponseContentModel<ClientTransportType> transportType = new WebResponseContentModel<ClientTransportType>();
            try
            {
                transportType = GetResponseFromWebRequest<WebResponseContentModel<ClientTransportType>>(WebServiceHelper.GetClientTransportTypeByID(transportTypeID), "get");
            }
            catch (Exception ex)
            {
                transportType.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return transportType;
        }

        public WebResponseContentModel<ClientTransportType> SaveTransportType(ClientTransportType newData)
        {
            WebResponseContentModel<ClientTransportType> model = new WebResponseContentModel<ClientTransportType>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<ClientTransportType>>(WebServiceHelper.SaveClientTransportType(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteTransportType(int transportTypeID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteClientTransportType(transportTypeID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<ClientFullModel> GetClientByName(string clientName)
        {
            WebResponseContentModel<ClientFullModel> client = new WebResponseContentModel<ClientFullModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<ClientFullModel>>(WebServiceHelper.GetClientByName(clientName), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<List<ClientSimpleModel>> GetSupplierList()
        {
            WebResponseContentModel<List<ClientSimpleModel>> dt = new WebResponseContentModel<List<ClientSimpleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<ClientSimpleModel>>>(WebServiceHelper.GetSupplierList(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<int> GetClientByNameOrInsert(string clientName)
        {
            WebResponseContentModel<int> client = new WebResponseContentModel<int>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<int>>(WebServiceHelper.GetClientByNameOrInsert(clientName), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }
        #endregion

        #region Employee

        public WebResponseContentModel<List<EmployeeFullModel>> GetAllEmployees()
        {
            WebResponseContentModel<List<EmployeeFullModel>> dt = new WebResponseContentModel<List<EmployeeFullModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<EmployeeFullModel>>>(WebServiceHelper.GetAllEmployees(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<List<EmployeeFullModel>> GetAllEmployeesByRoleID(int roleID)
        {
            WebResponseContentModel<List<EmployeeFullModel>> dt = new WebResponseContentModel<List<EmployeeFullModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<EmployeeFullModel>>>(WebServiceHelper.GetAllEmployeesByRoleID(roleID), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<EmployeeFullModel> GetEmployeeByID(int employeeID)
        {
            WebResponseContentModel<EmployeeFullModel> dt = new WebResponseContentModel<EmployeeFullModel>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<EmployeeFullModel>>(WebServiceHelper.GetEmployeeByID(employeeID), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<EmployeeFullModel> SaveEmployee(EmployeeFullModel newData)
        {
            WebResponseContentModel<EmployeeFullModel> model = new WebResponseContentModel<EmployeeFullModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<EmployeeFullModel>>(WebServiceHelper.SaveEmployee(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<List<PantheonUsers>> GetPantheonUsers()
        {
            WebResponseContentModel<List<PantheonUsers>> client = new WebResponseContentModel<List<PantheonUsers>>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<List<PantheonUsers>>>(WebServiceHelper.GetPantheonUsers(), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<bool> DeleteEmployee(int employeeID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteEmployee(employeeID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<List<RoleModel>> GetRoles()
        {
            WebResponseContentModel<List<RoleModel>> dt = new WebResponseContentModel<List<RoleModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<RoleModel>>>(WebServiceHelper.GetRoles(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }
        #endregion

        #region Dashboard

        public WebResponseContentModel<DashboardNOZModel> GetDashboardPDOData()
        {
            WebResponseContentModel<DashboardNOZModel> dt = new WebResponseContentModel<DashboardNOZModel>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<DashboardNOZModel>>(WebServiceHelper.GetDashboardNOZData(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        #endregion

        #region OptimalStockOrder

        public WebResponseContentModel<List<OptimalStockOrderModel>> GetOptimalStockOrders()
        {
            WebResponseContentModel<List<OptimalStockOrderModel>> dt = new WebResponseContentModel<List<OptimalStockOrderModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<OptimalStockOrderModel>>>(WebServiceHelper.GetOptimalStockOrders(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<OptimalStockOrderModel> GetOptimalStockOrderByID(int ID)
        {
            WebResponseContentModel<OptimalStockOrderModel> client = new WebResponseContentModel<OptimalStockOrderModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<OptimalStockOrderModel>>(WebServiceHelper.GetOptimalStockOrderByID(ID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<OptimalStockOrderModel> SaveOptimalStockOrder(OptimalStockOrderModel newData, bool submitCopiedOrder)
        {
            WebResponseContentModel<OptimalStockOrderModel> model = new WebResponseContentModel<OptimalStockOrderModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<OptimalStockOrderModel>>(WebServiceHelper.SaveOptimalStockOrder(submitCopiedOrder), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteOptimalStockOrder(int ID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteOptimalStockOrder(ID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<OptimalStockOrderPositionModel> GetOptimalStockOrderPositionsByOrderID(int orderID)
        {
            WebResponseContentModel<OptimalStockOrderPositionModel> client = new WebResponseContentModel<OptimalStockOrderPositionModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<OptimalStockOrderPositionModel>>(WebServiceHelper.GetOptimalStockOrderPositionsByOrderID(orderID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<OptimalStockOrderPositionModel> GetOptimalStockOrderPositionByID(int ID)
        {
            WebResponseContentModel<OptimalStockOrderPositionModel> client = new WebResponseContentModel<OptimalStockOrderPositionModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<OptimalStockOrderPositionModel>>(WebServiceHelper.GetOptimalStockOrderPositionByID(ID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<OptimalStockOrderPositionModel> SaveOptimalStockPositionOrder(OptimalStockOrderPositionModel newData)
        {
            WebResponseContentModel<OptimalStockOrderPositionModel> model = new WebResponseContentModel<OptimalStockOrderPositionModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<OptimalStockOrderPositionModel>>(WebServiceHelper.SaveOptimalStockPositionOrder(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> DeleteOptimalStockPosition(int ID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.DeleteOptimalStockPosition(ID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<List<ProductCategory>> GetCategoryList()
        {
            WebResponseContentModel<List<ProductCategory>> client = new WebResponseContentModel<List<ProductCategory>>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<List<ProductCategory>>>(WebServiceHelper.GetCategoryList(), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<List<OptimalStockTreeHierarchy>> GetOptimalStockTree(string productCategory, string color)
        {
            WebResponseContentModel<List<OptimalStockTreeHierarchy>> unLockInquiry = new WebResponseContentModel<List<OptimalStockTreeHierarchy>>();
            try
            {
                unLockInquiry = GetResponseFromWebRequest<WebResponseContentModel<List<OptimalStockTreeHierarchy>>>(WebServiceHelper.GetOptimalStockTree(productCategory, color), "get");
            }
            catch (Exception ex)
            {
                unLockInquiry.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return unLockInquiry;
        }

        public WebResponseContentModel<List<ProductColor>> GetColorListByCategory(string category)
        {
            WebResponseContentModel<List<ProductColor>> client = new WebResponseContentModel<List<ProductColor>>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<List<ProductColor>>>(WebServiceHelper.GetColorListByCategory(category), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<hlpOptimalStockOrderModel> GetProductsForSelectedOptimalStock(string color, hlpOptimalStockOrderModel newData)
        {
            WebResponseContentModel<hlpOptimalStockOrderModel> model = new WebResponseContentModel<hlpOptimalStockOrderModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<hlpOptimalStockOrderModel>>(WebServiceHelper.GetProductsForSelectedOptimalStock(color), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<List<OptimalStockOrderStatusModel>> GetOptimalStockStatuses()
        {
            WebResponseContentModel<List<OptimalStockOrderStatusModel>> dt = new WebResponseContentModel<List<OptimalStockOrderStatusModel>>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<List<OptimalStockOrderStatusModel>>>(WebServiceHelper.GetOptimalStockStatuses(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<OptimalStockOrderStatusModel> GetOptimalStockStatusByID(int statusID)
        {
            WebResponseContentModel<OptimalStockOrderStatusModel> client = new WebResponseContentModel<OptimalStockOrderStatusModel>();
            try
            {
                client = GetResponseFromWebRequest<WebResponseContentModel<OptimalStockOrderStatusModel>>(WebServiceHelper.GetOptimalStockStatusByID(statusID), "get");
            }
            catch (Exception ex)
            {
                client.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return client;
        }

        public WebResponseContentModel<bool> CopyOptimalStockOrderByID(int optimalStockOrderID)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.CopyOptimalStockOrderByID(optimalStockOrderID), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }
        #endregion

        #region Settings

        public WebResponseContentModel<SettingsModel> GetSettings()
        {
            WebResponseContentModel<SettingsModel> settings = new WebResponseContentModel<SettingsModel>();
            try
            {
                settings = GetResponseFromWebRequest<WebResponseContentModel<SettingsModel>>(WebServiceHelper.GetSettings(), "get");
            }
            catch (Exception ex)
            {
                settings.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return settings;
        }

        public WebResponseContentModel<SettingsModel> SaveSettings(SettingsModel newData)
        {
            WebResponseContentModel<SettingsModel> model = new WebResponseContentModel<SettingsModel>();

            try
            {
                model.Content = newData;
                model = PostWebRequestData<WebResponseContentModel<SettingsModel>>(WebServiceHelper.SaveSettings(), "post", model);
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        #endregion

        /*#region "Admin"
        public WebResponseContentModel<bool> CreatePDFAndSendPDOOrdersMultiple()
        {
            WebResponseContentModel<bool> dt = new WebResponseContentModel<bool>();
            try
            {
                dt = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.CreatePDFAndSendPDOOrdersMultiple(), "get");
            }
            catch (Exception ex)
            {
                dt.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return dt;
        }

        public WebResponseContentModel<bool> RunPantheon(string sFile, string sArgs)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.RunPantheon(sFile, sArgs), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<bool> ChangeConfigValue(string sConfigName, string sConfigValue)
        {
            WebResponseContentModel<bool> model = new WebResponseContentModel<bool>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<bool>>(WebServiceHelper.ChangeConfigValue(sConfigName, sConfigValue), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        public WebResponseContentModel<string> GetConfigValue(string sConfigName)
        {
            WebResponseContentModel<string> model = new WebResponseContentModel<string>();

            try
            {
                model = GetResponseFromWebRequest<WebResponseContentModel<string>>(WebServiceHelper.GetConfigValue(sConfigName), "get");
            }
            catch (Exception ex)
            {
                model.ValidationErrorAppSide = ConcatenateExceptionMessage(ex);
            }

            return model;
        }

        #endregion*/

        public T GetResponseFromWebRequest<T>(string uri, string requestMethod)
        {
            object obj = default(T);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = requestMethod.ToUpper();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string streamString = reader.ReadToEnd();

            obj = JsonConvert.DeserializeObject<T>(streamString);

            return (T)obj;
        }

        public T PostWebRequestData<T>(string uri, string requestMethod, T objectToSerialize)
        {
            object obj = default(T);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = requestMethod.ToUpper();
            request.ContentType = "application/json; charset=utf-8";

            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                string clientData = JsonConvert.SerializeObject(objectToSerialize);
                sw.Write(clientData);
                sw.Flush();
                sw.Close();
            }


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string streamString = reader.ReadToEnd();

            obj = JsonConvert.DeserializeObject<T>(streamString);

            return (T)obj;
        }

        private string ConcatenateExceptionMessage(Exception ex)
        {
            return ex.Message + " \r\n" + ex.Source + (ex.InnerException != null ? ex.InnerException.Message + " \r\n" + ex.Source : "");
        }

        public async Task<T> PostWebRequestDataAsync<T>(string uri, string requestMethod, T objectToSerialize)
        {
            object obj = default(T);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = requestMethod.ToUpper();
            request.ContentType = "application/json; charset=utf-8";

            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                string clientData = JsonConvert.SerializeObject(objectToSerialize);
                sw.Write(clientData);
                sw.Flush();
                sw.Close();
            }

            var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            // HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string streamString = reader.ReadToEnd();

            obj = JsonConvert.DeserializeObject<T>(streamString);

            return (T)obj;

        }

        public T PostWebRequestData2<T>(string uri, string requestMethod, T objectToSerialize)
        {
            object obj = default(T);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = requestMethod.ToUpper();
            request.ContentType = "application/json; charset=utf-8";

            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                string clientData = JsonConvert.SerializeObject(objectToSerialize);
                sw.Write(clientData);
                sw.Flush();
                sw.Close();
            }

            var response = (HttpWebResponse)WebRequestExtensions.GetResponseWithTimeout(request, 36000);

            // HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string streamString = reader.ReadToEnd();

            obj = JsonConvert.DeserializeObject<T>(streamString);

            return (T)obj;

        }
    }

    public static class WebRequestExtensions
    {
        public static Stream GetRequestStreamWithTimeout(
            this WebRequest request,
            int? millisecondsTimeout = null)
        {
            return AsyncToSyncWithTimeout(
                request.BeginGetRequestStream,
                request.EndGetRequestStream,
                millisecondsTimeout ?? request.Timeout);
        }

        public static WebResponse GetResponseWithTimeout(
            this HttpWebRequest request,
            int? millisecondsTimeout = null)
        {
            return AsyncToSyncWithTimeout(
                request.BeginGetResponse,
                request.EndGetResponse,
                millisecondsTimeout ?? request.Timeout);
        }

        private static T AsyncToSyncWithTimeout<T>(
            Func<AsyncCallback, object, IAsyncResult> begin,
            Func<IAsyncResult, T> end,
            int millisecondsTimeout)
        {
            var iar = begin(null, null);
            if (!iar.AsyncWaitHandle.WaitOne(millisecondsTimeout))
            {
                var ex = new TimeoutException();
                throw new WebException(ex.Message, ex, WebExceptionStatus.Timeout, null);
            }
            return end(iar);
        }
    }
}