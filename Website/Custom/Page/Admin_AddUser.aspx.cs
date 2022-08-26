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
    public partial class Admin_AddUser : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");
        string state;
        string autenth;

        protected void Page_Load(object sender, EventArgs e)
        {
            Button2.Enabled = false;
            FillGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                Guid newGUID = Guid.NewGuid();
                SqlCommand com = new SqlCommand("SaveorUpdate", conn);
                com.CommandType = CommandType.StoredProcedure;
                if (TextBox1.Text == "")
                {
                    HiddenField1.Value = newGUID.ToString();
                    com.Parameters.AddWithValue("@M", 0);
                    state = HiddenField1.Value.ToString();
                }
                else
                {
                    com.Parameters.AddWithValue("@M", 1);
                    state = "";
                }
                //com.Parameters.AddWithValue("@Id", (HiddenField1.Value == "" ? 0 : Convert.ToInt32(HiddenField1.Value)));
                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.Parameters.AddWithValue("@UserName", TextBox2.Text.Trim());
                com.Parameters.AddWithValue("@Email", TextBox3.Text.Trim());
                com.Parameters.AddWithValue("@Pass", TextBox4.Text.Trim());
                com.Parameters.AddWithValue("@Country", TextBox5.Text.Trim());
                if (CheckBox1.Checked == true)
                { com.Parameters.AddWithValue("@Admin", 1); }
                else
                { com.Parameters.AddWithValue("@Admin", 0); }
                com.ExecuteNonQuery();
                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = TextBox5.Text = "";
                Label1.Text = Label2.Text = "";
                Button1.Text = "Save";
                Button2.Enabled = false;
                CheckBox1.Checked = false;

                if (state == "")
                {
                    Label1.Text = "Update Succesfully";
                }
                else
                {
                    Label1.Text = "Saved Succesfully";
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
                //ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('" + ex.ToString() + "');", true);
            }

            FillGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand com = new SqlCommand("DeleteId", conn);
                com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.AddWithValue("@Id", Convert.ToInt32(HiddenField1.Value));
                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.ExecuteNonQuery();
                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = TextBox5.Text = "";
                Label1.Text = Label2.Text = "";
                Button1.Text = "Save";
                Button2.Enabled = false;
                Label1.Text = "Delete Successfully";
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
            TextBox1.Text = TextBox2.Text = TextBox3.Text = TextBox4.Text = TextBox5.Text = "";
            Label1.Text = Label2.Text = "";
            Button1.Text = "Save";
            Button2.Enabled = false;
            CheckBox1.Checked = false;
        }

        void FillGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataAdapter sda = new SqlDataAdapter("ViewAll", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                if (dta.AsEnumerable().Any())
                {
                    dta = dta.AsEnumerable().OrderBy(n => n.Field<string>("UserName")).CopyToDataTable();
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
                // int Id = Convert.ToInt32((sender as LinkButton).CommandArgument);
                string Id = (sender as LinkButton).CommandArgument;
                SqlDataAdapter sda = new SqlDataAdapter("ViewId", conn);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.AddWithValue("@Id", Id);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                //HiddenField1.Value = Id.ToString();
                HiddenField1.Value = dta.Rows[0]["Id"].ToString();
                TextBox1.Text = dta.Rows[0]["Id"].ToString();
                TextBox2.Text = dta.Rows[0]["UserName"].ToString();
                TextBox3.Text = dta.Rows[0]["Email"].ToString();
                TextBox4.Text = dta.Rows[0]["Password"].ToString();
                TextBox5.Text = dta.Rows[0]["Country"].ToString();
                autenth = dta.Rows[0]["Admin"].ToString();
                if (Convert.ToInt32(autenth) == 1)
                { CheckBox1.Checked = true; }
                else
                { CheckBox1.Checked = false; }
                Button1.Text = "Update";
                Button2.Enabled = true;
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