using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordfin.workflow.Entity
{
    public class Base
    {
        public string FormatAmount(string amount)
        {
            if (amount is string)
            {
                amount = amount.Replace(" kr", string.Empty).Replace(" €", string.Empty).Replace(" ", string.Empty).Replace(',', '.');
                var amountDecimal = Convert.ToDouble(amount, System.Globalization.CultureInfo.InvariantCulture);
                return amountDecimal.ToString().Replace(',', '.');
            }

            return amount;
        }

        public string FormatDateString(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
            {
                return date;
            }

            string[] formats = { "M/d/yyyy", "d/M/yyyy", "M-d-yyyy", "d-M-yyyy", "d-MMM-yy", "d-MMMM-yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "dd.MM.yyyy", "yyyy.MM.dd" };
            DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime newDate);

            return newDate.ToString("yyyy-MM-dd");
        }
    }
}
