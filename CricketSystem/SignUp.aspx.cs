using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem
{
    public partial class SignOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFirstname.Focus();
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Register();
        }
        public int GetUserId(string username)
        {
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                int userId = (from p in objUser.user_table where p.Username == username select p.User_id).FirstOrDefault();

                return userId;
            }
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
            else if (!getAge(txtIdNo.Text))
            {
                lblRegisterError.Text = "You must be 18 years or older to register.";
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

                LoginHistory(txtUsernames.Text);

                if (ddlType.SelectedValue == "Customer")
                {

                    Session["userId"] = GetUserId(txtUsernames.Text);
                    Response.Redirect("Customer/Index.aspx");
                }
                else if (ddlType.SelectedValue == "Supplier")
                {
                    Session["userId"] = GetUserId(txtUsernames.Text);
                    Response.Redirect("Supplier/Index.aspx");
                }

            }
        }


        private void LoginHistory(string username)
        {
            CricketSystemEntities context = new CricketSystemEntities();
            login_history_table tbl = new login_history_table();

            tbl.Username = username;
            tbl.User_id = GetUserId(username);
            tbl.DateAndTime = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            context.login_history_table.Add(tbl);
            context.SaveChanges();            
        }

        public bool getAge(string strnbr)
        {
            string yearID = "21,22,23,24,25,26,27,28,29" +
                "30,31,32,33,34,35,36,37,38,39," +
                "40,41,42,43,44,45,46,47,48,49," +
                "50,51,52,53,54,55,56,57,58,59," +
                "60,61,62,63,64,65,66,67,68,69," +
                "70,71,72,73,74,75,76,77,78,79," +
                "80,81,82,83,84,85,86,87,88,89," +
                "90,91,92,93,94,95,96,97,98,99," +
                "00,01";

            string years = strnbr.Substring(0, 2);

            if (yearID.Contains(years))
            {
                return true;
            }
            else {
                return false;
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