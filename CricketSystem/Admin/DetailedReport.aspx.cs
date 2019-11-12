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
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace CricketSystem.Admin
{
    public partial class DetailedReport : System.Web.UI.Page
    {
       // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
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

            if (!IsPostBack)
            {
                cldStartDate.Visible = false;
                cldEndDate.Visible = false;
                // Declare the query string.
                String queryString = "Select Order_id, Name,Price AS 'Price (R)',Quantity,Date,Time,Status From order_table order by Order_id Desc";

                // Run the query and bind the resulting DataSet
                // to the GridView control.
                DataSet ds = GetData(queryString);
                if (ds.Tables.Count > 0)
                {
                    grdOrders.DataSource = ds;
                    grdOrders.DataBind();
                }
                else
                {
                }
            }     
        }
        protected void ddlReportTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            String queryString = "";

            txtEndDate.Text = "";
            txtStartDate.Text = "";

            if (ddlTable.SelectedIndex == 0) {
                queryString = "Select Order_id, Name,Price AS 'Price (R)',Quantity,Date,Status From order_table order by Order_id Desc";
            }
            else if (ddlTable.SelectedIndex == 1)
            {
                queryString = "Select Hire_id, Name,Price AS 'Price (R)',Quantity,NoDays,Date,Status,ReturnDate From hired_table order by Hire_id Desc";
            }
            else if (ddlTable.SelectedIndex == 2)
            {
                queryString = "Select a.Pawn_id, b.Name,a.Quantity,a.Condition,b.Amount As 'Pawn Price(R)', a.AmountDue as 'Amount Due(R)',a.Date,a.collectionDate,a.Status From pawn_table a, pawn_product_table b where a.Pawn_Product_id = b.Pawn_Product_id order by a.Pawn_id Desc";
            }


            DataSet ds = GetData(queryString);
            if (ds.Tables.Count > 0)
            {
                grdOrders.DataSource = ds;
                grdOrders.DataBind();
            }
            else
            {
            }
        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        protected void cldStartDate_SelectionChanged(object sender, EventArgs e)
        {
            txtStartDate.Text = cldStartDate.SelectedDate.ToShortDateString();
            cldStartDate.Visible = false;
        }

        protected void cldEndDate_SelectionChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = cldEndDate.SelectedDate.ToShortDateString();
            cldEndDate.Visible = false;
        }
        protected void lnkStartDate_Click(object sender, EventArgs e)
        {
            if (cldStartDate.Visible)
            {
                cldStartDate.Visible = false;
            }
            else
            {
                cldStartDate.Visible = true;
            }
        }
        protected void lnkEndDate_Click(object sender, EventArgs e)
        {
            if (cldEndDate.Visible)
            {
                cldEndDate.Visible = false;
            }
            else
            {
                cldEndDate.Visible = true;
            }
        }

        protected void cldStartDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
        protected void cldEndDate_DayRender(object sender, DayRenderEventArgs e)
        {
           // if (e.Day.IsWeekend || e.Day.IsOtherMonth)
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.BackColor = System.Drawing.Color.Gray;
            }
        }
        protected void btnFilterByDate_Click(object sender, EventArgs e)
        {
            String queryString = "";
            lblerror.Text = "";

            if (txtStartDate.Text != "" && txtEndDate.Text != "")
            {
                if (ddlTable.SelectedIndex == 1)
                {
                    queryString = "Select Hire_id, Name,Price AS 'Price (R)',Quantity,NoDays,Date,Status,ReturnDate From hired_table where Date BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "' order by Hire_id Desc";
                }
                else if (ddlTable.SelectedIndex == 2)
                {
                    queryString = "Select a.Pawn_id, b.Name,a.Quantity,a.Condition,b.Amount As 'Pawn Price(R)', a.AmountDue as 'Amount Due(R)',a.Date,a.collectionDate,a.Status From pawn_table a, pawn_product_table b where a.Pawn_Product_id = b.Pawn_Product_id and a.Date BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "' order by a.Pawn_id Desc";
                }
                else
                {
                    queryString = "Select Order_id, Name,Price AS 'Price (R)',Quantity,Date,Status From order_table where Date BETWEEN '" + txtStartDate.Text + "' AND '" + txtEndDate.Text + "' order by Order_id Desc";
                }


                DataSet ds = GetData(queryString);
                if (ds.Tables.Count > 0)
                {
                    grdOrders.DataSource = ds;
                    grdOrders.DataBind();
                }
                else
                {
                }
            }
            else 
            {
                lblerror.Text = "Please select date from and date to.";
            }
        }
        DataSet GetData(String queryString)
        {
            // Retrieve the connection string stored in the Web.config file.
            String connectionString = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;

            DataSet ds = new DataSet();

            try
            {
                // Connect to the database and run the query.
                SqlConnection connection = new SqlConnection(connectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

                // Fill the DataSet.
                adapter.Fill(ds);

            }
            catch (Exception ex)
            {

            }

            return ds;
        }

        protected void grdOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int j = 0; j < e.Row.Cells.Count; j++)
            {
                e.Row.Cells[j].CssClass = "padding_left";
                e.Row.Cells[j].HorizontalAlign = HorizontalAlign.Left;
               
            }
        }
        protected void OnDataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = ddlTable.SelectedValue;
            row.Controls.Add(cell);

            row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            grdOrders.HeaderRow.Parent.Controls.AddAt(0, row);
        }
        protected void btnExportToWord_Click(object sender, EventArgs e)
        {
            string filename = ddlTable.SelectedValue;
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename + ".doc");
            Response.ContentType = "application/word";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
            grdOrders.HeaderRow.Style.Add("background-color", "#FFFFFF");
            grdOrders.RenderControl(htw);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            string filename = ddlTable.SelectedValue;
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment; filename=" + filename + ".xls");
            Response.ContentType = "application/excel";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);
            grdOrders.HeaderRow.Style.Add("background-color", "#FFFFFF");
            grdOrders.RenderControl(htw);
            Response.Write(stringWriter.ToString());
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnExportToPDF_Click(object sender, EventArgs e)
        {
            string filename = ddlTable.SelectedValue;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grdOrders.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            grdOrders.AllowPaging = true;
            grdOrders.DataBind();
        }
    }
}