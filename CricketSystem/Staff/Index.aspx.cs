using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Staff
{
    public partial class Index : System.Web.UI.Page
    {
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
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
    }
}