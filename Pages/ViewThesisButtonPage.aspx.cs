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
    public partial class ViewThesisButtonPage : System.Web.UI.Page
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

                SqlCommand AdminViewAllThesesProc = new SqlCommand("AdminViewAllTheses", conn);
                AdminViewAllThesesProc.CommandType = CommandType.StoredProcedure;


                SqlCommand AdminViewOnGoingThesesProc = new SqlCommand("AdminViewOnGoingTheses", conn);
                AdminViewOnGoingThesesProc.CommandType = CommandType.StoredProcedure;


                SqlParameter Count = AdminViewOnGoingThesesProc.Parameters.Add("@thesesCount", SqlDbType.Int);
                Count.Direction = System.Data.ParameterDirection.Output;


                conn.Open();

                AdminViewOnGoingThesesProc.ExecuteNonQuery();

                SqlDataReader rdr = AdminViewAllThesesProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {


                    Int32 ThesisserialNumber = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));


                    String Thesisfield = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("field")))
                    {
                        Thesisfield = rdr.GetString(rdr.GetOrdinal("field")).ToString();
                    }


                    String Thesistype = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("type")))
                    {
                        Thesistype = rdr.GetString(rdr.GetOrdinal("type")).ToString();
                    }


                    String Thesititle = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("title")))
                    {
                        Thesititle = rdr.GetString(rdr.GetOrdinal("title")).ToString();
                    }


                    String ThesisstartDate = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("startDate")))
                    {
                        ThesisstartDate = rdr.GetDateTime(rdr.GetOrdinal("startDate")).ToString();
                    }

                    String ThesisendDate = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("endDate")))
                    {
                        ThesisendDate = rdr.GetDateTime(rdr.GetOrdinal("endDate")).ToString();
                    }

                    String ThesisdefenseDate = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("defenseDate")))
                    {
                        ThesisdefenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate")).ToString();
                    }

                    String Thesisyears = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("years")))
                    {
                        Thesisyears = rdr.GetInt32(rdr.GetOrdinal("years")).ToString();
                    }


                    String Thesisgrade = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("grade")))
                    {
                        Thesisgrade = rdr.GetDecimal(rdr.GetOrdinal("grade")).ToString();
                    }


                    String Thesispayment_id = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("payment_id")))
                    {
                        Thesispayment_id = rdr.GetInt32(rdr.GetOrdinal("payment_id")).ToString();
                    }


                    String ThesisnoOfExtensions = null;
                    if (!rdr.IsDBNull(rdr.GetOrdinal("noOfExtensions")))
                    {
                        ThesisnoOfExtensions = rdr.GetInt32(rdr.GetOrdinal("noOfExtensions")).ToString();
                    }





                    if (Thesisfield == null)
                    {
                        Thesisfield = "-";
                    }
                    if (Thesistype == null)
                    {
                        Thesistype = "-";
                    }
                    if (Thesititle == null)
                    {
                        Thesititle = "-";
                    }

                    if (ThesisstartDate == null)
                    {
                        ThesisstartDate = "-";
                    }
                    if (ThesisendDate == null)
                    {
                        ThesisendDate = "-";
                    }
                    if (ThesisdefenseDate == null)
                    {
                        ThesisdefenseDate = "-";
                    }

                    if (Thesisyears == null)
                    {
                        Thesisyears = "-";
                    }
                    if (Thesisgrade == null)
                    {
                        Thesisgrade = "-";
                    }
                    if (Thesispayment_id == null)
                    {
                        Thesispayment_id = "-";
                    }
                    if (ThesisnoOfExtensions == null)
                    {
                        ThesisnoOfExtensions = "-";
                    }


                    HtmlTableRow tRow = new HtmlTableRow();

                    HtmlTableCell tc = new HtmlTableCell();
                    tc.InnerText = "" + ThesisserialNumber;
                    HtmlTableCell tc2 = new HtmlTableCell();
                    tc2.InnerText = Thesisfield;
                    HtmlTableCell tc3 = new HtmlTableCell();
                    tc3.InnerText = Thesistype;
                    HtmlTableCell tc4 = new HtmlTableCell();
                    tc4.InnerText = Thesititle;


                    HtmlTableCell tc5 = new HtmlTableCell();
                    tc5.InnerText = ThesisstartDate;
                    HtmlTableCell tc6 = new HtmlTableCell();
                    tc6.InnerText = ThesisendDate;
                    HtmlTableCell tc7 = new HtmlTableCell();
                    tc7.InnerText = ThesisdefenseDate;
                    HtmlTableCell tc8 = new HtmlTableCell();
                    tc8.InnerText = Thesisyears.ToString();
                    HtmlTableCell tc9 = new HtmlTableCell();
                    tc9.InnerText = "" + Thesisgrade;
                    HtmlTableCell tc10 = new HtmlTableCell();
                    tc10.InnerText = "" + Thesispayment_id;
                    HtmlTableCell tc11 = new HtmlTableCell();
                    tc11.InnerText = "" + ThesisnoOfExtensions;

                    tRow.Controls.Add(tc);
                    tRow.Controls.Add(tc2);
                    tRow.Controls.Add(tc3);
                    tRow.Controls.Add(tc4);
                    tRow.Controls.Add(tc5);
                    tRow.Controls.Add(tc6);
                    tRow.Controls.Add(tc7);
                    tRow.Controls.Add(tc8);
                    tRow.Controls.Add(tc9);
                    tRow.Controls.Add(tc10);
                    tRow.Controls.Add(tc11);


                    Table1.Rows.Add(tRow);
                    records = true;

                }

                conn.Close();
                if (!records)
                {
                    Table1.Visible = false;
                    Label1.Visible = false;
                    Label2.Visible = true;
                    Label3.Visible = false;
                }

                Label3.Text = "The number of theses that are currently going is : " + Count.Value;
            }
        }

        protected void BackToAdminHomePageViewThesisButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AdminHomePage.aspx");
        }
    }
}
