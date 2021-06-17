<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAutoAccount.aspx.cs" Inherits="Nordfin.frmAutoAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-color: #323e53;">
<head runat="server">
    <title></title>
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <link href="Styles/PSInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"" rel="stylesheet" />
     <script src="//code.jquery.com/jquery-1.11.0.min.js"></script>
     <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jsAutoAccount.js"></script>
    <script>
    
    </script>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
        <div style="background-color: #323e53;overflow:hidden">
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="First Name"></asp:Label><br />
                <asp:TextBox ID="txtFirstName" runat="server" required="required" autocomplete="nope" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>

            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Last Name"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server" autocomplete="nope" required="required" CssClass="form-control textboxModalColor"></asp:TextBox>

            </div>
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Org.Number"></asp:Label>
                <asp:TextBox ID="txtOrgNumber" runat="server" autocomplete="nope" required="required"  CssClass="form-control textboxModalColor" onkeypress="return isNumber(event)"></asp:TextBox>

            </div>
           
            <div>
                <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" Text="Mobile Number"></asp:Label>
                <div class="form-inline">
                    <asp:TextBox ID="txtCountryCode" ReadOnly="true" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" Style="width: 20%;" Text="+46"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtMobileNumber" runat="server" autocomplete="nope" required="required" CssClass="form-control textboxModalColor" Style="width: 79%;" onkeypress="return isNumber(event)"></asp:TextBox>
                </div>

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
                 <asp:Button Text="Submit" class="button panelButton form-control" Style="width: 150px;float: right;" runat="server"  ID="btnConfirm" OnClientClick="return EmailValidation();" OnClick="btnConfirm_Click"  />
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


            <div class="modal fade" id="mdlAutoAccountConfirm" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background: none;border:none;width: 100%;top:20px;">
                        <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                            <h5 class="modal-title" id="autoaccountModalLabel">Information</h5>
                             <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 16px;right: 10px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                        </div>
                        <div class="modal-body" style="background-color: #323e53; color: #fff;">
                            <div>
                                <p id="spnAutoAccountInfo" style="color: #fff; text-transform: uppercase; font-size: 12px;">
                                    Guide for registration:
                                    <br />  <br />
                                    &emsp;           1. By click Submit, you will create your account. 
                                    <br /><br />
                                    &emsp;          2. An email will then be sent to your specified email from<br />
                                    &emsp;   Creditsafe with log-in credentials. Be aware that this email
                                    <br />
                                    &emsp; could,on rare occasions, end up in your spam folder. 
                                    <br /><br />
                                    &emsp;            3. Click on the link in the email and choose a password. 
                                    <br /><br />
                                    &emsp;           4. COMPLETE – Log in with your Username and new Password and
                                    <br />
                                    &emsp;       start credit check your customers. 
                                    <br />
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                          
                            <asp:Button runat="server" CssClass="modalbutton" OnClientClick="PdfDownloadClick();"  OnClick="btnSubmit_Click" Text="Ok" ></asp:Button>
                            <button type="button" class="modalbutton"  data-dismiss="modal" onclick="window.parent.document.getElementById('FrameMaster').style.height = '425px';" >Close</button>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
