using System;
using System.Web.UI;
// Tambahan
using System.Data.SqlClient;
using System.Configuration;
using log4net;
using System.Net.Mail;
using System.Threading;

namespace FirstDemo
{
    public partial class User_Register : Page
    {
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void _SendEmail()
        {
            try
            {
                var FromEmail = new MailAddress("celepon.boy@gmail.com", "Yogie Sanjaya");
                var ToEmail = new MailAddress(txtEmail.Text, txtUserName.Text);
                var Subject = "Registration Summary";
                string emailBody = "";

                emailBody += "<p>" + "This is your account registration summary that has been submit" + "</p>";
                emailBody += "<p><b> " + "User Name" + " : </b>" + txtUserName.Text + "</p>";
                emailBody += "<p><b> " + "Email Address" + " : </b>" + txtEmail.Text + "</p>";
                emailBody += "<p><b> " + "Country" + " : </b>" + ddlCountry.SelectedValue + "</p>";
                emailBody += "<p><b> " + "Password" + " : </b>" + txtPassword.Text + "</p>";
                emailBody += "<p>" + "Thank you for being our website member.";
                emailBody += "Please don't give this <b> information </b> to everyone else.";
                emailBody += "For more information don't hesitate to visit our website ";
                emailBody += "<a href=\"yosa.localdev.info?utm_source=registration&utm_medium=email&utm_campaign=24-06-2019\">here</a>" + "</p>";

                Thread email = new Thread(delegate ()
                {
                    SmtpClient Client = new SmtpClient();
                    MailMessage msg = new MailMessage();

                    msg.To.Add(ToEmail);
                    msg.From = FromEmail;

                    try
                    {
                        msg.Subject = Subject;
                        msg.Body = emailBody;
                        msg.IsBodyHtml = true;
                        Client.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                });

                email.IsBackground = true;
                email.Start();

            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
                SqlCommand com = new SqlCommand();

                conn.Open();

                string checkuser = "select count(*) from Table_User where UserName ='" + txtUserName.Text + "'";
                com = new SqlCommand(checkuser, conn);
                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());

                if (temp == 1)
                {
                    conn.Close();
                    txtUserName.Text = string.Empty;

                    Response.Write("<script> window.alert('User already exist'); </script>");
                }
                else
                {
                    Guid newGUID = Guid.NewGuid();
                    string insert = "insert into Table_User (Id,UserName,Email,Password,Country,Admin) values (@Id,@Username,@Email,@Pass,@Country,@Admin)";
                    com = new SqlCommand(insert, conn);

                    com.Parameters.AddWithValue("@Id", newGUID.ToString());
                    com.Parameters.AddWithValue("@Username", txtUserName.Text);
                    com.Parameters.AddWithValue("@Email", txtEmail.Text);
                    com.Parameters.AddWithValue("@Pass", txtPassword.Text);
                    com.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.ToString());
                    com.Parameters.AddWithValue("@Admin", 0);
                    com.ExecuteNonQuery();

                    conn.Close();

                    _SendEmail();
                    Response.Write("<script> window.alert('Registration is successful'); " +
                       "window.location='/Custom/Page/User_Login.aspx'; </script> ");
                }
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }
    }
}