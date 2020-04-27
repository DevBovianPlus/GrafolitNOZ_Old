using DevExpress.Web;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.Client
{
    public partial class Client : ServerMasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Request.IsAuthenticated) RedirectHome();

            this.Master.PageHeadlineTitle = Title;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ASPxGridViewClient.DataBind();
            }
        }

        protected void ASPxGridViewClient_DataBinding(object sender, EventArgs e)
        {
            (sender as ASPxGridView).DataSource = CheckModelValidation(GetDatabaseConnectionInstance().GetAllClients());
            (sender as ASPxGridView).Settings.GridLines = GridLines.Both;
        }

        protected void ASPxGridViewClient_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] split = e.Parameters.Split(';');
            if (split[0] == "DblClick" && !String.IsNullOrEmpty(split[1]))
            {
                List<Enums.ClientSession> list = Enum.GetValues(typeof(Enums.ClientSession)).Cast<Enums.ClientSession>().ToList();
                ClearAllSessions(list);

                ASPxWebControl.RedirectOnCallback(GenerateURI("ClientForm.aspx", (int)Enums.UserAction.Edit, split[1]));
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<Enums.ClientSession> list = Enum.GetValues(typeof(Enums.ClientSession)).Cast<Enums.ClientSession>().ToList();
            ClearAllSessions(list);

            object valueID = ASPxGridViewClient.GetRowValues(ASPxGridViewClient.FocusedRowIndex, "idStranka");
            RedirectWithCustomURI("ClientForm.aspx", (int)Enums.UserAction.Delete, valueID);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            List<Enums.ClientSession> list = Enum.GetValues(typeof(Enums.ClientSession)).Cast<Enums.ClientSession>().ToList();
            ClearAllSessions(list);

            RedirectWithCustomURI("ClientForm.aspx", (int)Enums.UserAction.Add, 0);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            List<Enums.ClientSession> list = Enum.GetValues(typeof(Enums.ClientSession)).Cast<Enums.ClientSession>().ToList();
            ClearAllSessions(list);

            object valueID = ASPxGridViewClient.GetRowValues(ASPxGridViewClient.FocusedRowIndex, "idStranka");
            RedirectWithCustomURI("ClientForm.aspx", (int)Enums.UserAction.Edit, valueID);
        }
    }
}