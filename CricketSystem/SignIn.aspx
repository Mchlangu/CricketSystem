﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="CricketSystem.SignIn" %>

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
    <link href="lib/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Libraries CSS Files -->
    <link href="lib/nivo-slider/css/nivo-slider.css" rel="stylesheet">
    <link href="lib/owlcarousel/owl.carousel.css" rel="stylesheet">
    <link href="lib/owlcarousel/owl.transitions.css" rel="stylesheet">
    <link href="lib/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link href="lib/animate/animate.min.css" rel="stylesheet">
    <link href="lib/venobox/venobox.css" rel="stylesheet">
    <!-- Nivo Slider Theme -->
    <link href="css/nivo-slider-theme.css" rel="stylesheet">
    <!-- Main Stylesheet File -->
    <link href="css/style.css" rel="stylesheet">
    <!-- Responsive Stylesheet File -->
    <link href="css/responsive.css" rel="stylesheet">
    <link href="css/font-awesome.css" rel="stylesheet" type="text/css" media="all" /> 

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
                                <a class="navbar-brand page-scroll sticky-logo" href="index.html">
                                    <h1><span>e</span>Cricket</h1>
                                    <!-- Uncomment below if you prefer to use an image logo -->
                                    <!-- <img src="img/logo.png" alt="" title=""> -->
                                </a>
                            </div>
                            <!-- Collect the nav links, forms, and other content for toggling -->
                            <div class="collapse navbar-collapse main-menu bs-example-navbar-collapse-1" id="navbar-example">
                                <ul class="nav navbar-nav navbar-right">
                                    <li>
                                        <a class="page-scroll" href="Index.html">Home</a>
                                    </li>
                                    <li>
                                        <a class="page-scroll" href="Index.aspx">Products</a>
                                    </li>
                                    <li class="active">
                                        <a href="SignIn.aspx" class="page-scroll">Sign In</a>
                                    </li>
                                    <li>
                                        <a href="SignUp.aspx" class="page-scroll">Sign Up</a>
                                    </li>
                                </ul>
                            </div>
                            <!-- navbar-collapse -->
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
    <div class="container" style="margin-top: 100px !important; max-width:500px">
    <div class="form">
		<h2>Sign-In</h2>
        <asp:Label runat="server" ID="lblLoginError" Font-Size="Smaller" ForeColor="Red" /> 
        <div class="form-group">                           
			<asp:TextBox  runat="server" ID="txtUsername" class="form-control" placeholder="Username (*)" />
        </div>
        <div class="form-group">
			<asp:TextBox runat="server" type="password" ID="txtPassword" class="form-control" placeholder="Password (*)" />
            <br/>
            <input type="checkbox" onclick="ShowPassword()">Show Password
        </div>
       <div class="cta" style="color:dodgerblue;float: right;"><a href="ForgotPassword.aspx">Forgot Password?</a></div>
         <div class="form-group">
			<asp:Button runat="server" ID="btnLogin" type="submit" Text="Login" class="btn-primary" onclick="btnLogin_Click" />
          </div>
	</div>
    <div class="cta"><a href="SignUp.aspx">New User? - Create Account</a></div>
	</div>
        <a href="#" class="back-to-top"><i class="fa fa-chevron-up"></i></a>
        <!-- JavaScript Libraries -->
        <script src="lib/jquery/jquery.min.js"></script>
        <script src="lib/bootstrap/js/bootstrap.min.js"></script>
        <script src="lib/owlcarousel/owl.carousel.min.js"></script>
        <script src="lib/venobox/venobox.min.js"></script>
        <script src="lib/knob/jquery.knob.js"></script>
        <script src="lib/wow/wow.min.js"></script>
        <script src="lib/parallax/parallax.js"></script>
        <script src="lib/easing/easing.min.js"></script>
        <script src="lib/nivo-slider/js/jquery.nivo.slider.js" type="text/javascript"></script>
        <script src="lib/appear/jquery.appear.js"></script>
        <script src="lib/isotope/isotope.pkgd.min.js"></script>
        <!-- Contact Form JavaScript File -->
        <script src="contactform/contactform.js"></script>
        <script src="js/main.js"></script>

    <script>
        function ShowPassword() {
            var x = document.getElementById("txtPassword");
            if (x.type === "password") {
                x.type = "text";
            } else {
                x.type = "password";
            }
        }
    </script>
</form>
</body>
</html>

