using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Focus();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitUser();
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
        private void SubmitUser()
        {
            string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$";
            bool isEmailValid = Regex.IsMatch(txtEmail.Text, emailPattern);
            bool isEmailExist = IsEmailExist(txtEmail.Text);

            lblError.Text = "";
            lblError.ForeColor = Color.Red;

            if (txtEmail.Text == "") {
                lblError.Text = "Please provide Email Address";
            }
            else if (!isEmailValid)
            {
                lblError.Text = "Please enter a valid Email Address.";
            }
            else if (!isEmailExist){
                lblError.Text = "Email Address does not exist in our database.";
            }
            else
            {
                sendEmail();
            }         
        }
        public void sendEmail()
        {
            CricketSystemEntities objUser = new CricketSystemEntities();
            string emailTo = txtEmail.Text;
            var pass = (from r in objUser.user_table where r.Email == txtEmail.Text select r.Password).FirstOrDefault();

            lblError.Text = "";
            lblError.ForeColor = Color.Red;

            string body = "Your password is: "+pass;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(emailTo);
                mail.From = new MailAddress("ecricketsystem@gmail.com");
                mail.Subject = "Forgot Password";

                mail.Body = body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential("ecricketsystem@gmail.com", "GioSystem2"); // ***use valid credentials***
                smtp.Port = 587;

                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send(mail);

                lblError.Text = "Thank you, Your password has been sent to your email address.";
                lblError.ForeColor = Color.MidnightBlue;
            }
            catch (Exception ex)
            {
                //(ex.Message);
            }
        }
    }
}