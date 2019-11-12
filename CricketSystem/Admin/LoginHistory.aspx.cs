using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Admin
{
    public partial class LoginHistory : System.Web.UI.Page
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
                BindDataUser();
                BindDate();
            }
        }
        public void BindDate()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT(substring(DateAndTime,0,11)) AS Date FROM login_history_table"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    ddlFilterByDate.DataSource = cmd.ExecuteReader();
                    ddlFilterByDate.DataTextField = "Date";
                    ddlFilterByDate.DataValueField = "Date";
                    ddlFilterByDate.DataBind();
                    conn.Close();
                }
            }
            ddlFilterByDate.Items.Insert(0, new ListItem("-Filter By Date-", "0"));
            
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
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        private void BindDataUser()
        {
            string  strQuery = "select a.Firstname, a.Lastname, a.Username, b.DateAndTime from user_table a, login_history_table b where a.Username = b.Username AND a.user_type != 'Admin' order by b.id desc";
            SqlCommand cmd = new SqlCommand(strQuery);
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();
        }
        private void BindDataByUserType()
        {
            string strQuery = "";
            ddlFilterByDate.SelectedIndex = 0;

            string value = ddlFilterBy.SelectedValue;

            if (ddlFilterBy.SelectedIndex > 0)
            {
                strQuery = "select a.Firstname, a.Lastname, a.Username, b.DateAndTime from user_table a, login_history_table b where a.Username = b.Username AND a.user_type = '" + value + "' order by b.id desc";
            }
            else
            {
                strQuery = "select a.Firstname, a.Lastname, a.Username, b.DateAndTime from user_table a, login_history_table b where a.Username = b.Username AND a.user_type != 'Admin' order by b.id desc";

            }

            SqlCommand cmd = new SqlCommand(strQuery);
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();
        }
        private void BindDataByDate()
        {
            string strQuery = "";

            ddlFilterBy.SelectedIndex = 0;
            string day = ddlFilterByDate.SelectedValue;

            if (ddlFilterByDate.SelectedIndex > 0)
            {
                strQuery = "select a.Firstname, a.Lastname, a.Username, b.DateAndTime from user_table a, login_history_table b where a.Username = b.Username AND b.DateAndTime like '" + day + "%' order by b.id desc";
            }
            else
            {
                strQuery = "select a.Firstname, a.Lastname, a.Username, b.DateAndTime from user_table a, login_history_table b where a.Username = b.Username AND a.user_type != 'Admin' order by b.id desc";

            }

            SqlCommand cmd = new SqlCommand(strQuery);
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();
        }
        protected void OnUserPaging(object sender, GridViewPageEventArgs e)
        {
            if (ddlFilterBy.SelectedIndex > 0) {
                BindDataByUserType();
            }
            else if (ddlFilterByDate.SelectedIndex > 0)
            {
                BindDataByDate();
            }
            else {
                BindDataUser();
            }

            grdUser.PageIndex = e.NewPageIndex;
            grdUser.DataBind();
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].CssClass = "padding_left";
        }
        protected void ddlFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByUserType();
        }
        protected void ddlFilterByDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataByDate();
        }
    }
}