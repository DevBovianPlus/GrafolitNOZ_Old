using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsNOZ.OptimalStockOrder;
using DevExpress.Web;
using DevExpress.Web.Data;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class MainProductProducts_popup : ServerMasterPage
    {
        OptimalStockTreeHierarchy mainProduct;
        List<OptimalStockTreeHierarchy> listOfSelectedProducts;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            mainProduct = (OptimalStockTreeHierarchy)GetValueFromSession(Enums.OptimalStockOrderSession.MainProductPopup);

            listOfSelectedProducts =  GetOptimalStockOrderDataProvider().GetSelectedChildProducts();
            
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
            ASPxGridViewChildProducts.DataBind();
        }

        private bool AddOrEditEntityObject(bool add = false)
        {
            SetSelectedProductsToModel();

            return true;
        }

        #region Helper methods
        private void RemoveSessionsAndClosePopUP(bool confirm = false)
        {
            string confirmCancelAction = "Preklici";

            if (confirm)
                confirmCancelAction = "Potrdi";

            RemoveSession(Enums.OptimalStockOrderSession.MainProductPopup);

            ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", string.Format("window.parent.OnClosePopUpHandler('{0}','{1}');", confirmCancelAction, "MainProductNodeClick"), true);
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveSessionsAndClosePopUP();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            ProcessUserAction();
        }

        private void ProcessUserAction()
        {
            bool isValid = false;

            isValid = AddOrEditEntityObject(true);

            if (isValid)
            {
                RemoveSessionsAndClosePopUP(true);
            }
        }

        protected void ASPxGridViewChildProducts_DataBinding(object sender, EventArgs e)
        {
            if (mainProduct != null && mainProduct.Product.AllSubCategories != null)
            {
                // preverimo nosilca in v seznamu ne prikažemo nosilca
                int iIndex1 = mainProduct.Name.IndexOf(" (");
                if (iIndex1 > -1)
                {
                    string sNameMainProduct = mainProduct.Name.Substring(0, iIndex1);

                    var filteredProduct = mainProduct.Product.ChildProducts.Where(cp => cp.DOBAVITELJ.Contains(mainProduct.Product.DOBAVITELJ) && cp.NAZIV != sNameMainProduct).ToList();
                    (sender as ASPxGridView).DataSource = filteredProduct;
                }
            }
        }

        protected void ASPxGridViewChildProducts_DataBound(object sender, EventArgs e)
        {

        }

        private void SetSelectedProductsToModel()
        {
            listOfSelectedProducts = GetOptimalStockOrderDataProvider().GetSelectedChildProducts() ?? new List<OptimalStockTreeHierarchy>();
            bool addToList = false;

            var mainProd = listOfSelectedProducts.Where(l => l.ID == mainProduct.ID).FirstOrDefault();
            if (mainProd == null)
            {
                mainProd = mainProduct;
                addToList = true;
            }

            var selectedProd = ASPxGridViewChildProducts.GetSelectedFieldValues("IDENT").OfType<string>().ToList();
            if (selectedProd != null)
            {
                foreach (var item in selectedProd)
                {
                    var prod = mainProd.Product.ChildProducts.Where(cp => cp.IDENT == item).FirstOrDefault();
                    if (prod != null)
                    {
                        if (mainProd.Product.SelectedChildProducts == null)
                            mainProd.Product.SelectedChildProducts = new List<GetProductsByOptimalStockValuesModel> { prod };
                        else//tukaj preverimo če slučajno že obstaja takšen artikel notri, potem ga ne dodamo.
                        {
                            if (!mainProd.Product.SelectedChildProducts.Exists(sp => sp.IDENT == prod.IDENT))
                                mainProd.Product.SelectedChildProducts.Add(prod);
                        }
                    }
                }

                if (addToList)
                    listOfSelectedProducts.Add(mainProd);

                GetOptimalStockOrderDataProvider().SetSelectedChildProducts(listOfSelectedProducts);
            }
        }
    }
}