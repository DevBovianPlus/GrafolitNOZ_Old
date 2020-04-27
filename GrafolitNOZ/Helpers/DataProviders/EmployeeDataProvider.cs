using DatabaseWebService.Models;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers.DataProviders
{
    public class EmployeeDataProvider : ServerMasterPage
    {
        public void SetEmployeeFullModel(EmployeeFullModel model)
        {
           AddValueToSession(Enums.EmployeeSession.EmployeeModel, model);
        }

        public EmployeeFullModel GetEmployeeFullModel()
        {
            if (SessionHasValue(Enums.EmployeeSession.EmployeeModel))
                return (EmployeeFullModel)GetValueFromSession(Enums.EmployeeSession.EmployeeModel);

            return null;
        }
    }
}