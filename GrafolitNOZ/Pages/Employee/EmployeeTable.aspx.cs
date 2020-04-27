using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseWebService.ModelsPDO;
using DevExpress.Web;
using DatabaseWebService.ModelsPDO.Inquiry;
using GrafolitNOZ.Helpers;
using DatabaseWebService.Models;

namespace GrafolitNOZ.Pages.Employee
{
    public partial class EmployeeTable : ServerMasterPage
    {
        int employeeIDFocusedRowIndex = 0;
        int filterType = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!PrincipalHelper.IsUserAdmin() && !PrincipalHelper.IsUserSuperAdmin()) RedirectHome();

            if (!Request.IsAuthenticated) RedirectHome();

            this.Master.PageHeadlineTitle = Title;

            if (Request.QueryString[Enums.QueryStringName.recordId.ToString()] != null)
                employeeIDFocusedRowIndex = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.recordId.ToString()].ToString());

            if (Request.QueryString[Enums.QueryStringName.filter.ToString()] != null)
                filterType = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.filter.ToString()].ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (employeeIDFocusedRowIndex > 0)
                {
                    ASPxGridViewEmployee.FocusedRowIndex = ASPxGridViewEmployee.FindVisibleIndexByKeyValue(employeeIDFocusedRowIndex);
                    ASPxGridViewEmployee.ScrollToVisibleIndexOnClient = ASPxGridViewEmployee.FindVisibleIndexByKeyValue(employeeIDFocusedRowIndex);
                }

                ASPxGridViewEmployee.DataBind();
                InitializeEditDeleteButtons();
            }
        }

        protected void ASPxGridViewEmployee_DataBinding(object sender, EventArgs e)
        {
            List<EmployeeFullModel> list = CheckModelValidation(GetDatabaseConnectionInstance().GetAllEmployees());

            (sender as ASPxGridView).DataSource = list;
            (sender as ASPxGridView).Settings.GridLines = GridLines.Both;
        }

        private void InitializeEditDeleteButtons()
        {
            if (ASPxGridViewEmployee.VisibleRowCount <= 0)
            {
                btnEdit.ClientVisible = false;
                btnDelete.ClientVisible = false;
            }
        }

        protected void EmployeeCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            object valueID = null;

            ClearAllSessions(Enum.GetValues(typeof(Enums.EmployeeSession)).Cast<Enums.EmployeeSession>().ToList());

            if (CommonMethods.ParseInt(e.Parameter) != (int)Enums.UserAction.Add)
                valueID = ASPxGridViewEmployee.GetRowValues(ASPxGridViewEmployee.FocusedRowIndex, "idOsebe");
            
            
            
            bool isValid = SetSessionsAndOpenPopUp(e.Parameter, Enums.EmployeeSession.EmployeeID, valueID);

            PopupControlEmployee.ShowOnPageLoad = isValid;

        }

        protected void PopupControlEmployee_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            RemoveSession(Enums.CommonSession.UserActionPopUp);
            RemoveSession(Enums.EmployeeSession.EmployeeID);
            RemoveSession(Enums.EmployeeSession.EmployeeModel);
        }
    }
}