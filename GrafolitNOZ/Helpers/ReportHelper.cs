using DatabaseWebService.ModelsPDO.Inquiry;
using DevExpress.XtraPrinting;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers
{
    public static class ReportHelper
    {
        public static List<GroupedInquiryPositionsBySupplier> CreateSubmitInquiryReport(List<GroupedInquiryPositionsBySupplier> list)
        {
            //InquiryForSupplierReport report = null;
            DatabaseConnection db = new DatabaseConnection();
            string file = "";

            foreach (var item in list)
            {
                try
                {
                    file = FileIOHelper.InquiryReportDirectory + "\\" + FileIOHelper.InquiryFileName + ".pdf";
                    //report = new InquiryForSupplierReport(item);
                    //PdfExportOptions pdfOptions = report.ExportOptions.Pdf;

                    // Specify the quality of exported images.
                    //pdfOptions.ConvertImagesToJpeg = false;
                    //pdfOptions.ImageQuality = PdfJpegImageQuality.Highest;

                    //report.ExportToPdf(file, pdfOptions);

                    //Shranimo pot pdf-ja v ustrezen zapis v tabeli PovprasevanjePozicijaDobavitelj
                    item.ReportFilePath = file.Replace("\\", "/");
                    //db.SaveInquiryPositionSupplierPdfReport(item);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }                
            }
            return list;
        }
    }
}