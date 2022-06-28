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
    public partial class StudentRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void studentRegisterButtonClick(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String firstName = studentFirstName.Text;
            String lastName = studentLastName.Text;
            String email = studentEmail.Text;
            String password = studentPassword.Text;
            String faculty = studentFaculty.Text;
            String address = studentAddress.Text;
            int gucian = 1;
            if (GucianRadioButton.SelectedValue == "Non-Gucian")
            {
                gucian = 0;
            }
            if (firstName.Replace(" ", "") == "" || lastName.Replace(" ", "") == "" || email.Replace(" ", "") == "" || password.Replace(" ", "") == "" || faculty.Replace(" ", "") == "" || address.Replace(" ", "") == "")
            {
                studentRegisterErrorMessagesLabel.Visible = true;
                studentRegisterSuccessMessagesLabel.Visible = false;
                studentRegisterErrorMessagesLabel.InnerText = "All fields must be not empty";
            }
            else if (password.Replace(" ", "") != password)
            {
                studentRegisterErrorMessagesLabel.Visible = true;
                studentRegisterSuccessMessagesLabel.Visible = false;
                studentRegisterErrorMessagesLabel.InnerText = "Password Cannot Contain Spaces";
            }
            else
            {
                SqlCommand studentRegisterProc = new SqlCommand("studentRegister", conn);
                studentRegisterProc.CommandType = CommandType.StoredProcedure;

                studentRegisterProc.Parameters.Add(new SqlParameter("@first_name", firstName));
                studentRegisterProc.Parameters.Add(new SqlParameter("@last_name", lastName));
                studentRegisterProc.Parameters.Add(new SqlParameter("@faculty", faculty));
                studentRegisterProc.Parameters.Add(new SqlParameter("@Gucian", gucian));
                studentRegisterProc.Parameters.Add(new SqlParameter("@email", email));
                studentRegisterProc.Parameters.Add(new SqlParameter("@address", address));
                studentRegisterProc.Parameters.Add(new SqlParameter("@password", password));

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

                    studentRegisterProc.ExecuteNonQuery();
                    getLastIdProc.ExecuteNonQuery();

                    int lastId = Int16.Parse(id.Value.ToString());
                    String messageCorrect = "You Registered Successfully, Your ID is: " + lastId;
                    studentRegisterErrorMessagesLabel.Visible = false;
                    studentRegisterSuccessMessagesLabel.Visible = true;
                    studentRegisterSuccessMessagesLabel.InnerText = messageCorrect;
                    studentRegisterButton.Enabled = false;

                }
                else
                {
                    studentRegisterErrorMessagesLabel.Visible = true;
                    studentRegisterSuccessMessagesLabel.Visible = false;
                    studentRegisterErrorMessagesLabel.InnerText = "There is already a user registered with this email";
                }
                conn.Close();

            }
        }

        protected void studentBackTologinButtonClick(object sender, EventArgs e)
        {
            studentRegisterButton.Enabled = true;
            Response.Redirect("Login.aspx");
        }
    }
}