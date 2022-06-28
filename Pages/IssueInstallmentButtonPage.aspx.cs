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
    public partial class IssueInstallmentButtonPage : System.Web.UI.Page
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

        protected void AdminIssueInstallment_Click(object sender, EventArgs e)
        {



            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


           try
            {
                int paymentid = Int16.Parse(AdminIssueInstallmentPaymentNo.Text);
                DateTime startdate = AdminIssueInstallmentStartDate.SelectedDate;

                
                SqlCommand AdminIssueInstallPaymentProc = new SqlCommand("AdminIssueInstallPayment2", conn);
                AdminIssueInstallPaymentProc.CommandType = CommandType.StoredProcedure;

                AdminIssueInstallPaymentProc.Parameters.Add(new SqlParameter("@paymentID", paymentid));
                AdminIssueInstallPaymentProc.Parameters.Add(new SqlParameter("@InstallStartDate", startdate));
          

                SqlCommand checkfropaymentProc = new SqlCommand("checkfropayment", conn);
                checkfropaymentProc.CommandType = CommandType.StoredProcedure;
                checkfropaymentProc.Parameters.Add(new SqlParameter("@paymentid", paymentid));

                SqlParameter success = checkfropaymentProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                SqlCommand installmentsexistsProc = new SqlCommand("installmentsexists", conn);
                installmentsexistsProc.CommandType = CommandType.StoredProcedure;
                installmentsexistsProc.Parameters.Add(new SqlParameter("@Paymentid", paymentid));

                SqlParameter success2 = installmentsexistsProc.Parameters.Add("@success", SqlDbType.Bit);
                success2.Direction = System.Data.ParameterDirection.Output;




                conn.Open();
                checkfropaymentProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {
                    installmentsexistsProc.ExecuteNonQuery();

                    if (success2.Value.ToString().Equals("False"))
                    {
                        if (startdate.CompareTo(DateTime.Today)>=0)
                        {

                            AdminIssueInstallPaymentProc.ExecuteNonQuery();

                           // Response.Write("<center><span style= 'color:green'> Successful  </span> </center>");
                            Label6.Text = "<span style= 'color:green'> Successful  </span>";

                        }
                        else
                        {
                           // Response.Write("<center><span style= 'color:red'> Please enter an upcoming date  </span> </center>");
                            Label6.Text = "<span style= 'color:red'> Please enter an upcoming date  </span> ";


                        }

                    }
                    else
                    {

                       // Response.Write("<center><span style= 'color:red'> This Payment already have installments </span> </center>");
                        Label6.Text = "<span style= 'color:red'> This Payment already have installments </span>";


                    }
                }
                else
                {
                    // Response.Write("<center><span style= 'color:red'> Please check that you entered a valid Payment Number </span> </center>");
                    Label6.Text = "<span style= 'color:red'> Please check that you entered a valid Payment Number </span>";

                }

                conn.Close();
            }
            catch (Exception e1)
            {
               // Response.Write("<center><span style= 'color:red'> All the feilds can only contain numbers </span> </center>");
                Label6.Text = "<span style= 'color:red'> All Fields Must Be Not Empty </span>";

            }

        }

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AdminHomePage.aspx");
        }
    }
}