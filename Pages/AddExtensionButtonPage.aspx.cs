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
    public partial class AddExtensionButtonPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null || !Session["type"].Equals("Admin"))
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
            Response.Redirect("AdminHomePage.aspx");
        }


        protected void AdminAddExtension_Click(object sender, EventArgs e)
        {

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            try
            {
                int serial = Int16.Parse(AdminAddExtensionThesisNo.Text);


                
                SqlCommand AdminUpdateExtensionProc = new SqlCommand("AdminUpdateExtension", conn);
                AdminUpdateExtensionProc.CommandType = CommandType.StoredProcedure;

                AdminUpdateExtensionProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serial));


                SqlCommand checkfrothesisProc = new SqlCommand("checkfrothesis", conn);
                checkfrothesisProc.CommandType = CommandType.StoredProcedure;
                checkfrothesisProc.Parameters.Add(new SqlParameter("@thesisNo", serial));

                SqlParameter success = checkfrothesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;




                conn.Open();
                checkfrothesisProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {
                    AdminUpdateExtensionProc.ExecuteNonQuery();
                    Label6.Text = "<span style= 'color:green'> Successful  </span>";
                }
                else
                {
                    Label6.Text = "<span style= 'color:red'> Please check that you entered a valid Thesis Serial Number </span>";
                }

                conn.Close();
            }
            catch (Exception e1)
            {
                Label6.Text = "<span style= 'color:red'> Thesis Number Field Must Be Not Empty </span>";
            }

        }
    }
}