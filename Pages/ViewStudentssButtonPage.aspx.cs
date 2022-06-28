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
    public partial class ViewStudentssButtonPage : System.Web.UI.Page
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
            else
            {


                Boolean records = false;
                Label2.Visible = false;
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand ViewSupStudentsYearsProc = new SqlCommand("ViewSupStudentsYears3", conn);
                ViewSupStudentsYearsProc.CommandType = CommandType.StoredProcedure;

                int supid = Int16.Parse(Session["user"].ToString());
                ViewSupStudentsYearsProc.Parameters.Add(new SqlParameter("@supervisorID", supid));

                conn.Open();
                SqlDataReader rdr = ViewSupStudentsYearsProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {

                    String StudentId = rdr.GetInt32(rdr.GetOrdinal("id")).ToString();

                    String StudentFirstName = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("firstName")))
                    {
                        StudentFirstName = rdr.GetString(rdr.GetOrdinal("firstName")).ToString();
                    }

                    String StudentLastName = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("lastName")))
                    {
                        StudentLastName = rdr.GetString(rdr.GetOrdinal("lastName")).ToString();
                    }

                    String StudentYears = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("years")))
                    {
                        StudentYears = rdr.GetInt32(rdr.GetOrdinal("years")).ToString();
                    }


                    if (StudentFirstName == null)
                    {
                        StudentFirstName = "-";
                    }
                    if (StudentLastName == null)
                    {
                        StudentLastName = "-";
                    }
                    if (StudentYears == null)
                    {
                        StudentYears = "-";
                    }




                    HtmlTableRow tRow = new HtmlTableRow();

                    HtmlTableCell tb4 = new HtmlTableCell();
                    tb4.InnerText = "" + StudentId;

                    HtmlTableCell tb = new HtmlTableCell();
                    tb.InnerText = "" + StudentFirstName;
                    HtmlTableCell tb2 = new HtmlTableCell();
                    tb2.InnerText = StudentLastName;
                    HtmlTableCell tb3 = new HtmlTableCell();
                    tb3.InnerText = "" + StudentYears;

                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb2);
                    tRow.Controls.Add(tb3);


                    Table1.Rows.Add(tRow);
                    records = true;


                }
                conn.Close();
                if (!records)
                {
                    Table1.Visible = false;
                    Label1.Visible = false;
                    Label2.Visible = true;
                }
            }
        }

        protected void BackToASupervisorHomePageButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");

        }
    }
}