using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using log4net;
using System.Net.Mail;
using System.Threading;

namespace FirstDemo.Custom.Widget.Form
{
    public partial class FormFeedback : UserControl
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db_yosa.localdev.info"].ConnectionString);
        ILog logger = LogManager.GetLogger("ErrorLog");

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void _SendEmail()
        {
            try
            {
                var FromEmail = new MailAddress("celepon.boy@gmail.com", "Yogie Sanjaya");
                var ToEmail = new MailAddress(txtEmail.Text, txtName.Text);
                var Subject = "Feedback Summary";
                string emailBody = "";

                emailBody += "<p>" + "This is your feedback summary that has been submit" + "</p>";
                emailBody += "<p><b> " + "Name" + " : </b>" + txtName.Text + "</p>";
                emailBody += "<p><b> " + "Organisation" + " : </b>" + txtOrganisation.Text + "</p>";
                emailBody += "<p><b> " + "Position" + " : </b>" + txtPosition.Text + "</p>";
                emailBody += "<p><b> " + "Rate" + " : </b>" + ddlSatisfaction.SelectedValue + "</p>";
                emailBody += "<p><b> " + "Usability" + " : </b>" + rblUsability.SelectedValue + "</p>";
                emailBody += "<p><b> " + "Comment" + " : </b>" + txtComment.Text + "</p>";
                emailBody += "<p>" + "Thank you for your participation to make us better.";
                emailBody += "For more information don't hesitate to visit our website ";
                emailBody += "<a href=\"yosa.localdev.info?utm_source=feedback&utm_medium=email&utm_campaign=29-05-2019\">here</a>" + "</p>";

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

        private void _SaveFeedback()
        {
            try
            {
                conn.Open();
                string insert = "insert into Table_Feedback (Id,Name,Organisation,Position,Email,Satisfaction,Usability,Comment,DateSubmitted) values (@Id,@Name,@Organisation,@Position,@Email,@Satisfaction,@Usability,@Comment,@DateSubmitted)";
                SqlCommand com;
                Guid FeedbackGUID = Guid.NewGuid();
                com = new SqlCommand(insert, conn);
                com.Parameters.AddWithValue("@Id", FeedbackGUID.ToString());
                com.Parameters.AddWithValue("@Name", txtName.Text);
                com.Parameters.AddWithValue("@Organisation", txtOrganisation.Text);
                com.Parameters.AddWithValue("@Position", txtPosition.Text);
                com.Parameters.AddWithValue("@Email", txtEmail.Text);
                com.Parameters.AddWithValue("@Satisfaction", ddlSatisfaction.SelectedValue);
                com.Parameters.AddWithValue("@Usability", rblUsability.SelectedValue);
                com.Parameters.AddWithValue("@Comment", txtComment.Text);
                com.Parameters.AddWithValue("@DateSubmitted", DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"));
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                logger.Error(Environment.NewLine + "Message: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            _SaveFeedback();
            _SendEmail();
            txtName.Text = txtOrganisation.Text = txtPosition.Text = txtEmail.Text = txtComment.Text = string.Empty;
            ddlSatisfaction.SelectedIndex = rblUsability.SelectedIndex = 0;
        }
    }
}