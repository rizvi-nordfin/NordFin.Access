using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.DataAccessLayer
{
    public class TelesonGroupDataAccessLayer : DBBase, ITelsonGroupBusinessDataLayer
    {
        Tuple<IList<TelsonGroup>, IList<TelsonChart>, IList<ClientContracts>> ITelsonGroupBusinessDataLayer.GetTelsonGroupData(string ClientID)
        {
            DBInitialize("usp_getContractsDashboard");

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);


            IList<TelsonGroup> telsonData = new List<TelsonGroup>();
            IList<TelsonChart> telsonChart = new List<TelsonChart>();
            IList<ClientContracts> clientContracts = new List<ClientContracts>();
            if (ds.Tables[0].Rows.Count > 0)
            {

                telsonData = ds.Tables[0].AsEnumerable().Select(dataRow => new TelsonGroup
                {
                    StaticValue = dataRow.Field<int>("StaticValue"),
                    ColumnName = dataRow.Field<string>("TelsonColumn"),
                    SpecialCharc = dataRow.Field<string>("symbol"),
                    RowValue = (dataRow.Field<int>("NumberCast") == 1) ?
                    String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("TelsonData")) : Convert.ToString(Convert.ToInt32(dataRow.Field<decimal>("TelsonData"))),
                    PaymentValue = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("PaymentValue")),
                    NumberCast = dataRow.Field<int>("NumberCast"),
                    ColorCode = dataRow.Field<string>("ColorCode")
                }).ToList();
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                telsonChart = ds.Tables[1].AsEnumerable().Select(dataRow => new TelsonChart
                {
                    Column = dataRow.Field<string>("columnValue"),
                    Amount = Convert.ToString(dataRow.Field<decimal>("ContractAmount")),
                    Number = Convert.ToString(dataRow.Field<int>("ContractNumber"))
                }).ToList();


            }


            if (ds.Tables[2].Rows.Count > 0)
            {
                clientContracts = ds.Tables[2].AsEnumerable().Select(dataRow => new ClientContracts
                {
                    Name = dataRow.Field<string>("Name"),
                    Mortage = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("Mortage")),
                    CreditScore = Convert.ToString(dataRow.Field<string>("CreditScore"))
                }).ToList();


            }



            Tuple<IList<TelsonGroup>, IList<TelsonChart>, IList<ClientContracts>> tuple = new Tuple<IList<TelsonGroup>, IList<TelsonChart>, IList<ClientContracts>>(telsonData, telsonChart, clientContracts);
            return tuple;



        }

        DataSet ITelsonGroupBusinessDataLayer.getContractList(string clientID)
        {
            DBInitialize("usp_getContractOverview");

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return ds;
        }


        Tuple<IList<TelsonGroup>, IList<TelsonChart>> ITelsonGroupBusinessDataLayer.GetTelecomData()
        {
            DBInitialize("usp_getTelecomDetails");

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);


            IList<TelsonGroup> telsonData = new List<TelsonGroup>();
            IList<TelsonChart> telsonChart = new List<TelsonChart>();
            if (ds.Tables[0].Rows.Count > 0)
            {

                telsonData = ds.Tables[0].AsEnumerable().Select(dataRow => new TelsonGroup
                {
                    StaticValue = dataRow.Field<int>("StaticValue"),
                    ColumnName = dataRow.Field<string>("TelsonColumn"),
                    SpecialCharc = dataRow.Field<string>("symbol"),
                    RowValue = (dataRow.Field<int>("NumberCast") == 1) ?
                    String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("TelsonData")) : Convert.ToString(Convert.ToInt32(dataRow.Field<decimal>("TelsonData"))),
                    PaymentValue = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("PaymentValue")),
                    NumberCast = dataRow.Field<int>("NumberCast")
                 
                }).ToList();
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                telsonChart = ds.Tables[1].AsEnumerable().Select(dataRow => new TelsonChart
                {
                    Column = dataRow.Field<string>("columnValue"),
                    Amount = Convert.ToString(dataRow.Field<decimal>("ContractAmount")),
                    Number = Convert.ToString(dataRow.Field<int>("ContractNumber"))
                }).ToList();


            }

            Tuple<IList<TelsonGroup>, IList<TelsonChart>> tuple = new Tuple<IList<TelsonGroup>, IList<TelsonChart>>(telsonData, telsonChart);
            return tuple;



        }
    }
}
