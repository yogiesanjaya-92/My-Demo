using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Tambahan
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using FirstDemo.Custom.Utils;
using log4net;

namespace FirstDemo
{
    public partial class Admin_News : Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _LoadCatgeories();
                FillGrid();
                FillCategoryGrid();
                Button2.Enabled = false;
                BtnCategoryDelete.Enabled = false;
            }
        }

        private void _LoadCatgeories()
        {
            try
            {
                conn.Open();
                string Categories = "SELECT Table_News_Category.CategoryID, Table_News_Category.CategoryName FROM Table_News_Category";
                SqlDataAdapter sda = new SqlDataAdapter(Categories, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                DdlNewsCategory.DataSource = dta;
                DdlNewsCategory.DataBind();
                DdlNewsCategory.DataTextField = "CategoryName";
                //DdlNewsCategory.DataTextFormatString = "Sort By : {0:}";
                DdlNewsCategory.DataValueField = "CategoryID";
                DdlNewsCategory.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string insert = "insert into Table_News (Id,Date,Title,CategoryID,News) values (@Id,@Date,@Title,@CategoryID,@News)";
                string update = "update Table_News set Date = @Date, Title = @Title, CategoryID = @CategoryID, News = @News where Id = @Id";
                SqlCommand com;

                if (HiddenField1.Value == "")
                {
                    Guid NewsGUID = Guid.NewGuid();
                    HiddenField1.Value = NewsGUID.ToString();
                    com = new SqlCommand(insert, conn);
                }

                else
                {
                    com = new SqlCommand(update, conn);
                }

                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.Parameters.AddWithValue("@Date", DateTime.ParseExact(TextBox1.Text, "dd/MM/yyyy H:mm:ss", null).ToString("MM/dd/yyyy H:mm:ss"));
                com.Parameters.AddWithValue("@Title", TextBox2.Text);
                com.Parameters.AddWithValue("@CategoryID", DdlNewsCategory.SelectedValue);
                com.Parameters.AddWithValue("@News", TextBox3.Text);
                com.ExecuteNonQuery();

                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = "";
                Button1.Text = "Post";
                Button2.Enabled = false;
                Button4.Enabled = true;
                DdlNewsCategory.SelectedIndex = 0;
                FillGrid();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        private void FillGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string ViewEvery = "SELECT Table_News.Id, Table_News.Title, Table_News.Date, Table_News_Category.CategoryName, Table_News.News FROM Table_News, Table_News_Category WHERE Table_News.CategoryID = Table_News_Category.CategoryID";

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
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void lnk_OnClick(Object sender, EventArgs e)
        {
            try
            {
                string Id = (sender as LinkButton).CommandArgument;
                string ViewOne = "SELECT * From Table_News where Id = @Id";
                SqlDataAdapter sda = new SqlDataAdapter(ViewOne, conn);
                sda.SelectCommand.Parameters.AddWithValue("@Id", Id);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                HiddenField1.Value = dta.Rows[0][Constants.FieldName.NEWS_ID].ToString();
                TextBox1.Text = dta.Rows[0][Constants.FieldName.NEWS_DATE].ToString();
                TextBox2.Text = dta.Rows[0][Constants.FieldName.NEWS_TITLE].ToString();
                DdlNewsCategory.SelectedValue = dta.Rows[0][Constants.FieldName.NEWS_CATEGORY_ID].ToString();
                TextBox3.Text = dta.Rows[0][Constants.FieldName.NEWS_CONTENT].ToString();
                Button1.Text = "Update";
                Button2.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
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
                string DeleteOne = "DELETE From Table_News where Id = @Id";
                SqlCommand com = new SqlCommand(DeleteOne, conn);
                com.Parameters.AddWithValue("@Id", HiddenField1.Value);
                com.ExecuteNonQuery();
                conn.Close();

                HiddenField1.Value = "";
                TextBox1.Text = TextBox2.Text = TextBox3.Text = "";
                Button1.Text = "Post";
                Button2.Enabled = false;
                DdlNewsCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }

            FillGrid();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HiddenField1.Value = "";
            TextBox1.Text = TextBox2.Text = TextBox3.Text = "";
            Button1.Text = "Post";
            Button2.Enabled = false;
            Button4.Enabled = true;
            DdlNewsCategory.SelectedIndex = 0;

            Calendar1.Visible = false;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToString();
            Calendar1.Visible = false;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Calendar1.Visible = true;
            Button4.Enabled = false;
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            PnlNewsCategory.Visible = true;
            PnlNewsContent.Visible = false;
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            PnlNewsContent.Visible = true;
            PnlNewsCategory.Visible = false;
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
                    int idx = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.HeaderText == "News"));

                    if (e.Row.Cells[idx].Text.ToString().Length > 40)
                    {
                        e.Row.Cells[idx].Text = e.Row.Cells[idx].Text.ToString().Substring(0, 40) + "...";
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            FillGrid();
        }


        private void FillCategoryGrid()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string ViewEvery = "SELECT * FROM Table_News_Category";
                SqlDataAdapter sda = new SqlDataAdapter(ViewEvery, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                if (dta.AsEnumerable().Any())
                {
                    dta = dta.AsEnumerable().OrderBy(n => n.Field<string>("CategoryID")).CopyToDataTable();
                }
                GdvCategory.DataSource = dta;
                GdvCategory.DataBind();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void BtnCategoryAdd_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string fullPath = "";
                string insert = "insert into Table_News_Category (CategoryID,CategoryName,CategoryLogo,CategoryDescription) values (@CategoryID,@CategoryName,@CategoryLogo,@CategoryDescription)";
                string update = "update Table_News_Category set CategoryName = @CategoryName, CategoryLogo = @CategoryLogo, CategoryDescription = @CategoryDescription where CategoryID = @CategoryID";
                SqlCommand com;

                if (BtnCategoryAdd.Text.Equals("Add"))
                {
                    com = new SqlCommand(insert, conn);
                }
                else
                {
                    com = new SqlCommand(update, conn);
                }

                if (!string.IsNullOrWhiteSpace(FuImages.PostedFile.FileName))
                {
                    fullPath = Server.MapPath("~/Image/" + Path.GetFileName(FuImages.PostedFile.FileName));
                    FuImages.SaveAs(fullPath);
                }
                else
                {
                    fullPath = Server.MapPath(ImgCategoryLogo.ImageUrl);
                }

                com.Parameters.AddWithValue("@CategoryID", TxtCategoryID.Text);
                com.Parameters.AddWithValue("@CategoryName", TxtCategoryName.Text);
                com.Parameters.AddWithValue("@CategoryLogo", fullPath);
                com.Parameters.AddWithValue("@CategoryDescription", TxtCategoryDescription.Text);
                com.ExecuteNonQuery();

                conn.Close();

                TxtCategoryID.Text = TxtCategoryName.Text = TxtCategoryDescription.Text = "";
                ImgCategoryLogo.ImageUrl = "~/Image/no-image.jpg";
                BtnCategoryAdd.Text = "Add";
                BtnCategoryDelete.Enabled = false;
                FillCategoryGrid();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void BtnCategoryDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string DeleteOne = "DELETE From Table_News_Category where CategoryID = @CategoryID";
                SqlCommand com = new SqlCommand(DeleteOne, conn);
                com.Parameters.AddWithValue("@CategoryID", TxtCategoryID.Text);
                com.ExecuteNonQuery();
                conn.Close();

                string filePath = Server.MapPath(ImgCategoryLogo.ImageUrl);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                TxtCategoryID.Text = TxtCategoryName.Text = TxtCategoryDescription.Text = "";
                ImgCategoryLogo.ImageUrl = "~/Image/no-image.jpg";
                BtnCategoryAdd.Text = "Add";
                BtnCategoryDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }

            FillCategoryGrid();
        }

        protected void BtnCategoryClear_Click(object sender, EventArgs e)
        {
            TxtCategoryID.Text = TxtCategoryName.Text = TxtCategoryDescription.Text = "";
            ImgCategoryLogo.ImageUrl = "~/Image/no-image.jpg";
            BtnCategoryAdd.Text = "Add";
            BtnCategoryDelete.Enabled = false;
        }

        protected void GdvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    var boundFields = e.Row.Cells.Cast<DataControlFieldCell>().Select(cell => cell.ContainingField).ToList();
                    int idx = boundFields.IndexOf(boundFields.FirstOrDefault(f => f.HeaderText == "Description"));
                    if (idx >= 0)
                    {
                        if (e.Row.Cells[idx].Text.ToString().Length > 40)
                        {
                            e.Row.Cells[idx].Text = e.Row.Cells[idx].Text.ToString().Substring(0, 40) + "...";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void GdvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GdvCategory.PageIndex = e.NewPageIndex;
            FillCategoryGrid();
        }

        protected void LBtnCategory_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = (sender as LinkButton).CommandArgument;
                string ViewOne = "SELECT * From Table_News_Category where CategoryID = @CategoryID";
                SqlDataAdapter sda = new SqlDataAdapter(ViewOne, conn);
                sda.SelectCommand.Parameters.AddWithValue("@CategoryID", Id);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                TxtCategoryID.Text = dta.Rows[0]["CategoryID"].ToString();
                TxtCategoryName.Text = dta.Rows[0]["CategoryName"].ToString();
                TxtCategoryDescription.Text = dta.Rows[0]["CategoryDescription"].ToString();
                ImgCategoryLogo.ImageUrl = dta.Rows[0]["CategoryLogo"].ToString().Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");

                if (string.IsNullOrWhiteSpace(ImgCategoryLogo.ImageUrl))
                {
                    ImgCategoryLogo.ImageUrl = "~/Image/no-image.jpg";
                }

                BtnCategoryAdd.Text = "Update";
                BtnCategoryDelete.Enabled = true;
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }
    }
}