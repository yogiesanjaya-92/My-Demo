using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
// Tambahan
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace FirstDemo
{
    public partial class Admin_Post : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {
            FillGrid();
            Button2.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string insert = "insert into Table_Post (Id,Title,Content,DatePublished,Youtube) values (@Id,@Title,@Content,@DatePublished,@Youtube)";
                string update = "update Table_Post set Title = @Title, Content = @Content, DatePublished = @DatePublished, Youtube = @Youtube where Id = @Id";
                SqlCommand com;

                if (HiddenField1.Value == "")
                {
                    Guid newGUID = Guid.NewGuid();
                    HiddenField1.Value = newGUID.ToString();
                    com = new SqlCommand(insert, conn);
                }

                else
                {
                    com = new SqlCommand(update, conn);
                }

                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.Parameters.AddWithValue("@Title", TextBox1.Text);
                com.Parameters.AddWithValue("@Content", TextBox2.Text);
                com.Parameters.AddWithValue("@DatePublished", DateTime.ParseExact(TextBox3.Text, "dd/MM/yyyy H:mm:ss", null).ToString("MM/dd/yyyy H:mm:ss"));
                com.Parameters.AddWithValue("@Youtube", TextBox4.Text);
               com.ExecuteNonQuery();

                //ClientScript.RegisterStartupScript(this.GetType(), "Response", "alert('" + "Post is submitted" + "');", true);

                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = "";
                Button1.Text = "Submit";
                Button2.Enabled = false;
                FillGrid();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        void FillGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string ViewEvery = "SELECT * From Table_Post";
                SqlDataAdapter sda = new SqlDataAdapter(ViewEvery, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();

                if (dta.AsEnumerable().Any())
                {
                    dta = dta.AsEnumerable().OrderBy(n => n.Field<string>("Title")).CopyToDataTable();
                }
                GridView1.DataSource = dta;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void lnk_OnClick(Object sender, EventArgs e)
        {
            try
            {
                string Id = (sender as LinkButton).CommandArgument;
                string ViewOne = "SELECT * From Table_Post where Id = @Id";
                SqlDataAdapter sda = new SqlDataAdapter(ViewOne, conn);
                sda.SelectCommand.Parameters.AddWithValue("@Id", Id);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                HiddenField1.Value = dta.Rows[0]["Id"].ToString();
                TextBox1.Text = dta.Rows[0]["Title"].ToString();
                TextBox2.Text = dta.Rows[0]["Content"].ToString();
                TextBox3.Text = dta.Rows[0]["DatePublished"].ToString();
                TextBox4.Text = dta.Rows[0]["Youtube"].ToString();
                Button1.Text = "Update";
                Button2.Enabled = true;
                Button4.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string DeleteOne = "DELETE From Table_Post where Id = @Id";
                SqlCommand com = new SqlCommand(DeleteOne, conn);
                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.ExecuteNonQuery();
                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = "";
                Button1.Text = "Submit";
                Button2.Enabled = false;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }

            FillGrid();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HiddenField1.Value = "";
            TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = "";
            Button1.Text = "Submit";
            Button2.Enabled = false;
            Button4.Enabled = true;

            Calendar1.Visible = false;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox3.Text = Calendar1.SelectedDate.ToString();
            Calendar1.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
            Button4.Enabled = false;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //var boundFields = e.Row.Cells.Cast<DataControlFieldCell>().Select(cell => cell.ContainingField).Cast<BoundField>().ToList();
                    //int idx = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.DataField == "News"));

                    var boundFields = e.Row.Cells.Cast<DataControlFieldCell>().Select(cell => cell.ContainingField).ToList();
                    int idxContent = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.HeaderText == "Content"));
                    int idxYoutube = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.HeaderText == "Youtube"));

                    if (e.Row.Cells[idxContent].Text.ToString().Length > 40)
                    {
                        e.Row.Cells[idxContent].Text = e.Row.Cells[idxContent].Text.ToString().Substring(0, 40) + "...";
                    }
                    if (e.Row.Cells[idxYoutube].Text.ToString().Length > 20)
                    {
                        e.Row.Cells[idxYoutube].Text = e.Row.Cells[idxYoutube].Text.ToString().Substring(0, 20) + "...";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }
    }
}