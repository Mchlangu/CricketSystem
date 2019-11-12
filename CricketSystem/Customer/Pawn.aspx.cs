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

namespace CricketSystem.Customer
{
    public partial class Pawn : System.Web.UI.Page
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
            }
            else
            {
                Response.Redirect("../Index.html");
            }

            this.getImages("Select * from pawn_product_table ORDER BY Pawn_Product_id  DESC");

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

            this.getImages("Select * from pawn_product_table WHERE Name like '%" + value + "%' ORDER BY Product_id DESC");
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
                    if (productId != data["Pawn_Product_id"].ToString())
                    {
                        productId = data["Pawn_Product_id"].ToString();

                        Label lblName = new Label();
                        lblName.Text = data["Name"].ToString();
                        lblName.CssClass = "lbl";

                        Label lblinterest = new Label();
                        lblinterest.Text = "Original Amount (" + data["Amount"].ToString() + ")";
                        lblinterest.CssClass = "lbl";

                        Panel pnlInfo = new Panel();
                        pnlInfo.CssClass = "infoPnl";

                        ImageButton img = new ImageButton();
                        img.CssClass = "img";
                        img.ID = "prodImg" + productId;
                        img.Click += new ImageClickEventHandler(this.infoPanel_Click);

                        trackDublicate.Add(productId);
                        img.ImageUrl = data["Image_url"].ToString();

                        pnlInfo.Controls.Add(img);
                        pnlInfo.Controls.Add(lblName);
                        pnlInfo.Controls.Add(lblinterest);

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
        protected void infoPanel_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            String id = btn.ID.Replace("prodImg", "");

            Session["prod_id"] = id;
            Response.Redirect("PawnItemDetails.aspx");

        }
        public string GetUsername(int userid)
        {
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                string username = (from p in objUser.user_table where p.User_id == userid select p.Username).FirstOrDefault();

                return username.ToString();
            }

        }
    }
}