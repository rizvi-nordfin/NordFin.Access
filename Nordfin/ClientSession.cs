using System.Web;

namespace Nordfin
{
    public class ClientSession
    {
        #region Private Constants


        private const string admin = "ClientAdmin";

        private const string clientID = "ClientClientID";

        private const string userName = "ClientUserName";

        private const string userID = "ClientUserID";

        private const string labeluser = "ClientLabelUser";

        private const string clientName = "ClientClientName";
        private const string clientLand = "ClientClientLand";
        private const string notesCount = "ClientNotesCount";
        private const string allowManualInvoice = "AllowManualInvoice";
        private const string archive = "ClientArchive";

        #endregion


        #region Public Methods
        public static void FlushSession()
        {




            for (int i = 0; i < HttpContext.Current.Session.Count; i++)
            {
                if (HttpContext.Current.Session.Keys[i].ToString().StartsWith("Client") == false)
                {
                    HttpContext.Current.Session[i] = null;
                    HttpContext.Current.Session.Remove(HttpContext.Current.Session.Keys[i]);
                    i = i - 1;
                }
            }
        }
        #endregion


        public static string NotesCount
        {
            get
            {
                return (string)HttpContext.Current.Session[notesCount] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[notesCount] = value;
            }
        }


        public static string Admin
        {
            get
            {
                return (string)HttpContext.Current.Session[admin] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[admin] = value;
            }
        }

        public static string ClientID
        {
            get
            {
                return (string)HttpContext.Current.Session[clientID] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[clientID] = value;
            }
        }

        public static string UserName
        {
            get
            {
                return (string)HttpContext.Current.Session[userName] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[userName] = value;
            }
        }

        public static string UserID
        {
            get
            {
                return (string)HttpContext.Current.Session[userID] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[userID] = value;
            }
        }


        public static string LabelUser
        {
            get
            {
                return (string)HttpContext.Current.Session[labeluser] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[labeluser] = value;
            }
        }

        public static string ClientName
        {
            get
            {
                return (string)HttpContext.Current.Session[clientName] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[clientName] = value;
            }
        }

        public static string ClientLand
        {
            get
            {
                return (string)HttpContext.Current.Session[clientLand] ?? string.Empty;
            }

            set
            {
                HttpContext.Current.Session[clientLand] = value;
            }
        }

        public static bool AllowManualInvoice
        {
            get
            {
                return (bool)HttpContext.Current.Session[allowManualInvoice];
            }

            set
            {
                HttpContext.Current.Session[allowManualInvoice] = value;
            }
        }

        public static bool ClientArchive
        {
            get
            {
                return (bool)HttpContext.Current.Session[archive];
            }

            set
            {
                HttpContext.Current.Session[archive] = value;
            }
        }
    }
}


