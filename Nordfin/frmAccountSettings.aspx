<%@ Page Language="C#" MasterPageFile="~/Nordfin.Master" AutoEventWireup="true" CodeBehind="frmAccountSettings.aspx.cs"  Inherits="Nordfin.frmAccountSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NordfinContentHolder" runat="server"   style="background-color: #232D41;">
<asp:Panel DefaultButton="btnUpdateInfo" runat="server">
    <link href="Styles/AccountSettings.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>" rel="stylesheet" />

    <script src="Scripts/jsAccountSettings.js?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"></script>
    <div class="dashboardContainer">
        <div class="container-fluid">
            <div class="dashboardHeader">
                <div class="dashboardHeadline">Account Settings</div>
            </div>



            <div>
                <div class="row " style="height: 50px; margin-left: 0px;">


                    <div class="col-md-2 divPadding customerInfoSidebar" style="color: #FFFFFF;">
                        <div style="background-color: #3E4B64;">


                            <div class="divPaddingLeft" style="background-color: #475672">
                                <span class="customerdivHeading">FIRST NAME</span>

                                <asp:Label CssClass="customerdivText"  runat="server" ID="lblFirstName"></asp:Label>

                                  <hr class="divHrLine" />
                            </div>


                          

                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">LAST NAME</span>

                                <asp:Label CssClass="customerdivText" runat="server" ID="lblLastName"></asp:Label>


                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">COMPANY</span>

                                <asp:Label CssClass="customerdivText" runat="server" ID="lblCompanyName"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">USERNAME</span>

                                <asp:Label CssClass="customerdivText" runat="server" ID="lblUserName"></asp:Label>

                            </div>
                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">EMAIL</span>
                                <div onclick="showUpdaeInfo('CHANGE EMAIL',0);" style="width: 90%;">
                                    <asp:TextBox CssClass="customerdivText form-control textboxColor" runat="server" AutoCompleteType="Disabled" ID="txtEmail"></asp:TextBox>
                                </div>
                            </div>

                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">PHONE</span>
                                <div onclick="showUpdaeInfo('CHANGE PHONE NUMBER',1);" style="width: 90%;">
                                    <asp:TextBox CssClass="customerdivText form-control textboxColor" runat="server" AutoCompleteType="Disabled" ID="txtPhone"></asp:TextBox>
                                </div>
                            </div>

                            <hr class="divHrLine" />
                            <div class="divPaddingLeft">
                                <span class="customerdivHeading">CHANGE PASSWORD</span>
                                <div onclick="changePassword();" style="width:90%;">
                                    <asp:TextBox CssClass="customerdivText form-control textboxColor" Text="********************" Enabled="false"  runat="server" ID="txtChangePassword"></asp:TextBox>
                                </div>
                            </div>


                            <hr class="divHrLine" />

                        </div>
                       
                         <div class="updateInfoButtonContainer">
                         
                        </div>



                    </div>





                    <div class="col-md-10 tableFixHead table-responsive customerTable" style="background-color: #2C3850;">

                        
                        <asp:GridView ID="grdActivityLog" runat="server" EmptyDataRowStyle-CssClass="Emptyrow" AutoGenerateColumns="False" ViewStateMode="Enabled" Visible="true" Style="color: white; font-size: small;" ShowHeaderWhenEmpty="true" CssClass="table">
                            <HeaderStyle BackColor="#475672" />
                            <Columns>
                                 <asp:BoundField DataField="UserName" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="USER" SortExpression="UserName" />
                                 <asp:BoundField DataField="NoteDate" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="DATE" SortExpression="NoteDate" />
                                 <asp:BoundField DataField="InvoiceNumber" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="INVOICE" SortExpression="InvoiceNumber" />
                                    <asp:BoundField DataField="NoteType" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="TYPE" SortExpression="NoteType" />
                                   

                                   
                                    <asp:BoundField DataField="NoteText" HeaderStyle-CssClass="Notesalign" ItemStyle-CssClass="Notesalign" HeaderText="TEXT" SortExpression="NoteText" />
                            </Columns>
                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                        </asp:GridView>
                      
                     
                      
                    </div>



                </div>
                <div class="modal fade" id="mdlUpdateConfirm" style="top: 250px;" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content" style="background: none;border:none;">
                            <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                <h5 class="modal-title"  id="exampleModalLabel">UPDATE INFO</h5>
                               <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                            </div>
                            <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                <div>
                                    <span id="spnBody" style="color: #A9BFD5;">Your updated email will be used as username in future logins Continue? </span>
                                </div>
                            </div>
                            <div class="modal-footer" style="background-color: #323E53;padding:0px;">
                                <asp:Button ID="btnYes" runat="server" CssClass="modalbutton" Text="Yes " OnClick="btnUpdateInfo_Click" />
                                <button type="button" class="modalbutton" id="btnNo" data-dismiss="modal">No</button>
                            
                            </div>
                        </div>
                    </div>
                </div>

                <div data-ng-app="myApp" data-ng-controller="myCtrl">
                    <div class="modal fade" id="mdlReport" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 200px;background: none;border:none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="informModalLabel">CHANGE PASSWORD</h5>
                                     <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div>
                                        <label>
                                             <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;"> Old Password</span>
                                           
                        <asp:TextBox ID="txtOldPassword" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeyup='checkOldPassword(this)' TextMode="Password" ></asp:TextBox>
                                        </label>

                                         <span id="spnPassword" class="short">
                                            <span id="spnPasswordnotmatch" class="hide">Password is worng</span>
                                        </span>

                                    </div>
                                    <div style="margin-top: 12px;">
                                        <label>
                                              <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;">   New Password</span>
                                          
                        <asp:TextBox ID="txtPassword" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeyup='checkPassStr(this)' TextMode="Password"></asp:TextBox>
                                        </label>

                                        <span id="strength_message">
                                            <span id="NordfinContentHolder_lblstrPassword" class="hide">Minimum 8 character</span>
                                            <span id="NordfinContentHolder_lblWeak" class="hide" style="color:#f83030">Weak</span>
                                            <span id="NordfinContentHolder_lblGood" class="hide" style="color:#FFB100">Good</span>
                                            <span id="NordfinContentHolder_lblStrong" class="hide" style="color:lightgreen">Strong</span>

                                        </span>
                                    </div>

                                    <div style="margin-top: 12px;">
                                        <label>
                                              <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;">     Repeat Password </span>
                                          
                        <asp:TextBox ID="txtConfirmPassword" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" TextMode="Password" onkeyup='checkPassMatch(this)'></asp:TextBox>
                                        </label>

                                        <span id="passNotMatch" class="short">
                                            <span id="NordfinContentHolder_lblPasswordNotMatch" class="hide" style="color:#f83030">Password not matched</span>
                                             <span id="NordfinContentHolder_lblPasswordMatch" class="hide" style="color:lightgreen">Password matched</span>
                                        </span>
                                    </div>


                                </div>
                                <div class="modal-footer" style="background-color: #323E53; padding: 0px;">
                                    <button class="modalbutton" id="btnSubmit" data-ng-click="submitClick($event)">Submit</button>

                                </div>

                             

                            </div>
                        </div>
                    </div>



                     <div class="modal fade" id="mdlUpdateInfo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content" style="top: 200px;background: none;border:none;">
                                <div class="modal-header dashboardHeadline" style="background-color: #323e53; color: #fff;font-size:16px;">
                                    <h5 class="modal-title modalTextcolor dashboardHeadlineModal" id="updateInfoModalLabel"></h5>
                                     <button type="button" class="modalcloseButton" data-dismiss="modal" aria-label="Close" style="top: 35px;right: 20px;">
                                        <span aria-hidden="true">✕</span>
                                    </button>
                                </div>
                                <div class="modal-body" style="background-color: #323e53; color: #fff;">
                                    <div id="pnlPhoneNumber">
                                        <label>
                                             <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;"> New Number</span>
                                           
                        <asp:TextBox ID="txtNewNumber" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" ></asp:TextBox>
                                        </label>

                                       

                                    </div>

                                     <div id="pnlEmail">
                                        <label>
                                             <span style="color: #A9BFD5;    text-transform: uppercase;font-size:12px;"> New Email</span>
                                           
                        <asp:TextBox ID="txtNewEmail" runat="server" autocomplete="off" CssClass="form-control textboxModalColor" onkeyup='checkEmail(this)' ></asp:TextBox>
                                        </label>

                                        <span id="EmailValid" class="short">
                                            <span id="spnEmail" class="hide" style="color:#f83030">Enter a valid email</span>
                                        </span>

                                    </div>
                                 

                                   


                                </div>
                                <div class="modal-footer" style="background-color: #323E53; padding: 5px;">
                                    <asp:Button Text="Submit" class="button updateInfoButton form-control" runat="server" ID="btnUpdateInfo" OnClientClick="return confirmUpdate();" OnClick="btnUpdateInfo_Click"  Width="128px" />
                                </div>

                             

                            </div>
                        </div>
                    </div>
                </div>




            </div>

               <asp:Button runat="server" ID="submit" OnClick="submit_Click"   style="display:none;"/>
             <asp:HiddenField ID="hdnEmail" runat="server" />
            <asp:Panel CssClass="featureNotAvailableBGpnl hidden" ID="pnlInfo" runat="server" >
                <div class="featureNotAvailablepnl">
                    <div class="featureNotAvailableXpnl">✕</div>
                    A verification link has been sent to your new email address
                </div>
            </asp:Panel>

              <asp:Panel CssClass="featureNotAvailableBGpnlPass hidden" ID="Panel1" runat="server" >
                <div class="featureNotAvailablepnlPass">
                    <div class="featureNotAvailableXpnlPass">✕</div>
                   Password Changed
                </div>
            </asp:Panel>

        </div>
        </div>
    </asp:Panel>
</asp:Content>
