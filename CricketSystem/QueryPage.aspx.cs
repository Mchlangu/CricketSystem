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

namespace CricketSystem
{
    public partial class QueryPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string query = txtQuery.Text;

            SqlCommand cmd = new SqlCommand(query);
            cmd.Connection = con;
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();

                Label1.Text = "Added Successfully";
            }
            catch(Exception ex){
                Label1.Text = ex.Message;
            }
            con.Close();

        }
    }
}