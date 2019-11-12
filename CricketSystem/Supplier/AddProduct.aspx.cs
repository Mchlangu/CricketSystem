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
    public partial class AddProduct : System.Web.UI.Page
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

            txtProductname.Focus();
        }
        public string GetUsername(int userid)
        {
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                string username = (from p in objUser.user_table where p.User_id == userid select p.Username).FirstOrDefault();

                return username.ToString();
            }
        }

        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            AddProducts();
        }
        private void AddProducts()
        {

            CricketSystemEntities context = new CricketSystemEntities();
            product_table tbl = new product_table();
            string fileName = System.IO.Path.GetFileName(FileUploadAdd.FileName);
           

            if (txtProductname.Text == "")
            {
                lblRegisterError.Text = "Please provide Product name.";
            }
            else if (ddlType.SelectedIndex == 0)
            {
                lblRegisterError.Text = "Please select product type.";
            }
            else if (txtProductPrice.Text == "")
            {
                lblRegisterError.Text = "Please provide Product price.";
            }
            else if (txtProductNo.Text == "")
            {
                lblRegisterError.Text = "Please provide No of Products available in stock.";
            }
            else if (!Regex.Match(txtProductPrice.Text, "^\\$?(\\d{1,3},(\\d{3},)*\\d{3}|\\d+)(.\\d{1,4})?$").Success)
            {
                lblRegisterError.Text = "Invalid Product price.";
            }
            else if (FileUploadAdd.FileName == "")
            {
                lblRegisterError.Text = "Please select product Image.";
            }
            else
            {
                //Set the Image File Path.
                string filePath = "~/Pictures/Products/" + fileName;
                int userId = Convert.ToInt32(lblUsername.Text);
                //Save the Image File in Folder.
                FileUploadAdd.SaveAs(Server.MapPath(filePath));

                tbl.Name = UpperCaseFirst(txtProductname.Text);
                tbl.product_purpose = "Sell";
                tbl.Product_type = ddlType.SelectedValue;
                tbl.Price = txtProductPrice.Text;
                tbl.ProductNo = Convert.ToInt32(txtProductNo.Text);
                tbl.Image_url = filePath;
                tbl.Username = GetUsername(userId);
                tbl.User_id = userId;
                tbl.UserType = "Supplier";

                context.product_table.Add(tbl);
                context.SaveChanges();

                lblRegisterError.Text = "Product Added.";
                lblRegisterError.ForeColor = Color.MidnightBlue;

                TextBox[] txt = { txtProductname, txtProductPrice, txtProductNo };

                for (int i = 0; i < txt.Length; i++)
                {
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
    }
}