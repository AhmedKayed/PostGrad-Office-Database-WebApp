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
    public partial class CancelThesisButtonPage : System.Web.UI.Page
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

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");

        }

        protected void CancelThesis_Click(object sender, EventArgs e)
        {

            Label6.Text = "";
            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            try
            {
                int serialNo = Int16.Parse(SupervisorCancelThesisNo.Text);


                SqlCommand checkfrothesisProc = new SqlCommand("checkfrothesis", conn);
                checkfrothesisProc.CommandType = CommandType.StoredProcedure;
                checkfrothesisProc.Parameters.Add(new SqlParameter("@thesisNo", serialNo));

                SqlParameter success = checkfrothesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;




                SqlCommand CancelThesisProc = new SqlCommand("CancelThesis", conn);
                CancelThesisProc.CommandType = CommandType.StoredProcedure;


                CancelThesisProc.Parameters.Add(new SqlParameter("@thesisSerialNo", serialNo));


                SqlCommand checkifPRzeroProc = new SqlCommand("checkifPRzero", conn);
                checkifPRzeroProc.CommandType = CommandType.StoredProcedure;


                checkifPRzeroProc.Parameters.Add(new SqlParameter("@thesisSerialNo", serialNo));

                SqlParameter success2 = checkifPRzeroProc.Parameters.Add("@success", SqlDbType.Bit);
                success2.Direction = System.Data.ParameterDirection.Output;




                conn.Open();
                checkfrothesisProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {
                    checkifPRzeroProc.ExecuteNonQuery();
                    if (success2.Value.ToString().Equals("True"))
                    {
                        CancelThesisProc.ExecuteNonQuery();
                        Label6.Text = "<span style= 'color:green'> Successful </span>";


                    }
                    else
                    {
                        Label6.Text = "<span style= 'color:red'> The Last Progress Report Evaluation for this thesis is not zero </span>";

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
                Label6.Text = "<span style= 'color:red'>  Thesis Number Field Must Be Not Empty </span>";

            }

        }
    }
}