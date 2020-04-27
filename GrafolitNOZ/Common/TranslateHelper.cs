using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DatabaseWebService.Common.Enums.Enums;
using static GrafolitNOZ.Common.Enums;

namespace GrafolitNOZ.Common
{
    public static class TranslateHelper
    {
        public static string GetTranslateValueByContentAndLanguage(Language langT, ReportContentType _ReportCType)
        {
            string RetStr = "";

            switch (_ReportCType)
            {
                case ReportContentType.GREETINGS:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "Hello, \r\n\r\n We kindly ask for the best delivery date for the material:";
                            break;
                        case Language.HRV:
                            RetStr = "Pozdrav, \r \n \r \n Ljubazno vas molimo za najbolji datum isporuke materijala: ";
                            break;
                        case Language.SLO:
                            RetStr = "Pozdravljeni, \r \n \r \n Vljudno vas prosimo za najboljši možni dobavni rok za material:";
                            break;
                        default:
                            break;
                    }
                    break;
                case ReportContentType.REGARDS:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "Thank you and best regards,";
                            break;
                        case Language.HRV:
                            RetStr = "Hvala i srdačan pozdrav,";
                            break;
                        case Language.SLO:
                            RetStr = "Hvala in lep pozdrav,";
                            break;
                        default:
                            break;
                    }
                    break;
                case ReportContentType.INQUIRY:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "ENQUIRY";
                            break;
                        case Language.HRV:
                            RetStr = "UPIT";
                            break;
                        case Language.SLO:
                            RetStr = "POVPRAŠEVANJE";
                            break;
                        default:
                            break;
                    }
                    break;
                case ReportContentType.MATERIAL:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "MATERIAL";
                            break;
                        case Language.HRV:
                            RetStr = "ARTIKEL";
                            break;
                        case Language.SLO:
                            RetStr = "ARTIKEL";
                            break;
                        default:
                            break;
                    }
                    break;
                case ReportContentType.QUANTITY:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "QUANTITY";
                            break;
                        case Language.HRV:
                            RetStr = "KOLIČINA";
                            break;
                        case Language.SLO:
                            RetStr = "KOLIČINA";
                            break;
                        default:
                            break;
                    }
                    break;
                case ReportContentType.NOTES:
                    switch (langT)
                    {
                        case Language.ANG:
                            RetStr = "NOTES";
                            break;
                        case Language.HRV:
                            RetStr = "OPOMBA";
                            break;
                        case Language.SLO:
                            RetStr = "OPOMBA";
                            break;
                        default:
                            break;
                    }
                    break;
            }


            return RetStr;
        }
    }
}