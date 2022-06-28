using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmbizoMilestone3
{
    public partial class SupervisorRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void supervisorRegisterButtonClick(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String firstName = supervisorFirstName.Text;
            String lastName = supervisorLastName.Text;
            String email = supervisorEmail.Text;
            String password = supervisorPassword.Text;
            String faculty = supervisorFaculty.Text;

            if (firstName.Replace(" ", "") == "" || lastName.Replace(" ", "") == "" || email.Replace(" ", "") == "" || password.Replace(" ", "") == "" || faculty.Replace(" ", "") == "")
            {
                supervisorRegisterErrorMessagesLabel.Visible = true;
                supervisorRegisterSuccessMessagesLabel.Visible = false;
                supervisorRegisterErrorMessagesLabel.InnerText = "All fields must be not empty";
            }
            else if (password.Replace(" ", "") != password)
            {
                supervisorRegisterErrorMessagesLabel.Visible = true;
                supervisorRegisterSuccessMessagesLabel.Visible = false;
                supervisorRegisterErrorMessagesLabel.InnerText = "Password Cannot Contain Spaces";
            }
            else
            {
                SqlCommand supervisorRegisterProc = new SqlCommand("supervisorRegister", conn);
                supervisorRegisterProc.CommandType = CommandType.StoredProcedure;

                supervisorRegisterProc.Parameters.Add(new SqlParameter("@first_name", firstName));
                supervisorRegisterProc.Parameters.Add(new SqlParameter("@last_name", lastName));
                supervisorRegisterProc.Parameters.Add(new SqlParameter("@faculty", faculty));
                supervisorRegisterProc.Parameters.Add(new SqlParameter("@email", email));
                supervisorRegisterProc.Parameters.Add(new SqlParameter("@password", password));

                SqlCommand getLastIdProc = new SqlCommand("getLastId", conn);
                getLastIdProc.CommandType = CommandType.StoredProcedure;
                SqlParameter id = getLastIdProc.Parameters.Add("@id", SqlDbType.Int);
                id.Direction = System.Data.ParameterDirection.Output;

                SqlCommand checkemailProc = new SqlCommand("checkemail", conn);
                checkemailProc.CommandType = CommandType.StoredProcedure;
                checkemailProc.Parameters.Add(new SqlParameter("@email", email));

                SqlParameter success = checkemailProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkemailProc.ExecuteNonQuery();
                if (success.Value.ToString().Equals("True"))
                {
                    supervisorRegisterProc.ExecuteNonQuery();
                    getLastIdProc.ExecuteNonQuery();
                    conn.Close();

                    int lastId = Int16.Parse(id.Value.ToString());
                    String messageCorrect = "You Registered Successfully, Your ID is: " + lastId;
                    supervisorRegisterErrorMessagesLabel.Visible = false;
                    supervisorRegisterSuccessMessagesLabel.Visible = true;
                    supervisorRegisterSuccessMessagesLabel.InnerText = messageCorrect;
                    supervisorRegisterButton.Enabled = false;
                }
                else
                {
                    supervisorRegisterErrorMessagesLabel.Visible = true;
                    supervisorRegisterSuccessMessagesLabel.Visible = false;
                    supervisorRegisterErrorMessagesLabel.InnerText = "There is already a user registered with this email";
                }

                   

            }
        }

        protected void supervisorBackTologinButtonClick(object sender, EventArgs e)
        {
            supervisorRegisterButton.Enabled = true;
            Response.Redirect("Login.aspx");
        }
    }
}