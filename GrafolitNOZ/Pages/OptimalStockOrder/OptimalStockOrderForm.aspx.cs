using DatabaseWebService.ModelsNOZ.OptimalStockOrder;
using DevExpress.Web;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class OptimalStockOrderForm : ServerMasterPage
    {
        OptimalStockOrderModel model;
        int action;
        int optimalStockOrderID;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.Master.PageHeadlineTitle = this.Title;
            this.Master.DisableNavBar = true;
            ASPxGridViewProducts.Settings.GridLines = GridLines.Both;

            action = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.action.ToString()].ToString());
            optimalStockOrderID = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.recordId.ToString()].ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
                if (action == (int)Enums.UserAction.Edit || action == (int)Enums.UserAction.Delete)
                {
                    if (optimalStockOrderID > 0)
                    {
                        if (GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel() != null)
                            model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
                        else
                            model = CheckModelValidation(GetDatabaseConnectionInstance().GetOptimalStockOrderByID(optimalStockOrderID));

                        if (model != null)
                        {
                            GetOptimalStockOrderDataProvider().SetOptimalStockOrderModel(model);
                            FillForm();
                        }
                    }
                }

                UserActionConfirmBtnUpdate(btnSaveChanges, action);
                EnableUserControlsBasedOnStatus();//onemogočimo spreminjanje podatkov ali ponovno pošiljanje naročila.
            }
            else
            {
                if (model == null && SessionHasValue(Enums.OptimalStockOrderSession.OptimalStockOrderModel))
                    model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
            }
        }

        private void Initialize()
        {

        }

        private void FillForm()
        {
            txtNumber.Text = model.NarociloOptimalnihZalogStevilka;
            txtStatusName.Text = model.StatusNarocilaOptimalnihZalog.Naziv;
            txtPantheonOrderNumber.Text = model.NarociloID_P;
            txtSupplier.Text = model.Stranka.NazivPrvi;
            txtName.Text = model.Naziv;
            DateEditSubmitOrder.Date = model.DatumOddaje;
            txtQuantity.Text = model.Kolicina.ToString("N3");
            txtEmployee.Text = model.Zaposlen.Ime + " " + model.Zaposlen.Priimek;
            MemoNotes.Text = model.Opombe;

            ASPxGridViewProducts.DataBind();
        }

        private bool AddOrEditEntityObject(bool add = false, bool submitCopiedOrder = false)
        {
            if (add)
            {
                model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel() != null ? GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel() : new OptimalStockOrderModel();

                model.NarociloOptimalnihZalogID = 0;
                model.ts = DateTime.Now;
                model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
            }
            else if (model == null && !add)
            {
                model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();
            }

            model.tsUpdateUserID = PrincipalHelper.GetUserPrincipal().ID;

            //string sKoda = GetInquiryDataProvider().GetInquiryStatus().ToString();
            //model.StatusID = GetInquiryDataProvider().GetInquiryStatuses() != null ? GetInquiryDataProvider().GetInquiryStatuses().Where(ps => ps.Koda == sKoda).FirstOrDefault().StatusPovprasevanjaID : 0;

            model.Naziv = txtName.Text;
            model.DatumOddaje = DateEditSubmitOrder.Date;
            model.Opombe = MemoNotes.Text;

            if (submitCopiedOrder)
            {
                model.NarociloOddal = PrincipalHelper.GetUserPrincipal().ID;
                model.DatumOddaje = String.IsNullOrEmpty(DateEditSubmitOrder.Text) ? DateTime.Now : DateEditSubmitOrder.Date;
            }

            OptimalStockOrderModel returnModel = CheckModelValidation(GetDatabaseConnectionInstance().SaveOptimalStockOrder(model, submitCopiedOrder));

            if (returnModel != null)
            {
                //this we need if we want to add new client and then go and add new Plan with no redirection to Clients page
                model = returnModel;//if we need updated model in the same request;
                GetOptimalStockOrderDataProvider().SetOptimalStockOrderModel(model);
                optimalStockOrderID = model.NarociloOptimalnihZalogID;

                return true;
            }
            else
                return false;
        }

        protected void ASPxGridViewProducts_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {

        }

        protected void ASPxGridViewProducts_DataBinding(object sender, EventArgs e)
        {
            model = GetOptimalStockOrderDataProvider().GetOptimalStockOrderModel();

            if (model != null && model.NarociloOptimalnihZalogPozicija != null)
            {
                (sender as ASPxGridView).DataSource = model.NarociloOptimalnihZalogPozicija;
            }
        }

        #region HelperMethods

        private void ClearSessionsAndRedirect(bool isIDDeleted = false, bool stayOnForm = false, bool submitOrder = false)
        {
            string redirectString = "";
            List<QueryStrings> queryStrings = new List<QueryStrings> {
                new QueryStrings() { Attribute = Enums.QueryStringName.recordId.ToString(), Value = optimalStockOrderID.ToString() }
            };


            if (isIDDeleted)
                redirectString = "OptimalStockOrderTable.aspx";
            else
                redirectString = GenerateURI("OptimalStockOrderTable.aspx", queryStrings);

            /* if (stayOnForm)
             {
                 queryStrings.Add(new QueryStrings { Attribute = Enums.QueryStringName.action.ToString(), Value = ((int)Enums.UserAction.Edit).ToString() });
                 redirectString = GenerateURI("OptimalStockOrderForm.aspx", queryStrings);
             }*/


            List<Enums.OptimalStockOrderSession> list = Enum.GetValues(typeof(Enums.OptimalStockOrderSession)).Cast<Enums.OptimalStockOrderSession>().ToList();

            ClearAllSessions(list, redirectString, IsCallback);
        }

        private void EnableUserControlsBasedOnStatus()
        {
            if (model.StatusNarocilaOptimalnihZalog.Koda == DatabaseWebService.Common.Enums.Enums.StatusOfOptimalStock.KOPIRANO_NAROCILO.ToString())
            {
                btnSubmitOrder.ClientVisible = true;
            }
        }

        private bool DeleteObject()
        {
            return CheckModelValidation(GetDatabaseConnectionInstance().DeleteOptimalStockOrder(optimalStockOrderID));
        }

        private void ProcessUserAction(bool stayOnForm = false, bool submitCopiedOrder = false)
        {
            bool isValid = false;
            bool isDeleteing = false;

            switch (action)
            {
                case (int)Enums.UserAction.Add:
                    isValid = AddOrEditEntityObject(true, submitCopiedOrder);
                    break;
                case (int)Enums.UserAction.Edit:
                    isValid = AddOrEditEntityObject(false, submitCopiedOrder);
                    break;
                case (int)Enums.UserAction.Delete:
                    isValid = DeleteObject();
                    isDeleteing = true;
                    break;
            }

            if (isValid)
            {
                ClearSessionsAndRedirect(isDeleteing, stayOnForm);
            }
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearSessionsAndRedirect();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            ProcessUserAction();
        }

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            ProcessUserAction(false, true);
        }
    }
}