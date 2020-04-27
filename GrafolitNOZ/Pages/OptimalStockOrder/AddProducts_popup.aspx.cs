using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsPDO.Inquiry;
using DevExpress.Web;
using DevExpress.Web.Data;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.OptimalStockOrder
{
    public partial class AddProducts_popup : ServerMasterPage
    {
        int inquiryPosAction = -1;
        int inquiryPosID = 0;
        int cntArtikels = 0;
        InquiryPositionModel model;
        InquiryFullModel InqModel;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            inquiryPosAction = CommonMethods.ParseInt(GetStringValueFromSession(Enums.CommonSession.UserActionPopUp));
            inquiryPosID = 0;//CommonMethods.ParseInt(GetStringValueFromSession(Enums.InquirySession.InquiryPositionID));

            GridLookupCategory.GridView.Settings.GridLines = GridLines.Both;
            ASPxGridViewSelectedArtikel.Settings.GridLines = GridLines.Both;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                Initialize();
                if (inquiryPosAction == (int)Enums.UserAction.Edit || inquiryPosAction == (int)Enums.UserAction.Delete)
                {
                    if (inquiryPosID > 0)
                    {
                        if (GetInquiryDataProvider().GetInquiryPositionModel() != null)
                            model = GetInquiryDataProvider().GetInquiryPositionModel();
                        else
                        {
                            model = CheckModelValidation(GetDatabaseConnectionInstance().GetInquiryPositionByID(inquiryPosID));
                        }

                        if (model != null)
                        {
                            GetInquiryDataProvider().SetInquiryPositionModel(model);

                            if (GetInquiryDataProvider().GetInquiryModel() != null)
                                InqModel = GetInquiryDataProvider().GetInquiryModel();
                            else
                            {
                                InqModel = CheckModelValidation(GetDatabaseConnectionInstance().GetInquiryByID(model.PovprasevanjeID, false, 0));
                            }

                            FillForm();
                        }
                    }

                    
                }
                else if (inquiryPosAction == (int)Enums.UserAction.Add)
                {
                    SetFromDefaultValues();
                }
                UserActionConfirmBtnUpdate(btnConfirm, inquiryPosAction, true);
            }*/
        }

        private void Initialize()
        {
            GridLookupCategory.DataBind();
        }

        private void FillForm()
        {
            txtSupplierName.Text = model.DobaviteljNaziv_P;
            
            txtUnitOfMeasure1.Text = (model.Dobavitelj.PrivzetaEM != null) ? model.Dobavitelj.PrivzetaEM : "";
            GridLookupCategory.Text = (model.Dobavitelj.ZadnjaIzbranaKategorija != null) ? model.Dobavitelj.ZadnjaIzbranaKategorija : "";
            GridLookupKontaktnaOSeba.Text = model.DobaviteljKontaktOsebe;
            CheckBoxKupecViden.Checked = (model.KupecViden != null) ? model.KupecViden : false;
            ASPxGridViewSelectedArtikel.DataBind();
            GridLookupKontaktnaOSeba.DataBind();

            if (InqModel != null)
            {
                //če je status povpraševanje oddano (ODDANO), potem uporabniku ne dovolimo več dodajanja novih pozicij ali ponovnega pošiljanja ali shranjevanja povpraševanja
                if (InqModel.StatusPovprasevanja.Koda == DatabaseWebService.Common.Enums.Enums.StatusOfInquiry.POTRJEN.ToString() || (InqModel.StatusPovprasevanja.Koda == DatabaseWebService.Common.Enums.Enums.StatusOfInquiry.NAROCENO.ToString()) || (InqModel.StatusPovprasevanja.Koda == DatabaseWebService.Common.Enums.Enums.StatusOfInquiry.POSLANO_V_NABAVO.ToString()) || (InqModel.StatusPovprasevanja.Koda == DatabaseWebService.Common.Enums.Enums.StatusOfInquiry.USTVARJENO_NAROCILO.ToString()))
                    EnableUserControl(false);
            }
        }

        private void EnableUserControl(bool enable)
        {
            btnConfirm.ClientEnabled = enable;
            btnClear.ClientEnabled = enable;
            btnAddProduct.ClientEnabled = enable;
        }

        private bool AddOrEditEntityObject(bool add = false)
        {
            if (add)
            {
                model = new InquiryPositionModel();

                model.PovprasevanjePozicijaID = 0;
               // model.PovprasevanjeID = GetInquiryDataProvider().GetInquiryModel().PovprasevanjeID;

                model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
                model.tsUpdateUserID = PrincipalHelper.GetUserPrincipal().ID;
            }
            else if (model == null && !add)
            {
                //model = GetInquiryDataProvider().GetInquiryPositionModel();
            }

            if (model.PovprasevanjePozicijaArtikel != null)//tiste pozicije ki smo na novo dodali jim moramo nastaviti PovprasevanjePozicijaDobaviteljID na 0 kajti zdaj imajo nastavljene samo začasne id-je
                model.PovprasevanjePozicijaArtikel.Where(ppd => ppd.NewAdd).ToList().ForEach(ppd => ppd.PovprasevanjePozicijaArtikelID = 0);


            if (model != null)
            {
                if (model.PovprasevanjePozicijaArtikel != null)
                {
                    model.Artikli = GetArtikels(model);
                }
            }

            model.DobaviteljKontaktOsebe = GridLookupKontaktnaOSeba.Text;
           
            model.KupecViden = CheckBoxKupecViden.Checked;



            /*InquiryPositionModel newModel = CheckModelValidation(GetDatabaseConnectionInstance().SaveInquiryPosition(model));

            if (newModel != null)//If new record is added we need to refresh aspxgridview. We add new record to session model.
            {
                model = newModel;
                inquiryPosID = model.PovprasevanjePozicijaID;
                GetInquiryDataProvider().SetInquiryPositionModel(model);

                if (inquiryPosAction != (int)(Enums.UserAction.Add))
                {
                    var pozicije = GetInquiryDataProvider().GetInquiryModel().PovprasevanjePozicija;
                    var removeItem = pozicije.Where(pp => pp.PovprasevanjePozicijaID == inquiryPosID).FirstOrDefault();

                    pozicije.Remove(removeItem);
                }

                GetInquiryDataProvider().GetInquiryModel().PovprasevanjePozicija.Add(model);

                return true;
            }
            else
            {
                return false;
            }*/
            return true;
        }

        //private string GetSelectedKontaktnaOseba()
        //{
        //    int Counter = GridLookupKontaktnaOSeba.GridView.VisibleRowCount - 1;


        //    using (GridLookupKontaktnaOSeba)
        //    {
        //        foreach (DataRow row in GridLookupKontaktnaOSeba.GridView.Rows)
        //        {
        //            for (int i = 0; i <= Counter; i++)
        //            {
        //                DataRow row2 = GridLookupKontaktnaOSeba.GridView.GetDataRow(i);

        //                if (Convert.ToInt32(row["FacilityID"]) == Convert.ToInt32(row2["FacilityID"]))
        //                {
        //                    GridLookupKontaktnaOSeba.GridView.Selection.SelectRow(i);
        //                }
        //            }
        //        }
        //    }


        //    for (int i = 0; i < GridLookupKontaktnaOSeba.GridView.VisibleRowCount; i++)
        //    {
        //        if (GridLookupKontaktnaOSeba.GridView.Selection.IsRowSelected(i) == true)
        //        {
        //           DataRow selDR = GridLookupKontaktnaOSeba.GridView.i;
        //        }
        //    }

        //}

        //string sKontOseb = "";

        //    return sKontOseb;
        //}

        private void SetFromDefaultValues()
        { }

        protected void GridLookupCategory_DataBinding(object sender, EventArgs e)
        {
            List<ProductCategory> list = CheckModelValidation(GetDatabaseConnectionInstance().GetCategoryList());
            (sender as ASPxGridLookup).DataSource = list;
        }

        protected void GridLookupKontOseba_DataBinding(object sender, EventArgs e)
        {
            /*InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
            if (model != null)
            {
                List<ContactPersonModel> list = CheckModelValidation(GetDatabaseConnectionInstance().GetContactPersonModelListByClientID(model.DobaviteljID));
                (sender as ASPxGridLookup).DataSource = list;
                if (list == null || list.Count == 0)
                {
                    AddValueToSession(Enums.CommonSession.UserActionNestedPopUp, (int)Enums.UserAction.Add);
                    GetClientDataProvider().SetClientID(model.DobaviteljID);
                    PopupControlAddContactPerson.ShowOnPageLoad = true;

                }
                else if (list.Count == 1)
                {
                    GridLookupKontaktnaOSeba.Value = list[0].idKontaktneOsebe;
                }
            }*/
        }

        protected void GridLookupSupplier_DataBinding(object sender, EventArgs e)
        {
            //TODO: pridobimo source iz SQL funkcije.Nato pretvorimo podatke v objekte tipa InquiryPositionSupplierModel in jih združimo z tistimi ki so že izbrani
            //samo podatke posodobimo iz povpraševanja pozicije (vmesna tabela PovprasevanjePozicijaDobavitelj)
            string dobavitelj = DatabaseWebService.Common.Enums.Enums.TypeOfClient.DOBAVITELJ.ToString();
            List<ClientSimpleModel> clients = CheckModelValidation(GetDatabaseConnectionInstance().GetAllClients(dobavitelj));

            (sender as ASPxGridLookup).DataSource = clients;
        }

        #region Helper methods
        private void RemoveSessionsAndClosePopUP(bool confirm = false)
        {
            string confirmCancelAction = "Preklici";

            if (confirm)
                confirmCancelAction = "Potrdi";

            RemoveSession(Enums.CommonSession.UserActionPopUp);
            
            RemoveSession(Enums.CommonSession.SearchString);
           

            ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", string.Format("window.parent.OnClosePopUpHandler('{0}','{1}');", confirmCancelAction, "InquiryPos"), true);
        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            RemoveSessionsAndClosePopUP();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

            /*InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
            if (model != null)
            {
                CheckModelValidation(GetDatabaseConnectionInstance().DeleteInquiryPositionArtikles(model.PovprasevanjePozicijaArtikel));
                model.PovprasevanjePozicijaArtikel = null;
            }
            GetInquiryDataProvider().SetInquiryPositionModel(model);
            ASPxGridViewSelectedArtikel.DataBind();*/
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            ProcessUserAction();
        }

        private bool DeleteObject()
        {
            /*var isDeleted = CheckModelValidation(GetDatabaseConnectionInstance().DeleteInquiryPosition(inquiryPosID));

            if (isDeleted)
            {
                var pozicije = GetInquiryDataProvider().GetInquiryModel().PovprasevanjePozicija;
                var removeItem = pozicije.Where(pp => pp.PovprasevanjePozicijaID == inquiryPosID).FirstOrDefault();

                pozicije.Remove(removeItem);
            }

            return isDeleted;*/
            return true;
        }

        private void ProcessUserAction()
        {
            bool isValid = false;
            bool confirm = false;

            switch (inquiryPosAction)
            {
                case (int)Enums.UserAction.Add:
                    isValid = AddOrEditEntityObject(true);
                    confirm = true;
                    break;
                case (int)Enums.UserAction.Edit:
                    isValid = AddOrEditEntityObject();
                    confirm = true;
                    break;
                case (int)Enums.UserAction.Delete:
                    isValid = DeleteObject();
                    confirm = true;
                    break;
            }

            if (isValid)
            {
                RemoveSessionsAndClosePopUP(confirm);
            }
            else
                ShowClientPopUp("Something went wrong. Contact administrator", 1);
        }

        private void AddSupplierToForm(ClientSimpleModel cSupplier)
        {
            /*ClientFullModel Supplier = CheckModelValidation(GetDatabaseConnectionInstance().GetClientByNameOrInsert(cSupplier.NazivPrvi));

            InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
            if (model == null)
            {
                model = new InquiryPositionModel();

                model.PovprasevanjePozicijaID = 0;
                model.PovprasevanjeID = GetInquiryDataProvider().GetInquiryModel().PovprasevanjeID;

                model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
                model.tsUpdateUserID = PrincipalHelper.GetUserPrincipal().ID;


            }
            if (Supplier != null)
            {
                model.Dobavitelj = Supplier;
                model.DobaviteljID = Supplier.idStranka;
                model.DobaviteljNaziv_P = Supplier.NazivPrvi;
                txtSupplierName.Text = Supplier.NazivPrvi;
                txtUnitOfMeasure1.Text = (Supplier.PrivzetaEM != null) ? Supplier.PrivzetaEM : "";
                GridLookupCategory.Text = (Supplier.ZadnjaIzbranaKategorija != null) ? Supplier.ZadnjaIzbranaKategorija : "";
            }
            GetInquiryDataProvider().SetInquiryPositionModel(model);*/
        }

        protected void CallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
           /* if (e.Parameter == "StartSearchPopup")
            {
                AddValueToSession(Enums.CommonSession.SearchString, txtSupplierSearch.Text.Trim());

                List<ClientSimpleModel> model;
                model = CheckModelValidation(GetDatabaseConnectionInstance().GetSupplierByName(txtSupplierSearch.Text.Trim()));

                if (model.Count > 1)
                {
                    PopupControlSearchSupplier.ShowOnPageLoad = true;
                }
                else if (model.Count == 1)
                {
                    AddSupplierToForm(model[0]);
                    GridLookupKontaktnaOSeba.DataBind();
                }
                else if (model.Count == 0)
                {
                    PopupControlSearchSupplier.ShowOnPageLoad = true;
                }
            }
            else if (e.Parameter == "RefreshContactPersons")
            {
                GridLookupKontaktnaOSeba.DataBind();
                txtSupplierName.Text = GetStringValueFromSession(Enums.InquirySession.ReturnSupplierVal);
                InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
                if (model != null)
                {
                    txtUnitOfMeasure1.Text = (model.Dobavitelj != null && model.Dobavitelj.PrivzetaEM != null) ? model.Dobavitelj.PrivzetaEM : "";
                }
            }
            else if (e.Parameter == "AddContactPersons")
            {
                InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
                if (model != null)
                {
                    AddValueToSession(Enums.CommonSession.UserActionNestedPopUp, (int)Enums.UserAction.Add);
                    GetClientDataProvider().SetClientID(model.DobaviteljID);
                    PopupControlAddContactPerson.ShowOnPageLoad = true;
                }
            }
            else if (e.Parameter == "AddArtikel")
            {
                InquiryPositionModel model = GetInquiryDataProvider().GetInquiryPositionModel();
                if (model != null)
                {
                    InquiryPositionArtikelModel modelArtikel = new InquiryPositionArtikelModel();

                    cntArtikels = CommonMethods.ParseInt(GetStringValueFromSession(Enums.InquirySession.AddArtikelCnt));
                    cntArtikels++;
                    AddValueToSession(Enums.InquirySession.AddArtikelCnt, cntArtikels);
                    modelArtikel.PovprasevanjePozicijaArtikelID = cntArtikels;
                    modelArtikel.KategorijaNaziv = GridLookupCategory.Text;
                    modelArtikel.Naziv = txtName.Text;
                    modelArtikel.Kolicina1 = CommonMethods.ParseDecimal(txtQuantity1.Text);
                    modelArtikel.EnotaMere1 = txtUnitOfMeasure1.Text;
                    modelArtikel.Kolicina2 = CommonMethods.ParseDecimal(txtQuantity2.Text);
                    modelArtikel.EnotaMere2 = txtUnitOfMeasure2.Text;

                    modelArtikel.OpombaNarocilnica = MemoNotes.Text;
                    modelArtikel.NewAdd = true;
                    if (model.PovprasevanjePozicijaArtikel == null) model.PovprasevanjePozicijaArtikel = new List<InquiryPositionArtikelModel>();
                    model.PovprasevanjePozicijaArtikel.Add(modelArtikel);
                    AddValueToSession(Enums.CommonSession.UserActionPopUp, (int)Enums.UserAction.Edit);
                    GetInquiryDataProvider().SetInquiryPositionModel(model);

                    ASPxGridViewSelectedArtikel.DataBind();
                }


            }*/
        }

        private string GetArtikels(InquiryPositionModel modelPos)
        {
            CommonMethods.LogThis("Enter function");
            string r = "";
            if (modelPos.PovprasevanjePozicijaArtikel != null)
            {
                CommonMethods.LogThis("1");
                foreach (var item in modelPos.PovprasevanjePozicijaArtikel)
                {
                    CommonMethods.LogThis("2");
                    CommonMethods.LogThis(item.Naziv);
                    r += item.KategorijaNaziv + "-" + item.Naziv.TrimEnd() + ", ";
                }
                CommonMethods.LogThis("3");
                if (r.Length > 0)
                    r = r.Remove(r.LastIndexOf(", "), 2);
            }
            CommonMethods.LogThis("4");
            return r;
        }

        protected void ASPxGridViewSelectedArtikel_DataBinding(object sender, EventArgs e)
        {
           /* model = GetInquiryDataProvider().GetInquiryPositionModel();
            if (model != null)
                (sender as ASPxGridView).DataSource = model.PovprasevanjePozicijaArtikel;*/
        }

        protected void PopupControlSearchSupplier_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            RemoveSession(Enums.CommonSession.SearchString);
          
        }

        protected void ASPxGridViewSelectedArtikel_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
           /* model = GetInquiryDataProvider().GetInquiryPositionModel();
            InquiryFullModel iFullModel = GetInquiryDataProvider().GetInquiryModel();

            if (model != null)
            {
                List<InquiryPositionArtikelModel> updateList = model.PovprasevanjePozicijaArtikel ?? new List<InquiryPositionArtikelModel>();
                InquiryPositionArtikelModel inquiryPosArtikel = null;
                List<InquiryPositionArtikelModel> itemsToDelete = new List<InquiryPositionArtikelModel>();

                Type myType = typeof(InquiryPositionArtikelModel);
                List<PropertyInfo> myPropInfo = myType.GetProperties().ToList();

                //Spreminjanje zapisov v gridu
                foreach (ASPxDataUpdateValues item in e.UpdateValues)
                {
                    inquiryPosArtikel = new InquiryPositionArtikelModel();

                    foreach (DictionaryEntry obj in item.Keys)//we set table ID
                    {
                        PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                        if (info != null)
                        {
                            inquiryPosArtikel = updateList.Where(ips => ips.PovprasevanjePozicijaArtikelID == (int)obj.Value).FirstOrDefault();
                            break;
                        }
                    }

                    foreach (DictionaryEntry obj in item.NewValues)
                    {
                        PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                        if (info != null)
                        {
                            info.SetValue(inquiryPosArtikel, obj.Value);

                            var itmIFL = iFullModel.PovprasevanjePozicijaArtikel.Where(ppa => ppa.PovprasevanjePozicijaArtikelID == inquiryPosArtikel.PovprasevanjePozicijaArtikelID).FirstOrDefault();
                            if (itmIFL != null)
                            {
                                info.SetValue(itmIFL, obj.Value);
                            }
                        }
                    }
                }

                //Brisanje zapisov v gridu
                foreach (ASPxDataDeleteValues item in e.DeleteValues)
                {
                    inquiryPosArtikel = new InquiryPositionArtikelModel();

                    foreach (DictionaryEntry obj in item.Keys)//we set table ID
                    {
                        PropertyInfo info = myPropInfo.Where(prop => prop.Name.Equals(obj.Key.ToString())).FirstOrDefault();

                        if (info != null)
                        {
                            inquiryPosArtikel = updateList.Where(ips => ips.PovprasevanjePozicijaArtikelID == (int)obj.Value).FirstOrDefault();
                            break;
                        }
                    }

                    if (iFullModel != null)
                    {
                        var itmIFL = iFullModel.PovprasevanjePozicijaArtikel.Where(ppa => ppa.PovprasevanjePozicijaArtikelID == inquiryPosArtikel.PovprasevanjePozicijaArtikelID).FirstOrDefault();
                        if (itmIFL != null)
                        {
                            iFullModel.PovprasevanjePozicijaArtikel.Remove(itmIFL);
                        }
                    }
                    updateList.Remove(inquiryPosArtikel);
                    itemsToDelete.Add(inquiryPosArtikel);
                }
                GetInquiryDataProvider().SetInquiryModel(iFullModel);


                model.PovprasevanjePozicijaArtikel = updateList;
                GetInquiryDataProvider().SetInquiryPositionModel(model);

                //Delete inquiry positions items
                if (itemsToDelete.Count > 0)
                    CheckModelValidation(GetDatabaseConnectionInstance().DeleteInquiryPositionArtikles(itemsToDelete));
            }

            e.Handled = true;*/
        }

        protected void PopupControlAddContactPerson_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            RemoveSession(Enums.ClientSession.ContactPersonID);
            RemoveSession(Enums.ClientSession.ContactPersonModel);
            RemoveSession(Enums.CommonSession.UserActionNestedPopUp);
            RemoveSession(Enums.ClientSession.ClientID);
        }
    }
}