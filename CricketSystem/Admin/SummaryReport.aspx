<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryReport.aspx.cs" Inherits="CricketSystem.Admin.SummaryReport" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
            background-color: darkgreen;
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

     .instock {
        width: 100px;
        height: 50px;
        display: block;
        position: fixed;
        z-index: 1;
        margin-left: 40px;
        margin-top: -5px;
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
                                <a class="page-scroll" href="Index.aspx">Home</a>
                                <a class="page-scroll" href="Buy.aspx">Stock Products</a>
                                <a class="page-scroll" href="Orders.aspx">Orders</a>
                                  <a class="page-scroll" href="HiredProducts.aspx">Hired Products</a>
                                  <a href="AddStaff.aspx" class="page-scroll">Add Staff</a>
                                  <a href="AddProduct.aspx" class="page-scroll">Add Product</a>
                                  <a href="Products.aspx" class="page-scroll">Products</a>
                                  <a href="AddPawnProduct.aspx" class="page-scroll">Add Pawn Product</a>
                                  <a href="PawnedProducts.aspx" class="page-scroll">Pawn Request</a>
                                  <a href="PawnProducts.aspx" class="page-scroll">Added Pawn Products</a>
                                  <a href="SummaryReport.aspx" class="page-scroll">Report</a>
                                  <a href="Update.aspx" class="page-scroll">Update Profile</a>
                                  <a href="LoginHistory.aspx" class="page-scroll">Login History</a>
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
          <!-- Start  contact -->
    <div class="container" style="margin-top: 100px  !important;">
     <div class="dropdown" style="color:midnightblue; display:block;float: right; font-weight:bold;margin-bottom: 20px;text-align:right;">
        <a style="text-decoration:line-through" href="SummaryReport.aspx">Summary Report</a>
         <br />
        <a href="DetailedReport.aspx" >Detailed Report</a>
    </div>
       <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none" ></asp:Label>
        <h4>Summary Report</h4>
            <asp:DropDownList ID="ddlFilterByDate" style="width:200px; height:35px; color:#000" runat="server" OnSelectedIndexChanged="ddlFilterByDate_SelectedIndexChanged" AutoPostBack="True">
            </asp:DropDownList> 
           <asp:DropDownList ID="ddlFilterByStatus" style="width:200px; height:35px; color:#000" runat="server" OnSelectedIndexChanged="ddlFilterByStatus_SelectedIndexChanged" AutoPostBack="True">
             <asp:ListItem>-Filter by Status-</asp:ListItem>
                <asp:ListItem>Pending</asp:ListItem>
                <asp:ListItem>On Process</asp:ListItem>
                <asp:ListItem>Approved</asp:ListItem>
                <asp:ListItem>Ready For Collection</asp:ListItem>
                <asp:ListItem>Done</asp:ListItem>
              </asp:DropDownList> 
            <asp:Panel runat="server" ID="pnlProducts" style="text-align:center">	
            <br/>
            <table style="width:100%;">
              <tr>
                <td style="width:50%;">Total Sold Items</td>
                <td style="width:50%;">Total Hired Items</td>
               </tr>
               <tr>
                <td>
                    <asp:Panel ID="totSoldOrders" runat="server" style="padding-top: 20px;" Height="80px" Width="100%" BackColor="#003399" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" ForeColor="White">
                        <asp:Label ID="lblTotSoldOrders" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    </asp:Panel>
                </td>
                 <td>
                    <asp:Panel ID="totHiredOrders" runat="server" style="padding-top: 20px;" Height="80px" Width="100%" BackColor="DarkGreen" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="2px" ForeColor="White">
                        <asp:Label ID="lblTotHiredOrders" runat="server" Text="0" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                    </asp:Panel>
                 </td>
               </tr>
                <tr>
                <td>
                <asp:Chart ID="ChartOrderBar" runat="server">
                     <Titles>  
                        <asp:Title ShadowOffset="3" Name="Items" /> 
                    </Titles>  
                    <Legends>  
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />  
                    </Legends>  
                    <Series>
                        <asp:Series Name="Default" ChartType="StackedBar"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                </td>
                <td>
                 <asp:Chart ID="ChartHiredBar" runat="server" >
                     <Titles>  
                        <asp:Title ShadowOffset="3" Name="Items" /> 
                    </Titles>  
                    <Legends>  
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Row" />  
                    </Legends>  
                    <Series>
                        <asp:Series Name="Default" ChartType="StackedBar"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY>
                                <MajorGrid Enabled="False" />
                            </AxisY>
                            <AxisX>
                                <MajorGrid Enabled="False" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                </td>
              </tr>
            <tr>
                <td>
                 <asp:Chart ID="ChartOrderPie" runat="server">
                     <Titles>  
                        <asp:Title ShadowOffset="3" Name="Items" /> 
                    </Titles>  
                    <Legends>  
                        <asp:Legend Alignment="Center" Docking="Bottom" Name="Default" LegendStyle="Column" />  
                    </Legends>  
                    <Series>
                        <asp:Series Name="Default" ChartType="Doughnut"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                </td>
                <td>
                 <asp:Chart ID="ChartHiredPie" runat="server">
                     <Titles>  
                        <asp:Title ShadowOffset="3" Name="Items" /> 
                    </Titles>  
                    <Legends>  
                        <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default" LegendStyle="Column" />  
                    </Legends>  
                    <Series>
                        <asp:Series Name="Default" ChartType="Doughnut"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                </td>

            </tr>
           </table>
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
        <script>

            function removestorage() {
                localStorage.removeItem("orderstatus");
                sessionStorage.clear();
            }
        </script>
</form>
</body>
</html>




