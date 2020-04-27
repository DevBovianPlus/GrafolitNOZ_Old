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
            //model = CheckModelValidation(GetDatabaseConnectionInstance().GetSupplierByName(searchString));
            //GetInquiryDataProvider().SetSearchedSupplierListModel(model);

            ASPxGridViewSupplierSearchResult.DataBind();
        }

        #region Helper methods
        private void RemoveSessionsAndClosePopUP(bool confirm = false)
        {
            string confirmCancelAction = "Preklici";

            if (confirm)
                confirmCancelAction = "Potrdi";

            //RemoveSession(Enums.CommonSession.SearchString);
            //RemoveSession(Enums.InquirySession.SelectedSupplierPopup);
            //RemoveSession(Enums.InquirySession.SupplierListModel);

            ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", string.Format("window.parent.OnClosePopUpHandler('{0}','{1}');", confirmCancelAction, "SupplierSearch"), true);
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveSessionsAndClosePopUP();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //TODO:
            //Pridobi seznam obkljukanih dobaviteljev
            //prepiši jih vnov objekt tipa InquiryPositionSupplier
            //vsak objekt dodaj v seznam GetInquiryDataProvider().GetInquiryPositionModel().PovprasevanjePozicijaDobavitelj
            //Shrani v sejo nov objekt GetInquiryDataProvider().SetInquiryPositionModel()
            //Preveri če izbrani dobavitelj v seznamu že obstaja. Če, potem ga ne dodajamo.

            List<string> selectedSuppliers = ASPxGridViewSupplierSearchResult.GetSelectedFieldValues("NazivPrvi").OfType<string>().ToList();

            if (selectedSuppliers != null && selectedSuppliers.Count > 0)
            {
                string sSelectedSupplier = ASPxGridViewSupplierSearchResult.GetSelectedFieldValues("NazivPrvi")[0].ToString().Trim();

                //AddValueToSession(Enums.InquirySession.ReturnSupplierVal, sSelectedSupplier);

                //ClientFullModel Supplier = CheckModelValidation(GetDatabaseConnectionInstance().GetClientByNameOrInsert(sSelectedSupplier));

                //InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
                //if (model == null)
                //{
                //    model = new InquiryPositionModel();

                //    model.PovprasevanjePozicijaID = 0;
                //    model.PovprasevanjeID = GetInquiryDataProvider().GetInquiryModel().PovprasevanjeID;

                //    model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
                //    model.tsUpdateUserID = PrincipalHelper.GetUserPrincipal().ID;


                //}
                //if (Supplier != null)
                //{
                //    model.Dobavitelj = Supplier;
                //    model.DobaviteljID = Supplier.idStranka;
                //    model.DobaviteljNaziv_P = sSelectedSupplier;
                //}
                //GetInquiryDataProvider().SetInquiryPositionModel(model);
            }


            RemoveSessionsAndClosePopUP(true);
        }

        protected void ASPxGridViewSupplierSearchResult_DataBinding(object sender, EventArgs e)
        {
            //model = GetInquiryDataProvider().GetSearchedSupplierListModel();

            if (model != null)
                (sender as ASPxGridView).DataSource = model;
        }
    }
}