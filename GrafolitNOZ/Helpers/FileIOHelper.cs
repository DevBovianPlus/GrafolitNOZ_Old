using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers
{
    public static class FileIOHelper
    {
        static string inquiryReportDirectory = "InquiryReports";
        static string inquiryFileNamePrefix = "Inquiry_";

        public static string InquiryReportDirectory
        {
            get
            {
                var directoryPath = AppDomain.CurrentDomain.BaseDirectory + inquiryReportDirectory;
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                return directoryPath;
            }
        }

        public static string InquiryFileName
        {
            get
            {
                return inquiryFileNamePrefix + DateTime.Now.ToString("dd_MM_yyyy_hh_mm_") + DateTime.Now.Ticks.ToString();
            }
        }
    }
}