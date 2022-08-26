using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

namespace FirstDemo
{
    public partial class Admin_Feedback : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void FillGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string ViewEvery = "SELECT * From Table_Feedback";
                SqlDataAdapter sda = new SqlDataAdapter(ViewEvery, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();

                var rows = dta.AsEnumerable();
                rows = rows.Where(fb => fb.Field<DateTime>("DateSubmitted") >= DateTime.ParseExact(TxtDateFrom.Text, "yyyy-MM-dd", null) && fb.Field<DateTime>("DateSubmitted") <= DateTime.ParseExact(TxtDateTo.Text, "yyyy-MM-dd", null));
                rows = rows.OrderByDescending(n => n.Field<DateTime>("DateSubmitted"));

                if (rows.Any())
                {
                    dta = rows.CopyToDataTable();
                }
                else
                {
                    dta.Rows.Clear();
                }

                GdvFeedback.DataSource = dta;
                GdvFeedback.DataBind();

                if (GdvFeedback.Rows.Count.Equals(0))
                {
                    BtnDownload.Enabled = false;
                }
                else
                {
                    BtnDownload.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void GdvFeedback_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var boundFields = e.Row.Cells.Cast<DataControlFieldCell>().Select(cell => cell.ContainingField).ToList();
                    int idx = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.HeaderText == "Comment"));

                    if (e.Row.Cells[idx].Text.ToString().Length > 30)
                    {
                        e.Row.Cells[idx].Text = e.Row.Cells[idx].Text.ToString().Substring(0, 30) + "...";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void GdvFeedback_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GdvFeedback.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string txtFile = string.Empty;
                int idx = GdvFeedback.PageIndex;
                //Adding Column Name In Text File.  
                foreach (TableCell cell in GdvFeedback.HeaderRow.Cells)
                {
                    txtFile += cell.Text + "\t";
                }

                txtFile += "\r\n";

                for (int i = 0; i < GdvFeedback.PageCount; i++)
                {
                    GdvFeedback.SetPageIndex(i);
                    //Adding Data Column Values in Text File  
                    foreach (GridViewRow row in GdvFeedback.Rows)
                    {
                        foreach (TableCell cell in row.Cells)
                        {
                            txtFile += cell.Text + "\t";
                        }
                        txtFile += "\r\n";
                    }
                }

                GdvFeedback.PageIndex = idx;

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=Feedback.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(txtFile);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void BtnFilter_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}