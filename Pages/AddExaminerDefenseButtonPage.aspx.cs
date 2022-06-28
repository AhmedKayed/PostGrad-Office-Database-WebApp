using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AmbizoMilestone3
{
    public partial class AddExaminerDefenseButtonPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || !Session["type"].Equals("Supervisor"))
            {
                Session["user"] = null;
                Session["type"] = null;
                Session["gucian"] = null;
                Response.Redirect("Login.aspx");
            }

            Label6.Text = "";
        }

        protected void SupervisorAddExaminertoDefense_Click(object sender, EventArgs e)
        {


            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            Label6.Text = "";


            try
            {
                int serialNo = Int16.Parse(SupervisorAddExaminerThesisNo.Text);
                int examinerid = Int16.Parse(SupervisorAddExaminerID.Text);
                DateTime date = SupervisorAddExaminerDate.SelectedDate;


                SqlCommand checkExaminerIdProc = new SqlCommand("checkExaminerId", conn);
                checkExaminerIdProc.CommandType = CommandType.StoredProcedure;
                checkExaminerIdProc.Parameters.Add(new SqlParameter("@id", examinerid));

                SqlParameter success = checkExaminerIdProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                


                SqlCommand checkdateAndThesisProc = new SqlCommand("checkdateAndThesis", conn);
                checkdateAndThesisProc.CommandType = CommandType.StoredProcedure;
                checkdateAndThesisProc.Parameters.Add(new SqlParameter("@serialNo", serialNo));
                checkdateAndThesisProc.Parameters.Add(new SqlParameter("@date", date));

                SqlParameter success2 = checkdateAndThesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success2.Direction = System.Data.ParameterDirection.Output;



                SqlCommand AddExaminer2Proc = new SqlCommand("AddExaminerNew", conn);
                AddExaminer2Proc.CommandType = CommandType.StoredProcedure;

                AddExaminer2Proc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serialNo));
                AddExaminer2Proc.Parameters.Add(new SqlParameter("@DefenseDate", date));
                AddExaminer2Proc.Parameters.Add(new SqlParameter("@id", examinerid));

                

                SqlCommand checkexaminerinthesisProc = new SqlCommand("checkexaminerinthesis", conn);
                checkexaminerinthesisProc.CommandType = CommandType.StoredProcedure;
                checkexaminerinthesisProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serialNo));
                checkexaminerinthesisProc.Parameters.Add(new SqlParameter("@ExminerID", examinerid));

                SqlParameter success3 = checkexaminerinthesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success3.Direction = System.Data.ParameterDirection.Output;


                conn.Open();
                checkExaminerIdProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {

                    checkdateAndThesisProc.ExecuteNonQuery();

                    if (success2.Value.ToString().Equals("False"))
                    {
                        Label6.Text = "<span style= 'color:red'> There is no Defense with this date for this thesis </span>";


                    }
                    else
                    {
                        checkexaminerinthesisProc.ExecuteNonQuery();

                        if (success3.Value.ToString().Equals("True"))
                        {

                            AddExaminer2Proc.ExecuteNonQuery();
                            Label6.Text = "<span style= 'color:green'> Successful </span>";

                        }
                        else
                        {
                            Label6.Text = "<span style= 'color:red'> This Examiner is already in this Defense </span>";

                        }

                    }


                }
                else
                {
                    Label6.Text = "<span style= 'color:red'> There is no Examiner with this ID </span>";

                }
                conn.Close();


            }
            catch (Exception e1)
            {

                Label6.Text = "<span style= 'color:red'> <span style= 'color:red'> Examiner ID and Thesis Serial Number Fields Must Be Not Empty </span> </span>";

            }
        }

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");

        }
    }
}