using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;



namespace Nordfin.workflow.DataAccessLayer
{
    public class UserDataAccessLayer : DBBase, IUserBusinessDataLayer
    {

        Users IUserBusinessDataLayer.GetUser(string UserName, string Password)
        {
            Users user = new Users();
            DBInitialize("usp_getUserName");
            DatabaseName.AddInParameter(DBBaseCommand, "@Username", System.Data.DbType.String, UserName);
            DatabaseName.AddInParameter(DBBaseCommand, "@Password", System.Data.DbType.String, Password);

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                user = ds.Tables[0].AsEnumerable().Select(dataRow => new Users
                {
                    UserName = dataRow.Field<string>("Username")
                    ,
                    Admin = dataRow.Field<int>("Admin"),
                    ClientID = dataRow.Field<int>("ClientID"),
                    UserID = dataRow.Field<int>("ID"),
                    LabelUser = dataRow.Field<string>("LabelUser")
                }).ToList()[0];
            }
            return user;


        }


        IList<ClientList> IUserBusinessDataLayer.GetClientList(int Admin, int ClientID, string UserID, out string BatchValues, out int Contracts)
        {
            IList<ClientList> objClientList = new List<ClientList>();

            DBInitialize("usp_getClientName");
            DatabaseName.AddInParameter(DBBaseCommand, "@Admin", System.Data.DbType.Int32, Admin);
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, ClientID);
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.String, UserID);
            DatabaseName.AddOutParameter(DBBaseCommand, "@BatchesValue", DbType.String, 50);
            DatabaseName.AddOutParameter(DBBaseCommand, "@Contracts", DbType.Int32, 50);
            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            objClientList = ds.Tables[0].AsEnumerable().Select(dataRow => new ClientList
            {
                ClientName = dataRow.Field<string>("ClientName")
               ,
                ClientID = dataRow.Field<int>("ClientID"),

            }).ToList();

            BatchValues = Convert.ToString(DatabaseName.GetParameterValue(DBBaseCommand, "@BatchesValue"));
            Contracts =  Convert.ToInt32(DatabaseName.GetParameterValue(DBBaseCommand, "@Contracts"));
            return objClientList;
        }

        void IUserBusinessDataLayer.UpdateClientID(string UserName, string ClientID)
        {
            DBInitialize("usp_setClientID");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@Username", System.Data.DbType.String, UserName);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);

        }

        int IUserBusinessDataLayer.InsertLoginUserInformation(LoginUserInformation loginUserInformation)
        {
            DBInitialize("usp_setLogUserLogin");
            DatabaseName.AddInParameter(DBBaseCommand, "@Email", System.Data.DbType.String, loginUserInformation.Email);
            DatabaseName.AddInParameter(DBBaseCommand, "@UserIP", System.Data.DbType.String, loginUserInformation.IP);
            DatabaseName.AddInParameter(DBBaseCommand, "@HostName", System.Data.DbType.String, loginUserInformation.HostName);
            DatabaseName.AddInParameter(DBBaseCommand, "@City", System.Data.DbType.String, loginUserInformation.City);
            DatabaseName.AddInParameter(DBBaseCommand, "@Region", System.Data.DbType.String, loginUserInformation.Region);
            DatabaseName.AddInParameter(DBBaseCommand, "@Country", System.Data.DbType.String, loginUserInformation.Country);
            DatabaseName.AddInParameter(DBBaseCommand, "@Org", System.Data.DbType.String, loginUserInformation.Org);
            DatabaseName.AddInParameter(DBBaseCommand, "@Loc", System.Data.DbType.String, loginUserInformation.Loc);
            DatabaseName.AddInParameter(DBBaseCommand, "@Zip", System.Data.DbType.String, loginUserInformation.Postal);
            DatabaseName.AddInParameter(DBBaseCommand, "@Browser", System.Data.DbType.String, loginUserInformation.BrowserName);
            DatabaseName.AddInParameter(DBBaseCommand, "@Version", System.Data.DbType.String, loginUserInformation.Version);
            DatabaseName.AddInParameter(DBBaseCommand, "@OS", System.Data.DbType.String, loginUserInformation.OS);
            DatabaseName.AddInParameter(DBBaseCommand, "@LastGenerated", System.Data.DbType.String, loginUserInformation.CILastReGenerate);
            DatabaseName.AddInParameter(DBBaseCommand, "@Status", System.Data.DbType.Int32, loginUserInformation.iStatus);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);

            return 0;
        }
        int IUserBusinessDataLayer.UpdateSessionID(string UserName, long SessionID)
        {
            DBInitialize("usp_setSessionID");
            DatabaseName.AddInParameter(DBBaseCommand, "@SessionID", System.Data.DbType.Int64, SessionID);
            DatabaseName.AddInParameter(DBBaseCommand, "@Username", System.Data.DbType.String, UserName);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);

            return 0;

        }

        int IUserBusinessDataLayer.checkEmailVerification(string token)
        {
            DBInitialize("usp_setAdminEmailVerification");
            try
            {
                DatabaseName.AddInParameter(DBBaseCommand, "@token", System.Data.DbType.String, token);
                DatabaseName.AddOutParameter(DBBaseCommand, "@count", DbType.Int32, 50);
                DatabaseName.ExecuteNonQuery(DBBaseCommand);
                int iCount = Convert.ToInt32(DatabaseName.GetParameterValue(DBBaseCommand, "@count"));
                return iCount;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
