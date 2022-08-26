using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using log4net;
using FirstDemo.Custom.Utils;
using System.Web;

namespace FirstDemo.Custom.Widget.News
{
    public partial class NewsListing : UserControl
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");
        private int pageIndex = 0;
        private int pageSize = 4;
        private int maxPage = 0;
        private string reqID = "";
        private string reqSort = "";
        private string reqCategory = "";
        private string currentUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _LoadCatgeories();
                _ReadUrl();
                _FillPage();
            }
            else
            {
                _ReadCurrentState();
            }
        }

        private void _LoadCatgeories()
        {
            try
            {
                conn.Open();
                string Categories = "SELECT Table_News_Category.CategoryName FROM Table_News_Category";
                SqlDataAdapter sda = new SqlDataAdapter(Categories, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                conn.Close();
                DdlNewsCategory.DataSource = dta;
                DdlNewsCategory.DataBind();
                DdlNewsCategory.DataTextField = "CategoryName";
                DdlNewsCategory.DataValueField = "CategoryName";
                DdlNewsCategory.DataBind();
                DdlNewsCategory.Items.Insert(0, "-");
                DdlNewsCategory.SelectedItem.Equals("-");
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        private void _ReadUrl()
        {
            if (!Int32.TryParse(Request.QueryString["page"], out pageIndex))
            {
                pageIndex = 1;
            }
            else
            {
                if (pageIndex < 1)
                {
                    pageIndex = 1;
                }
            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                reqID = Request.QueryString["id"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["category"]))
            {
                reqCategory = Request.QueryString["category"];
                DdlNewsCategory.SelectedValue = reqCategory;
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sort"]))
            {
                reqSort = Request.QueryString["sort"];
                DdlNewsSort.SelectedValue = reqSort;
            }
        }

        private void _ProcessUrl()
        {
            currentUrl = Request.Url.AbsolutePath;
            int queryIndex = currentUrl.LastIndexOf('?');
            if (queryIndex >= 0)
            {
                currentUrl = currentUrl.Substring(0, queryIndex);
            }

            string query = "";
            query += !string.IsNullOrWhiteSpace(reqCategory) ? "category=" + reqCategory : "";
            query += !string.IsNullOrWhiteSpace(reqSort) ? (!string.IsNullOrWhiteSpace(query) ? "&" : "") + "sort=" + reqSort : "";
            query += !string.IsNullOrWhiteSpace(pageIndex.ToString()) ? (!string.IsNullOrWhiteSpace(query) ? "&" : "") + "page=" + pageIndex.ToString() : "";

            if (!string.IsNullOrWhiteSpace(query))
            {
                currentUrl += "?" + query;
            }
        }

        private void _ReadCurrentState()
        {
            Int32.TryParse(DdlNewsPage.SelectedValue, out pageIndex);
            Int32.TryParse(HypNewsMaxPage.Text, out maxPage);

            reqCategory = DdlNewsCategory.SelectedValue;
            reqSort = DdlNewsSort.SelectedValue;

            if (pageIndex == 0 && maxPage == 0)
            {
                pageIndex = 1;
            }
        }

        private void _FillPage()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string ViewEvery = "SELECT Table_News.Id, Table_News.Title, Table_News.Date, Table_News_Category.CategoryLogo, Table_News_Category.CategoryName, Table_News.News FROM Table_News, Table_News_Category WHERE Table_News.CategoryID = Table_News_Category.CategoryID";
                SqlDataAdapter sda = new SqlDataAdapter(ViewEvery, conn);
                DataTable dta = new DataTable();
                sda.Fill(dta);
                int content = Convert.ToInt32(dta.Rows.Count.ToString());
                conn.Close();

                var rows = dta.AsEnumerable();

                if (!string.IsNullOrWhiteSpace(reqCategory))
                {
                    if (!reqCategory.Equals("-"))
                    {
                        rows = rows.Where(n => n.Field<string>(Constants.FieldName.NEWS_CATEGORY_NAME).Equals(reqCategory));
                    }
                }

                switch (reqSort)
                {
                    case "A-Z":
                        rows = rows.OrderBy(n => n.Field<string>(Constants.FieldName.NEWS_TITLE));
                        break;

                    case "Z-A":
                        rows = rows.OrderByDescending(n => n.Field<string>(Constants.FieldName.NEWS_TITLE));
                        break;
                    case "Newest":
                        rows = rows.OrderByDescending(n => n.Field<DateTime>(Constants.FieldName.NEWS_DATE));
                        break;
                    case "Oldest":
                        rows = rows.OrderBy(n => n.Field<DateTime>(Constants.FieldName.NEWS_DATE));
                        break;
                    default:
                        rows = rows.OrderByDescending(n => n.Field<DateTime>(Constants.FieldName.NEWS_DATE));
                        break;
                }

                if (rows.Any())
                {
                    dta = rows.CopyToDataTable();
                }
                else
                {
                    dta.Rows.Clear();
                }

                decimal rowCount = 0;

                if (dta != null)
                {
                    if (dta.Rows.Count > 0)
                    {
                        rowCount = dta.Rows.Count;
                        dta = dta.AsEnumerable().Skip((pageIndex - 1) * (pageSize)).Take(pageSize).CopyToDataTable();
                    }
                }

                RptNewsList.DataSource = dta;
                RptNewsList.DataBind();
                HypNewsMaxPage.Text = "0";

                if (dta != null)
                {
                    if (dta.Rows.Count > 0)
                    {
                        maxPage = (int)Math.Ceiling(rowCount / pageSize);
                        HypNewsMaxPage.Text = maxPage.ToString();
                        DdlNewsPage.DataSource = Enumerable.Range(1, maxPage);
                        DdlNewsPage.DataBind();
                        DdlNewsPage.SelectedIndex = ((maxPage == 1) ? 1 : pageIndex) - 1;
                    }
                    else
                    {
                        DdlNewsPage.DataSource = null;
                        DdlNewsPage.DataBind();
                        DdlNewsPage.Items.Add(new ListItem("0"));
                    }
                }
                else
                {
                    DdlNewsPage.DataSource = null;
                    DdlNewsPage.DataBind();
                    DdlNewsPage.Items.Add(new ListItem("0"));
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }


        protected void RptNewsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlGenericControl LblTitle = e.Item.FindControl("LblTitle") as HtmlGenericControl;
            Literal LitDate = e.Item.FindControl("LitDate") as Literal;
            Literal LitContent = e.Item.FindControl("LitContent") as Literal;
            HyperLink HypNewsLink = e.Item.FindControl("HypNewsLink") as HyperLink;
            HyperLink HypImageLink = e.Item.FindControl("HypImageLink") as HyperLink;
            
            DataRowView item = e.Item.DataItem as DataRowView;

            if (HypImageLink != null)
            {
                string newsCategoryLogo = item.Row.Field<string>(Constants.FieldName.NEWS_CATEGORY_LOGO);
                HypImageLink.NavigateUrl = newsCategoryLogo.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
                HypImageLink.Attributes.Add("Style", "background-image:" + newsCategoryLogo.Replace(HttpContext.Current.Server.MapPath("~/"), "url(/").Replace(@"\", "/") + ")");
            }

            if (HypNewsLink != null)
            {
                string newsID = item.Row.Field<string>(Constants.FieldName.NEWS_ID);
                HypNewsLink.NavigateUrl = Request.Url.AbsolutePath + "?id=" + newsID;
            }

            if (LblTitle != null)
            {
                string newsTitle = item.Row.Field<string>(Constants.FieldName.NEWS_TITLE);
                LblTitle.InnerText = !string.IsNullOrWhiteSpace(newsTitle) ? newsTitle : String.Empty;
            }

            if (LitDate != null)
            {
                string stringDate = item.Row.Field<DateTime>(Constants.FieldName.NEWS_DATE).ToString("dd-MMM-yyyy");
                LitDate.Text = !string.IsNullOrWhiteSpace(stringDate) ? stringDate : String.Empty;
            }

            if (LitContent != null)
            {
                string newsContent = item.Row.Field<string>(Constants.FieldName.NEWS_CONTENT);
                LitContent.Text = !string.IsNullOrWhiteSpace(newsContent) ? (newsContent.Count() >= 80 ? newsContent.Substring(0, 80) + "(...)" : newsContent) : String.Empty;
            }

        }

        protected void LnkNewsPrev_Click(object sender, EventArgs e)
        {
            Int32.TryParse(DdlNewsPage.SelectedValue, out pageIndex);
            Int32.TryParse(HypNewsMaxPage.Text, out maxPage);

            if (pageIndex == 0 && maxPage == 0)
            {
                pageIndex = 1;
            }
            else if (pageIndex > 1)
            {
                pageIndex = pageIndex - 1;
            }
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }

        protected void LnkNewsNext_Click(object sender, EventArgs e)
        {
            Int32.TryParse(DdlNewsPage.SelectedValue, out pageIndex);
            Int32.TryParse(HypNewsMaxPage.Text, out maxPage);

            if (pageIndex == 0 && maxPage == 0)
            {
                pageIndex = 1;
            }
            else if (pageIndex < maxPage)
            {
                pageIndex = pageIndex + 1;
            }
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }

        protected void DdlNewsPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32.TryParse(DdlNewsPage.SelectedValue, out pageIndex);
            Int32.TryParse(HypNewsMaxPage.Text, out maxPage);

            if (pageIndex == 0 && maxPage == 0)
            {
                pageIndex = 1;
            }
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }

        protected void DdlNewsSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            reqSort = DdlNewsSort.SelectedValue;
            pageIndex = 1;
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }

        protected void DdlNewsCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            reqCategory = DdlNewsCategory.SelectedValue;
            pageIndex = 1;
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }
    }
}