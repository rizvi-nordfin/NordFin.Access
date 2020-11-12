using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Data;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class ClientInformationDataAccessLayer : DBBase, IClientInformationBusinessDataLayer
    {
        Tuple<ClientInformation, ClientDetails> IClientInformationBusinessDataLayer.getClientInformationLogin(string ClientID, string UserID)
        {
            ClientInformation clientInformation = new ClientInformation();
            DBInitialize("usp_getClientInformationLogin");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(UserID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                clientInformation.AccessSuccess = dataSet.Tables[0].Rows[0].Field<int>("AccessSuccess");
                clientInformation.AccessFail = dataSet.Tables[0].Rows[0].Field<int>("AccessFail");
                clientInformation.MypageSuccess = dataSet.Tables[0].Rows[1].Field<int>("AccessSuccess");
                clientInformation.MypageFail = dataSet.Tables[0].Rows[1].Field<int>("AccessFail");
            }

            ClientDetails clientDetails = new ClientDetails();
            if (dataSet.Tables.Count > 1 && dataSet.Tables[1].Rows.Count > 0)
            {
                clientDetails = dataSet.Tables[1].AsEnumerable().Select(dataRow => new ClientDetails
                {

                    Orgnumber = dataRow.Field<string>("Orgnumber"),
                    Clientaddress = dataRow.Field<string>("Clientaddress"),
                    ClientPostalCode = dataRow.Field<string>("ClientPostalCode"),
                    ClientCity = dataRow.Field<string>("ClientCity"),
                    ClientLand = dataRow.Field<string>("ClientLand"),
                    ContactEmail = dataRow.Field<string>("ContactEmail")

                }).ToList()[0];
            }

            Tuple<ClientInformation, ClientDetails> tuple = new Tuple<ClientInformation, ClientDetails>(clientInformation, clientDetails);
            return tuple;


        }
    }
}
