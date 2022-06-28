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
    public partial class ViewStudentPublicationsButtonPage : System.Web.UI.Page
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
            Table1.Visible = false;
            Label6.Visible = false;
            Label2.Visible = false;
            Label7.Text = "";
        }

        protected void BackToASupervisorHomePageButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");
        }

        protected void ViewStudentPublicationGetPublicationButtonClick(object sender, EventArgs e)
        {
            

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            Label7.Text = "";

            try
            {
                int StudentID = Int16.Parse(ViewStudentPublicationStudentId.Text);

                
                SqlCommand ViewAStudentPublicationsProc = new SqlCommand("ViewAStudentPublications", conn);
                ViewAStudentPublicationsProc.CommandType = CommandType.StoredProcedure;

                ViewAStudentPublicationsProc.Parameters.Add(new SqlParameter("@StudentID", StudentID));


                SqlCommand checkfrostudentProc = new SqlCommand("checkfrostudent", conn);
                checkfrostudentProc.CommandType = CommandType.StoredProcedure;

                checkfrostudentProc.Parameters.Add(new SqlParameter("@studentid", StudentID));


                SqlParameter success = checkfrostudentProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;




                conn.Open();

                checkfrostudentProc.ExecuteNonQuery();

                if (success.Value.ToString().Equals("True"))
                {
                    Boolean records = false;
                    ViewAStudentPublicationsProc.ExecuteNonQuery();
                   




                    SqlDataReader rdr = ViewAStudentPublicationsProc.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        Int32 PubID = rdr.GetInt32(rdr.GetOrdinal("id"));
                        String PubTitle = rdr.GetString(rdr.GetOrdinal("title"));
                       
                        
                        String PubDate = null;
                        if (!rdr.IsDBNull(rdr.GetOrdinal("dateOfPublication")))
                        {

                            PubDate = rdr.GetDateTime(rdr.GetOrdinal("dateOfPublication")).ToString();

                        }
                        String PubPlace = null;
                        if (!rdr.IsDBNull(rdr.GetOrdinal("place")))
                        {
                            PubPlace = rdr.GetString(rdr.GetOrdinal("place"));
                        }

                        String PubAccepted = null;
                        if (!rdr.IsDBNull(rdr.GetOrdinal("accepted")))
                        {
                            PubAccepted = rdr.GetBoolean(rdr.GetOrdinal("accepted")).ToString();
                        }


                        String Pubhost = null;
                        if (!rdr.IsDBNull(rdr.GetOrdinal("host")))
                        {
                            Pubhost = rdr.GetString(rdr.GetOrdinal("host"));
                        }


                        if (PubDate == null)
                        {
                            PubDate = "-";
                        }
                        if (PubPlace == null)
                        {
                            PubPlace = "-";
                        }
                        if (PubAccepted == null)
                        {
                            PubAccepted = "-";
                        }
                        if (Pubhost == null)
                        {
                            Pubhost = "-";
                        }



                        HtmlTableRow tRow = new HtmlTableRow();
                        HtmlTableCell tb = new HtmlTableCell();
                        tb.InnerText = "" + PubID;
                        HtmlTableCell tb2 = new HtmlTableCell();
                        tb2.InnerText = PubTitle;
                        HtmlTableCell tb3 = new HtmlTableCell();
                        tb3.InnerText = "" + PubDate.ToString();
                        HtmlTableCell tb4 = new HtmlTableCell();
                        tb4.InnerText = "" + PubPlace.ToString();
                        HtmlTableCell tb5 = new HtmlTableCell();
                        tb5.InnerText = "" + PubAccepted.ToString();
                        HtmlTableCell tb6 = new HtmlTableCell();
                        tb6.InnerText = "" + Pubhost.ToString();




                        tRow.Controls.Add(tb);
                        tRow.Controls.Add(tb2);
                        tRow.Controls.Add(tb3);
                        tRow.Controls.Add(tb4);
                        tRow.Controls.Add(tb5);
                        tRow.Controls.Add(tb6);


                        Table1.Rows.Add(tRow);
                        records = true;

                    }
                    if (records)
                    {
                        Table1.Visible = true;
                        Label6.Visible = true;

                    }
                    else
                    {
                        Label2.Visible = true;
                    }

                }
                else
                {
                   // Response.Write("<center><span style= 'color:red'> There is no Student with this ID </span> </center>");
                    Label7.Text = "<span style= 'color:red'> There is no Student with this ID </span> ";



                }
                conn.Close();
            }
            catch (Exception e1){
               // Response.Write("<center><span style= 'color:red'> ID can only contain numbers </span> </center>");
                Label7.Text = "<span style= 'color:red'>ID Must Be Not Empty </span> ";

            }
        }
    }
}