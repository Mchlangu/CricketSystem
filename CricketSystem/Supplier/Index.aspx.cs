using CricketSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Supplier
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
        SqlCommand cmd;
        SqlDataReader data;
        // Declare variable used to store value of Total
        private decimal TotalSales = (decimal)0.0;
        ArrayList trackDublicate = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMain.Controls.Clear();
            if (Session["userId"] != null)
            {
                lblUsername.Text = Session["userId"].ToString();
                lblNames.Text = getNames(Convert.ToInt32( lblUsername.Text)); 
            }
            else
            {
                Response.Redirect("../Index.html");
            }

            this.getImages("Select * from product_table WHERE product_purpose = 'Sell' AND UserType !='Supplier' ORDER BY Product_id DESC");


            if (!this.IsPostBack)
            {
                displayProductsSales();
            }
        }
        public string getNames(int userid)
        {
            string names = "";
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                names = (from r in objUser.user_table where r.User_id == userid select r.Firstname + " " + r.Lastname).FirstOrDefault();
            }

            return names;
        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        protected void OnSearchClick(object sender, EventArgs e)
        {
            string value = txtSearchproduct.Text;

            this.getImages("Select * from product_table WHERE Name like '%" + value + "%' AND product_purpose = 'Sell' AND UserType !='Supplier' ORDER BY Product_id DESC");
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            lblGridRecord.Text = "";
            Button btn = (Button)sender;
            String id = btn.ID.Replace("product", "");
            lblError.Text = "";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM product_table WHERE UserType !='Supplier' AND Product_id =" + id, con))
            {
                con.Open();
                SqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    AddToCart(data["Name"].ToString(), Convert.ToDecimal(data["Price"]), 1, Convert.ToDecimal(data["Price"]));
                }

                con.Close();
            }
        }
        protected void OnAdd(object sender, EventArgs e)
        {
            lblError.Text = "";
            GridViewRow rowProduct = (sender as Button).NamingContainer as GridViewRow;
            DataTable dtProduct = ViewState["dtProducts"] as DataTable;
            if (grdProduct.Rows.Count > 0)
            {
                int index = rowProduct.RowIndex;

                TextBox quant = (TextBox)grdProduct.Rows[index].FindControl("txtQuantity");

                string prodName = dtProduct.Rows[rowProduct.RowIndex]["name"].ToString();
                int quantity = Convert.ToInt32(quant.Text);
                decimal price = Convert.ToDecimal(dtProduct.Rows[rowProduct.RowIndex]["price"]);


                //quantity = quantity + 1;

                dtProduct.Rows[rowProduct.RowIndex]["quantity"] = quantity;
                dtProduct.Rows[rowProduct.RowIndex]["total_price"] = quantity * price;

                ViewState["dtProducts"] = dtProduct;
                BindGrid();
            }
        }
        public void getImages(String query)
        {

            pnlMain.Controls.Clear();
            con.Open();
            using (cmd = new SqlCommand(query, con))
            {
                data = cmd.ExecuteReader();
                String productId = "";
                int count = 0;
                while (data.Read())
                {
                    if (productId != data["Product_id"].ToString())
                    {
                        productId = data["Product_id"].ToString();

                        Label lblOutletName = new Label();
                        lblOutletName.Text = data["Name"].ToString();
                        lblOutletName.CssClass = "lbl";

                        Label lblDate = new Label();
                        lblDate.Text = "R " + data["Price"].ToString();
                        lblDate.CssClass = "lbl";

                        Label lblNoProduct = new Label();
                        lblNoProduct.Text = "In Stock (" + data["ProductNo"].ToString() + ")";
                        lblNoProduct.CssClass = "lbl";

                        Panel pnlInfo = new Panel();
                        pnlInfo.CssClass = "infoPnl";

                        ImageButton img = new ImageButton();
                        img.CssClass = "img";
                        img.ID = "prodImg" + productId;

                        Button btn = new Button();
                        btn.ID = "product" + productId;
                        btn.CssClass = "btns";
                        btn.Text = "Add To Cart";
                        btn.Click += new EventHandler(this.AddToCart_Click);
                        btn.Enabled = true;
                        btn.BackColor = Color.DarkGreen;
                        

                        ImageButton imgstock = new ImageButton();
                        imgstock.CssClass = "instock";
                        imgstock.ID = "imgstock" + productId;
                        imgstock.ImageUrl = "../img/ouofstock.jpg";

                        trackDublicate.Add(productId);
                        img.ImageUrl = data["Image_url"].ToString();

                        pnlInfo.Controls.Add(img);
                        pnlInfo.Controls.Add(lblOutletName);
                        pnlInfo.Controls.Add(lblDate);
                        pnlInfo.Controls.Add(lblNoProduct);

                        if (Convert.ToInt32(data["ProductNo"].ToString()) == 0)
                        {
                            pnlInfo.Controls.Add(imgstock);
                        }
                        else
                        {
                            pnlInfo.Controls.Add(btn);
                        }
                        

                        pnlMain.Controls.Add(pnlInfo);
                        count++;

                    }
                }

                if (count == 0)
                {
                    Label lblFeedback = new Label();
                    lblFeedback.ForeColor = Color.Red;
                    lblFeedback.Text = "No Records Found";

                    pnlMain.Controls.Add(lblFeedback);
                }
                con.Close();
            }

        }

        public void displayProductsSales()
        {
            DataTable dtProducts = new DataTable();
            dtProducts.Columns.Add("name");
            dtProducts.Columns.Add("Price");
            dtProducts.Columns.Add("quantity");
            dtProducts.Columns.Add("total_price");
            ViewState["dtProducts"] = dtProducts;
            //Session["dtProducts"] = dtProducts;
            BindGrid();
        }
        public void BindGrid()
        {
            grdProduct.DataSource = ViewState["dtProducts"] as DataTable;
            grdProduct.DataBind();
        }
        protected void OnDelete(object sender, EventArgs e)
        {
            GridViewRow rowProduct = (sender as ImageButton).NamingContainer as GridViewRow;
            DataTable dtProduct = ViewState["dtProducts"] as DataTable;
            dtProduct.Rows.RemoveAt(rowProduct.RowIndex);
            ViewState["dtProducts"] = dtProduct;
            //Session["dtProducts"] = dtProduct;
            BindGrid();
            hidePurchaseButton();
            lblError.Text = "";
        }
        public void hidePurchaseButton()
        {
            int count = grdProduct.Rows.Count;
            if (count < 1)
            {
                btnPurchase.CssClass = "hide";
            }
            else
            {
                btnPurchase.CssClass = "show";
            }
        }
        protected void AddToCart(string Name, decimal Price, int Quantity, decimal Total)
        {
            lblGridRecord.Text = "";
            bool found = false;
            DataTable dtProduct = ViewState["dtProducts"] as DataTable;

            try
            {
                if (grdProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < grdProduct.Rows.Count; i++)
                    {
                        string prodName = dtProduct.Rows[i]["name"].ToString();

                        if (prodName == Name)
                        {
                            int quantity = Convert.ToInt32(dtProduct.Rows[i]["quantity"]);
                            decimal price = Convert.ToDecimal(dtProduct.Rows[i]["price"]);
                            quantity = quantity + 1;

                            dtProduct.Rows[i]["quantity"] = quantity;
                            dtProduct.Rows[i]["total_price"] = quantity * price;
                            found = true;

                            ViewState["dtProducts"] = dtProduct;
                            BindGrid();
                        }
                    }

                }
                else
                {
                    found = false;
                }


                if (!found)
                {
                    try
                    {
                        dtProduct.Rows.Add(Name, Price, Quantity, Total);
                        ViewState["dtProducts"] = dtProduct;
                        //Session["dtProducts"] = dtProduct;
                        BindGrid();

                        int count = grdProduct.Rows.Count;
                        if (count < 1)
                        {
                            btnPurchase.CssClass = "hide";
                        }
                        else
                        {
                            btnPurchase.CssClass = "show";
                        }
                    }
                    catch (System.IndexOutOfRangeException ex)
                    {
                        System.ArgumentException argEx = new System.ArgumentException("Index is out of range", "index", ex);
                        throw argEx;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        protected void grdProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].CssClass = "padding_left";
                }
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalSales += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_price"));

                lblTotalDue.Text = TotalSales.ToString();
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total: R" + TotalSales.ToString();
                e.Row.Cells[0].Font.Size = 12;
                e.Row.Cells[0].Font.Bold = true;
                e.Row.Cells[0].ForeColor = Color.White;

                lblTotalDue.Text = TotalSales.ToString();
            }
        }
        public int GetProductId(string product, string usertype)
        {

            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                int productId = (from p in objUser.product_table where p.UserType == usertype && p.Name == product && p.product_purpose == "Sell" select p.Product_id).FirstOrDefault();

                return productId;
            }

        }
        public string GetUsername(int userid)
        {

            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                string username = (from p in objUser.user_table where p.User_id == userid select p.Username).FirstOrDefault();

                return username.ToString();
            }

        }
        public int GetProductNo(int productId)
        {

            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                int purchCount = (from p in objUser.product_table where p.Product_id == productId select p.ProductNo).FirstOrDefault();

                return purchCount;
            }

        }

        public void SavePaymentDetails(int orderId)
        {
            using (CricketSystemEntities context = new CricketSystemEntities())
            {

                payment_table tbl = new payment_table();

                tbl.Purchase_id = orderId;
                tbl.Purchase_Type = "Sell";
                tbl.User_id = Convert.ToInt32(lblUsername.Text);
                tbl.Bank = ddlBank.SelectedValue;
                tbl.Card_number = card_number.Text;
                tbl.Card_holder = card_holder.Text;
                tbl.Cvv = cvv.Text;
                tbl.ExpMM = Convert.ToInt32(expMM.Text);
                tbl.ExpYY = Convert.ToInt32(expYY.Text);
                tbl.Date = DateTime.Now.ToShortDateString();
                tbl.Time = DateTime.Now.ToShortTimeString();
                tbl.totatAmount = Convert.ToDecimal(lblTotalDue.Text);
                tbl.Status = "Success";

                context.payment_table.Add(tbl);
                context.SaveChanges();
            }

        }
        public void SaveOrder()
        {

            CricketSystemEntities context = new CricketSystemEntities();
            //calling delivery table

            int count = grdProduct.Rows.Count;
            var id = context.order_table.Count() + 1;
            int userId = Convert.ToInt32(lblUsername.Text);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    int prodId = GetProductId(grdProduct.Rows[i].Cells[0].Text, "Admin");
                    int remainingProd = GetProductNo(prodId);
                    //string 
                    order_table tbl = new order_table();

                    TextBox quant = (TextBox)grdProduct.Rows[i].FindControl("txtQuantity");

                    tbl.Name = grdProduct.Rows[i].Cells[0].Text;
                    tbl.Order_no = id;
                    tbl.Product_id = GetProductId(grdProduct.Rows[i].Cells[0].Text, "Admin");
                    tbl.User_id = Convert.ToInt32(lblUsername.Text);
                    tbl.Username = GetUsername(userId);
                    tbl.Price = Convert.ToDecimal(grdProduct.Rows[i].Cells[1].Text);
                    tbl.Quantity = Convert.ToInt32(quant.Text);
                    tbl.Date = DateTime.Now.ToShortDateString();
                    tbl.Time = DateTime.Now.ToShortTimeString();
                    tbl.OrderBy = "Customer";
                    tbl.Status = "Pending";
                    UpdateStock(tbl.Quantity, tbl.Name);

                    //sendEmail();
                    context.order_table.Add(tbl);
                }

                try
                {
                    context.SaveChanges();
                    SavePaymentDetails(id);
                    System.Threading.Thread.Sleep(1000);
                    Response.Redirect("Confirmation.aspx");

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;

                }
            }
        }

        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            int count = grdProduct.Rows.Count;
            int userId = Convert.ToInt32(lblUsername.Text);
            lblError.Text = "";
            bool isResults = false;

            if (count > 0)
            {

                for (int i = 0; i < count; i++)
                {
                    int prodId = GetProductId(grdProduct.Rows[i].Cells[0].Text, "Admin");
                    int remainingProd = GetProductNo(prodId);
                    //string 
                    order_table tbl = new order_table();

                    TextBox quant = (TextBox)grdProduct.Rows[i].FindControl("txtQuantity");

                    if (Convert.ToInt32(quant.Text) < 1)
                    {
                        isResults = false;
                        lblError.Text = grdProduct.Rows[i].Cells[0].Text + " Quantity cannot be less than 1";
                        break;
                    }
                    else if (remainingProd > Convert.ToInt32(quant.Text))
                    {
                        isResults = true;
                    }
                    else if (remainingProd < Convert.ToInt32(quant.Text))
                    {
                        isResults = false;
                        lblError.Text = "Only " + remainingProd + " items remaining for " + grdProduct.Rows[i].Cells[0].Text;
                        break;
                    }
                }

                if (isResults)
                {
                    System.Threading.Thread.Sleep(1000);
                    pnlPayment.CssClass = "show_panel";
                    pnlProducts.CssClass = "hide_panel";


                    //GridViewRow rowProduct = (sender as Button).NamingContainer as GridViewRow;
                    //DataTable dtProduct = ViewState["dtProducts"] as DataTable;
                    //if (grdProduct.Rows.Count > 0)
                    //{
                    //    int index = rowProduct.RowIndex;

                    //    TextBox quant = (TextBox)grdProduct.Rows[index].FindControl("txtQuantity");

                    //    string prodName = dtProduct.Rows[rowProduct.RowIndex]["name"].ToString();
                    //    int quantity = Convert.ToInt32(quant.Text);
                    //    decimal price = Convert.ToDecimal(dtProduct.Rows[rowProduct.RowIndex]["price"]);


                    //    //quantity = quantity + 1;

                    //    dtProduct.Rows[rowProduct.RowIndex]["quantity"] = quantity;
                    //    dtProduct.Rows[rowProduct.RowIndex]["total_price"] = quantity * price;

                    //    ViewState["dtProducts"] = dtProduct;
                    //    BindGrid();
                    //}
                }

            }
        }
        protected void btnPay_Click(object sender, EventArgs e)
        {

            if (ddlBank.SelectedIndex == 0)
            {
                lblPayError.Text = "Select Bank Name.";
            }
            else if (card_holder.Text == "")
            {
                lblPayError.Text = "Provide Card Holder Name.";
            }
            else if (card_number.Text == "")
            {
                lblPayError.Text = "Provide Card Number.";
            }
            else if (card_number.Text.Length != 16)
            {
                lblPayError.Text = "Invalid Card Number.";
            }
            else if (cvv.Text == "")
            {
                lblPayError.Text = "Provide CVV.";
            }
            else if (cvv.Text.Length < 3)
            {
                lblPayError.Text = "Invalid CVV number.";
            }
            else if (expMM.Text == "")
            {
                lblPayError.Text = "Provide Month.";
            }
            else if (expMM.Text.Length != 2 || Convert.ToInt32(expMM.Text) < 0 || Convert.ToInt32(expMM.Text) > 12)
            {
                lblPayError.Text = "Invalid Month.";
            }
            else if (expYY.Text == "")
            {
                lblPayError.Text = "Provide Year.";
            }
            else if (expYY.Text.Length != 2)
            {
                lblPayError.Text = "Invalid Year.";
            }
            else if (Convert.ToInt32(expYY.Text) < 19)
            {
                lblPayError.Text = "Year must not be lessthan the current year.";
            }
            else if (Convert.ToInt32(expMM.Text) == 19 && Convert.ToInt32(expYY.Text) <= DateTime.Now.Day)
            {
                lblPayError.Text = "Year must not be lessthan the current date.";
            }
            else
            {
                SaveOrder();
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1000);
            pnlPayment.CssClass = "hide_panel";
            pnlProducts.CssClass = "show_panel";
        }
        public void UpdateStock(int soldnumber, string name) 
        {
            using (CricketSystemEntities ctx = new CricketSystemEntities())
            {
                var p = (from y in ctx.product_table
                         where y.Name.Equals(name) && y.product_purpose.Equals("Sell") && y.UserType.Equals("Admin")                        
                         select y)
                         .ToList();
                foreach (var x in p)
                {
                    x.ProductNo = x.ProductNo - soldnumber;
                    ctx.SaveChanges();
                }
            }
        }
    }
}