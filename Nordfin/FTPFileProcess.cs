using Nordfin.workflow.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;


namespace Nordfin
{
    public class FTPFileProcess
    {
        private string FTPDomain { get; set; }
        private string FTPUserName { get; set; }
        private string FTPPassword { get; set; }
        private string FTPAzureUserName { get; set; }
        private string FTPAzurePassword { get; set; }
        private string FTPAzureDomain { get; set; }
        public FTPFileProcess()
        {
            FTPUserName = System.Configuration.ConfigurationManager.AppSettings["FTPUserName"].ToString();
            FTPPassword = System.Configuration.ConfigurationManager.AppSettings["FTPPassword"].ToString();
            FTPDomain = System.Configuration.ConfigurationManager.AppSettings["FTPDomain"].ToString();
            FTPAzureUserName = System.Configuration.ConfigurationManager.AppSettings["FTPAzureUserName"].ToString();
            FTPAzurePassword = System.Configuration.ConfigurationManager.AppSettings["FTPAzurePassword"].ToString();
            FTPAzureDomain = System.Configuration.ConfigurationManager.AppSettings["FTPAzureDomain"].ToString();
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CertificateValidation);
        }


        public string GetFilesDetailsFromFTP(string ClientName,string FileName)
        {
            IList<FTPFileDetails> objFTPFileDetails = new List<FTPFileDetails>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPDomain + ClientName);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.EnableSsl = true;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {

                            while (!reader.EndOfStream)
                            {
                                string ftpFileName = reader.ReadLine().ToString().Split('/')[1];
                                if (ftpFileName.Substring(0, FileName.Length) == FileName)
                                {

                                    DateTime dt1 = GetFtpFileDateTime(ftpFileName, ClientName);
                                    if ((objFTPFileDetails != null && objFTPFileDetails.Count > 0) && dt1 > objFTPFileDetails[0].FileCreationDate)
                                    {
                                        objFTPFileDetails.RemoveAt(0);
                                        objFTPFileDetails.Add(new FTPFileDetails() { FileCreationDate = dt1, FileName = ftpFileName });
                                    }
                                    else if (objFTPFileDetails.Count == 0)
                                        objFTPFileDetails.Add(new FTPFileDetails() { FileCreationDate = dt1, FileName = ftpFileName });

                                }
                            }
                        }
                    }
                        
                }





            }
            catch (WebException ex)
            {
                throw new Exception((ex.Response as FtpWebResponse).StatusDescription);
            }

            return (objFTPFileDetails.Count > 0) ? objFTPFileDetails[0].FileName : "";

        }

        private DateTime GetFtpFileDateTime(string FileName,string FolderName)
        {
            string uri = FTPDomain + FolderName + "/" + FileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.EnableSsl = true;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
               

        }

        public bool FileDownload(string FolderName,string subfolder,string FileName,out string ResultFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPAzureDomain + FolderName +  "/" + subfolder + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(FTPAzureUserName, FTPAzurePassword);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.EnableSsl = true;
            try
            {
                using (FtpWebResponse objresponse = (FtpWebResponse)request.GetResponse())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        objresponse.GetResponseStream().CopyTo(stream);
                        string sDirectory = "~/Documents/" + HttpContext.Current.Session.SessionID;
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(sDirectory));

                        }
                        string sFileName = "~/Documents/" + HttpContext.Current.Session.SessionID + "/" + FileName;
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath(sFileName), stream.ToArray());
                        ResultFileName = "";
                    }
                    return true;
                }
            }
            catch(Exception ex)
            {
                bool bfalse = FileArchiveDownload(FolderName, FileName,out string sResultFileName);
                ResultFileName = sResultFileName;
                return bfalse;
            }
        }


        public bool FileArchiveDownload(string FolderName, string FileName, out string ResultFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPDomain + FolderName + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.EnableSsl = true;
            try
            {
                using (FtpWebResponse objresponse = (FtpWebResponse)request.GetResponse())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        objresponse.GetResponseStream().CopyTo(stream);
                        string sDirectory = "~/Documents/" + HttpContext.Current.Session.SessionID;
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(sDirectory));

                        }
                        string sFileName = "~/Documents/" + HttpContext.Current.Session.SessionID + "/" + FileName;
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath(sFileName), stream.ToArray());
                        ResultFileName = "";
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                string sFileNameDownload=  GetCombinedFilesDetailsFromFTP(FolderName, FileName.Replace("_inv.pdf",""));
                bool bReturn = CombinedFileDownload(FolderName, sFileNameDownload,out string sCombinedResultFileName );
                ResultFileName = sCombinedResultFileName;
                return bReturn;
            }
        }

        private bool CertificateValidation(Object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors Errors)
        {

            return true;
        }



        public string GetCombinedFilesDetailsFromFTP(string ClientName, string FileName)
        {
            IList<FTPFileDetails> objFTPFileDetails = new List<FTPFileDetails>();
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPDomain + ClientName);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
                request.UseBinary = true;
                request.UsePassive = true;
                request.KeepAlive = true;
                request.EnableSsl = true;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            try
                            {
                                while (!reader.EndOfStream)
                                {
                                    string ftpCombinedFileName = reader.ReadLine().ToString().Split('/')[1];

                                    if (ftpCombinedFileName != "Archive")
                                    {
                                        try
                                        {
                                            if (ftpCombinedFileName.Substring(0, FileName.Length) == FileName)
                                            {

                                                DateTime dt1 = GetFtpFileDateTime(ftpCombinedFileName, ClientName);
                                                if ((objFTPFileDetails != null && objFTPFileDetails.Count > 0) && dt1 > objFTPFileDetails[0].FileCreationDate)
                                                {
                                                    objFTPFileDetails.RemoveAt(0);
                                                    objFTPFileDetails.Add(new FTPFileDetails() { FileCreationDate = dt1, FileName = ftpCombinedFileName });
                                                }
                                                else if (objFTPFileDetails.Count == 0)
                                                    objFTPFileDetails.Add(new FTPFileDetails() { FileCreationDate = dt1, FileName = ftpCombinedFileName });

                                            }
                                        }
                                        catch {
                                            //catch the issue
                                        }
                                    }
                                }
                            }
                            catch {
                                //catch the issue
                            }
                        }
                    }

                }





            }
            catch (WebException ex)
            {
                //catch the issue
            }

            return (objFTPFileDetails.Count > 0) ? objFTPFileDetails[0].FileName : "";

        }



        private DateTime GetFtpCombinedFileDateTime(string FileName, string FolderName)
        {
            string uri = FTPDomain + FolderName + "/" + FileName;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
            request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.EnableSsl = true;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }


        }

        public bool CombinedFileDownload(string FolderName, string FileName,out string sCombinedResultFileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTPDomain + FolderName + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(FTPUserName, FTPPassword);
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.EnableSsl = true;
            try
            {
                using (FtpWebResponse objresponse = (FtpWebResponse)request.GetResponse())
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        objresponse.GetResponseStream().CopyTo(stream);
                        string sDirectory = "~/Documents/" + HttpContext.Current.Session.SessionID;
                        if (!Directory.Exists(HttpContext.Current.Server.MapPath(sDirectory)))
                        {
                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath(sDirectory));

                        }
                        string sFileName = "~/Documents/" + HttpContext.Current.Session.SessionID + "/" + FileName;
                        File.WriteAllBytes(HttpContext.Current.Server.MapPath(sFileName), stream.ToArray());
                        sCombinedResultFileName = FileName;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                sCombinedResultFileName = "";
                return false;
            }
        }
   
    }
}