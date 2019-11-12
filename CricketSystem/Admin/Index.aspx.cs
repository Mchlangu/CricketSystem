using CricketSystem.Models;
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
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
        SqlCommand cmd;
        SqlDataReader data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                lblUsername.Text = Session["userId"].ToString();
                lblNames.Text = getNames(Convert.ToInt32(lblUsername.Text));
            }
            else
            {
                Response.Redirect("../Index.html");
            }

            if (!IsPostBack)
            {
                BindDataUser();
                this.GetStockAlert("Select * from product_table");
            }
        }
        public string getNames(int username)
        {

            string names = "";
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                names = (from r in objUser.user_table where r.User_id == username select r.Firstname + " " + r.Lastname).FirstOrDefault();
            }

            return names;
        }
        public void GetStockAlert(String query)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
            {
                pnlAlertMessage.Controls.Clear();
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                {
                    data = cmd.ExecuteReader();
                    int count = 0;
                    while (data.Read())
                    {
                        int stock = Convert.ToInt32(data["ProductNo"].ToString());
                        
                        Label lblAlert = new Label();
                        lblAlert.Text = "";

                        if (stock == 0)
                        {
                            lblAlert.Text = "Product: (" + data["Name"].ToString() + ") ran out of stock ("+stock+") available";
                        }
                        else if (stock < 21)
                        {
                            lblAlert.Text = "Product: (" + data["Name"].ToString() + ") running out of stock (" + stock + ") available";
                        }

                        lblAlert.CssClass = "lbl";

                        Panel pnlInfo = new Panel();
                        pnlInfo.CssClass = "infoPnl";

                        pnlInfo.Controls.Add(lblAlert);

                        pnlAlertMessage.Controls.Add(pnlInfo);

                        count++;
                    }

                    if (count == 0) {
                        pnlAlert.CssClass = "hidealert";
                    }
                }
                conn.Close();
            }

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
            string strQuery = "";
            string value = ddlFilterBy.SelectedValue;

            if (ddlFilterBy.SelectedIndex == 0)
            {
                strQuery = "select * from user_table where user_type != 'Admin' order by firstname asc";
            }
            else
            {
                strQuery = "select * from user_table where user_type = '" + value + "' order by firstname asc";
            }

            SqlCommand cmd = new SqlCommand(strQuery);
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();
        }
        protected void OnUserPaging(object sender, GridViewPageEventArgs e)
        {
            BindDataUser();
            grdUser.PageIndex = e.NewPageIndex;
            grdUser.DataBind();
        }

        protected void DeleteUser(object sender, EventArgs e)
        {
            LinkButton lnkRemove = (LinkButton)sender;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from  user_table where " +
            "User_id=@User_id;";
            cmd.Parameters.Add("@User_id", SqlDbType.VarChar).Value = lnkRemove.CommandArgument;
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();
            BindDataUser();
        }
        protected void EditUser(object sender, GridViewEditEventArgs e)
        {
            grdUser.EditIndex = e.NewEditIndex;
            BindDataUser();
        }
        protected void CancelEditUser(object sender, GridViewCancelEditEventArgs e)
        {
            grdUser.EditIndex = -1;
            BindDataUser();
        }
        protected void UpdateUser(object sender, GridViewUpdateEventArgs e)
        {
            string userId = ((Label)grdUser.Rows[e.RowIndex].FindControl("lblUserId")).Text;
            string firstname = ((TextBox)grdUser.Rows[e.RowIndex].FindControl("txtFirstname")).Text;
            string lastname = ((TextBox)grdUser.Rows[e.RowIndex].FindControl("txtLastname")).Text;
            string cell = ((TextBox)grdUser.Rows[e.RowIndex].FindControl("txtCellno")).Text;
            string email = ((TextBox)grdUser.Rows[e.RowIndex].FindControl("txtEmail")).Text;
            string status = ((DropDownList)grdUser.Rows[e.RowIndex].FindControl("ddlStatus")).SelectedValue;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update user_table set Firstname=@Firstname,Lastname=@Lastname,Cellno=@Cellno, Email=@Email, Status=@Status " +
             "where User_id=@User_id;";

            cmd.Parameters.Add("@User_id", SqlDbType.VarChar).Value = userId;
            cmd.Parameters.Add("@Firstname", SqlDbType.VarChar).Value = firstname;
            cmd.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = lastname;
            cmd.Parameters.Add("@Cellno", SqlDbType.VarChar).Value = cell;
            cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = status;
            grdUser.EditIndex = -1;
            grdUser.DataSource = GetData(cmd);
            grdUser.DataBind();

            BindDataUser();
        }

        protected void grdUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[0].CssClass = "padding_left";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dRow = (DataRowView)e.Row.DataItem;

                if (dRow.Row["Status"].ToString() == "Deactivated")
                {
                    e.Row.Cells[6].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[6].Font.Bold = true;
                }
            }
        }
        protected void ddlFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataUser();
        }
    }
}