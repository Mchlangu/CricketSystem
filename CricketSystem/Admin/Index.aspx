<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CricketSystem.Admin.Index" %>

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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style>
    .alert {
        background-color: darkorange;
        z-index: 99;
        color: aliceblue;
        position: fixed;
        width: 100%;
        opacity: 0.8;
        top: 0;
        text-align:center;
        margin-top: 70px;
    }

    .closebtn {
      margin-left: 15px;
      color: white;
      font-weight: bold;
      float: right;
      font-size: 22px;
      line-height: 20px;
      cursor: pointer;
      transition: 0.3s;
    }

    .closebtn:hover {
      color: black;
    }

        .hidealert {
            display:none;
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
    <asp:Panel ID="pnlAlert" runat="server" class="alert">
      <span class="closebtn" onclick="this.parentElement.style.display='none'; setSession();" >&times;</span> 
        <strong>STOCK ALERT!</strong>
        <asp:Panel ID="pnlAlertMessage" runat="server"></asp:Panel>
    </asp:Panel>
    <!-- header end -->
          <!-- Start  contact -->
    <div class="container" style="margin-top: 100px  !important;width: 80% !important;">
               <asp:Label ID="lblUsername" runat="server" style="color:#ccc;display:none" ></asp:Label>
       <p> Welcome Admin: <asp:Label ID="lblNames" runat="server" style="color:#ccc;" ></asp:Label> </p>
             <h3>Registered Users</h3>
             <asp:DropDownList ID="ddlFilterBy" style="width:200px; height:35px; color:#000" runat="server" OnSelectedIndexChanged="ddlFilterBy_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>-Filter by User Type-</asp:ListItem>
                <asp:ListItem Value="Staff">Staff</asp:ListItem>
                <asp:ListItem Value="Customer">Customers</asp:ListItem>
                <asp:ListItem Value="Supplier">Suppliers</asp:ListItem>
            </asp:DropDownList> 
                    <br/><br />
                    <div id="dvUserGrid" style ="padding:10px;width:100%; position:relative; margin:0; display:block;">
                           <asp:GridView ID="grdUser" runat="server"  Width = "100%" ShowHeaderWhenEmpty="true" EmptyDataText="No record Found" EmptyDataRowStyle-Font-Size="Large" EmptyDataRowStyle-ForeColor="Red"
                            AutoGenerateColumns = "False" Font-Names = "Arial" 
                            Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B"  
                            HeaderStyle-BackColor = "green" AllowPaging ="True"  ShowFooter = "True" onrowediting="EditUser"
                            onrowupdating="UpdateUser" OnPageIndexChanging="OnUserPaging" onrowcancelingedit="CancelEditUser" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="grdUser_RowDataBound" PageSize="20" >
                           <Columns>
                            <asp:TemplateField HeaderText = "ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserId" runat="server" Text='<%# Eval("User_id")%>'></asp:Label>
                                </ItemTemplate> 
                         </asp:TemplateField> 
                            <asp:TemplateField  HeaderText = "Firstname">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstname" runat="server" Text='<%# Eval("Firstname")%>'></asp:Label>
                                </ItemTemplate>  
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFirstname" runat="server" Text='<%# Eval("Firstname")%>'></asp:TextBox>
                                </EditItemTemplate>  
                                <ItemStyle></ItemStyle>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText = "Lastname">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastname" runat="server" Text='<%# Eval("Lastname")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLastname" runat="server" Text='<%# Eval("Lastname")%>'></asp:TextBox>
                                </EditItemTemplate> 
                           <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText = "Cellphone">
                                <ItemTemplate>
                                    <asp:Label ID="lblCellno" runat="server" Text='<%# Eval("Cellno")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCellno" runat="server"  Text='<%# Eval("Cellno")%>'></asp:TextBox>
                                </EditItemTemplate>  
                            <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField  HeaderText = "Email Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEmail" runat="server"  Text='<%# Eval("Email")%>'></asp:TextBox>
                                </EditItemTemplate>  
                            <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                          <asp:TemplateField  HeaderText = "Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                       <asp:ListItem Value="Active">Activate</asp:ListItem>
                                       <asp:ListItem Value="Deactivated">Deactivate</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>  
                        <ItemStyle></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("User_id")%>' 
                                     OnClientClick = "return confirm('Are you sure you want to delete this user?')"
                                    Text = "Delete" ForeColor="Red" OnClick = "DeleteUser"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:CommandField  ShowEditButton="True" /> 
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
            $(document).ready(function () {

                if (sessionStorage.getItem("orderstatus") == "accepted") {
                    $(".alert").hide();
                }
                else {
                    $(".alert").show();
                }
            });

            function setSession() {
                sessionStorage.setItem("orderstatus", "accepted");
            }

            function removestorage() {
                localStorage.removeItem("orderstatus");
                sessionStorage.clear();
            }
        </script>

<%--        <script>
            function myFunction() {
                var x = document.getElementById("myLinks");
                if (x.style.display === "block") {
                    x.style.display = "none";
                } else {
                    x.style.display = "block";
                }
            }
    </script>--%>
</form>
</body>
</html>


