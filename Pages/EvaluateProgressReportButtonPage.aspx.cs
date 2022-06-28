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
    public partial class EvaluateProgressReportButtonPage : System.Web.UI.Page
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

        }

        protected void EvaluatePR_Click(object sender, EventArgs e)
        {

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            Label6.Text = "";
            try
            {
                int serialNo = Int16.Parse(SupervisorEvaluatePRThesisNo.Text);
                int reportNo = Int16.Parse(SupervisorEvaluatePRReportNo.Text);
                int Eval = Int16.Parse(SupervisorEvaluatePREvaluation.Text);


                SqlCommand checkfrothesisProc = new SqlCommand("checkfrothesis", conn);
                checkfrothesisProc.CommandType = CommandType.StoredProcedure;
                checkfrothesisProc.Parameters.Add(new SqlParameter("@thesisNo", serialNo));

                SqlParameter success = checkfrothesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;


                SqlCommand checkthesisPRProc = new SqlCommand("checkthesisPR", conn);
                checkthesisPRProc.CommandType = CommandType.StoredProcedure;
                checkthesisPRProc.Parameters.Add(new SqlParameter("@TheisSerialNo", serialNo));
                checkthesisPRProc.Parameters.Add(new SqlParameter("@No", reportNo));

                SqlParameter success2 = checkthesisPRProc.Parameters.Add("@success", SqlDbType.Int);
                success2.Direction = System.Data.ParameterDirection.Output;

                

                SqlCommand EvaluateProgressReportProc = new SqlCommand("EvaluateProgressReport", conn);
                EvaluateProgressReportProc.CommandType = CommandType.StoredProcedure;

                int supid = Int16.Parse(Session["user"].ToString());

                EvaluateProgressReportProc.Parameters.Add(new SqlParameter("@supervisorID", supid));
                EvaluateProgressReportProc.Parameters.Add(new SqlParameter("@thesisSerialNo", serialNo));
                EvaluateProgressReportProc.Parameters.Add(new SqlParameter("@progressReportNo", reportNo));
                EvaluateProgressReportProc.Parameters.Add(new SqlParameter("@evaluation", Eval));

                
                SqlCommand supervisorhasthesisProc = new SqlCommand("supervisorhasthesis", conn);
                supervisorhasthesisProc.CommandType = CommandType.StoredProcedure;
                supervisorhasthesisProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serialNo));
                supervisorhasthesisProc.Parameters.Add(new SqlParameter("@Supid", supid));
                supervisorhasthesisProc.Parameters.Add(new SqlParameter("@ProgressNo", reportNo));


                SqlParameter success3 = supervisorhasthesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success3.Direction = System.Data.ParameterDirection.Output;



                conn.Open();
                checkfrothesisProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {

                    checkthesisPRProc.ExecuteNonQuery();

                    if (success2.Value.ToString().Equals("0"))
                    {
                        Label6.Text = "<span style= 'color:red'> There is no Progress Report with this Report No for this thesis </span>";

                    }
                    else
                    {
                       if(Eval>3 || Eval < 0)
                        {
                            Label6.Text = "<span style= 'color:red'> The Evaluation grade must be between 0 and 3 </span>";

                        }
                        else
                        {
                            supervisorhasthesisProc.ExecuteNonQuery();


                            if (success3.Value.ToString().Equals("True"))
                            {
                                EvaluateProgressReportProc.ExecuteNonQuery();
                                Label6.Text = "<span style= 'color:green'> Successful </span>";

                            }
                            else
                            {
                                Label6.Text = "<span style= 'color:red'> You are not the supervisor of the student of this Progress Report </span>";

                            }
                        }


                    }


                }
                else
                {
                    Label6.Text = "<span style= 'color:red'> There is no Thesis with this serial No. </span>";

                }
                conn.Close();


            }
            catch (Exception e1)
            {
                Label6.Text = "<span style= 'color:red'> All Fields Must Be Not Empty </span>";

            }

        }

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");
        }
    }
}