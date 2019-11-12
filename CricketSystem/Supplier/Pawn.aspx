<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pawn.aspx.cs" Inherits="CricketSystem.Supplier.Pawn" %>

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
<style type="text/css">
        .img{
            width: 100px;
            height: 100px;
            display: block;
            margin-bottom: 7px;
            position: relative;
            margin: 0 auto;
        }
        .txt {
        width:70px;
        text-align:center;
        }

        .infoPnl{
            z-index: 100;
            margin: 20px;
            padding: 10px;
            border : 1px dotted gray;
            width: 200px;
            border-radius: 2px;
            display: inline-block;
            height: auto;
            vertical-align: bottom;
            overflow: hidden;
        }
        .infoPnl:hover{
            border-color: #4350EA;
            cursor: pointer;
            
        }
        .panelMain{
            /*display: list-item;*/
            text-align: center;
        }
        .lbl{
            display: block;
            text-align: center;
        }
        .auto-style2 {
            text-align: left;
        }
        .btns{            
            width: 50%;
            height: 30px;
            font-size: small;
            background-color: red;
            color: #fff;
            border: none;
            margin-top: 10px;
        }

        .padding_left{
            padding-left:15px
        }

        .show_panel{
            display:block;
        }

         .hide_panel{
            display:none;
        }

    .dropdown_list{
     outline: none;
    display: block;
    width: 100%;
    border: 1px solid #d9d9d9;
    margin: 0 0 20px;
    padding: 10px 15px;
    box-sizing: border-box;
    font-wieght: 400;
    -webkit-transition: 0.3s ease;
    transition: 0.3s ease;
    font-size: 14px;
    color:#000;
    }

    .btn-search {
        color: #fff;
        background-color: #337ab7;
        border-color: #2e6da4;
        width: 100px;
        height: 34px;
        border-radius: 6px;
       margin-left: 7px;
    }

    .grdview {
        margin-bottom: -20px;
    }

</style>
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
                              <a class="navbar-brand page-scroll sticky-logo" href="Index.aspx">
                               <h1><span>e</span>Cricket</h1>
                              </a>
                              <div id="myLinks">
                                <a class="page-scroll" href="Index.aspx">Buy Products</a>
                                <a class="page-scroll" href="Orders.aspx">My Orders</a>
                                <a href="Hire.aspx" class="page-scroll">Hire Products</a>
                                <a href="Pawn.aspx" class="page-scroll">Pawn Products</a>
                                <a href="PawnedProducts.aspx" class="page-scroll">My Pawn Requests</a>
                                <a class="page-scroll" href="HiredProducts.aspx">My Hired Products</a>
                                <a class="page-scroll" href="SoldOrders.aspx">Sell Products <span class="fa fa-shopping-cart"></span></a>
                                <a href="Update.aspx" class="page-scroll">Update Profile</a>
                                <asp:LinkButton runat="server" ID="LinkButton1" OnClick="OnSignOut" OnClientClick="removestorage()">Sign Out</asp:LinkButton>
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
          <!-- Start  contact -->
    <div class="container" style="margin-top: 100px !important;">
        <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none" ></asp:Label>
        <h4>Pawn Products</h4>
        <asp:Panel runat="server" ID="pnlProducts" style="text-align:center">	
                <table style="margin: auto;">	
                <tr>	            
                    <td><asp:TextBox ID="txtSearchproduct" style="width: 200px" runat="server" CssClass="form-control" placeholder="Search by product name"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearch" runat="server" CssClass="btn-search" Text="Search" OnClick="OnSearchClick"></asp:Button></td>
                </tr>
                </table>
                 <div>
                    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain"></asp:Panel>
                 </div>
            </asp:Panel>
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




