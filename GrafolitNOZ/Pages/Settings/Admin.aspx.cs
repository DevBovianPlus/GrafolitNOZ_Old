
using GrafolitNOZ.Common;
using GrafolitNOZ.Helpers;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrafolitNOZ.Pages.Settings
{
    public partial class Admin : ServerMasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            this.Master.PageHeadlineTitle = this.Title;
            this.Master.DisableNavBar = true;

            AllowUserWithRole(Enums.UserRole.SuperAdmin);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetLogs_Click(object sender, EventArgs e)
        {
            byte[] bytes = CheckModelValidation(GetDatabaseConnectionInstance().GetWebServiceLogFile());
            //byte[] UtilityServbytes = CheckModelValidation(GetDatabaseConnectionInstance().GetUtilityServiceLogFile());

            string applicationLogFile = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
            byte[] applicationBytes = System.IO.File.ReadAllBytes(applicationLogFile);

            List<FileToDownload> list = new List<FileToDownload> { new FileToDownload { Name = "WebServiceLog.txt", Content = bytes, Extension=".txt" },
                new FileToDownload { Name = "ApplicationLog", Content = applicationBytes, Extension=".txt" }, /*new FileToDownload { Name = "UtilityServiceLog.txt", Content = UtilityServbytes, Extension=".txt" }*/ };

            byte[] zip = CommonMethods.GetZipMemmoryStream(list);

            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment;filename=Logs.zip");
            Response.Buffer = true;
            Response.BinaryWrite(zip);

            Response.Flush();
            Response.End();
        }
    }
}