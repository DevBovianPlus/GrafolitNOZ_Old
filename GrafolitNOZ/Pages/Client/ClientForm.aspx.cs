using DatabaseWebService.Models;
using DatabaseWebService.Models.Client;
using DatabaseWebService.ModelsOTP.Client;
using DevExpress.Web;
using Newtonsoft.Json;
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DatabaseWebService.ModelsPDO;

namespace GrafolitNOZ.Pages.Client
{
    public partial class ClientForm : ServerMasterPage
    {
        ClientFullModel model = null;
        int clientID = -1;
        int action = -1;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            this.Master.PageHeadlineTitle = Title;

            action = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.action.ToString()].ToString());
            clientID = CommonMethods.ParseInt(Request.QueryString[Enums.QueryStringName.recordId.ToString()].ToString());

            this.Master.DisableNavBar = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
                if (action == (int)Enums.UserAction.Edit || action == (int)Enums.UserAction.Delete)
                {
                    if (clientID > 0)
                    {
                        if (GetClientDataProvider().GetClientFullModel() != null)
                            model = GetClientDataProvider().GetClientFullModel();
                        else
                        {
                            model = CheckModelValidation(GetDatabaseConnectionInstance().GetClient(clientID));
                        }

                        if (model != null)
                        {
                            GetClientDataProvider().SetClientFullModel(model);
                            FillForm();
                        }
                    }
                }
                else if (action == (int)Enums.UserAction.Add)
                {
                    SetFromDefaultValues();
                }
                UserActionConfirmBtnUpdate(btnConfirm, action);
                ASPxGridViewContactPerson.DataBind();
            }
            else
            {
                if (model == null && SessionHasValue(Enums.ClientSession.ClientFullModel))
                    model = GetClientDataProvider().GetClientFullModel();
            }
        }

        private void FillForm()
        {
            txtNazivPrvi.Text = model.NazivPrvi;
            txtNazivDrugi.Text = model.NazivDrugi;
            txtStevPoste.Text = model.StevPoste;
            txtNazivPoste.Text = model.NazivPoste;
            txtEmail.Text = model.Email;
            txtTelefon.Text = model.Telefon;
            txtFAX.Text = model.FAX;
            txtNaslov.Text = model.Naslov;

            ComboBoxSkrbnik.SelectedIndex = model.StrankaZaposleni.Count > 0 ? ComboBoxSkrbnik.Items.IndexOfValue(model.StrankaZaposleni[0].idOsebe.ToString()) : ComboBoxSkrbnik.Items.IndexOfValue(PrincipalHelper.GetUserPrincipal().ID.ToString());
            ComboBoxTip.SelectedIndex = model.TipStrankaID > 0 ? ComboBoxTip.Items.IndexOfValue(model.TipStrankaID.ToString()) : 0;
            //ComboBoxJezik.SelectedIndex = model.JezikID > 0 ? ComboBoxJezik.Items.IndexOfValue(model.JezikID.ToString()) : 0;


            if (PrincipalHelper.IsUserWarehouseKeeper() || PrincipalHelper.IsUserUser())
            {
                ComboBoxSkrbnik.BackColor = Color.LightGray;
                ComboBoxSkrbnik.ReadOnly = true;
                ComboBoxSkrbnik.Enabled = false;
            }


            if (SessionHasValue(Enums.CommonSession.StayOnFormAndOpenPopup))
            {
                bool isValid = SetSessionsAndOpenPopUp(((int)Enums.UserAction.Add).ToString(), Enums.ClientSession.ContactPersonID, null);
                PopupControlContactPerson.ShowOnPageLoad = isValid;
                RemoveSession(Enums.CommonSession.StayOnFormAndOpenPopup);
                ClientScript.RegisterStartupScript(GetType(), "CommonJS", string.Format("$('#tab-contact a[href=\"#contact\"]').tab('show');"), true);
            }
        }

        private bool AddOrEditEntityObject(bool add = false)
        {
            if (add)
            {
                model = new ClientFullModel();

                model.idStranka = 0;
                model.ts = DateTime.Now;
                model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;

            }
            else if (model == null && !add)
            {
                model = (ClientFullModel)GetValueFromSession(Enums.ClientSession.ClientFullModel);
            }

            if (model.StrankaZaposleni == null || model.StrankaZaposleni.Count <= 0)
            {
                model.StrankaZaposleni = new List<ClientEmployeeModel>();
                int employeeID = CommonMethods.ParseInt(ComboBoxSkrbnik.Value.ToString());
                if (employeeID > 0)
                {
                    ClientEmployeeModel clientEmployee = new ClientEmployeeModel();
                    clientEmployee.idOsebe = employeeID;
                    clientEmployee.idStranka = model.idStranka;
                    clientEmployee.ts = DateTime.Now;
                    clientEmployee.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
                    model.StrankaZaposleni.Add(clientEmployee);
                }
            }
            else
            {
                model.StrankaZaposleni[0].idOsebe = CommonMethods.ParseInt(ComboBoxSkrbnik.Value.ToString());
            }

            model.NazivPrvi = txtNazivPrvi.Text;
            model.NazivDrugi = txtNazivDrugi.Text;
            model.Naslov = txtNaslov.Text;
            model.StevPoste = txtStevPoste.Text;
            model.NazivPoste = txtNazivPoste.Text;
            model.Email = txtEmail.Text;
            model.Telefon = txtTelefon.Text;
            model.FAX = txtFAX.Text;
            model.TipStrankaID = CommonMethods.ParseInt(ComboBoxTip.Value);
            //model.JezikID = CommonMethods.ParseInt(ComboBoxJezik.Value);


            ClientFullModel returnModel = CheckModelValidation(GetDatabaseConnectionInstance().SaveClientChanges(model));

            if (returnModel != null)
            {
                //this we need if we want to add new client and then go and add new Plan with no redirection to Clients page
                GetClientDataProvider().SetClientFullModel(returnModel);
                clientID = returnModel.idStranka;

                //TODO: ADD new item to session and if user has added new client and create data bind.
                return true;
            }
            else
                return false;
        }

        private bool DeleteObject()
        {
            return CheckModelValidation(GetDatabaseConnectionInstance().DeleteClient(clientID));
        }

        private void ProcessUserAction()
        {
            bool isValid = false;
            bool isDeleteing = false;

            switch (action)
            {
                case (int)Enums.UserAction.Add:
                    isValid = AddOrEditEntityObject(true);
                    break;
                case (int)Enums.UserAction.Edit:
                    isValid = AddOrEditEntityObject();
                    break;
                case (int)Enums.UserAction.Delete:
                    isValid = DeleteObject();
                    isDeleteing = true;
                    break;
            }

            if (isValid)
            {
                ClearSessionsAndRedirect(isDeleteing);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            ProcessUserAction();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearSessionsAndRedirect();
        }

        #region Initialization
        private void Initialize()
        {
            ComboBoxTip.DataBind();
            //ComboBoxJezik.DataBind();
            ComboBoxSkrbnik.DataBind();
        }

        private void SetFromDefaultValues()
        {
            ComboBoxSkrbnik.SelectedIndex = ComboBoxSkrbnik.Items.IndexOfValue(PrincipalHelper.GetUserPrincipal().ID.ToString());
            ComboBoxTip.SelectedIndex = ComboBoxTip.Items.IndexOfValue("-1");
            //ComboBoxJezik.SelectedIndex = ComboBoxJezik.Items.IndexOfValue("-1");
        }
        #endregion

        #region DataBindings
        protected void ComboBoxTip_DataBinding(object sender, EventArgs e)
        {
            List<ClientType> types = CheckModelValidation(GetDatabaseConnectionInstance().GetClientTypes());
            (sender as ASPxComboBox).DataSource = SerializeToDataTable(types, "TipStrankaID", "Naziv");
        }

        protected void ComboBoxJezik_DataBinding(object sender, EventArgs e)
        {
            /*List<LanguageModel> types = CheckModelValidation(GetDatabaseConnectionInstance().GetLanguages());
            (sender as ASPxComboBox).DataSource = SerializeToDataTable(types, "JezikID", "Naziv");*/
        }

        protected void ComboBoxSkrbnik_DataBinding(object sender, EventArgs e)
        {
            List<EmployeeFullModel> employees = CheckModelValidation(GetDatabaseConnectionInstance().GetAllEmployees());
            DataTable dt = new DataTable();

            string listEmployees = JsonConvert.SerializeObject(employees);
            dt = JsonConvert.DeserializeObject<DataTable>(listEmployees);
            dt.Columns.Add("CelotnoIme", typeof(string), "Ime + ' ' + Priimek");
            DataRow row = dt.NewRow();
            row["idOsebe"] = -1;
            row["Ime"] = "Izberi...";
            row["Priimek"] = "";
            dt.Rows.InsertAt(row, 0);
            (sender as ASPxComboBox).DataSource = dt;
        }

        protected void ComboBoxTipPrevoza_DataBinding(object sender, EventArgs e)
        {
            List<ClientTransportType> types = CheckModelValidation(GetDatabaseConnectionInstance().GetAllTransportTypes());
            (sender as ASPxComboBox).DataSource = SerializeToDataTable(types, "TipPrevozaID", "Naziv");
        }
        #endregion

        #region Helper methods
        private void ClearSessionsAndRedirect(bool isIDDeleted = false)
        {
            string redirectString = "";
            List<QueryStrings> queryStrings = new List<QueryStrings> {
                new QueryStrings() { Attribute = Enums.QueryStringName.recordId.ToString(), Value = clientID.ToString() }
            };

            if (isIDDeleted)
                redirectString = "Client.aspx";
            else
                redirectString = GenerateURI("Client.aspx", queryStrings);

            RemoveSession(Enums.CommonSession.UserActionPopUp);

            List<Enums.ClientSession> list = Enum.GetValues(typeof(Enums.ClientSession)).Cast<Enums.ClientSession>().ToList();
            ClearAllSessions(list, redirectString);
        }
        #endregion

        protected void CallbackPanelContactPerson_Callback(object sender, CallbackEventArgsBase e)
        {
            if (action == (int)Enums.UserAction.Add)
            {
                AddOrEditEntityObject(true);
                ASPxWebControl.RedirectOnCallback(GenerateURI("ClientForm.aspx", (int)Enums.UserAction.Edit, clientID));
                AddValueToSession(Enums.CommonSession.StayOnFormAndOpenPopup, true);
            }
            else
            {
                object value = ASPxGridViewContactPerson.GetRowValues(ASPxGridViewContactPerson.FocusedRowIndex, "idKontaktneOsebe");
                bool isValid = SetSessionsAndOpenPopUp(e.Parameter, Enums.ClientSession.ContactPersonID, value);

                PopupControlContactPerson.ShowOnPageLoad = isValid;
            }
        }

        protected void ASPxGridViewContactPerson_DataBinding(object sender, EventArgs e)
        {
            if (model != null)
            {
                (sender as ASPxGridView).DataSource = model.KontaktneOsebe;
            }
        }

        protected void PopupControlContactPerson_WindowCallback(object source, PopupWindowCallbackArgs e)
        {
            RemoveSession(Enums.ClientSession.ContactPersonID);
            RemoveSession(Enums.ClientSession.ContactPersonModel);
            RemoveSession(Enums.CommonSession.UserActionPopUp);
        }

        protected void ASPxGridViewContactPerson_DataBound(object sender, EventArgs e)
        {
            if (ASPxGridViewContactPerson.VisibleRowCount <= 0)
                EnabledDeleteAndEditBtnPopUp(btnEdit, btnDelete);
            else
                contactBadge.InnerHtml = ASPxGridViewContactPerson.VisibleRowCount.ToString();
        }
    }
}