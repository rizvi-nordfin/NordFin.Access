using Newtonsoft.Json.Linq;
using Nordfin.workflow.Entity;
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
                case "Sverige":
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
                case "Sverige":
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

        public static string Execute(string invNumber)
        {
            var r = Regex.Replace(invNumber, "[^0-9]", "");

            var n = int.TryParse(r, out var x) ? x : 0;

            // n is invoice number which is int or only numbers

            /* if invoice number is more than 9 digits it will keep only 9 digits from the right and remove the rest but 
             it will not change invoice number in the file name */

            if (r.Length > 9)
                n = int.TryParse(r.Remove(0, r.Length - 9), out var m) ? m : 0;
            const int i = 1000000000;
            const int n2 = 100000;
            var newPath = "";
            var index = 0;
            var n1 = 0;
            var n3 = n2;

            while (index < i)
            {
                index++;
                if (n > n1 && n <= n3)
                {
                    newPath = n1 + "_" + n3;
                    break;
                }
                n1 += n2;
                n3 += n2;
            }
            return newPath;
        }

        public static Column ConstructColumn(string value, string index)
        {
            return new Column
            {
                Index = index?.Trim(),
                Col = value?.Trim(),
            };
        }

        public static Row ConstructRow(string type = null)
        {
            return new Row
            {
                Type = type?.Trim(),
            };
        }

        public static Row ConstructHeaderRow(IEnumerable<TransformationHeader> headerElements)
        {
            int index = 1;
            Row headerRow = ConstructRow("Header");
            foreach (var header in headerElements)
            {
                headerRow.Col.Add(ConstructColumn(header.HeaderName, (index++).ToString()));
            }

            return headerRow;
        }

        public static InvoiceFile ConstructInvoiceFile(Inv invoice, Client client, List<ManualInvoiceMapping> manualInvoiceMappings, List<TransformationHeader> transformationHeaders)
        {
            var invoiceFile = new InvoiceFile();
            var invoiceText = new InvoiceText();
            var invoiceDetail = new InvoiceDetail();
            invoiceFile.Invoices.Add(invoice);
            invoiceFile.Client = client;
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(invoiceFile);
            var jsonTokens = JToken.Parse(jsonString);
            var invoiceTextMapping = manualInvoiceMappings.Where(h => h.SectionName == "InvoiceText").Select(h => h);
            var invoiceDetailMapping = manualInvoiceMappings.Where(h => h.SectionName == "InvoiceDetail").Select(h => h);
            var parsing = manualInvoiceMappings.FirstOrDefault(h => h.SectionName == "Parsing" && h.MappingValue == "UseInvoiceRow")?.OutputValue;
            bool.TryParse(parsing, out bool useInvoiceRow);

            if(invoiceDetailMapping.Any())
            {
                invoiceDetail.Rows.AddRange(CreateTransformationRows(jsonTokens, invoiceDetailMapping));
            }

            if (!useInvoiceRow)
            {
                invoiceText.Rows.AddRange(CreateTransformationRows(jsonTokens, invoiceTextMapping));
            }
            else
            {
                var headers = transformationHeaders.Where(h => h.SectionName == "InvoiceText").Select(h => h);
                invoiceText.Rows.Add(ConstructHeaderRow(headers));

                var rowId = 0;
                foreach (var invoiceRow in invoice.InvoiceRows)
                {
                    var row = ConstructRow();
                    var index = 1;
                    foreach (var item in invoiceTextMapping)
                    {
                        var manualTag = item.MappingValue.Replace("[x]", "[" + rowId.ToString() + "]");
                        var tokenValue = jsonTokens.SelectToken(manualTag)?.ToString();
                        var value = tokenValue ?? item.AdditionalText;
                        row.Col.Add(ConstructColumn(value, index.ToString()));
                        index++;
                    }

                    invoiceText.Rows.Add(row);
                    rowId++;
                }
            }

            invoiceText.Columns = invoiceText.Rows.Max(r => r.Col.Count);
            invoice.Print.InvoiceText = invoiceText;

            invoiceDetail.Columns = invoiceDetail.Rows.Any()? invoiceDetail.Rows.Max(r => r.Col.Count) : 0;
            invoice.Print.InvoiceDetail = invoiceDetail;
            return invoiceFile;
        }

        private static List<Row> CreateTransformationRows(JToken jToken, IEnumerable<ManualInvoiceMapping> mappings)
        {
            var rowList = new List<Row>();
            foreach (var item in mappings)
            {
                var row = item.MappingValue == "Header" ? ConstructRow("Header") : ConstructRow();
                row.Col.Add(ConstructColumn(item.OutputValue, "1"));
                var value = jToken.SelectToken(item.MappingValue)?.ToString() + " " + item.AdditionalText;
                row.Col.Add(ConstructColumn(value, "2"));
                rowList.Add(row);
            }
            return rowList;
        }
    }
}