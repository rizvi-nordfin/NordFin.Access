using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;

using System.Data;
using System.Linq;


namespace Nordfin.workflow.DataAccessLayer
{
    public class AccountSettingsDataAccessLayer : DBBase, IAccountSettingsBusinessDataLayer
    {
        Tuple<AccountSettings,DataTable> IAccountSettingsBusinessDataLayer.getAccountSettingsInfo(string ClientID,string UserID)
        {
           return getAccountSettingsInfo(ClientID, UserID);
        }
        protected Tuple<AccountSettings, DataTable> getAccountSettingsInfo(string ClientID, string UserID)
        {
            AccountSettings accountSettings = new AccountSettings();
            DBInitialize("usp_getAccountSettingsInfo");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(UserID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                accountSettings = dataSet.Tables[0].AsEnumerable().Select(dataRow => new AccountSettings
                {
                    FirstName = dataRow.Field<string>("FirstName"),
                    LastName = dataRow.Field<string>("LastName"),

                    UserName = dataRow.Field<string>("UserName"),
                    Email = dataRow.Field<string>("Email"),
                    PhoneNumber = dataRow.Field<string>("PhoneNumber"),
                    CompanyName = dataRow.Field<string>("CompanyName")

                }).ToList()[0];
            }
            Tuple<AccountSettings, DataTable> tuple = new Tuple<AccountSettings, DataTable>(accountSettings, dataSet.Tables[1]);
            return tuple;
        }

        int IAccountSettingsBusinessDataLayer.checkPasswordExists(string sUserID, string sPasswordHash)
        {
            DBInitialize("usp_getUserExsits");
            try
            {
                DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32,Convert.ToInt32(sUserID));
                DatabaseName.AddInParameter(DBBaseCommand, "@Password", System.Data.DbType.String, sPasswordHash);
                DatabaseName.AddOutParameter(DBBaseCommand, "@Count", DbType.Int32, 50);
                DatabaseName.ExecuteNonQuery(DBBaseCommand);
                int iUserCount = Convert.ToInt32(DatabaseName.GetParameterValue(DBBaseCommand, "@Count"));
                return iUserCount;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        int IAccountSettingsBusinessDataLayer.UpdateUserPassword(string userID, string password,string phoneNumber)
        {
            DBInitialize("usp_setAdminUserInformation");
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(userID));
            DatabaseName.AddInParameter(DBBaseCommand, "@userPassword", System.Data.DbType.String, password);
            DatabaseName.AddInParameter(DBBaseCommand, "@userPhone", System.Data.DbType.String, phoneNumber);
            DatabaseName.AddOutParameter(DBBaseCommand, "@Count", DbType.Int32, 50);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            int iUserCount = Convert.ToInt32(DatabaseName.GetParameterValue(DBBaseCommand, "@Count"));
            return iUserCount;
        }

        int IAccountSettingsBusinessDataLayer.UpdateUserEmail(string userID, UserEmail userEmail)
        {
            DBInitialize("usp_setUpdateUserEmail");
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(userID));
            DatabaseName.AddInParameter(DBBaseCommand, "@updateEmail", System.Data.DbType.String, userEmail.Email);
            DatabaseName.AddInParameter(DBBaseCommand, "@token", System.Data.DbType.String, userEmail.token);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            return 0;
        }


    }
}
