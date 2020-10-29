using Nordfin.workflow.BusinessDataLayerInterface;
using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Nordfin.workflow.DataAccessLayer
{
    public class PaymentInformationDataAccessLayer : DBBase, IPaymentInformationBusinessDataLayer
    {

        Tuple<PaymentInformation,IList<Notes>> IPaymentInformationBusinessDataLayer.GetPaymentInformation(string InvoiceNumber, string ClientID)
        {
            
            PaymentInformation objpaymentinfo = new PaymentInformation();

            DBInitialize("usp_getPaymentInformation");
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceNum", System.Data.DbType.String, InvoiceNumber);
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(ClientID));


            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objpaymentinfo.PaymentReference = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                objpaymentinfo.Delivery = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                objpaymentinfo.Collectionstatus = ds.Tables[0].Rows[0].ItemArray[2].ToString();
                objpaymentinfo.CollectionDate = string.IsNullOrEmpty(ds.Tables[0].Rows[0].ItemArray[3].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[3]);
                objpaymentinfo.DueDate = string.IsNullOrEmpty(ds.Tables[0].Rows[0].ItemArray[4].ToString()) ? DateTime.MinValue : Convert.ToDateTime(ds.Tables[0].Rows[0].ItemArray[4]);
                objpaymentinfo.Collectionstop = ds.Tables[0].Rows[0].ItemArray[5].ToString();
                objpaymentinfo.Collectionstopuntil = ds.Tables[0].Rows[0].ItemArray[6].ToString();
                objpaymentinfo.Paymentmethod = ds.Tables[0].Rows[0].ItemArray[7].ToString();
                objpaymentinfo.Purchased = ds.Tables[0].Rows[0].ItemArray[8].ToString();
                objpaymentinfo.Contested = ds.Tables[0].Rows[0].ItemArray[9].ToString();
                objpaymentinfo.ContestedDate = ds.Tables[0].Rows[0].ItemArray[10].ToString();

            }
            IList<Notes> objlstNotes = new List<Notes>();
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                objlstNotes = ds.Tables[1].AsEnumerable().Select(dataRow => new Notes
                {
                    InvoiceNumber = dataRow.Field<string>("InvoiceID"),
                    NoteType = dataRow.Field<string>("NoteType"),
                    NoteDate =Convert.ToDateTime(dataRow.Field<string>("NoteDate")).ToString("yyyy-MM-dd HH:mm"),
                    UserName = dataRow.Field<string>("UserName"),
                    NoteText = dataRow.Field<string>("NoteText")

                }).ToList();
            }
            Tuple<PaymentInformation, IList<Notes>> tuple = new Tuple<PaymentInformation, IList<Notes>>(objpaymentinfo, objlstNotes);
            return tuple;

        }

        PaymentInformationDTO IPaymentInformationBusinessDataLayer.GetPaymentInformationPayments(string InvoiceID)
        {
            PaymentInformationDTO objPaymentInfoList = new PaymentInformationDTO();

            DBInitialize("usp_getPaymentInformationPayments");
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceID", System.Data.DbType.Int32, Convert.ToInt32(InvoiceID));

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    objPaymentInfoList.objPaymentsList = ds.Tables[0].AsEnumerable().Select(dataRow => new Payments
                    {
                        PayDate = dataRow.Field<DateTime>("paydate").ToString("yyyy-MM-dd")
                        ,
                        PayAmount = dataRow.Field<string>("payamount"),
                        PayRef = dataRow.Field<string>("payref")
                    }).ToList();
                }
                else
                    objPaymentInfoList.objPaymentsList.Add(new Payments());
                if (ds.Tables[1].Rows.Count > 0)
                {

                    objPaymentInfoList.objFeesList = ds.Tables[1].AsEnumerable().Select(dataRow => new Fees
                    {
                        FeeDate = dataRow.Field<DateTime>("feedate").ToString("yyyy-MM-dd"),
                        FeeAmount = dataRow.Field<string>("amount"),
                        FeeRemainingAmount = dataRow.Field<string>("remainingamount"),
                        FeeType = dataRow.Field<string>("feetype"),
                        FeeID = dataRow.Field<int>("FeeID"),
                        FeeCurrency = dataRow.Field<string>("Currency")

                    }).ToList();
                }
                else
                    objPaymentInfoList.objFeesList.Add(new Fees());

                if (ds.Tables[2].Rows.Count > 0)
                {

                    objPaymentInfoList.objInterestList = ds.Tables[2].AsEnumerable().Select(dataRow => new Interest
                    {
                        InterestID = dataRow.Field<int>("InterestID"),
                        InterestDate = dataRow.Field<string>("interestdate"),
                        InterestAmount = dataRow.Field<string>("interestamount"),
                        InterestRemainingAmount = dataRow.Field<string>("remainingamount")

                    }).ToList();
                }
                else
                    objPaymentInfoList.objInterestList.Add(new Interest());
                if (ds.Tables[3].Rows.Count > 0)
                {

                    objPaymentInfoList.objPayoutList = ds.Tables[3].AsEnumerable().Select(dataRow => new Payout
                    {
                        BankAccount = dataRow.Field<string>("BankAccount"),
                        OverpayCreditID = dataRow.Field<int>("overpay_id"),
                        PayoutID= dataRow.Field<int>("PayoutID")
                    }).ToList();
                }
                else
                    objPaymentInfoList.objPayoutList.Add(new Payout() { BankAccount = "", OverpayCreditID = -1 });

                if (ds.Tables[4].Rows.Count > 0)
                {

                    objPaymentInfoList.objTransList = ds.Tables[4].AsEnumerable().Select(dataRow => new Transaction
                    {

                        BookingDate = dataRow.Field<string>("BookingDate"),
                        TransactionAmount = dataRow.Field<string>("TransactionAmount"),
                        TransactionText = dataRow.Field<string>("TransactionText"),
                        TransactionType = dataRow.Field<string>("TransactionType")
                    }).ToList();
                }
                else
                    objPaymentInfoList.objTransList.Add(new Transaction());
            }
            return objPaymentInfoList;
        }

        IList<Fees> IPaymentInformationBusinessDataLayer.savePaymentFeeDetails(FessDetails objFeeDetails)
        {
            DBInitialize("usp_setPaymentFees");
            DatabaseName.AddInParameter(DBBaseCommand, "@amount", System.Data.DbType.String, objFeeDetails.PayAmount);
            DatabaseName.AddInParameter(DBBaseCommand, "@userID", System.Data.DbType.Int32, Convert.ToInt32(objFeeDetails.UserID));
            DatabaseName.AddInParameter(DBBaseCommand, "@currency", System.Data.DbType.String, objFeeDetails.Feecurrency);
            DatabaseName.AddInParameter(DBBaseCommand, "@feeID", System.Data.DbType.Int32, Convert.ToInt32(objFeeDetails.PayFeeID));
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceID", System.Data.DbType.Int32, Convert.ToInt32(objFeeDetails.InvoiceID));
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(objFeeDetails.ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@userName", System.Data.DbType.String, objFeeDetails.UserName);
            DatabaseName.AddInParameter(DBBaseCommand, "@invoiceNum", System.Data.DbType.String, objFeeDetails.InvoiceNumber);
            DatabaseName.AddInParameter(DBBaseCommand, "@custNum", System.Data.DbType.String, objFeeDetails.CustNum);


            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            IList<Fees> objFeesList = new List<Fees>();
            if (ds.Tables.Count > 0)
            {

                objFeesList = ds.Tables[0].AsEnumerable().Select(dataRow => new Fees
                {
                    FeeDate = dataRow.Field<DateTime>("feedate").ToString("yyyy-MM-dd"),
                    FeeAmount = dataRow.Field<string>("amount"),
                    FeeRemainingAmount = dataRow.Field<string>("remainingamount"),
                    FeeType = dataRow.Field<string>("feetype"),
                    FeeID = dataRow.Field<int>("FeeID"),
                    FeeCurrency = dataRow.Field<string>("Currency")

                }).ToList();
            }

            return objFeesList;
        }

        int IPaymentInformationBusinessDataLayer.savePayout(Payout objpayout)
        {
            DBInitialize("usp_setPayoutDetails");
            DatabaseName.AddInParameter(DBBaseCommand, "@BankAccount", System.Data.DbType.String, objpayout.BankAccount);
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceID", System.Data.DbType.Int32, objpayout.InvoiceID);
            DatabaseName.AddInParameter(DBBaseCommand, "@Active", System.Data.DbType.Int32, objpayout.Active);
            DatabaseName.AddInParameter(DBBaseCommand, "@UserID", System.Data.DbType.Int32, objpayout.UserID);
            DatabaseName.AddInParameter(DBBaseCommand, "@OverpayCreditID", System.Data.DbType.Int32, objpayout.OverpayCreditID);
            DatabaseName.AddOutParameter(DBBaseCommand, "@PayoutID", DbType.Int32, 50);
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            int iPayoutID = Convert.ToInt32(DatabaseName.GetParameterValue(DBBaseCommand, "PayoutID"));
            return iPayoutID;
        }
        IList<Interest> IPaymentInformationBusinessDataLayer.updateInterest(int InteresrID)
        {
            DBInitialize("usp_setInterest");
            DatabaseName.AddInParameter(DBBaseCommand, "@InterestID", System.Data.DbType.Int32, InteresrID);
            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);
            IList<Interest> objInterestList = new List<Interest>();
            if (ds.Tables.Count > 0)
            {

                objInterestList = ds.Tables[0].AsEnumerable().Select(dataRow => new Interest
                {
                    InterestID = dataRow.Field<int>("InterestID"),
                    InterestDate = dataRow.Field<string>("interestdate"),
                    InterestAmount = dataRow.Field<string>("interestamount"),
                    InterestRemainingAmount = dataRow.Field<string>("remainingamount")

                }).ToList();
            }

            return objInterestList;
        }

        Tuple<Notes,IList<Notes>> IPaymentInformationBusinessDataLayer.insertInterest(Notes objNotes)
        {
            Notes objNotesinfo = new Notes();
            DBInitialize("usp_setNotes");
            DatabaseName.AddInParameter(DBBaseCommand, "@NotesText", System.Data.DbType.String, objNotes.NoteText);
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.InvoiceID));
            DatabaseName.AddInParameter(DBBaseCommand, "@CustomerID", System.Data.DbType.String, objNotes.CustomerID);
            DatabaseName.AddInParameter(DBBaseCommand, "@UserID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.UserID));
            DatabaseName.AddInParameter(DBBaseCommand, "@ClientID", System.Data.DbType.Int32, Convert.ToInt32(objNotes.ClientID));
            DatabaseName.AddInParameter(DBBaseCommand, "@UserName", System.Data.DbType.String, objNotes.UserName);
            DatabaseName.AddInParameter(DBBaseCommand, "@InvoiceNum", System.Data.DbType.String, objNotes.InvoiceNumber);
            DatabaseName.AddInParameter(DBBaseCommand, "@NewDueDate", System.Data.DbType.String, objNotes.DueDateNewValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@OldDueDate", System.Data.DbType.String, objNotes.DueDateOldValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@NewCollStopUntil", System.Data.DbType.String, objNotes.CollectionStopUntilNewValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@OldCollStopUntil", System.Data.DbType.String, objNotes.CollectionStopUntilOldValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@NewCollStop", System.Data.DbType.String, objNotes.CollectionStopNewValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@OldCollStop", System.Data.DbType.String, objNotes.CollectionStopOldValue);
            DatabaseName.AddInParameter(DBBaseCommand, "@Contested", System.Data.DbType.String, objNotes.Contested);
            DatabaseName.AddInParameter(DBBaseCommand, "@ContestedDate", System.Data.DbType.String, objNotes.ContestedDate);

            DataSet ds = DatabaseName.ExecuteDataSet(DBBaseCommand);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objNotesinfo.DueDateNewValue = ds.Tables[0].Rows[0].ItemArray[0].ToString();
                objNotesinfo.CollectionStopNewValue = ds.Tables[0].Rows[0].ItemArray[1].ToString();
                objNotesinfo.CollectionStopUntilNewValue = ds.Tables[0].Rows[0].ItemArray[2].ToString();
              
            }
            IList<Notes> objlstNotes = new List<Notes>();
            if (ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                objlstNotes = ds.Tables[1].AsEnumerable().Select(dataRow => new Notes
                {
                    InvoiceNumber = dataRow.Field<string>("InvoiceNumber"),
                    NoteType = dataRow.Field<string>("NoteType"),
                    NoteDate = Convert.ToDateTime(dataRow.Field<string>("NoteDate")).ToString("yyyy-MM-dd HH:mm"),
                    UserName = dataRow.Field<string>("UserName"),
                    NoteText = dataRow.Field<string>("NoteText")

                }).ToList();
            }
            Tuple<Notes, IList<Notes>> tuple = new Tuple<Notes, IList<Notes>>(objNotesinfo, objlstNotes);
            return tuple;
        }


        int IPaymentInformationBusinessDataLayer.removePayout(int payoutID)
        {
            DBInitialize("usp_setRemovePayout");
            DatabaseName.AddInParameter(DBBaseCommand, "@payoutID", System.Data.DbType.Int32, payoutID);
           
            DatabaseName.ExecuteNonQuery(DBBaseCommand);
            return 0;
        }
    }
}
