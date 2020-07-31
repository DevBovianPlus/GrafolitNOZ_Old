using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsPDO.Inquiry;
using DevExpress.Web;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrafolitNOZ.Common;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class SearchSupplier_popup : ServerMasterPage
    {
        string searchString = "";
        List<ClientSimpleModel> model;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            //searchString = GetStringValueFromSession(Enums.CommonSession.SearchString);
            //GetInquiryDataProvider().GetInquiryPositionModel();

            ASPxGridViewSupplierSearchResult.Settings.GridLines = GridLines.Both;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            int tempID = 0;
            model = CheckModelValidation(GetDatabaseConnectionInstance().GetSupplierList());

            if (model != null)
                model.ForEach(a => a.TempID = ++tempID);

            GetOptimalStockOrderDataProvider().SetSearchedSupplierListModel(model);

            ASPxGridViewSupplierSearchResult.DataBind();
        }

        #region Helper methods
        private void RemoveSessionsAndClosePopUP(bool confirm = false)
        {
            string confirmCancelAction = "Preklici";

            if (confirm)
                confirmCancelAction = "Potrdi";


            RemoveSession(Enums.OptimalStockOrderSession.SearchedSupplierList);

            ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", string.Format("window.parent.OnClosePopUpHandler('{0}','{1}');", confirmCancelAction, "SupplierSearch"), true);
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveSessionsAndClosePopUP();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            List<string> selectedSuppliers = ASPxGridViewSupplierSearchResult.GetSelectedFieldValues("NazivPrvi").OfType<string>().ToList();

            if (selectedSuppliers != null && selectedSuppliers.Count > 0)
            {
                var supplierlist = GetOptimalStockOrderDataProvider().GetSupplierList();
                var searchedSupplierList = GetOptimalStockOrderDataProvider().GetSearchedSupplierListModel();

                foreach (var item in selectedSuppliers)
                {
                    var supplier = searchedSupplierList.Where(s => s.NazivPrvi == item).FirstOrDefault();
                    if (supplier != null && !supplierlist.Exists(s => s.NazivPrvi == supplier.NazivPrvi))//če v seznamu najdemo takšnega dobavitelja ki še ni v že obstoječem seznamu na gridlookup-u
                        supplierlist.Add(supplier);
                }

                GetOptimalStockOrderDataProvider().SetSupplierList(supplierlist);
            }

            RemoveSessionsAndClosePopUP(true);
        }

        protected void ASPxGridViewSupplierSearchResult_DataBinding(object sender, EventArgs e)
        {
            model = GetOptimalStockOrderDataProvider().GetSearchedSupplierListModel();

            if (model != null)
                (sender as ASPxGridView).DataSource = model;
        }
    }
}