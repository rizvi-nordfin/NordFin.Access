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
        Tuple<IList<TelsonGroup>, IList<TelsonChart>> ITelsonGroupBusinessDataLayer.GetTelsonGroupData(string ClientID)
        {
            DBInitialize("usp_test");

            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);


            IList<TelsonGroup> telsonData = new List<TelsonGroup>();
            IList<TelsonChart> telsonChart = new List<TelsonChart>();
            if (ds.Tables[0].Rows.Count > 0)
            {

                telsonData = ds.Tables[0].AsEnumerable().Select(dataRow => new TelsonGroup
                {
                    ColumnName = dataRow.Field<string>("TelsonColumn"),
                    SpecialCharc= dataRow.Field<string>("symbol"),
                    RowValue = (dataRow.Field<int>("NumberCast") == 1) ?
                    String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", dataRow.Field<decimal>("TelsonData")) : Convert.ToString(Convert.ToInt32(dataRow.Field<decimal>("TelsonData")))
                }).ToList();
            }


            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    telsonChart = ds.Tables[1].AsEnumerable().Select(dataRow => new TelsonChart
            //    {
            //        Column = dataRow.Field<string>("columnValue"),
            //        Amount =Convert.ToString(dataRow.Field<int>("totalAmount")),
            //        Number = Convert.ToString(dataRow.Field<int>("invoiceNumber"))
            //    }).ToList();


            //}

            DateTime date = DateTime.Now;
            DateTime newDate = date.AddMonths(-12);


            telsonChart.Add(new TelsonChart { Column = "2019/12", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/01", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/02", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/03", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/04", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/05", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/06", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/07", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/08", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/09", Number = "0", Amount = "0" });
            telsonChart.Add(new TelsonChart { Column = "2020/10", Number = "130", Amount = "547862" });
            telsonChart.Add(new TelsonChart { Column = "2020/11", Number = "136 ", Amount = "278937" });









            Tuple<IList<TelsonGroup>, IList<TelsonChart>> tuple = new Tuple<IList<TelsonGroup>, IList<TelsonChart>>(telsonData, telsonChart);
            return tuple;

           
          
        }
    }
}
