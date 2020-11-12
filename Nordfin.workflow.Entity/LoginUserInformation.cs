
namespace Nordfin.workflow.Entity
{
    public class LoginUserInformation
    {
        public string Email { get; set; }
        public string WhenTry { get; set; }
        public string IP { get; set; }
        public string HostName { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

        public string Loc { get; set; }

        public string TimeZone { get; set; }
        public string Postal { get; set; }
        public string BrowserName { get; set; }
        public string Version { get; set; }
        public string OS { get; set; }
        public string CILastReGenerate { get; set; }
        public int CaptchaStatus { get; set; }
        public string Org { get; set; }
        public int iStatus { get; set; }

        public bool IsAccess { get; set; }

        public bool IsMobile { get; set; }


    }
}
