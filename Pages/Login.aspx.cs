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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void login(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                String email = emailid.Text;
                String pass = password.Text;
                if (emailid.Text == "" || password.Text.Replace(" ", "") == "")
                {
                    throw new Exception();
                }
                
                SqlCommand checkemailpassProc = new SqlCommand("checkemailpass", conn);
                checkemailpassProc.CommandType = CommandType.StoredProcedure;

                checkemailpassProc.Parameters.Add(new SqlParameter("@email", email));
                checkemailpassProc.Parameters.Add(new SqlParameter("@password", pass));

                SqlParameter success2 = checkemailpassProc.Parameters.Add("@success", SqlDbType.Bit);
                success2.Direction = System.Data.ParameterDirection.Output;
                SqlParameter userid = checkemailpassProc.Parameters.Add("@id", SqlDbType.Int);
                userid.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkemailpassProc.ExecuteNonQuery();
             
                if (success2.Value.ToString().Equals("True"))
                {
                    Session["user"] = Int32.Parse(userid.Value.ToString());
                    SqlCommand getUserTypeProc = new SqlCommand("getUserType", conn);
                    getUserTypeProc.CommandType = CommandType.StoredProcedure;
                    getUserTypeProc.Parameters.Add(new SqlParameter("@id", Int32.Parse(userid.Value.ToString())));
                    SqlParameter type = getUserTypeProc.Parameters.Add("@type", SqlDbType.VarChar, 10);
                    type.Direction = System.Data.ParameterDirection.Output;

                    getUserTypeProc.ExecuteNonQuery();

                    if (type.Value.ToString().Equals("Gucian") || type.Value.ToString().Equals("NonGucian"))
                    {
                        Session["type"] = "Student";

                        if (type.Value.ToString().Equals("Gucian"))
                        {
                            Session["gucian"] = 1;
                        }
                        else
                        {
                            Session["gucian"] = -1;
                        }
                        Response.Redirect("StudentHomePage.aspx");
                    }
                    else if (type.Value.ToString().Equals("Supervisor"))
                    {
                        Session["type"] = "Supervisor";
                        Response.Redirect("SupervisorHomePage.aspx");
                    }
                    else if (type.Value.ToString().Equals("Examiner"))
                    {
                        Session["type"] = "Examiner";
                        Response.Redirect("ExaminerHomePage.aspx");
                    }
                    else
                    {
                        Session["type"] = "Admin";
                        Response.Redirect("AdminHomePage.aspx");
                    }

                }
                else
                {
                    loginPageErrorMessagesLabel.Visible = true;
                    loginPageErrorMessagesLabel.InnerText = "Wrong Email or Password";
                }
                conn.Close();

            }

            catch (Exception e1)
            {
                loginPageErrorMessagesLabel.Visible = true;
                loginPageErrorMessagesLabel.InnerText = "All Fields Must Be Not Empty";
            }


        }

        protected void studentregClick(object sender, EventArgs e)
        {
            Response.Redirect("StudentRegister.aspx");
        }

        protected void supervisorregClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorRegister.aspx");
        }

        protected void examinerregClick(object sender, EventArgs e)
        {
            Response.Redirect("ExaminerRegister.aspx");
        }
    }
}