<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPSInformation.aspx.cs" Inherits="Nordfin.frmPSInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="Styles/bootstrap.min.css" rel="stylesheet" />
        <link href="Styles/PSInformation.css?version=<%=ConfigurationManager.AppSettings["VersionConfiguration"].ToString() %>"" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <textarea id="txtPSInfo" runat="server" class="form-control textboxModalColor textareaHeight"></textarea>
        </div>
    </form>
</body>
</html>
