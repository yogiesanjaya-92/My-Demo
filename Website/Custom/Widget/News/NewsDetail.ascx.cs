using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using log4net;
using FirstDemo.Custom.Utils;

namespace FirstDemo.Custom.Widget.News
{
    public partial class NewsDetail : UserControl
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");
        private string reqID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            _ReadUrl();
            _FillPage();
        }

        private void _ReadUrl()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                reqID = Request.QueryString["id"];
            }
        }

        private void _FillPage()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(reqID))
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }

                    string ViewByID = "SELECT Table_News.Id, Table_News.Title, Table_News.Date, Table_News_Category.CategoryLogo, Table_News_Category.CategoryName, Table_News_Category.CategoryDescription, Table_News.News FROM Table_News, Table_News_Category WHERE Table_News.CategoryID = Table_News_Category.CategoryID AND Table_News.Id = @Id";
                    //string ViewByID = "SELECT * From Table_News where Id = @Id";
                    SqlDataAdapter sda = new SqlDataAdapter(ViewByID, conn);
                    sda.SelectCommand.Parameters.AddWithValue("@Id", reqID);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    conn.Close();

                    if (dta != null)
                    {
                        if (dta.Rows.Count > 0)
                        {
                            string newsTitle = dta.Rows[0].Field<string>(Constants.FieldName.NEWS_TITLE);
                            string newsCategoryLogo = dta.Rows[0].Field<string>(Constants.FieldName.NEWS_CATEGORY_LOGO).Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
                            string newsCategoryName = dta.Rows[0].Field<string>(Constants.FieldName.NEWS_CATEGORY_NAME);
                            string newsCategoryDescription = dta.Rows[0].Field<string>(Constants.FieldName.NEWS_CATEGORY_DESCRIPTION);
                            string newsDate = dta.Rows[0].Field<DateTime>(Constants.FieldName.NEWS_DATE).ToString("dddd, dd MMMM yyyy");
                            string newsContent = dta.Rows[0].Field<string>(Constants.FieldName.NEWS_CONTENT);

                            LblTitle.InnerText = !string.IsNullOrWhiteSpace(newsTitle) ? newsTitle : string.Empty;
                            ImgCategoryLogo.ImageUrl = !string.IsNullOrWhiteSpace(newsCategoryLogo) ? newsCategoryLogo : string.Empty;
                            LitCategory.Text = !string.IsNullOrWhiteSpace(newsCategoryName) ? newsCategoryName : string.Empty;
                            LitCategoryDescription.Text = !string.IsNullOrWhiteSpace(newsCategoryDescription) ? newsCategoryDescription : string.Empty;
                            LitDate.Text = !string.IsNullOrWhiteSpace(newsDate) ? newsDate : string.Empty;
                            LitContent.Text = !string.IsNullOrWhiteSpace(newsContent) ? newsContent : string.Empty;
                        }
                    }

                    HypNewsBack.NavigateUrl = Request.Url.AbsolutePath;
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }
    }
}