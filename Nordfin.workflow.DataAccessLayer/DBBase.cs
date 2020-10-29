using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Configuration;


namespace Nordfin.workflow.DataAccessLayer
{
    public class DBBase
    {
        public Database DatabaseName { get; set; }
        public DbCommand DBBaseCommand { get; set; }

        

        protected void DBInitialize(string storedProcedure)
        {
            try
            {
                string Connection = "";
               Connection = ConfigurationManager.ConnectionStrings["NordfinConnec"].ToString();
                InitializeDatabase(storedProcedure, Connection);
                
            }
            catch(Exception EX)
            {
                //catch the block
            }
        }

        private void InitializeDatabase(string storedProcedure, string Connection)
        {
            if (!string.IsNullOrEmpty(Connection))
            {


                DatabaseName = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(Connection);
                DbConnection objCon = DatabaseName.CreateConnection();
                try
                {
                    objCon.Open();
                    DBBaseCommand = DatabaseName.GetStoredProcCommand(storedProcedure);
                    objCon.Close();
                }
                catch (Exception EX)
                {
                    //catch the block

                }
                finally
                {
                    if (objCon != null)
                        objCon.Close();
                }
            }
               
              
        }
    }
}
