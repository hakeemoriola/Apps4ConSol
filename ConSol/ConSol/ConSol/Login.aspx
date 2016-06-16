<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ConSol.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title><%=ConfigurationManager.AppSettings["AppName"] %> : <%=ConfigurationManager.AppSettings["CompanyName"] %> </title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- The styles -->
    <link id="bs-css" href="css/bootstrap-classic.css" rel="stylesheet">
    <style type="text/css">
        body {
            padding-bottom: 40px;
        }

        .sidebar-nav {
            padding: 9px 0;
        }
    </style>
    <link href="css/bootstrap-responsive.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/jquery-ui-1.8.21.custom.css" rel="stylesheet">
    <link href='css/fullcalendar.css' rel='stylesheet'>
    <link href='css/fullcalendar.print.css' rel='stylesheet' media='print'>
    <link href='css/chosen.css' rel='stylesheet'>
    <link href='css/uniform.default.css' rel='stylesheet'>
    <link href='css/colorbox.css' rel='stylesheet'>
    <link href='css/jquery.cleditor.css' rel='stylesheet'>
    <link href='css/jquery.noty.css' rel='stylesheet'>
    <link href='css/noty_theme_default.css' rel='stylesheet'>
    <link href='css/elfinder.min.css' rel='stylesheet'>
    <link href='css/elfinder.theme.css' rel='stylesheet'>
    <link href='css/jquery.iphone.toggle.css' rel='stylesheet'>
    <link href='css/opa-icons.css' rel='stylesheet'>
    <link href='css/uploadify.css' rel='stylesheet'>
    <!-- The HTML5 shim, for IE6-8 support of HTML5 elements -->
    <!--[if lt IE 9]>
	  <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <!-- The fav icon -->
    <link rel="shortcut icon" href="img/favicon.ico">
</head>
<body>
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="row-fluid">
                <div class="span12 center login-header">
                    <img src="images/logo.png">
                </div>
                <!--/span-->
            </div>
            <!--/row-->
            <div class="row-fluid">
                <div class="well span5 center login-box">
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <div class="alert alert-info">
                        Please login with your User name and Password.
                    </div>
                    <form id="Form1" class="form-horizontal" runat="server">
                        <fieldset>
                            <div class="input-prepend" title="Username" data-rel="tooltip">
                                <span class="add-on"><i class="icon-user"></i></span>
                                <asp:TextBox ID="username" runat="server" CssClass="input-large span10"></asp:TextBox>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="input-prepend" title="Password" data-rel="tooltip">
                                <span class="add-on"><i class="icon-lock"></i></span>
                                <asp:TextBox ID="password" TextMode="Password" class="input-large span10" runat="server"></asp:TextBox>
                            </div>
                            <div class="clearfix">
                            </div>
                            <div class="input-prepend">
                                <label class="remember" for="remember">
                                    <asp:CheckBox ID="remember" runat="server" />
                                    Remember me</label>
                            </div>
                            <div class="clearfix">
                            </div>
                            <p class="center span5">
                                <asp:Button ID="btnCommand" CssClass="btn btn-primary" runat="server" Text="Login"
                                    OnClick="btnCommand_Click" />
                            </p>
                        </fieldset>
                    </form>
                </div>
                <!--/span-->
            </div>
            <!--/row-->
        </div>
        <!--/fluid-row-->
    </div>
    <!--/.fluid-container-->
    <!-- external javascript
	================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <!-- jQuery -->
    <script src="Scripts/jquery-1.7.2.min.js"></script>
    <!-- jQuery UI -->
    <script src="Scripts/jquery-ui-1.8.21.custom.min.js"></script>
    <!-- transition / effect library -->
    <script src="Scripts/bootstrap-transition.js"></script>
    <!-- alert enhancer library -->
    <script src="Scripts/bootstrap-alert.js"></script>
    <!-- modal / dialog library -->
    <script src="Scripts/bootstrap-modal.js"></script>
    <!-- custom dropdown library -->
    <script src="Scripts/bootstrap-dropdown.js"></script>
    <!-- scrolspy library -->
    <script src="Scripts/bootstrap-scrollspy.js"></script>
    <!-- library for creating tabs -->
    <script src="Scripts/bootstrap-tab.js"></script>
    <!-- library for advanced tooltip -->
    <script src="Scripts/bootstrap-tooltip.js"></script>
    <!-- popover effect library -->
    <script src="Scripts/bootstrap-popover.js"></script>
    <!-- button enhancer library -->
    <script src="Scripts/bootstrap-button.js"></script>
    <!-- accordion library (optional, not used in demo) -->
    <script src="Scripts/bootstrap-collapse.js"></script>
    <!-- carousel slideshow library (optional, not used in demo) -->
    <script src="Scripts/bootstrap-carousel.js"></script>
    <!-- autocomplete library -->
    <script src="Scripts/bootstrap-typeahead.js"></script>
    <!-- tour library -->
    <script src="Scripts/bootstrap-tour.js"></script>
    <!-- library for cookie management -->
    <script src="Scripts/jquery.cookie.js"></script>
    <!-- calander plugin -->
    <script src='Scripts/fullcalendar.min.js'></script>
    <!-- data table plugin -->
    <script src='Scripts/jquery.dataTables.min.js'></script>
    <!-- chart libraries start -->
    <script src="Scripts/excanvas.js"></script>
    <script src="Scripts/jquery.flot.min.js"></script>
    <script src="Scripts/jquery.flot.pie.min.js"></script>
    <script src="Scripts/jquery.flot.stack.js"></script>
    <script src="Scripts/jquery.flot.resize.min.js"></script>
    <!-- chart libraries end -->
    <!-- select or dropdown enhancer -->
    <script src="Scripts/jquery.chosen.min.js"></script>
    <!-- checkbox, radio, and file input styler -->
    <script src="Scripts/jquery.uniform.min.js"></script>
    <!-- plugin for gallery image view -->
    <script src="Scripts/jquery.colorbox.min.js"></script>
    <!-- rich text editor library -->
    <script src="Scripts/jquery.cleditor.min.js"></script>
    <!-- notification plugin -->
    <script src="Scripts/jquery.noty.js"></script>
    <!-- file manager library -->
    <script src="Scripts/jquery.elfinder.min.js"></script>
    <!-- star rating plugin -->
    <script src="Scripts/jquery.raty.min.js"></script>
    <!-- for iOS style toggle switch -->
    <script src="Scripts/jquery.iphone.toggle.js"></script>
    <!-- autogrowing textarea plugin -->
    <script src="Scripts/jquery.autogrow-textarea.js"></script>
    <!-- multiple file upload plugin -->
    <script src="Scripts/jquery.uploadify-3.1.min.js"></script>
    <!-- history.js for cross-browser state change on ajax -->
    <script src="Scripts/jquery.history.js"></script>
    <!-- application script for Charisma demo -->
    <script src="Scripts/script.js"></script>
</body>
</html>