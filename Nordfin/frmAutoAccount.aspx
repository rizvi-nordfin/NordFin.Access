<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAutoAccount.aspx.cs" Inherits="Nordfin.frmAutoAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-color: #323e53;">
<head runat="server">
    <title></title>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/PSInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"" rel="stylesheet" />
     <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script>
        function PdfDownloadClick() {
            $("#PnlDownloadprogress").css("display", "block");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
        <div style="background-color: #323e53;overflow:hidden">
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="First Name"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server" required="required" autocomplete="nope" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>

            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Last Name"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" autocomplete="nope" required="required" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Org.Number"></asp:Label>
                <asp:TextBox ID="txtOrgNumber" runat="server" autocomplete="nope" required="required"  CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>

            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Country Code"></asp:Label>
                <asp:TextBox ID="txtCountryCode" ReadOnly="true" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" Text="+46"></asp:TextBox>

            </div>
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Mobile Number"></asp:Label>
                <asp:TextBox ID="txtMobileNumber" runat="server" autocomplete="nope" required="required" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtEMail" runat="server"  autocomplete="nope" required="required" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>

            <div class="mt-2 form-group">
                <div style="display: inline;">
                    <input type="checkbox" id="chkTerms" class="larger" runat="server" name="checkBox2" />
                    <span style="color: #A9BFD5;">By checking the box, you agree to our
                                        <a class="aHyperLink" href="Files/CSSE%20ÅF-villkor%20AutoAccount%2020190101.pdf" target="_blank">Terms and Conditions</a>
                    </span>
                </div>
               <%-- <a href="Files/CSSE%20ÅF-villkor%20AutoAccount%2020190101.pdf">Files/CSSE ÅF-villkor AutoAccount 20190101.pdf</a>--%>
            </div>
            <div>
                 <asp:Button Text="Submit" class="button panelButton form-control" Style="width: 150px;float: right;" runat="server"  ID="btnSubmit" OnClientClick="PdfDownloadClick();"  OnClick="btnSubmit_Click" />
            </div>
           <%-- <div class="mt-4 row align-items-start">

                <div class="col">
                  
                </div>
                <div class="col float-right" >Second </div>
            </div>--%>

        </div>

        <div id="PnlDownloadprogress" style="display: none;background-color: #323e53;   ">
            <div style="text-align: center; color: #3DADC5;">
                <span>Processing please wait...</span>
            </div>
            <div class="progress">

                <div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 100%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
            </div>
        </div>
    </form>
</body>
</html>
