using CricketSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CricketSystem.Customer
{
    public partial class PawnItemDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["prod_id"] != null)
            {
                lblItemId.Text = Session["prod_id"].ToString();
            }
            else
            {
                Response.Redirect("../Index.html");
            }

            if (!this.IsPostBack)
            {
                getPawnProduct();
                cldCollectionDate.Visible = false;
            }
        }
        protected void cldCollectionDate_SelectionChanged(object sender, EventArgs e)
        {
            txtCollectionDate.Text = cldCollectionDate.SelectedDate.ToShortDateString();
            cldCollectionDate.Visible = false;
        }
        protected void lnkCollectionDate_Click(object sender, EventArgs e)
        {
            if (cldCollectionDate.Visible)
            {
                cldCollectionDate.Visible = false;
            }
            else
            {
                cldCollectionDate.Visible = true;
            }
        }
        protected void cldCollectionDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }

            if (e.Day.Date <= DateTime.Now)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
        protected void getPawnProduct()
        {
            int prod_id = Convert.ToInt32(lblItemId.Text);
            using (CricketSystemEntities objUser = new CricketSystemEntities())
            {
                var results = objUser.pawn_product_table.Where(v => v.Pawn_Product_id == prod_id);
                var count = results.Count();
                if (count > 0)
                {
                    foreach (var r in results)
                    {
                        prod_img.ImageUrl = r.Image_url;
                        txtName.Text = r.Name;
                        txtAmount.Text = r.Amount.ToString();
                        txtType.Text = r.Product_type;
                    }
                }
            }
        }
        protected void btnlnkEstimate_Click(object sender, EventArgs e)
        {
            lblRegisterError.Text = "";
            lblReturnMessage.Text = "";
            lblYouGet.Text = "";
            lblYouReturn.Text = "";

            if (txtCollectionDate.Text == "")
            {
                lblRegisterError.Text = "Please Select collection Date.";
            }
            else if (txtQuantity.Text == "")
            {
                lblRegisterError.Text = "Please provide Quantity.";
            }
            else if (Convert.ToInt32(txtQuantity.Text) < 1)
            {
                lblRegisterError.Text = "Quantity cannot be lessthan 1.";
            }
            else if (ddlConditon.SelectedIndex == 0)
            {
                lblRegisterError.Text = "Please select Conditon type.";
            }
            else
            {
                CricketSystemEntities context = new CricketSystemEntities();
                pawn_table tbl = new pawn_table();
                int prod_id = Convert.ToInt32(lblItemId.Text);
                int userid = Convert.ToInt32(Session["userId"].ToString());
                string collectionDate = txtCollectionDate.Text;
                int Quantity = Convert.ToInt32(txtQuantity.Text);
                string condition = ddlConditon.SelectedValue;
                double fee = Convert.ToDouble(txtAmount.Text);
                double ammountdue = 0;
                double topay = 0;

                if (ddlConditon.SelectedIndex == 1)
                {
                    topay = Quantity * fee * 0.7;
                    ammountdue = topay + (topay * 0.29);
                }
                else if (ddlConditon.SelectedIndex == 2)
                {
                    topay = Quantity * fee * 0.6;
                    ammountdue = topay + (topay * 0.32);
                }
                else {
                    topay = Quantity * fee * 0.45;
                    ammountdue = topay + (topay * 0.35);
                }


                ammountdue = Math.Round(ammountdue, 2);
                topay = Math.Round(topay, 2);

                    lblReturnMessage.Text = "The item(s) will be kept for not more than 90 days";
                    lblYouGet.Text = "Amount you will get R" + topay;
                    lblYouReturn.Text = "Amount you must return R" + ammountdue;
                }

        }
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            lblRegisterError.Text = "";
            //lblReturnMessage.Text = "";
            //lblYouGet.Text = "";
            //lblYouReturn.Text = "";

            if (txtCollectionDate.Text == "")
            {
                lblRegisterError.Text = "Please Select collection Date.";
            }
            else if (txtQuantity.Text == "")
            {
                lblRegisterError.Text = "Please provide Quantity.";
            }
            else if (ddlConditon.SelectedIndex == 0)
            {
                lblRegisterError.Text = "Please select Conditon type.";
            }
            else if (lblReturnMessage.Text == "") {
                lblRegisterError.Text = "Please 'Get Estimation' before you proceed.";
            }
            else
            {
                CricketSystemEntities context = new CricketSystemEntities();
                pawn_table tbl = new pawn_table();
                int prod_id = Convert.ToInt32(lblItemId.Text);
                int userid = Convert.ToInt32(Session["userId"].ToString());
                string collectionDate = txtCollectionDate.Text;
                int Quantity = Convert.ToInt32(txtQuantity.Text);
                string condition = ddlConditon.SelectedValue;
                double fee = Convert.ToDouble(txtAmount.Text);
                double ammountdue = 0;
                double topay = 0;

                if (ddlConditon.SelectedIndex == 1)
                {
                    topay = Quantity * fee * 0.7;
                    ammountdue = topay + (topay * 0.29);
                }
                else if (ddlConditon.SelectedIndex == 2)
                {
                    topay = Quantity * fee * 0.6;
                    ammountdue = topay + (topay * 0.32);
                }
                else
                {
                    topay = Quantity * fee * 0.45;
                    ammountdue = topay + (topay * 0.35);
                }

                tbl.Pawn_Product_id = prod_id;
                tbl.User_id = userid;
                tbl.Quantity = Quantity;
                tbl.LoanAmount = Convert.ToDecimal(topay);
                tbl.AmountDue = Convert.ToDecimal(ammountdue);
                tbl.Status = "Pending";
                tbl.Condition = condition;
                tbl.Date = DateTime.Now.ToShortDateString();
                tbl.Time = DateTime.Now.ToShortTimeString();
                tbl.collectionDate = collectionDate;
                context.pawn_table.Add(tbl);
                context.SaveChanges();

                Response.Redirect("Pawn.aspx");
            }
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pawn.aspx");
        }
    }
}