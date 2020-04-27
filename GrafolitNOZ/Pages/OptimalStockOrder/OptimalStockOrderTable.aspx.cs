using DevExpress.Web;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class OptimalStockOrderTable : ServerMasterPage
    {
        int optimalStockOrderIDFocusedRowIndex = 0;
        int filterType = 0;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Master.PageHeadlineTitle = this.Title;

            ASPxGridViewOptimalStockOrder.Settings.GridLines = GridLines.Both;


            if (Request.QueryString[Enums.QueryStringName.recordId.ToString()] != null)
                optimalStockOrderIDFocusedRowIndex = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.recordId.ToString()].ToString());

            if (Request.QueryString[Enums.QueryStringName.filter.ToString()] != null)
                filterType = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.filter.ToString()].ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (optimalStockOrderIDFocusedRowIndex > 0)
                {
                    ASPxGridViewOptimalStockOrder.FocusedRowIndex = ASPxGridViewOptimalStockOrder.FindVisibleIndexByKeyValue(optimalStockOrderIDFocusedRowIndex);
                    ASPxGridViewOptimalStockOrder.ScrollToVisibleIndexOnClient = ASPxGridViewOptimalStockOrder.FindVisibleIndexByKeyValue(optimalStockOrderIDFocusedRowIndex);
                }

                ASPxGridViewOptimalStockOrder.DataBind();
                InitializeEditDeleteButtons();
            }
        }

        protected void CallbackPanelOptimalStockOrder_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
        {
            ClearAllSessions(Enum.GetValues(typeof(Enums.OptimalStockOrderSession)).Cast<Enums.OptimalStockOrderSession>().ToList());
            object optimalStockOrderID = ASPxGridViewOptimalStockOrder.GetRowValues(ASPxGridViewOptimalStockOrder.FocusedRowIndex, "NarociloOptimalnihZalogID");

            int userAction = CommonMethods.ParseInt(e.Parameter);

            if (userAction == (int)Enums.UserAction.Add)
            {
                ASPxWebControl.RedirectOnCallback(GenerateURI("OptimalStockOrderAdd.aspx", userAction, 0));
            }
            else
                ASPxWebControl.RedirectOnCallback(GenerateURI("OptimalStockOrderForm.aspx", userAction, optimalStockOrderID));
        }

        protected void ASPxGridViewOptimalStockOrder_DataBinding(object sender, EventArgs e)
        {
            var list = CheckModelValidation(GetDatabaseConnectionInstance().GetOptimalStockOrders());
            if (list != null)
            {
                if (filterType > 0)
                {
                    string statusKoda = "";
                    switch (filterType)
                    {
                        case 1:
                            statusKoda = DatabaseWebService.Common.Enums.Enums.StatusOfOptimalStock.ODDANO.ToString();
                            list = list.Where(l => l.StatusNarocilaOptimalnihZalog.Koda == statusKoda).ToList();
                            break;
                        case 2:
                            statusKoda = DatabaseWebService.Common.Enums.Enums.StatusOfOptimalStock.USTVARJENO_NAROCILO.ToString();
                            list = list.Where(l => l.StatusNarocilaOptimalnihZalog.Koda == statusKoda).ToList();
                            break;
                    }
                }

                (sender as ASPxGridView).DataSource = list.OrderByDescending(o => o.NarociloOptimalnihZalogID);
            }
        }

        protected void ASPxGridViewOptimalStockOrder_DataBound(object sender, EventArgs e)
        {
            EnableButtonBasedOnGridRows(ASPxGridViewOptimalStockOrder, btnAdd, btnEdit, btnDelete);
        }

        protected void PopupControlOptimalStockOrderAdd_WindowCallback(object source, DevExpress.Web.PopupWindowCallbackArgs e)
        {
            ClearAllSessions(Enum.GetValues(typeof(Enums.OptimalStockOrderSession)).Cast<Enums.OptimalStockOrderSession>().ToList());
        }

        private void InitializeEditDeleteButtons()
        {
            if (ASPxGridViewOptimalStockOrder.VisibleRowCount <= 0)
            {
                btnEdit.ClientVisible = false;
                btnDelete.ClientVisible = false;
            }
        }

        protected void btnCopyOrder_Click(object sender, EventArgs e)
        {
            object valueID = ASPxGridViewOptimalStockOrder.GetRowValues(ASPxGridViewOptimalStockOrder.FocusedRowIndex, "NarociloOptimalnihZalogID");
            ClearAllSessions(Enum.GetValues(typeof(Enums.OptimalStockOrderSession)).Cast<Enums.OptimalStockOrderSession>().ToList());

            CheckModelValidation(GetDatabaseConnectionInstance().CopyOptimalStockOrderByID(CommonMethods.ParseInt(valueID)));

            ASPxGridViewOptimalStockOrder.DataBind();
        }
    }
}