using Newtonsoft.Json;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseWebService.ModelsNOZ;

namespace GrafolitNOZ
{
    public partial class Home : System.Web.UI.Page
    {
        DatabaseConnection dbConn;

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            if (Request.IsAuthenticated)
            {
                MasterPageFile = "~/Main.Master";
                dbConn = new DatabaseConnection();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                ASPxFormLayoutLogin.Visible = false;
                FormLayoutWrap.Style.Add("display", "none");
                MainDashboard.Style.Add("display", "block");
                this.Master.PageHeadlineTitle = "NOZ - Naročilo optimalnih zalog";

                DashboardNOZModel data = dbConn.GetDashboardPDOData().Content;
                if (data != null)
                {
                    lblAllOrders.Text = data.OrderCount.ToString();
                    lblSubmitedOrders.Text = data.SubmitedOrders.ToString();
                    lblCreatedOrder.Text = data.CreatedOrders.ToString();
                }
            }
        }

        protected void LoginCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            Authentication auth = new Authentication();
            bool signInSuccess = false;
            string message = "";
            string username = CommonMethods.Trim(txtUsername.Text);
            string password = CommonMethods.Trim(txtPassword.Text);

            try
            {
                if (username != "" && password != "")
                {
                    CommonMethods.LogThis("Before Authenticate");
                    signInSuccess = auth.Authenticate(username, password);
                    CommonMethods.LogThis("After Authenticate");
                }
            }
            catch (Exception ex)
            {
                CommonMethods.LogThis(ex.Message);
                message = ex.Message;
            }

            if (signInSuccess)
            {
                Session.Remove("PreviousPage");
            }
            else
            {
                LoginCallback.JSProperties["cpResult"] = message;
            }
        }

        protected void ChartsCallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            if (e.Parameter == "RefreshCharts" && Request.IsAuthenticated)
            {
                DashboardNOZModel data = dbConn.GetDashboardPDOData().Content;

                ChartsCallbackPanel.JSProperties["cpChartData"] = JsonConvert.SerializeObject(data.CurrentYearOrder);
                ChartsCallbackPanel.JSProperties["cpChartDataEmployees"] = JsonConvert.SerializeObject(data.EmployeesOrderCount);
                /*ChartsCallbackPanel.JSProperties["cpChartDataTransporters"] = JsonConvert.SerializeObject(data.TransporterRecallCount);
                ChartsCallbackPanel.JSProperties["cpChartDataRoutes"] = JsonConvert.SerializeObject(data.RouteRecallCount);
                ChartsCallbackPanel.JSProperties["cpChartDataSupplier"] = JsonConvert.SerializeObject(data.SupplierRecallCount);*/
            }
        }
    }
}