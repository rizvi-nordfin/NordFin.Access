<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPSInformation.aspx.cs" Inherits="Nordfin.frmPSInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="background-color:#323e53;overflow:unset;">
<head runat="server">
    <title></title>
        <link href="Styles/bootstrap.min.css" rel="stylesheet" />
        <link href="Styles/PSInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label4" Text="Date"></asp:Label>
            <asp:TextBox ID="txtLatestEventDate" runat="server" ReadOnly="true" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>

        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label3" Text="Status"></asp:Label>
            <asp:TextBox ID="txtInternalStatus" runat="server" ReadOnly="true" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>
        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label1" Text="Debt Amount"></asp:Label>
            <asp:TextBox ID="txtDebtAMount" runat="server" autocomplete="off" ReadOnly="true" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>

        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label2" Text="Remaining Amount"></asp:Label>
            <asp:TextBox ID="txtRemainingAmount" ReadOnly="true" runat="server" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>
        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label5" Text="Remaining Interest"></asp:Label>
            <asp:TextBox ID="txtRemainInterest" runat="server" ReadOnly="true" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>
        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label6" Text="Remaining Fees"></asp:Label>
            <asp:TextBox ID="txtRemainFees" runat="server" ReadOnly="true" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>

        </div>
        <div style="background-color: #323e53;">
            <asp:Label Style="color: #A9BFD5; text-transform: uppercase; font-size: 12px;" runat="server" ID="Label7" Text="Remaining Total"></asp:Label>
            <asp:TextBox ID="txtRemainTotal" runat="server" ReadOnly="true" autocomplete="off" CssClass="form-control textboxModalColor"></asp:TextBox>


        </div>

    </form>
</body>
</html>
