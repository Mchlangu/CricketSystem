using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Admin
{
    public partial class AddStaff : System.Web.UI.Page
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

            txtFirstname.Focus();
        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Register();
        }
        private void Register()
        {

            CricketSystemEntities context = new CricketSystemEntities();
            user_table tbl = new user_table();

            string gender = "";

            lblRegisterError.Text = "";
            lblRegisterError.ForeColor = Color.Red;

            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";


            string phonePattern = @"^((?:\+27|27)|0)(=99|72|82|73|83|74|78|84|86|79|81|71|76|64|65|66|60|61|62|63|65|66|67)(\d{7})$";
            string IDCodePattern = @"^(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))$";

            bool isEmailValid = Regex.IsMatch(txtEmail.Text, emailPattern);
            bool isEmailExist = IsEmailExist(txtEmail.Text);

            bool isIDValid = Regex.IsMatch(txtIdNo.Text, IDCodePattern);
            bool isIDExist = IsIdNumberExist(txtIdNo.Text);

            bool isPhoneValid = Regex.IsMatch(txtCellno.Text, phonePattern);
            bool isCellExist = IsCellNoExist(txtCellno.Text);

            bool isUsernameExist = IsUsernameExist(txtUsernames.Text);

            string age = getAge(txtIdNo.Text);

            if (txtFirstname.Text == "")
            {
                lblRegisterError.Text = "Please enter Firstname.";
            }
            else if (!Regex.Match(UpperCaseFirst(txtFirstname.Text), "^[A-Z][a-zA-Z]*$").Success)
            {
                lblRegisterError.Text = "Invalid Firstname.";
            }
            else if (txtLastname.Text == "")
            {
                lblRegisterError.Text = "Please enter Lastname.";
            }
            else if (!Regex.Match(UpperCaseFirst(txtLastname.Text), "^[A-Z][a-zA-Z-]*$").Success)
            {
                lblRegisterError.Text = "Invalid Lastname.";
            }
            else if (txtIdNo.Text == "")
            {
                lblRegisterError.Text = "Please enter ID Number.";
            }
            else if (!isIDValid)
            {
                lblRegisterError.Text = "Please enter a valid ID Number.";
            }
            else if (Convert.ToInt32(age) < 18)
            {
                lblRegisterError.Text = "User must be 18 years or older to register.";
            }
            else if (isIDExist)
            {
                lblRegisterError.Text = "ID Number already exist.";
            }
            else if (ddlType.SelectedIndex == 0)
            {
                lblRegisterError.Text = "Please select your UserType.";
            }
            else if (txtCellno.Text == "")
            {
                lblRegisterError.Text = "Please enter Cell Number.";
            }
            else if (!isPhoneValid)
            {
                lblRegisterError.Text = "Please enter a valid Cell Number.";
            }
            else if (isCellExist)
            {
                lblRegisterError.Text = "Cellphone Number already exist.";
            }
            else if (txtEmail.Text == "")
            {
                lblRegisterError.Text = "Please enter Email Address.";
            }
            else if (!isEmailValid)
            {
                lblRegisterError.Text = "Please enter a valid Email Address.";
            }
            else if (isEmailExist)
            {
                lblRegisterError.Text = "Email already exist";
            }
            else if (txtUsernames.Text == "")
            {
                lblRegisterError.Text = "Please enter Username.";
            }
            else if (isUsernameExist)
            {
                lblRegisterError.Text = "Username already exist";
            }
            else if (txtPasswords.Text == "")
            {
                lblRegisterError.Text = "Please enter Password.";
            }
            else if (txtPasswords.Text.Length < 6)
            {
                lblRegisterError.Text = "Password must be atleast 6 characters long.";
                lblRegisterError.ForeColor = Color.Red;
            }
            else
            {
                var id = context.user_table.Count() + 1;

                if (Convert.ToInt32(txtIdNo.Text.Substring(6, 4)) > 4999)
                {
                    gender = "Male";
                }
                else
                {
                    gender = "Female";
                }

                tbl.User_type = ddlType.SelectedValue;
                tbl.Firstname = UpperCaseFirst(txtFirstname.Text);
                tbl.Lastname = UpperCaseFirst(txtLastname.Text);
                tbl.IDNumber = txtIdNo.Text;
                tbl.Gender = gender;
                tbl.Cellno = txtCellno.Text;
                tbl.Email = txtEmail.Text;
                tbl.Username = txtUsernames.Text;
                tbl.Password = txtPasswords.Text;
                tbl.Date_created = DateTime.Now.Date;
                tbl.Status = "Active";

                context.user_table.Add(tbl);
                context.SaveChanges();

                lblRegisterError.Text = "Staff Added.";
                lblRegisterError.ForeColor = Color.MidnightBlue;

                TextBox [] txt = { txtFirstname, txtLastname, txtIdNo, txtCellno, txtEmail, txtPasswords, txtUsernames };

                for (int i = 0; i < txt.Length; i++) {
                    txt[i].Text = "";
                }

                ddlType.SelectedIndex = 0;
            }
        }

        public string getAge(string strnbr)
        {
            int year = int.Parse(strnbr.Substring(0, 2)) + 1900;
            string years = (DateTime.Now.Year - year).ToString();
            return years;
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
        public bool IsIdNumberExist(string IdNNumber)
        {
            using (CricketSystemEntities dc = new CricketSystemEntities())
            {
                var v = dc.user_table.Where(a => a.IDNumber == IdNNumber).FirstOrDefault();
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