using DatabaseWebService.Models.Client;
using GrafolitNOZ.Common;
using GrafolitNOZ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Helpers.DataProviders
{
    public class ClientDataProvider :ServerMasterPage
    {
        public void SetClientFullModel(ClientFullModel model)
        {
           AddValueToSession(Enums.ClientSession.ClientFullModel, model);
        }

        public ClientFullModel GetClientFullModel()
        {
            if (SessionHasValue(Enums.ClientSession.ClientFullModel))
                return (ClientFullModel)GetValueFromSession(Enums.ClientSession.ClientFullModel);

            return null;
        }

        public void SetClientID(int clientID)
        {
            AddValueToSession(Enums.ClientSession.ClientID, clientID);
        }

        public int GetClientID()
        {
            if (SessionHasValue(Enums.ClientSession.ClientID))
                return (int)GetValueFromSession(Enums.ClientSession.ClientID);

            return -1;
        }

        public void SetContactPersonModel(ContactPersonModel model)
        {
            AddValueToSession(Enums.ClientSession.ContactPersonModel, model);
        }

        public ContactPersonModel GetContactPersonModel()
        {
            if (SessionHasValue(Enums.ClientSession.ContactPersonModel))
                return (ContactPersonModel)GetValueFromSession(Enums.ClientSession.ContactPersonModel);

            return null;
        }
    }
}