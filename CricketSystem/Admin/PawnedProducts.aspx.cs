using CricketSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Web.UI.HtmlControls;

namespace CricketSystem.Admin
{
    public partial class PawnedProducts : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                lblUsername.Text = Session["userId"].ToString();
            }
            else
            {
                Response.Redirect("../Index.html");
            }

            if (!IsPostBack)
            {
                BindDataOrders();
            }
        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        private DataTable GetData(SqlCommand cmd)
        {
            con.Close();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            return dt;
        }
        private void BindDataOrders()
        {
            string strQuery = "";

            strQuery = "select c.Pawn_id, b.Pawn_Product_id, a.Firstname, a.Lastname,b.Name,c.Quantity,c.LoanAmount, b.Amount, c.AmountDue,c.Status from user_table a, pawn_product_table b, pawn_table c where a.User_id = c.User_id and c.Pawn_Product_id = b.Pawn_Product_id order by b.Pawn_Product_id desc";

            SqlCommand cmd = new SqlCommand(strQuery);
            grdOrders.DataSource = GetData(cmd);
            grdOrders.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strQuery = "";

            strQuery = "select c.Pawn_id, b.Pawn_Product_id, a.Firstname, a.Lastname,b.Name,c.Quantity,c.LoanAmount, b.Amount, c.AmountDue,c.Status from user_table a, pawn_product_table b, pawn_table c where a.User_id = c.User_id and c.Pawn_Product_id = b.Pawn_Product_id AND a.Firstname like '%" + txtSearch.Text + "%' order by b.Pawn_Product_id desc";

            SqlCommand cmd = new SqlCommand(strQuery);
            grdOrders.DataSource = GetData(cmd);
            grdOrders.DataBind();
        }
        protected void OnOrdersPaging(object sender, GridViewPageEventArgs e)
        {
            BindDataOrders();
            grdOrders.PageIndex = e.NewPageIndex;
            grdOrders.DataBind();
        }

        protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].CssClass = "padding_left";
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
        protected void OpenModal(object sender, CommandEventArgs e)
        {
            Popup(true);
            lblOrderNo.Text = e.CommandArgument.ToString();
            DynamicalllAddList(Convert.ToInt32(e.CommandArgument.ToString()));
        }
        //To show message after performing operations
        void Popup(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
            }
        }
        protected void lnkCloseClick(object sender, EventArgs e)
        {
            Popup(false);
        }
        protected void btnClosepopup_Click(object sender, EventArgs e)
        {
            Popup(false);
        }
        public void updateStatus(int Pawn_Product_id)
        {
            if (ddlStatus.SelectedIndex > 0)
            {
                using (CricketSystemEntities ctx = new CricketSystemEntities())
                {
                    var p = (from y in ctx.pawn_table
                             where y.Pawn_Product_id.Equals(Pawn_Product_id)
                             select y)
                             .ToList();
                    foreach (var x in p)
                    {
                        x.Status = ddlStatus.SelectedValue;
                        ctx.SaveChanges();
                        BindDataOrders();
                    }
                }
            }
            else
            {
                lblUpdateResults.Text = "Please select status to update.";
                lblUpdateResults.ForeColor = Color.Red;
            }

        }
        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (Session["Pawn_Product_id"].ToString() != "")
            {
                int Pawn_Product_id = Convert.ToInt32(Session["Pawn_Product_id"].ToString());
                updateStatus(Pawn_Product_id);
            }
        }
        protected void DynamicalllAddList(int id)
        {

            pnlOrders.Controls.Clear();
            con.Open();
            SqlCommand cmd;
            SqlDataReader data;
            String query = "Select * from pawn_table WHERE Pawn_id = " + id + " ORDER BY Pawn_id DESC";
            Session["Pawn_Product_id"] = id.ToString();

            using (cmd = new SqlCommand(query, con))
            {
                data = cmd.ExecuteReader();
                int count = 0;
                HtmlTableRow row = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();

                while (data.Read())
                {
                    Label Quantity = new Label();
                    Quantity.Text = "Quantity: " + data["Quantity"].ToString();

                    Label LoanAmount = new Label();
                    LoanAmount.Text = "Loan Amount: " + data["LoanAmount"].ToString();

                    Label Amount = new Label();
                    Amount.Text = "Amount Due: " + data["AmountDue"].ToString();

                    Label Status = new Label();
                    Status.Text = "Status: " + data["Status"].ToString();

                    Panel pnlInfo = new Panel();
                    pnlInfo.CssClass = "infoPnl";

                    //pnlInfo.Controls.Add(new LiteralControl("====================== Pawn No " + (count + 1) + " ===================="));
                    pnlInfo.Controls.Add(new LiteralControl("<br />"));
                    pnlInfo.Controls.Add(Quantity);
                    pnlInfo.Controls.Add(new LiteralControl("<br />"));
                    pnlInfo.Controls.Add(LoanAmount);
                    pnlInfo.Controls.Add(new LiteralControl("<br />"));
                    pnlInfo.Controls.Add(Amount);
                    pnlInfo.Controls.Add(new LiteralControl("<br />"));
                    pnlInfo.Controls.Add(Status);

                    pnlOrders.Controls.Add(pnlInfo);
                    count++;
                }

                if (count == 0)
                {
                    Label lblFeedback = new Label();
                    lblFeedback.ForeColor = Color.Red;
                    lblFeedback.Text = "No Records Found";

                    pnlOrders.Controls.Add(lblFeedback);
                }
                con.Close();
            }

        }
    }
}