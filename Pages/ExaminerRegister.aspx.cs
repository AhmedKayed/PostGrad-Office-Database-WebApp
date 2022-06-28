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
    public partial class ExaminerRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void examinerRegisterButtonClick(object sender, EventArgs e)
        {

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            String firstName = ExaminerFirstName.Text;
            String lastName = ExaminerLastName.Text;
            String email = ExaminerEmail.Text;
            String password = ExaminerPassword.Text;
            String FeildOfWork = ExaminerFeildOfWork.Text;

            int national = 0;
            if (ExaminerCheckBox.Checked)
            {
                national = 1;
            }
            if (firstName.Replace(" ", "") == "" || lastName.Replace(" ", "") == "" || email.Replace(" ", "") == "" || password.Replace(" ", "") == "" || FeildOfWork.Replace(" ", "") == "")
            {
                examinerRegisterErrorMessagesLabel.Visible = true;
                examinerRegisterSuccessMessagesLabel.Visible = false;
                examinerRegisterErrorMessagesLabel.InnerText = "All fields must be not empty";
            }
            else if (password.Replace(" ", "") != password)
            {
                examinerRegisterErrorMessagesLabel.Visible = true;
                examinerRegisterSuccessMessagesLabel.Visible = false;
                examinerRegisterErrorMessagesLabel.InnerText = "Password Cannot Contain Spaces";
            }
            else
            {
                SqlCommand ExaminerRegisterProc = new SqlCommand("ExaminerRegister", conn);
                ExaminerRegisterProc.CommandType = CommandType.StoredProcedure;

                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@first_name", firstName));
                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@last_name", lastName));
                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@FeildOfWork", FeildOfWork));
                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@isNational", national));
                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@email", email));
                ExaminerRegisterProc.Parameters.Add(new SqlParameter("@password", password));

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
                    ExaminerRegisterProc.ExecuteNonQuery();
                    getLastIdProc.ExecuteNonQuery();

                    int lastId = Int16.Parse(id.Value.ToString());
                    String messageCorrect = "You Registered Successfully, Your ID is: " + lastId;
                    examinerRegisterErrorMessagesLabel.Visible = false;
                    examinerRegisterSuccessMessagesLabel.Visible = true;
                    examinerRegisterSuccessMessagesLabel.InnerText = messageCorrect;
                    examinerRegisterButton.Enabled = false;
                }
                else
                {
                    examinerRegisterErrorMessagesLabel.Visible = true;
                    examinerRegisterSuccessMessagesLabel.Visible = false;
                    examinerRegisterErrorMessagesLabel.InnerText = "There is already a user registered with this email";

                }

                conn.Close();



            }
        }

        protected void examinerBackTologinButtonClick(object sender, EventArgs e)
        {
            examinerRegisterButton.Enabled = true;
            Response.Redirect("Login.aspx");
        }

    }
}