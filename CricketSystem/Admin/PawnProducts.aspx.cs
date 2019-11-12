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

namespace CricketSystem.Admin
{
    public partial class PawnProducts : System.Web.UI.Page
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
                BindDataProd();
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
        private void BindDataProd()
        {
            string strQuery = "";

            strQuery = "select * from pawn_product_table order by Pawn_Product_id desc";

            SqlCommand cmd = new SqlCommand(strQuery);
            grdProd.DataSource = GetData(cmd);
            grdProd.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string strQuery = "";

            strQuery = "select * from pawn_product_table where Name like '%" + txtSearch.Text + "%' order by Pawn_Product_id desc";

            SqlCommand cmd = new SqlCommand(strQuery);
            grdProd.DataSource = GetData(cmd);
            grdProd.DataBind();
        }
        protected void OnProdPaging(object sender, GridViewPageEventArgs e)
        {
            BindDataProd();
            grdProd.PageIndex = e.NewPageIndex;
            grdProd.DataBind();
        }

        protected void DeleteProd(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from  pawn_product_table where " +
            "Pawn_Product_id=@Pawn_Product_id;";
            cmd.Parameters.Add("@Pawn_Product_id", SqlDbType.VarChar).Value = lnkRemove.CommandArgument;
            grdProd.DataSource = GetData(cmd);
            grdProd.DataBind();
            BindDataProd();

        }
        protected void EditProd(object sender, GridViewEditEventArgs e)
        {
            grdProd.EditIndex = e.NewEditIndex;
            BindDataProd();
        }
        protected void CancelEditProd(object sender, GridViewCancelEditEventArgs e)
        {
            grdProd.EditIndex = -1;
            BindDataProd();
        }
        protected void UpdateProd(object sender, GridViewUpdateEventArgs e)
        {
            string prodId = ((Label)grdProd.Rows[e.RowIndex].FindControl("lblProd_Id")).Text;
            string producttype = ((TextBox)grdProd.Rows[e.RowIndex].FindControl("txtProduct_type")).Text;
            string name = ((TextBox)grdProd.Rows[e.RowIndex].FindControl("txtName")).Text;
            string Price = ((TextBox)grdProd.Rows[e.RowIndex].FindControl("txtPrice")).Text;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update pawn_product_table set Product_type=@Product_type, Name=@Name,Price=@Price " +
             "where Pawn_Product_id=@Pawn_Product_id;";

            cmd.Parameters.Add("@Product_id", SqlDbType.VarChar).Value = prodId;
            cmd.Parameters.Add("@Product_type", SqlDbType.VarChar).Value = producttype;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@Price", SqlDbType.VarChar).Value = Price;
            grdProd.EditIndex = -1;
            grdProd.DataSource = GetData(cmd);
            grdProd.DataBind();

            BindDataProd();
        }

        protected void grdProd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].CssClass = "padding_left";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
    }
}