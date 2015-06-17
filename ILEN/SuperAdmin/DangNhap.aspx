<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DangNhap.aspx.cs" Inherits="SuperAdmin_DangNhap" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>Home | online.inlen.vn</title>
    <link rel="icon" href="../images/INLEN-Half Logo.png" type="image/x-icon">
    <meta name="keyword" content="Online INLEN Group">
    <meta name="description" content="Onlien INLEN Group">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Latest Compiled and minified CSS -->
    <link rel="stylesheet" type="text/css" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css">
    <!-- jQuery Library -->
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <!-- Latest Compiled Javascript -->
    <script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
    <!-- Customer CSS -->
    <link rel="stylesheet" type="text/css" href="../css/style.css">
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container-fluid header">
            <a href="https://inlen.vn/" target="_blank">
                <img class="img-responsive" src="../images/INLEN-Online.png" title="INLEN" alt="INLEN" height="180px"></a>
        </div>
    </nav>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <div class="container">
        <div class="form-login">
            <div class="well">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="row">
                            <img class="img-responsive" src="../images/loginicon.png" title="INLEN Art" alt="INLEN Art">
                        </div>
                        <div class="row">
                            <h4 class="text-center">Welcome to INLEN Intranet !</h4>
                            <p>Notes: To keep your account secure always close all browser windows at the end of your session.</p>
                        </div>
                    </div>
                    <div class="col-sm-8">
                        <div class="row">
                            <h2>Login</h2>
                        </div>
                        <div class="row">
                            <svg height="15" width="100%">
  								<line x1="0" y1="0" x2="100%" y2="0" style="stroke: rgb(102,204,255); stroke-width: 5" />
  								Sorry, your browser does not support inline SVG.
							</svg>
                        </div>
                        <div class="row">
                            <form runat="server" class="form-horizontal" role="form" method="post">
                                <div class="form-group">
                                    <label class="control-lable col-sm-2" for="username">Username: </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="txtUsername" class="form-control" placeholder="Please enter your username" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-lable col-sm-2" for="password">Password: </label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" class="form-control" placeholder="Please enter your password" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-4">
                                        <asp:CheckBox runat="server" ID="chkGhiNho" class="checkbox" Text="Remember me next time" />
                                        <br />
                                        <asp:Label runat="server" ID="lblThongBao"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-4">
                                        <asp:Button runat="server"
                                            OnClick="btnLogin_Click"
                                            ID="btnLogin" Text="Login" class="btn btn-info" />
                                    </div>
                                </div>
                            </form>
                        </div>

                    </div>
                </div>
            </div>
            <br>
        </div>
    </div>
    <nav class="navbar navbar-inverse navbar-fixed-bottom">
        <p class="text-center text-muted footer">Copyright &copy 2015 INLEN Company | All rights reserved</p>
    </nav>
</body>
</html>
