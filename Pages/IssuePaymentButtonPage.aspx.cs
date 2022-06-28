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
    public partial class IssuePaymentButtonPage : System.Web.UI.Page
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



        protected void AdminIssuePayment_Click(object sender, EventArgs e)
        {

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


           try
            {
                int serial = Int16.Parse(AdminIssuePaymentThesisNo.Text);
                int amount = Int16.Parse(AdminIssuePaymentPaymentAmount.Text);
                int installments = Int16.Parse(AdminIssuePaymentNoOfInstallments.Text);
                int fund = Int16.Parse(AdminIssuePaymentFundPercentage.Text);


                SqlCommand AdminIssueThesisPaymentProc = new SqlCommand("AdminIssueThesisPayment", conn);
                AdminIssueThesisPaymentProc.CommandType = CommandType.StoredProcedure;

                AdminIssueThesisPaymentProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serial));
                AdminIssueThesisPaymentProc.Parameters.Add(new SqlParameter("@amount", amount));
                AdminIssueThesisPaymentProc.Parameters.Add(new SqlParameter("@noOfInstallments", installments));
                AdminIssueThesisPaymentProc.Parameters.Add(new SqlParameter("@fundPercentage", fund));

                SqlCommand checkfrothesisProc = new SqlCommand("checkfrothesis", conn);
                checkfrothesisProc.CommandType = CommandType.StoredProcedure;
                checkfrothesisProc.Parameters.Add(new SqlParameter("@thesisNo", serial));

                SqlParameter success = checkfrothesisProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                SqlCommand thesishaspaymentProc = new SqlCommand("thesishaspayment", conn);
                thesishaspaymentProc.CommandType = CommandType.StoredProcedure;
                thesishaspaymentProc.Parameters.Add(new SqlParameter("@thesisNo", serial));

                SqlParameter success2 = thesishaspaymentProc.Parameters.Add("@success", SqlDbType.Bit);
                success2.Direction = System.Data.ParameterDirection.Output;




                conn.Open();
                checkfrothesisProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {
                    checkfrothesisProc.ExecuteNonQuery();

                    thesishaspaymentProc.ExecuteNonQuery();
                    if (success2.Value.ToString().Equals("True"))
                    {
                        if (fund >= 100 || fund < 0)
                        {
                            Label6.Text = "<span style= 'color:red'>Please enter a fund percentage between 0 and 99  </span> ";

                        }
                        else
                        {

                            if (amount <= 0 )
                            {
                                Label6.Text = "<span style= 'color:red'>Please enter a valid payment amount  </span> ";
                            }
                            else
                            {
                                if (installments <= 0)
                                {
                                    Label6.Text = "<span style= 'color:red'>Please enter a valid Number of Installments </span> ";

                                }
                                else
                                {



                                    AdminIssueThesisPaymentProc.ExecuteNonQuery();
                                    Label6.Text = "<span style= 'color:green'>Successful  </span> ";
                                }
                            }
                        }
                    }
                    else
                    {
                        Label6.Text = "<span style= 'color:red'> This Thesis already has a payment </span> ";

                    }

                }
                else
                {
                    Label6.Text = "<span style= 'color:red'> Please check that you entered a valid Thesis Serial Number </span> ";

                }

                conn.Close();
            }
            catch (Exception e1)
            {
                Label6.Text = "<span style= 'color:red'> All Fields Must Only Contain Numbers and Not Empty </span> ";

            }


        }
    }
}