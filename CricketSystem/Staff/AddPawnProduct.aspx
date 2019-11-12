<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPawnProduct.aspx.cs" Inherits="CricketSystem.Staff.AddPawnProduct" %>

<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>eCricket</title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta content="" name="keywords">
    <meta content="" name="description">
    <!-- Favicons -->
    <link href="img/log.jpg" rel="icon">

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
    <!-- =======================================================
      Theme Name: eBusiness
      Theme URL: https://bootstrapmade.com/ebusiness-bootstrap-corporate-template/
      Author: BootstrapMade.com
      License: https://bootstrapmade.com/license/
    ======================================================= -->
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
                            <!-- Top Navigation Menu -->
                            <div class="topnav">
                              <a class="navbar-brand page-scroll sticky-logo" href="Orders.aspx">
                               <h1><span>e</span>Cricket</h1>
                              </a>
                              <div id="myLinks">
                                <a class="page-scroll" href="Orders.aspx">Home</a>
                                <a class="page-scroll" href="Buy.aspx">Stock Products</a>
                                <a class="page-scroll" href="Orders.aspx">Orders</a>
                                <a class="page-scroll" href="HiredProducts.aspx">Hired Products</a>
                                <a href="AddProduct.aspx" class="page-scroll">Add Product</a>
                                <a href="Products.aspx" class="page-scroll">Products</a>
                                <a href="AddPawnProduct.aspx" class="page-scroll">Add Pawn Product</a>
                                <a href="PawnedProducts.aspx" class="page-scroll">Pawn Request</a>
                                <a href="Update.aspx" class="page-scroll">Update Profile</a>
                                <asp:LinkButton runat="server" ID="lnkSignOut" OnClick="OnSignOut" OnClientClick="removestorage()">Sign Out</asp:LinkButton>
                              </div>
                              <a href="javascript:void(0);" class="icon" onclick="myFunction()">
                                <i class="fa fa-bars"></i>
                              </a>
                            </div>
                      </div>
                            <!-- navbar-collapse -->
                    </div>
                </div>
            </div>
        <!-- header-area end -->
    </header>
    <!-- header end -->
    <div class="container" style="margin-top: 100px !important;">
        <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none;"></asp:Label>
    <div class="form">
			<h2>Add Pawn Product</h2>
                <asp:Label runat="server" ID="lblRegisterError" Font-Size="Smaller" ForeColor="Red" /><br />
        <div class="form-group"> 
			<asp:TextBox  runat="server" ID="txtProductname" class="form-control" placeholder="Productname" />
        </div> 
        <div class="form-group"> 
                <asp:DropDownList ID="ddlType" runat="server" class="form-control">
                <asp:ListItem>-Select Product Type-</asp:ListItem>
                <asp:ListItem>Footwear</asp:ListItem>
                <asp:ListItem>Kit</asp:ListItem>
                <asp:ListItem>Hat</asp:ListItem>
                <asp:ListItem>Bag</asp:ListItem>
                <asp:ListItem>Ball</asp:ListItem>
                <asp:ListItem>Bat</asp:ListItem>
                <asp:ListItem>Glove</asp:ListItem>
                <asp:ListItem>Pads</asp:ListItem>
                <asp:ListItem>Stumps</asp:ListItem>
                <asp:ListItem>Other</asp:ListItem>
                </asp:DropDownList>
        </div>    
        <div class="form-group"> 
			<asp:TextBox  runat="server" ID="txtAmount" class="form-control" placeholder="Amount" />
        </div>       
        <div class="form-group"> 
            <asp:FileUpload ID="FileUploadAdd" runat="server" ForeColor="#000"  />   
        </div>
        <div class="form-group"> 
				<asp:Button runat="server" ID="btnAdd" type="submit" Text="Add" class="btn-primary" onclick="btnAdd_Click" />
        </div>
	</div>
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



