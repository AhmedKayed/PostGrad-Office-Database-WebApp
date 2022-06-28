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
    public partial class ViewSupervisorsButtonPage : System.Web.UI.Page
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
            else
            {


                Boolean records = false;
                Label2.Visible = false;
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand AdminListSupProc = new SqlCommand("AdminListSup", conn);
                AdminListSupProc.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader rdr = AdminListSupProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {

                    String SupervisorName = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("name")))
                    {
                        SupervisorName = rdr.GetString(rdr.GetOrdinal("name")).ToString();
                    }

                    Int32 SupervisorId = rdr.GetInt32(rdr.GetOrdinal("id"));


                    String SupervisorEmail = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("email")))
                    {
                        SupervisorEmail = rdr.GetString(rdr.GetOrdinal("email")).ToString();
                    }


                    String SupervisorPassword = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("password")))
                    {
                        SupervisorPassword = rdr.GetString(rdr.GetOrdinal("password")).ToString();
                    }


                    String SupervisorFaculty = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("faculty")))
                    {
                        SupervisorFaculty = rdr.GetString(rdr.GetOrdinal("faculty")).ToString();
                    }

                    if (SupervisorName == null)
                    {
                        SupervisorName = "-";
                    }
                    if (SupervisorFaculty == null)
                    {
                        SupervisorFaculty = "-";
                    }
                    if (SupervisorEmail == null)
                    {
                        SupervisorEmail = "-";
                    }
                    if (SupervisorPassword == null)
                    {
                        SupervisorPassword = "-";
                    }


                    HtmlTableRow tRow = new HtmlTableRow();
                    HtmlTableCell tb = new HtmlTableCell();
                    tb.InnerText = "" + SupervisorId;
                    HtmlTableCell tb2 = new HtmlTableCell();
                    tb2.InnerText = SupervisorName;
                    HtmlTableCell tb3 = new HtmlTableCell();
                    tb3.InnerText = SupervisorFaculty;
                    HtmlTableCell tb4 = new HtmlTableCell();
                    tb4.InnerText = SupervisorEmail;
                    HtmlTableCell tb5 = new HtmlTableCell();
                    tb5.InnerText = SupervisorPassword;

                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb2);
                    tRow.Controls.Add(tb3);
                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb5);

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

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AdminHomePage.aspx");
        }
    }
}