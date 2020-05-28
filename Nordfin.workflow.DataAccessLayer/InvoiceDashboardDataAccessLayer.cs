using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class InvoiceDashboardDataAccessLayer : DBBase, IInvoiceDashboardBusinessDataLayer
    {
        DataSet IInvoiceDashboardBusinessDataLayer.getOverviewDashboard(string ClientID)
        {
           
            DBInitialize("usp_getDashboardOverview");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            return dataSet;
        }

        IList<BatchVolume> IInvoiceDashboardBusinessDataLayer.getBatchVolumeDashboard(string ClientID)
        {



            DBInitialize("usp_getDashboardBatchVolume");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.String, ClientID);

            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);

            IList<BatchVolume> objBatchVolumesList = new List<BatchVolume>();
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                objBatchVolumesList = dataSet.Tables[0].AsEnumerable().Select(dataRow => new BatchVolume
                {
                    Year = dataRow.Field<int>("Year"),
                    Month = dataRow.Field<int>("Month"),
                    InvoiceNumber = dataRow.Field<string>("Invoice Number"),
                    InvoiceAmount = string.Format("{0:#,###0}", decimal.Truncate(ConvertStringToDecimal(dataRow.Field<string>("Invoice Amount")))).Replace(","," ") 
                }).ToList();
            }

            return objBatchVolumesList;
        }


        DataSet IInvoiceDashboardBusinessDataLayer.getCurrentYearChart(string ClientID)
        {

            DBInitialize("usp_getCollectionCurrenYear");
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));

            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);


            return dataSet;
        }

        public decimal ConvertStringToDecimal(string sDecimal)
        {
            CultureInfo cultures = new CultureInfo("en-US");

            return Convert.ToDecimal(sDecimal.Replace(" ", ""), cultures);
        }


        IList<Notification> IInvoiceDashboardBusinessDataLayer.getNotificationNotes(string clientID)
        {
            DBInitialize("usp_getNotificationNotes");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);

            IList<Notification> objNotificationList = new List<Notification>();
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                objNotificationList = dataSet.Tables[0].AsEnumerable().Select(dataRow => new Notification
                {
                    NotesText = dataRow.Field<string>("NoteText"),
                    NotesUser = dataRow.Field<string>("UserName"),
                    NotesInvoice = dataRow.Field<string>("InvoiceNumber"),
                  
                }).ToList();
            }
            return objNotificationList;
        }

        CustomerInfoDTO IInvoiceDashboardBusinessDataLayer.getCustomerData(string clientID)
        {
            CustomerInfoDTO customerInfoDTO = new CustomerInfoDTO();
            DBInitialize("usp_getCustomerData");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(clientID));
            DatabaseName.AddOutParameter(DBBaseCommand, "@clientLand", DbType.String, -1);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
            IList<CustomerData> objCustomerDataList = new List<CustomerData>();
            IList<Demographics> objDemographicsList = new List<Demographics>();
            IList<CustomerMap> objCustomerMapList = new List<CustomerMap>();
            IList<CustomerRegion> objCustomerRegionList = new List<CustomerRegion>();
            IList<CustomerInvoiceNumber> objInvoiceNumberList = new List<CustomerInvoiceNumber>();
            IList<CustomerInvoiceAmount> objInvoiceAmountList = new List<CustomerInvoiceAmount>();
            IList<CustomerGrowth> customerGrowthsList = new List<CustomerGrowth>();
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                objCustomerDataList = dataSet.Tables[0].AsEnumerable().Select(dataRow => new CustomerData
                {
                    custNumber = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", dataRow.Field<int>("custCount")),
                    custType = dataRow.Field<string>("custType"),
                    TotalCust = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", dataRow.Field<int>("Total")),
                    custPercentage = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", (Convert.ToDecimal(dataRow.Field<int>("custCount")) / Convert.ToDecimal(dataRow.Field<int>("Total"))) * 100)
                    
                }).ToList();
            }
            if (dataSet.Tables[1].Columns.Count > 0)
            {
                DataTable dataTable = dataSet.Tables[1];

                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    Demographics demographics = new Demographics();
                    demographics.custColName = dataTable.Columns[i].ColumnName;

                    demographics.custRowValue = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0}", Convert.ToInt32(dataTable.Rows[0][dataTable.Columns[i].ColumnName]));
                    try
                    {
                        demographics.custPercentage = String.Format(CultureInfo.GetCultureInfo("sv-SE"), "{0:#,0.00}", (Convert.ToDecimal(dataTable.Rows[0][dataTable.Columns[i].ColumnName]) / Convert.ToDecimal(dataSet.Tables[0].Rows[0].Field<int>("PRVtotal"))) * 100);
                    }
                    catch
                    {
                        demographics.custPercentage = "0";
                    }
                    objDemographicsList.Add(demographics);

                }

            }

            //if (dataSet.Tables[2].Rows.Count > 0)
            //{

            //    objCustomerMapList = dataSet.Tables[2].AsEnumerable().Select(dataRow => new CustomerMap
            //    {
            //        CustomerCity = dataRow.Field<string>("CustomerCity"),
            //        CustomerNumber = dataRow.Field<int>("CustomerNumber")


            //    }).ToList();

            //}

            if (dataSet.Tables[2].Rows.Count > 0)
            {

                objCustomerRegionList = dataSet.Tables[2].AsEnumerable().Select(dataRow => new CustomerRegion
                {
                    CustRegion = dataRow.Field<string>("CustomerRegion"),
                    CustTotal = dataRow.Field<int>("Total")
                }).ToList();

            }

            if (dataSet.Tables[3].Rows.Count > 0)
            {

                objInvoiceNumberList = dataSet.Tables[3].AsEnumerable().Select(dataRow => new CustomerInvoiceNumber
                {
                    InvoiceDate = Convert.ToString(dataRow.Field<int>("Year")) + "/" + Convert.ToString(dataRow.Field<int>("Month")),
                    Total = dataRow[2].ToString()

                }).ToList();

            }

            if (dataSet.Tables[4].Rows.Count > 0)
            {

                objInvoiceAmountList = dataSet.Tables[4].AsEnumerable().Select(dataRow => new CustomerInvoiceAmount
                {
                    InvoiceDate = Convert.ToString(dataRow.Field<int>("Year")) + "/" + Convert.ToString(dataRow.Field<int>("Month")),
                    Total = dataRow[2].ToString()

                }).ToList();

            }
            if (dataSet.Tables[5].Rows.Count > 0)
            {

               
                CustomerGrowth customerGrowth = new CustomerGrowth();
                int yearCount =dataSet.Tables[5].AsEnumerable().Sum(a => a.Field<int>("YearDate"));
                int monthCount = 0;
                var TotalCount = dataSet.Tables[5].AsEnumerable().Where(a => a.Field<int>("MonthDate") == Convert.ToInt32(DateTime.Now.ToString("MM"))).ToList();
                if (TotalCount.Count > 0)
                    monthCount = Convert.ToInt32(TotalCount[0][0]);
                else
                    monthCount = 0;

                customerGrowth.YearCount = (yearCount > 0) ? "+" + Convert.ToString(yearCount) : Convert.ToString(yearCount);

                customerGrowth.MonthCount = (monthCount > 0) ? "+" + Convert.ToString(monthCount) : Convert.ToString(monthCount);

                customerGrowthsList.Add(customerGrowth);

            }
            else
            {
                CustomerGrowth customerGrowth = new CustomerGrowth();
                customerGrowth.YearCount = "0";
                customerGrowth.MonthCount = "0";

               customerGrowthsList.Add(customerGrowth);
            }

            //objCustomerGrowthList
            //if (dataSet.Tables[2].Rows.Count > 0)
            //{

            //    objCustomerMapList = dataSet.Tables[2].AsEnumerable().Select(dataRow => new CustomerMap
            //    {
            //        CustomerCity = dataRow.Field<string>("CustomerCity"),
            //        CustomerNumber = dataRow.Field<int>("CustomerNumber")


            //    }).ToList();

            //}

            customerInfoDTO.objCustomerData = objCustomerDataList;
            customerInfoDTO.objDemographicsList = objDemographicsList;
            customerInfoDTO.objCustomerMapList = objCustomerMapList;
            customerInfoDTO.objCustomerRegionList = objCustomerRegionList;
            customerInfoDTO.objInvoiceAmountList = objInvoiceAmountList;
            customerInfoDTO.objInvoiceNumberList = objInvoiceNumberList;
            customerInfoDTO.objCustomerGrowthList = customerGrowthsList;

            customerInfoDTO.objClientLand = Convert.ToString(DatabaseName.GetParameterValue(DBBaseCommand, "@clientLand"));
            return customerInfoDTO;
        }

        int IInvoiceDashboardBusinessDataLayer.setCustomerRegion(string sPostalCode,string sCustomerRegion,string ClientID, string IsMatch)
        {
            DBInitialize("usp_setCustomerRegion");
            DatabaseName.AddInParameter(DBBaseCommand, "@CustomerPostalCode", System.Data.DbType.String, sPostalCode);
            DatabaseName.AddInParameter(DBBaseCommand, "@CustomerRegion", System.Data.DbType.String, sCustomerRegion);
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@isPincode", System.Data.DbType.String, IsMatch);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            return 0;
        }
        IList<CustomerMap> IInvoiceDashboardBusinessDataLayer.GetCustomerMapRegion(string ClientID,string IsMatch) 
        {
            CustomerInfoDTO customerInfoDTO = new CustomerInfoDTO();
            DBInitialize("usp_getCustomerDataRegion");
            DatabaseName.AddInParameter(DBBaseCommand, "@clientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@isPincode", System.Data.DbType.String, IsMatch);
            DataSet dataSet = DatabaseName.ExecuteDataSet(DBBaseCommand);
          
            IList<CustomerMap> objCustomerMapList = new List<CustomerMap>();

            if (dataSet.Tables[0].Rows.Count > 0)
            {

                objCustomerMapList = dataSet.Tables[0].AsEnumerable().Select(dataRow => new CustomerMap
                {
                    CustomerCity = dataRow.Field<string>("CustomerCity"),
                    CustomerNumber = dataRow.Field<int>("CustomerNumber")


                }).ToList();

            }
            return objCustomerMapList;
        }

    }
}
