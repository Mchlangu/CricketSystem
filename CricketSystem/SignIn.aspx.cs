using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }
        public int GetUserId(string username)
        {
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                int userId = (from p in objUser.user_table where p.Username == username select p.User_id).FirstOrDefault();

                return userId;
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LoginUser(txtUsername.Text, txtPassword.Text);
        }
        private void LoginUser(string userName, string password)
        {
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                if (userName == "" && password == "")
                {
                    lblLoginError.Text = "Provide Username and Password.";
                }
                else
                {
                    var results = objUser.user_table.Where(v => v.Username == userName
                   && v.Password == password);

                    var count = results.Count();

                    if (count > 0)
                    {
                        var a = from p in objUser.user_table where p.Username == userName && p.Status == "Active" select p;
                        if (a.Any())
                        {
                            var role = (from r in objUser.user_table where r.Username == userName select r.User_type).FirstOrDefault();

                            LoginHistory(userName);                         

                            if (role == "Admin")
                            {
                                Session["userId"] = GetUserId(userName);
                                Response.Redirect("Admin/Index.aspx");
                            }
                            else if (role == "Customer")
                            {
                                Session["userId"] = GetUserId(userName);
                                Response.Redirect("Customer/Index.aspx");
                            }
                            else if (role == "Supplier")
                            {
                                Session["userId"] = GetUserId(userName);
                                Response.Redirect("Supplier/Index.aspx");
                            }
                            else if (role == "Staff")
                            {
                                Session["userId"] = GetUserId(userName);
                                Response.Redirect("Staff/Orders.aspx");
                            }
                            
                        }
                        else
                        {
                            lblLoginError.Text = "Your account is currently Deactivated - Please contact us.";
                        }
                    }
                    else
                    {
                        lblLoginError.Text = "User does not exist in our database.";
                    }
                }
            }
        }

        private void LoginHistory(string username)
        {
            CricketSystemEntities context = new CricketSystemEntities();
            login_history_table tbl = new login_history_table();

            tbl.Username = username;
            tbl.User_id = GetUserId(username);
            tbl.DateAndTime = DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString();

            context.login_history_table.Add(tbl);
            context.SaveChanges();
        }
        // Converting the first letter to Upper Case.
        static string UpperCaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            else
            {
                return char.ToUpper(s[0]) + s.Substring(1);
            }
        }
    }
}