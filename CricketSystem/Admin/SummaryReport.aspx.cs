using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
using System.Configuration;
using CricketSystem.Models; 

namespace CricketSystem.Admin
{
    public partial class SummaryReport : System.Web.UI.Page
    {
       SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
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
                BindDate();
                BindChart(ChartOrderBar, "Select Name,Count(Name) from order_table WHERE OrderBy = 'Customer' GROUP BY Name ORDER BY COUNT(Name) DESC", "Ordered Products");
                BindPieChart(ChartOrderPie, "Select  Name,Count(Name) from order_table WHERE OrderBy = 'Customer' GROUP BY Name ORDER BY COUNT(Name) DESC");

                BindChart(ChartHiredBar, "Select Name,Count(Name) from hired_table GROUP BY Name ORDER BY COUNT(Name) DESC", "Hired Products");
                BindPieChart(ChartHiredPie, "Select  Name,Count(Name) from hired_table GROUP BY Name ORDER BY COUNT(Name) DESC");
            }

            CricketSystemEntities context = new CricketSystemEntities();

            //counting all Orders
            var countOrdersAll = context.order_table.Count(t => t.OrderBy == "Customer");
            lblTotSoldOrders.Text = countOrdersAll.ToString();

            //counting all Orders
            var countHiredAll = context.hired_table.Count();
            lblTotHiredOrders.Text = countHiredAll.ToString();
        }
        public void BindDate()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT(Date) FROM order_table"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    ddlFilterByDate.DataSource = cmd.ExecuteReader();
                    ddlFilterByDate.DataTextField = "Date";
                    ddlFilterByDate.DataValueField = "Date";
                    ddlFilterByDate.DataBind();
                    conn.Close();
                }
            }
            ddlFilterByDate.Items.Insert(0, new ListItem("-Filter By Date-", "0"));

        }
        public void BindStatus()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT(Status) FROM order_table"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    conn.Open();
                    ddlFilterByDate.DataSource = cmd.ExecuteReader();
                    ddlFilterByDate.DataTextField = "Status";
                    ddlFilterByDate.DataValueField = "Status";
                    ddlFilterByDate.DataBind();
                    conn.Close();
                }
            }
            ddlFilterByDate.Items.Insert(0, new ListItem("-Filter By Date-", "0"));

        }
        protected void OnSignOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("../Index.html");
        }
        private void BindDataUser()
        {
            string status = ddlFilterByStatus.SelectedValue;
            string day = ddlFilterByDate.SelectedValue;

            CricketSystemEntities context = new CricketSystemEntities();

            if (ddlFilterByStatus.SelectedIndex > 0)
            {
                //counting all Orders
                var countOrdersAll = context.order_table.Count(t => t.OrderBy == "Customer" && t.Status == status);
                lblTotSoldOrders.Text = countOrdersAll.ToString();

                //counting all Orders
                var countHiredAll = context.hired_table.Count(t => t.Status == status);
                lblTotHiredOrders.Text = countHiredAll.ToString();

                BindChart(ChartOrderBar, "Select Name,Count(Name) from order_table WHERE OrderBy = 'Customer' AND Status = '"+status+"' GROUP BY Name ORDER BY COUNT(Name) DESC", "Ordered Products");
                BindPieChart(ChartOrderPie, "Select  Name,Count(Name) from order_table WHERE OrderBy = 'Customer' AND Status = '" + status + "' GROUP BY Name ORDER BY COUNT(Name) DESC");

                BindChart(ChartHiredBar, "Select Name,Count(Name) from hired_table WHERE Status = '" + status + "' GROUP BY Name ORDER BY COUNT(Name) DESC", "Hired Products");
                BindPieChart(ChartHiredPie, "Select  Name,Count(Name) from hired_table WHERE Status = '" + status + "' GROUP BY Name ORDER BY COUNT(Name) DESC");
            }
            else if (ddlFilterByDate.SelectedIndex > 0)
            {
                //counting all Orders
                var countOrdersAll = context.order_table.Count(t => t.OrderBy == "Customer" && t.Date == day);
                lblTotSoldOrders.Text = countOrdersAll.ToString();

                //counting all Orders
                var countHiredAll = context.hired_table.Count(t => t.Date == day);
                lblTotHiredOrders.Text = countHiredAll.ToString();

                BindChart(ChartOrderBar, "Select Name,Count(Name) from order_table WHERE OrderBy = 'Customer' AND Date = '" + day + "' GROUP BY Name ORDER BY COUNT(Name) DESC", "Ordered Products");
                BindPieChart(ChartOrderPie, "Select  Name,Count(Name) from order_table WHERE OrderBy = 'Customer' AND Date = '" + day + "' GROUP BY Name ORDER BY COUNT(Name) DESC");

                BindChart(ChartHiredBar, "Select Name,Count(Name) from hired_table WHERE Date = '" + day + "' GROUP BY Name ORDER BY COUNT(Name) DESC", "Hired Products");
                BindPieChart(ChartHiredPie, "Select  Name,Count(Name) from hired_table WHERE Date = '" + day + "' GROUP BY Name ORDER BY COUNT(Name) DESC");
            }
            else
            {

                //counting all Orders
                var countOrdersAll = context.order_table.Count(t => t.OrderBy == "Customer");
                lblTotSoldOrders.Text = countOrdersAll.ToString();

                //counting all Orders
                var countHiredAll = context.hired_table.Count();
                lblTotHiredOrders.Text = countHiredAll.ToString();
                BindChart(ChartOrderBar, "Select Name,Count(Name) from order_table WHERE OrderBy = 'Customer' GROUP BY Name ORDER BY COUNT(Name) DESC", "Ordered Products");
                BindPieChart(ChartOrderPie, "Select  Name,Count(Name) from order_table WHERE OrderBy = 'Customer' GROUP BY Name ORDER BY COUNT(Name) DESC");

                BindChart(ChartHiredBar, "Select Name,Count(Name) from hired_table GROUP BY Name ORDER BY COUNT(Name) DESC", "Hired Products");
                BindPieChart(ChartHiredPie, "Select  Name,Count(Name) from hired_table GROUP BY Name ORDER BY COUNT(Name) DESC");
            }
        }
        protected void ddlFilterByStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFilterByDate.SelectedIndex = 0;
            BindDataUser();
        }
        protected void ddlFilterByDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFilterByStatus.SelectedIndex = 0;
            BindDataUser();
        }
        public void BindChart(Chart chart, string query, string title)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }

                chart.Series[0].Points.DataBindXY(x, y);
                chart.Series[0].Label = "#VALY";
                chart.Titles.Add(title);

                // Create a new legend called "Legend2".
                chart.Legends.Add(new Legend("Default"));
                chart.Series["Default"].LegendText = "#VALX";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void BindPieChart(Chart chart, string query)
        {
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                string[] x = new string[dt.Rows.Count];
                int[] y = new int[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[i][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[i][1]);
                }

                chart.Series[0].Points.DataBindXY(x, y);
                chart.Series[0].Label = "#PERCENT";
                // Create a new legend called "Legend2".
                chart.Legends.Add(new Legend("Legend2"));
                chart.Series["Default"].LegendText = "#VALX";

                // Set Docking of the Legend chart to the Default Chart Area.
                chart.Legends["Default"].DockedToChartArea = "Default";

                // Assign the legend to Series1.
                chart.Series["Default"].Legend = "Legend2";
                chart.Series["Default"].IsVisibleInLegend = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}