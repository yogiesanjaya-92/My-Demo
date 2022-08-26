using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using FirstDemo.Custom.Utils;
using log4net;

namespace FirstDemo.Custom.Widget.Post
{
    public partial class PostListing : UserControl
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");
        DataTable dta;
        private string reqTitle = "";
        private string currentUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _LoadPostTitle();
                _ReadUrl();
                _LoadPostContent();
            }
            else
            {
                _ReadCurrentState();
            }
        }

        private void _ReadUrl()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["title"]))
            {
                reqTitle = Request.QueryString["title"];
                ddlPost.SelectedValue = reqTitle;
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
            query += !string.IsNullOrWhiteSpace(reqTitle) ? "title=" + reqTitle : "";

            if (!string.IsNullOrWhiteSpace(query))
            {
                currentUrl += "?" + query;
            }
        }

        private void _ReadCurrentState()
        {
            reqTitle = ddlPost.SelectedValue;
        }

        private void _LoadPostTitle()
        {
            try
            {
                conn.Open();
                string View = "SELECT * From Table_Post ";
                SqlDataAdapter sda = new SqlDataAdapter(View, conn);
                dta = new DataTable();
                sda.Fill(dta);
                conn.Close();

                if (dta.AsEnumerable().Any())
                {
                    dta = dta.AsEnumerable().OrderBy(n => n.Field<string>("Title")).CopyToDataTable();
                }

                foreach (DataRow dr in dta.Rows)
                {
                    ddlPost.Items.Add(dr["Title"].ToString());
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        private void _LoadPostContent()
        {
            try
            {
                lblTitle.Text = dta.Rows[Convert.ToInt32(ddlPost.SelectedIndex.ToString())].Field<string>("Title");
                txtContent.Text = dta.Rows[Convert.ToInt32(ddlPost.SelectedIndex.ToString())].Field<string>("Content");
                lblDate.Text = dta.Rows[Convert.ToInt32(ddlPost.SelectedIndex.ToString())].Field<DateTime>("DatePublished").ToString("dddd, dd MMMM yyyy");
                string youtubeURL = dta.Rows[Convert.ToInt32(ddlPost.SelectedIndex.ToString())].Field<string>("Youtube");
                FrmPostYoutube.Src = "https://www.youtube.com/embed/" + CommonUtils.ExtractYoutubeVideoId(youtubeURL);
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine  + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void ddlPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            reqTitle = ddlPost.SelectedValue;
            _ProcessUrl();
            Response.Redirect(currentUrl);
        }
    }
}