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

namespace CricketSystem
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

            this.getImages("Select * from product_table WHERE UserType !='Supplier' ORDER BY Product_id DESC");


            if (!this.IsPostBack)
            {
                displayProductsSales();
            }
        }
        protected void OnSearchClick(object sender, EventArgs e)
        {
            string value = txtSearchproduct.Text;

            this.getImages("Select * from product_table WHERE Name like '%" + value + "%' AND UserType !='Supplier' ORDER BY Product_id DESC");
        }

        protected void AddToCart_Click(object sender, EventArgs e)
        {
            lblGridRecord.Text = "";
            Button btn = (Button)sender;
            String id = btn.ID.Replace("product", "");

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM product_table WHERE Product_id =" + id, con))
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

                        if (data["product_purpose"].ToString().ToLower() == "sell")
                        {
                            lblDate.Text = "R " + data["Price"].ToString();
                        }
                        else {
                            lblDate.Text = "R " + data["Price"].ToString() +" / per day";
                        }
                        lblDate.CssClass = "lbl";

                        Panel pnlInfo = new Panel();
                        pnlInfo.CssClass = "infoPnl";

                        ImageButton img = new ImageButton();
                        img.CssClass = "img";
                        img.ID = "prodImg" + productId;

                        //Button btn = new Button();
                        //btn.ID = "product" + productId;
                        //btn.CssClass = "btns";
                        //btn.Text = "Add To Cart";
                        //btn.Click += new EventHandler(this.AddToCart_Click);

                        trackDublicate.Add(productId);
                        img.ImageUrl = data["Image_url"].ToString();

                        pnlInfo.Controls.Add(img);
                        pnlInfo.Controls.Add(lblOutletName);
                        pnlInfo.Controls.Add(lblDate);
                       // pnlInfo.Controls.Add(btn);

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
            Session["dtProducts"] = dtProducts;
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
            Session["dtProducts"] = dtProduct;
            BindGrid();
            hidePurchaseButton();
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
                            Session["dtProducts"] = dtProduct;
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
                        Session["dtProducts"] = dtProduct;
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
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total: R" + TotalSales.ToString();
                e.Row.Cells[0].Font.Size = 12;
                e.Row.Cells[0].Font.Bold = true;
                e.Row.Cells[0].ForeColor = Color.White;
            }
        }
        protected void btnPurchase_Click(object sender, EventArgs e)
        {
         
            
        }
    }
}