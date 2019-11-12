<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Buy.aspx.cs" Inherits="CricketSystem.Admin.Buy" EnableEventValidation="false" %>

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
        <a href="#">Buy Products</a>
         <br />
        <a href="OurOrders.aspx" >Ordered Products</a>
    </div>
       <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none" ></asp:Label>
        <h4>Purchase Products</h4>
        <asp:Panel runat="server" ID="pnlProducts" style="text-align:center">	
                <asp:GridView ID="grdProduct" runat="server" class="table" AutoGenerateColumns="False" 
                    Font-Size="Medium" HeaderStyle-BackColor="#3AC0F2" 
                    HeaderStyle-ForeColor="White" onrowdatabound="grdProduct_RowDataBound" 
                    Width="100%" FooterStyle-VerticalAlign="Bottom" ShowFooter="true" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="Black">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="Product Name"/>
                        <asp:BoundField DataField="price" HeaderText="Price(each)"/>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" style="width: 100px;padding-left: 10px;" TextMode="Number" Text='<%# Eval("Quantity") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                              <asp:Button ID="OnAdd" style="width:auto;height:20px" runat="server" BorderStyle="None" Text="Update" OnClick="OnAdd" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="total_price" HeaderText="Item Total" />                     
                        <asp:TemplateField>
                            <ItemTemplate>
                                    <asp:ImageButton ID="OnDelete" style="width:20px;height:20px" runat="server" BorderStyle="None" OnClick="OnDelete" src="../img/drop.jpg" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#000" BorderStyle="Solid" ForeColor="#ccc" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderStyle BackColor="#333333" ForeColor="White" Font-Bold="True" />
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#242121" />
                </asp:GridView>
                <asp:Label ID="lblError" runat="server" style="color:red;" ></asp:Label>
                <asp:Button ID="btnPurchase" runat="server" style="width:100px;height:30px;font-size:small;background-color:red;color:#fff;border:none;margin-top:10px;" class="hide" Text="Checkout" OnClick="btnPurchase_Click"  />
                <br />
                <asp:Label runat="server" ID="lblGridRecord" style="font-size:large; font-weight:bold;"  />
                <div class="agile_top_brands_grids">
                <table style="margin: auto;">	
                <tr>	            
                    <td><asp:TextBox ID="txtSearchproduct" style="width: 200px" runat="server" CssClass="form-control" placeholder="Search by product name"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearch" runat="server" CssClass="btn-search" Text="Search" OnClick="OnSearchClick"></asp:Button></td>
                </tr>
                </table>
                 <div>
                    <asp:Panel ID="pnlMain" runat="server" CssClass="panelMain"></asp:Panel>
                 </div>
		        </div>
            </asp:Panel>
        <asp:Panel ID="pnlPayment" runat="server" CssClass="hide_panel" style="max-width: 500px;margin: 0 auto;border: solid 1px #ccc;padding: 10px;">
                    <form id="mondidopayform">
                        <div class="wrp">
                            <div id="addCard">
                                <div class="panel">
                                    <h2>Card Payment</h2>
                                    <h3>R<asp:Label ID="lblTotalDue" runat="server" style="color:#ccc;" ></asp:Label> ZAR</h3>
                                    <br/>
                                    <asp:Label ID="lblPayError" runat="server" style="color:red;" ></asp:Label>
                                    <div class="form-group col-lg-12">
                                        <label>Bank Name</label>
                                        <asp:DropDownList ID="ddlBank" runat="server" class="form-control">
                                        <asp:ListItem>-Select Bank-</asp:ListItem>
                                        <asp:ListItem>ABSA</asp:ListItem>
                                        <asp:ListItem>CAPITEC</asp:ListItem>
                                        <asp:ListItem>FNB</asp:ListItem>
                                        <asp:ListItem>NEDBANK</asp:ListItem>
                                        <asp:ListItem>STANDARD BANK</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                     <div class="form-group col-lg-12">
                                        <label>Card Holder Name</label>
                                        <asp:TextBox ID="card_holder" runat="server" class="form-control" name="card_holder" style="width:100%;" placeholder="Firstname Lastname" />
                                    </div>
                                </div>
                                <div class="panel">
                                    <div class="col-sm-5 form-group">
                                        <label>Card Number</label>
                                        <asp:TextBox ID="card_number" runat="server" class="form-control" data-encrypted-attribute="card_number" MaxLength="16" style="width:100%;" placeholder="4111111111111111" />
                                    </div>
                                    <div class="col-sm-3 col-xs-6 form-group third float-left">
                                        <label>CVV</label>
                                        <asp:TextBox ID="cvv" runat="server" class="form-control" maxlength="3" TextMode="Number" data-encrypted-attribute="card_cvv" style="width:90px;" placeholder="000" />
                                    </div>
                                    <div class="col-sm-2 col-xs-3 form-group third float-left">
                                        <label>Month</label>
                                        <asp:TextBox ID="expMM" runat="server" class="form-control" maxlength="2" TextMode="Number" style="width:70px;" placeholder="01" />
                                    </div>
                                    <div class="col-sm-2 col-xs-3 form-group third float-left">
                                        <label>Year</label>
                                        <asp:TextBox ID="expYY" runat="server" class="form-control" maxlength="2" TextMode="Number" style="width:70px;" placeholder="15" />
                                    </div>
                                </div>
                                <div class="stripes bottom"> </div>
                                <div style="width:100%; text-align:center;">                            
                                    <asp:Button  ID="btnPay" runat="server" Text="Finish Payment" class="btn-success" style="margin-bottom:20px;" OnClick="btnPay_Click" />
                                </div>
                                <div style="margin-bottom: 15px;">
                                    <div id="credit-card-list">
                                        <img alt="Mastercard" class="card_mastercard card_icon" src="images/mastercard.png">
                                        <img alt="Visa" class="card_visa card_icon" src="images/visa.png">
                                        <img alt="Amex" class="card_amex card_icon" src="images/amex.png">
                                        <img alt="Discover" class="card_discover card_icon" src="images/discover.png">
                                        <img alt="Maestro" class="card_maestro  card_icon" src="images/maestro.png">
                                    </div>
                                </div>
                                <div style="width:100%; text-align:center;">
                                <asp:LinkButton  ID="btnCancel" runat="server" Text="Cancel" style="text-align:center; text-decoration:underline;margin-bottom:20px; margin-top:20px;" OnClick="btnCancel_Click" />
                                </div>
                                </div>
                        </div>
                    </form>
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



