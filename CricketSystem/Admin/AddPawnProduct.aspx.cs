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
    public partial class AddPawnProduct : System.Web.UI.Page
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
            pawn_product_table tbl = new pawn_product_table();
            string fileName = System.IO.Path.GetFileName(FileUploadAdd.FileName);


            if (txtProductname.Text == "")
            {
                lblRegisterError.Text = "Please provide Product name.";
            }
            else if (ddlType.SelectedIndex == 0)
            {
                lblRegisterError.Text = "Please select product type.";
            }
            else if (txtAmount.Text == "")
            {
                lblRegisterError.Text = "Please provide product amount.";
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
                tbl.Product_type = ddlType.SelectedValue;
                tbl.Amount = Convert.ToDecimal(txtAmount.Text);
                tbl.Image_url = filePath;

                context.pawn_product_table.Add(tbl);
                context.SaveChanges();

                lblRegisterError.Text = "Product Added.";
                lblRegisterError.ForeColor = Color.MidnightBlue;

                TextBox[] txt = { txtProductname, txtAmount };

                for (int i = 0; i < txt.Length; i++)
                {
                    txt[i].Text = "";
                }

                ddlType.SelectedIndex = 0;
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
    }
}