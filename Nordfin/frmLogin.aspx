<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Title="NFC ACCESS" Inherits="Nordfin.frmLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link href="Styles/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.3.1.slim.min.js"></script>
    <link href="Styles/Login.css" rel="stylesheet" />
    <script src="Scripts/popper.min.js"></script>
     <script src="https://www.google.com/recaptcha/api.js?onload=renderRecaptcha&render=explicit" async defer></script>
  
    <link rel="icon" type="image/png" href="Images/NFC_logo_black.png" sizes="128x128"/>

  

     <script src="Scripts/jsLogin.js"></script>

</head>
<body>

    <div class="container-fluid">
        <div class="row">

            <div class="col-md-4 col-sm-4 col-xs-12">
            </div>
            <div class="col-md-4 col-sm-4 col-xs-12 loginBox">
                <div class="form-group col-md-12 header">
                    <h4>Log in to your NordFin account</h4>
                </div>
                <form id="frmLogin" runat="server">

                   

                    <div class="form-group">
                        <label for="exampleInputEmail1">UserName</label>
                        <input required="required" class="form-control" id="txtUserName" runat="server" aria-describedby="emailHelp" placeholder="Enter Username" />

                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Password</label>
                        <input required="required" type="password" class="form-control" runat="server" id="txtPassword" placeholder="Password" />
                    </div>


                    <div>

                        <div style="width: 100%;" id="ReCaptchContainer"></div>
                        <asp:Label ID="lblMessage1" runat="server"></asp:Label>
                    </div>
                    <br />
                    <asp:Button type="submit" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Login" class="btn btn-primary btn-block"></asp:Button>
                </form>

            </div>

            <div class="col-md-4 col-sm-4 col-xs-12">
            </div>
        </div>



    </div>
</body>
</html>
