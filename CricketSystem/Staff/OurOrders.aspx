﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OurOrders.aspx.cs" Inherits="CricketSystem.Staff.OurOrders" %>

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

 <style type="text/css">
        #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }

     .infoPnl {
         text-align:left;
         
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
     <div class="dropdown" style="color:midnightblue; display:block;float: right; font-weight:bold;margin-bottom: 20px;text-align:right;">
        <a href="Buy.aspx">Buy Products</a>
         <br />
        <a href="#" >Ordered Products</a>
    </div>
    <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none;"></asp:Label>
    <div class="form">
			<h2>Purchased Orders</h2>
                <div id="dvGrid" style ="padding:10px;width:100%; position:relative; margin:0; display:block;">
                           <asp:GridView ID="grdOrders" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No records found" EmptyDataRowStyle-HorizontalAlign="Center" EmptyDataRowStyle-ForeColor="Red" Width = "100%"
                            AutoGenerateColumns = "False" Font-Names = "Arial" 
                            Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B"  
                            HeaderStyle-BackColor = "green" AllowPaging ="True"  ShowFooter = "True"
                             OnPageIndexChanging="OnOrdersPaging"  CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdOrders_RowDataBound" PageSize="20" >
                           <Columns>
                         <asp:TemplateField HeaderText = "Order No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrder_no" runat="server" Text='<%# Eval("Order_no")%>'></asp:Label>
                                </ItemTemplate> 
                         </asp:TemplateField> 
                            <asp:TemplateField  HeaderText = "Username">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Username")%>'></asp:Label>
                                </ItemTemplate>  
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Username")%>'></asp:TextBox>
                                </EditItemTemplate>  
                                <ItemStyle></ItemStyle>
                            </asp:TemplateField> 
                            <asp:TemplateField  HeaderText = "Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDate" runat="server"  Text='<%# Eval("Date")%>'></asp:TextBox>
                                </EditItemTemplate>  
                            <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText = "Time">
                                <ItemTemplate>
                                    <asp:Label ID="lblTime" runat="server" Text='<%# Eval("Time")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTime" runat="server"  Text='<%# Eval("Time")%>'></asp:TextBox>
                                </EditItemTemplate>  
                            <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText = "">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblViewOrder" runat="server" Text="View Order" ForeColor="DodgerBlue" CommandArgument = '<%# Eval("Order_no")%>' OnCommand="OpenModal"></asp:LinkButton>
                                </ItemTemplate>
                            <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                           </Columns> 
                           <AlternatingRowStyle BackColor="White"  />
                               <EditRowStyle BackColor="#2461BF" />
                               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>
                               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                               <RowStyle BackColor="#EFF3FB" />
                               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                               <SortedAscendingCellStyle BackColor="#F5F7FB" />
                               <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                               <SortedDescendingCellStyle BackColor="#E9EBEF" />
                               <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView> 
                  </div>
	</div>
</div>

      <asp:Panel ID="pnlpopup" runat="server" BackColor="White" style="width:50%; height:auto; z-index:111;background-color: White; position: absolute; left: 25%; top: 12%; border: outset 2px gray;padding:5px;display:none; min-width:300px">
            <table style="width: 100%; height: 100%;">
                <tr style="background-color: #0924BC">
                    <td colspan="2" style="color:White; font-weight: bold; font-size: 1.2em; padding:3px; text-align:center">
                        Order Details <asp:LinkButton ID="lnkClose" runat="server" OnClick="lnkCloseClick" style="float:right; color:#fff;"><span class="fa fa-close"></span></asp:LinkButton>
                    </td>
                </tr>
            
            <tr><td><h3>Order No. <asp:label ID="lblOrderNo" runat="server" /></h3></td></tr>
            <tr style="text-align:center"><td>
                <asp:Panel ID="pnlOrders" runat="server">
                    
                </asp:Panel>
                <asp:Button ID="btnClosepopup" runat="server" style="width:100px; height:45px;background-color: #0924BC; color:#fff;" Text="Close" OnClick="btnClosepopup_Click" />  
                </td></tr>
           </table>       
        </asp:Panel>

        <script type="text/javascript">
            function ShowPopup() {
                $('#mask').show();
                $('#<%=pnlpopup.ClientID %>').show();
            }
            function HidePopup() {
                $('#mask').hide();
                $('#<%=pnlpopup.ClientID %>').hide();
                }

                $(".btnClose").live('click', function () {
                    HidePopup();
                });
            </script>

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
