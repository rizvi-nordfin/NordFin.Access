using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class CustomerInfoDTO
    {
        public IList<CustomerData> objCustomerData = new List<CustomerData>();
        public IList<Demographics> objDemographicsList = new List<Demographics>();
        public IList<CustomerMap> objCustomerMapList = new List<CustomerMap>();
        public IList<CustomerRegion> objCustomerRegionList = new List<CustomerRegion>();
        public IList<CustomerInvoiceNumber> objInvoiceNumberList = new List<CustomerInvoiceNumber>();
        public IList<CustomerInvoiceAmount> objInvoiceAmountList = new List<CustomerInvoiceAmount>();
        public string objClientLand = "";
        public IList<CustomerGrowth> objCustomerGrowthList = new List<CustomerGrowth>();
        
    }
}
