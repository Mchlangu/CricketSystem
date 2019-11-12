using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Supplier
{
    public partial class Update : System.Web.UI.Page
    {
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

            if (!this.IsPostBack)
            {
                getUserProfile();
            }
        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        protected void getUserProfile()
        {
            int userid = Convert.ToInt32(lblUsername.Text);
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                var results = objUser.user_table.Where(v => v.User_id == userid);
                var count = results.Count();
                if (count > 0)
                {
                    foreach (var r in results)
                    {
                        txtFirstname.Text = r.Firstname;
                        txtLastname.Text = r.Lastname;
                        txtCellno.Text = r.Cellno;
                        txtEmail.Text = r.Email;
                    }
                }
            }
        }
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            lblUpdateResults.Text = "";
            int userid = Convert.ToInt32(lblUsername.Text);
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";

            string phonePattern = @"^((?:\+27|27)|0)(=99|72|82|73|83|74|78|84|79|81|71|76|64|65|66|60|61|62|63|67)(\d{7})$";

            bool isEmailValid = Regex.IsMatch(txtEmail.Text, emailPattern);
            bool isEmailExist = IsEmailExist(txtEmail.Text);

            bool isPhoneValid = Regex.IsMatch(txtCellno.Text, phonePattern);
            bool isCellExist = IsCellNoExist(txtCellno.Text);

            bool isUsernameExist = IsUsernameExist(txtUsername.Text);

            if (!Regex.Match(UpperCaseFirst(txtFirstname.Text), "^[A-Z][a-zA-Z]*$").Success)
            {
                lblUpdateResults.Text = "Invalid Firstname.";
            }
            else if (txtLastname.Text == "")
            {
                lblUpdateResults.Text = "Please enter Lastname.";
            }
            else if (!Regex.Match(UpperCaseFirst(txtLastname.Text), "^[A-Z][a-zA-Z-]*$").Success)
            {
                lblUpdateResults.Text = "Invalid Lastname.";
            }
            else if (txtCellno.Text == "")
            {
                lblUpdateResults.Text = "Please provide Cellphone number to update";
                lblUpdateResults.ForeColor = Color.Red;

            }
            else if (!isPhoneValid)
            {
                lblUpdateResults.Text = "Please enter a valid Cell Number.";
                lblUpdateResults.ForeColor = Color.Red;
            }
            else if (txtUsername.Text == "")
            {
                lblUpdateResults.Text = "Please provide Username to update";
                lblUpdateResults.ForeColor = Color.Red;
            }
            else if (txtEmail.Text == "")
            {
                lblUpdateResults.Text = "Please provide Email address to update";
                lblUpdateResults.ForeColor = Color.Red;
            }
            else if (!isEmailValid)
            {
                lblUpdateResults.Text = "Please enter a valid Email Address.";
                lblUpdateResults.ForeColor = Color.Red;
            }
            else
            {
                using (CricketSystemEntities ctx = new CricketSystemEntities())
                {
                    var p = (from y in ctx.user_table
                             where y.User_id.Equals(userid)
                             select y)
                             .ToList();
                    foreach (var x in p)
                    {
                        x.Firstname = txtFirstname.Text;
                        x.Lastname = txtLastname.Text;
                        x.Cellno = txtCellno.Text;
                        x.Username = txtUsername.Text;
                        x.Email = txtEmail.Text;

                        ctx.SaveChanges();

                        lblUpdateResults.Text = "Profile Updated Successfully";
                        lblUpdateResults.ForeColor = Color.MidnightBlue;

                        getUserProfile();
                    }
                }
            }
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

        public bool IsEmailExist(string emailID)
        {
            using (CricketSystemEntities dc = new CricketSystemEntities())
            {
                var v = dc.user_table.Where(a => a.Email == emailID).FirstOrDefault();
                if (v != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsUsernameExist(string Username)
        {
            using (CricketSystemEntities dc = new CricketSystemEntities())
            {
                var v = dc.user_table.Where(a => a.Username == Username).FirstOrDefault();
                if (v != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsCellNoExist(string CellNo)
        {
            using (CricketSystemEntities dc = new CricketSystemEntities())
            {
                var v = dc.user_table.Where(a => a.Cellno == CellNo).FirstOrDefault();
                if (v != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}