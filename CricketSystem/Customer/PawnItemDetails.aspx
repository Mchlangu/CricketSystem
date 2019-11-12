<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PawnItemDetails.aspx.cs" Inherits="CricketSystem.Customer.PawnItemDetails" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>eCricket</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="" name="keywords">
    <meta content="" name="description">
    <!-- Favicons -->
    <link href="../img/log.jpg" rel="icon">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,400i,600,700|Raleway:300,400,400i,500,500i,700,800,900" rel="stylesheet">
    <!-- Bootstrap CSS File -->
    <link href="../lib/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Libraries CSS Files -->
    <link href="../lib/nivo-slider/css/nivo-slider.css" rel="stylesheet">
    <link href="../lib/owlcarousel/owl.carousel.css" rel="stylesheet">
    <link href="../lib/owlcarousel/owl.transitions.css" rel="stylesheet">
    <link href="../lib/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link href="../lib/animate/animate.min.css" rel="stylesheet">
    <link href="../lib/venobox/venobox.css" rel="stylesheet">
    <!-- Nivo Slider Theme -->
    <link href="../css/nivo-slider-theme.css" rel="stylesheet">
    <!-- Main Stylesheet File -->
    <link href="../css/style.css" rel="stylesheet">
    <!-- Responsive Stylesheet File -->
    <link href="../css/responsive.css" rel="stylesheet">
    <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" media="all" /> 

</head>
<body data-spy="scroll" data-target="#navbar-example">
    <form id="form1" runat="server">
    <div id="preloader"></div>
    <header>
        <!-- header-area start -->
        <div id="sticker" class="header-area" style="background-color: midnightblue;">
            <div class="container">
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <!-- Navigation -->
                        <nav class="navbar navbar-default">
                            <!-- Brand and toggle get grouped for better mobile display -->
                            <div class="navbar-header">
                                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target=".bs-example-navbar-collapse-1" aria-expanded="false">
                                    <span class="sr-only">Toggle navigation</span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                    <span class="icon-bar"></span>
                                </button>
                                <!-- Brand -->
                                <a class="navbar-brand page-scroll sticky-logo" href="Index.aspx"">
                                    <h1><span>e</span>Cricket</h1>
                                    <!-- Uncomment below if you prefer to use an image logo -->
                                    <!-- <img src="img/logo.png" alt="" title=""> -->
                                </a>
                            </div>                            
                        </nav>
                        <!-- END: Navigation -->
                    </div>
                </div>
            </div>
        </div>
        <!-- header-area end -->
    </header>
    <!-- header end -->
    <!-- Start  contact -->
    <div class="container" style="margin-top: 100px !important; width:600px">
        <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none;"></asp:Label>
        <asp:Label ID="lblItemId" runat="server" style="color:#ccc;display:none;"></asp:Label>
		<h2>Confirm Pawning</h2>
        
        <div class="form-group"> 
            <asp:Image ID="prod_img" runat="server" style="width:50px; height:50px;" />
        </div>
        <div style="padding-bottom:10px">
            <asp:Label runat="server" ID="lblRegisterError" Font-Size="Smaller" ForeColor="Red" />
            <asp:Label runat="server" ID="lblReturnMessage" Font-Size="Smaller" ForeColor="DodgerBlue" /><br/>
            <asp:Label runat="server" ID="lblYouGet" Font-Size="Smaller" ForeColor="DodgerBlue" /><br/>
            <asp:Label runat="server" ID="lblYouReturn" Font-Size="Smaller" ForeColor="DodgerBlue" />
        </div>
        <table>
            <tr>
            <td>Name:</td>
			<td><asp:TextBox Enabled="false" style="margin-bottom: 10px;width:250px; padding-left:10px" runat="server" ID="txtName" class="form-control" placeholder="Firstname" /></td>
            </tr>
        <tr style="display:none">
            <td>Original Price:</td> 
			<td><asp:TextBox Enabled="false" style="margin-bottom: 10px;width:250px;padding-left:10px" runat="server" ID="txtAmount" class="form-control" placeholder="Lastname" /></td>
        </tr>
        <tr>
           <td> Product Type:</td>
			<td><asp:TextBox Enabled="false" runat="server" style="margin-bottom: 10px;width:250px;padding-left:10px" ID="txtType" class="form-control" placeholder="Cellphone Number" /></td>
        </tr>
        <tr>
           <td> Collection Date:</td>
			<td>                
                <asp:TextBox ID="txtCollectionDate" style="margin-bottom: 10px;" runat="server" Height="28px" ReadOnly="True" Placeholder="Collection Date From"></asp:TextBox>
                <asp:LinkButton ID="lnkCollectionDate" runat="server" OnClick="lnkCollectionDate_Click"><span class="fa fa-calendar"></span></asp:LinkButton> 
                <asp:Calendar ID="cldCollectionDate" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" OnDayRender="cldCollectionDate_DayRender" OnSelectionChanged="cldCollectionDate_SelectionChanged" ShowGridLines="True" Width="220px">  
                <DayHeaderStyle BackColor="DodgerBlue" Font-Bold="True" Height="1px" />  
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />  
                <OtherMonthDayStyle ForeColor="#CC9966" />  
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />  
                <SelectorStyle BackColor="#FFCC66" />  
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />  
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />  
                </asp:Calendar>
			</td>
        </tr>
        <tr>
           <td> Quantity:</td>
			<td><asp:TextBox  runat="server" style="margin-bottom: 10px;width:250px" ID="txtQuantity" class="form-control" TextMode="Number" placeholder="No of Items" /></td>
        </tr>
        <tr>
           <td> Still in good condition?:</td>
			<td>
               <asp:DropDownList ID="ddlConditon" runat="server" class="form-control" style="margin-bottom: 10px;width:250px">
                <asp:ListItem>-Condition-</asp:ListItem>
                <asp:ListItem>Excellent</asp:ListItem>
                <asp:ListItem>Good</asp:ListItem>
                <asp:ListItem>Poor</asp:ListItem>
                </asp:DropDownList>
			</td>
        </tr>
        <tr>
           <td>
               <asp:LinkButton runat="server" style="margin-bottom: 10px;font-weight:bold;text-decoration:underline;" ID="lnkEstimate" type="submit" Text="Get Estimation" onclick="btnlnkEstimate_Click" />
           </td>
            <td>
                <asp:Button runat="server" style="margin-bottom: 10px;width:100px" ID="btnUpdateProfile" type="submit" Text="Submit" class="btn-primary" onclick="btnUpdateProfile_Click" />
                <asp:Button runat="server" style="margin-bottom: 10px;width:100px" ID="btnCancel" type="submit" Text="Cancel" class="btn-primary" onclick="btnCancel_Click" />
             <td>
        </tr>
        </table>
            </div>
        
        <a href="#" class="back-to-top"><i class="fa fa-chevron-up"></i></a>
        <!-- JavaScript Libraries -->
        <script src="../lib/jquery/jquery.min.js"></script>
        <script src="../lib/bootstrap/js/bootstrap.min.js"></script>
        <script src="../lib/owlcarousel/owl.carousel.min.js"></script>
        <script src="../lib/venobox/venobox.min.js"></script>
        <script src="../lib/knob/jquery.knob.js"></script>
        <script src="../lib/wow/wow.min.js"></script>
        <script src="../lib/parallax/parallax.js"></script>
        <script src="../lib/easing/easing.min.js"></script>
        <script src="../lib/nivo-slider/js/jquery.nivo.slider.js" type="text/javascript"></script>
        <script src="../lib/appear/jquery.appear.js"></script>
        <script src="../lib/isotope/isotope.pkgd.min.js"></script>
        <!-- Contact Form JavaScript File -->
        <script src="../contactform/contactform.js"></script>
        <script src="../js/main.js"></script>
</form>
</body>
</html>
