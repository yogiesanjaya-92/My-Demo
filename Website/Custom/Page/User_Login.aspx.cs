using System;
using System.Web.UI;
// Tambahan
using System.Data.SqlClient;
using System.Configuration;
using log4net;

namespace FirstDemo
{
    public partial class User_Login : Page
    {
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from Table_User where UserName ='" + txtUserName.Text + "'";
                SqlCommand com = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();

                if (temp == 1)
                {
                    conn.Open();
                    string checkpassword = "select password from Table_User where UserName ='" + txtUserName.Text + "'";
                    SqlCommand passcom = new SqlCommand(checkpassword, conn);
                    string password = passcom.ExecuteScalar().ToString().Replace(" ", "");
                    string checkadmin = "select admin from Table_User where UserName ='" + txtUserName.Text + "'";
                    SqlCommand admincom = new SqlCommand(checkadmin, conn);
                    int admin = Convert.ToInt32(admincom.ExecuteScalar().ToString().Replace(" ", ""));
                    if (password == txtPassword.Text && admin == 1)
                    {
                        Session["New"] = txtUserName.Text;
                        Response.Write("<script> window.alert('Password is correct'); " +
                            "window.location='/Custom/Page/Admin_AddUser.aspx'; </script> ");
                    }
                    else if (password == txtPassword.Text && admin == 0)
                    {
                        Session["New"] = txtUserName.Text;
                        Response.Write("<script> window.alert('Password is correct'); " +
                            "window.location='/Custom/Page/User_Home.aspx'; </script> ");
                    }
                    else
                    {
                        Response.Write("<script> window.alert('Password is not correct'); </script>");
                    }
                }
                else
                {
                    Response.Write("<script> window.alert('UserName is not correct'); </script>");
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }
    }
}