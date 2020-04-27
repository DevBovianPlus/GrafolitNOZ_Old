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
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.Client
{
    public partial class ContactPerson_popup : ServerMasterPage
    {
        int userAction = -1;
        int contactPersonID = 0;
        ContactPersonModel model;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            userAction = CommonMethods.ParseInt(GetStringValueFromSession(Enums.CommonSession.UserActionPopUp));
            contactPersonID = CommonMethods.ParseInt(GetStringValueFromSession(Enums.ClientSession.ContactPersonID));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initialize();
                if (userAction == (int)Enums.UserAction.Edit || userAction == (int)Enums.UserAction.Delete)
                {
                    if (contactPersonID > 0)
                    {
                        if (GetClientDataProvider().GetContactPersonModel() != null)
                            model = GetClientDataProvider().GetContactPersonModel();
                        else
                        {
                            model = GetClientDataProvider().GetClientFullModel().KontaktneOsebe.Where(ko => ko.idKontaktneOsebe == contactPersonID).FirstOrDefault();
                        }

                        if (model != null)
                        {
                            GetClientDataProvider().SetContactPersonModel(model);
                            FillForm();
                        }
                    }
                }
                else if (userAction == (int)Enums.UserAction.Add)
                {
                    SetFromDefaultValues();
                }
                UserActionConfirmBtnUpdate(btnConfirm, userAction, true);
            }
        }

        private void Initialize()
        {

        }

        private void FillForm()
        {
            txtName.Text = model.NazivKontaktneOsebe;
            txtSignature.Text = model.NazivPodpis;
            txtPhone.Text = model.Telefon;
            txtPhoneGSM.Text = model.GSM;
            txtFax.Text = model.Fax;
            txtEmail.Text = model.Email;
            MemoNotes.Text = model.Opombe;
        }

        private bool AddOrEditEntityObject(bool add = false)
        {
            if (add)
            {
                model = new ContactPersonModel();

                model.idKontaktneOsebe = 0;
                model.idStranka = GetClientDataProvider().GetClientFullModel().idStranka;

                model.tsIDOsebe = PrincipalHelper.GetUserPrincipal().ID;
            }
            else if (model == null && !add)
            {
                model = GetClientDataProvider().GetContactPersonModel();
            }

            model.NazivKontaktneOsebe = txtName.Text;
            model.NazivPodpis = txtSignature.Text;
            model.Telefon = txtPhone.Text;
            model.GSM = txtPhoneGSM.Text;
            model.Fax = txtFax.Text;
            model.Email = txtEmail.Text;
            model.Opombe = MemoNotes.Text;

            ContactPersonModel newModel = CheckModelValidation(GetDatabaseConnectionInstance().SaveContactPersonChanges(model));

            if (newModel != null)//If new record is added we need to refresh aspxgridview. We add new record to session model.
            {
                model = newModel;

                if (userAction != (int)(Enums.UserAction.Add))
                {
                    var pozicije = GetClientDataProvider().GetClientFullModel().KontaktneOsebe;
                    var removeItem = pozicije.Where(ko => ko.idKontaktneOsebe == contactPersonID).FirstOrDefault();

                    pozicije.Remove(removeItem);
                }

                if (GetClientDataProvider().GetClientFullModel().KontaktneOsebe == null)
                    GetClientDataProvider().GetClientFullModel().KontaktneOsebe = new List<ContactPersonModel>();

                GetClientDataProvider().GetClientFullModel().KontaktneOsebe.Add(model);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetFromDefaultValues()
        { }

        #region Helper methods
        private void RemoveSessionsAndClosePopUP(bool confirm = false)
        {
            string confirmCancelAction = "Preklici";

            if (confirm)
                confirmCancelAction = "Potrdi";

            RemoveSession(Enums.ClientSession.ContactPersonID);
            RemoveSession(Enums.ClientSession.ContactPersonModel);
            RemoveSession(Enums.CommonSession.UserActionPopUp);

            ClientScript.RegisterStartupScript(GetType(), "ANY_KEY", string.Format("window.parent.OnClosePopUpHandler('{0}','{1}');", confirmCancelAction, "ContactPerson"), true);
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

        private bool DeleteObject()
        {
            var isDeleted = CheckModelValidation(GetDatabaseConnectionInstance().DeleteContactPerson(contactPersonID, GetClientDataProvider().GetClientFullModel().idStranka));

            if (isDeleted)
            {
                var pozicije = GetClientDataProvider().GetClientFullModel().KontaktneOsebe;
                var removeItem = pozicije.Where(ko => ko.idKontaktneOsebe == contactPersonID).FirstOrDefault();

                pozicije.Remove(removeItem);
            }

            return isDeleted;
        }

        private void ProcessUserAction()
        {
            bool isValid = false;
            bool confirm = false;

            switch (userAction)
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
    }
}