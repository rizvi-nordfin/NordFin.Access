using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Nordfin
{
    public static class Utilities
    {
        public static string BuildOcr(string invoice, int ocrLength, string prefix, string country)
        {
            switch(country)
            {
                case "Sweden":
                    return BuildOcrForSweden(invoice, ocrLength, prefix);
                case "Finland":
                    return BuildOcrForFinland(invoice, prefix);
                default:
                    return string.Empty;
            }
        }

        public static int CalculateCheckSum(string inputString, string country)
        {
            switch (country)
            {
                case "Sweden":
                    return CalculateCheckSumSweden(inputString);
                case "Finland":
                    return CalculateCheckSumFinland(inputString);
                default:
                    return 0;
            }
        }

        private static string BuildOcrForSweden(string invoice, int ocrLength, string prefix)
        {
            string ocr;
            string lengthdigit;

            invoice = Regex.Replace(invoice, "[^0-9]", string.Empty);
            var inputLength = (prefix.Length + invoice.Length + 2).ToString();
            if (ocrLength == 0)
            {
                lengthdigit = inputLength.Substring(inputLength.Length - 1, 1);
                ocr = prefix + invoice + lengthdigit;
            }
            else if (ocrLength < inputLength.Length)
            {
                return "Error";
            }
            else
            {
                if (ocrLength == 2)
                {
                    lengthdigit = ocrLength.ToString().Substring(1, 1);
                }
                else
                {
                    lengthdigit = ocrLength.ToString();
                }

                ocr = prefix + invoice.PadLeft(ocrLength - prefix.Length - invoice.Length - 2, '0') + lengthdigit;
            }

            ocr += CalculateCheckSumSweden(ocr);
            return ocr;
        }

        private static string BuildOcrForFinland(string invoice, string prefix)
        {
            string ocr;
            invoice = Regex.Replace(invoice, "[^0-9]", string.Empty);
            ocr = prefix + invoice;
            ocr += CalculateCheckSumFinland(ocr);
            return ocr;
        }

        private static int CalculateCheckSumSweden(string inputString)
        {
            int checksum = 0;
            int check;
            var len = inputString.Length;

            while (len != 0)
            {
                len -= 1;
                check = int.Parse(inputString.Substring(len, 1)) * 2;
                if (check > 9)
                {
                    checksum = checksum + int.Parse(check.ToString().Substring(0, 1)) + (check % 10);
                }
                else
                {
                    checksum += check;
                }

                if (len > 0)
                {
                    len -= 1;
                    check = int.Parse(inputString.Substring(len, 1));
                    checksum += check;
                }
            }

            if (checksum.ToString().EndsWith("0"))
            {
                check = 0;
            }
            else
            {
                check = 10 - int.Parse(checksum.ToString().Substring(checksum.ToString().Length - 1, 1));
            }

            return check;
        }

        private static int CalculateCheckSumFinland(string inputString)
        {
            int len = inputString.Length;
            int check = 0;

            while (len != 0)
            {
                len -= 1;
                check = (int)(check + (Convert.ToDouble(inputString.ToString().Substring(len, 1)) * 7));
                if (len > 0)
                {
                    len -= 1;
                    check = (int)(check + (Convert.ToDouble(inputString.ToString().Substring(len, 1)) * 3));
                }

                if (len > 0)
                {
                    len -= 1;
                    check = (int)(check + Convert.ToDouble(inputString.ToString().Substring(len, 1)));
                }
            }

            if (check.ToString().EndsWith("0"))
            {
                check = 0;
            }
            else
            {
                check = (int)(10 - Convert.ToDouble(check.ToString().Substring(check.ToString().Length - 1, 1)));
            }

            return check;
        }
    }
}