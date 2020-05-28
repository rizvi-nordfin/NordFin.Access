using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Nordfin.workflow.Entity;
using Nordfin.workflow.BusinessDataLayerInterface;

namespace Nordfin.workflow.DataAccessLayer
{
    public class UserLoginInformationDataAccessLayer: DBBase, IUserLoginInformationBusinessDataLayer
    {
        DataSet IUserLoginInformationBusinessDataLayer.GetUserLoginInformation(string UserName, long Date)
        {
            DBInitialize("usp_getUserLoginInformation");
            DatabaseName.AddInParameter(DBBaseCommand, "@CurrentTime", System.Data.DbType.Int64, Date);
            
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        DataSet IUserLoginInformationBusinessDataLayer.GetTrafficDetails()
        {
            DBInitialize("usp_getUserLoginInfoDetails");
         

            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

    }
}
