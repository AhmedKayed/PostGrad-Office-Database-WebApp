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
    public partial class StudentHomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"]==null || !Session["type"].Equals("Student"))
            {
                Session["user"] = null;
                Session["type"] = null;
                Session["gucian"] = null;
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (Session["gucian"].Equals(1))
                {
                    studentViewMyCourses.Visible = false;
                    studentProfileTableUnderGradIdColumn.Visible = true;
                }
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand viewMyProfileProc = new SqlCommand("viewMyProfile", conn);
                viewMyProfileProc.CommandType = CommandType.StoredProcedure;

                viewMyProfileProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

                conn.Open();
                SqlDataReader rdr = viewMyProfileProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    Int32 StudentId = rdr.GetInt32(rdr.GetOrdinal("id"));

                    String StudentFirstName = "-";
                    try
                    {
                        StudentFirstName = rdr.GetString(rdr.GetOrdinal("firstName"));
                    }
                    catch (Exception e2)
                    {

                    }

                    String StudentLastName = "-";
                    try
                    {
                        StudentLastName = rdr.GetString(rdr.GetOrdinal("lastName"));
                    }
                    catch (Exception e2)
                    {

                    }

                    String type = "-";
                    try
                    {
                        type = rdr.GetString(rdr.GetOrdinal("type"));
                    }
                    catch (Exception e2)
                    {

                    }

                    String StudentFaculty = "-";
                    try
                    {
                        StudentFaculty = rdr.GetString(rdr.GetOrdinal("Faculty"));
                    }
                    catch (Exception e2)
                    {

                    }

                    Decimal StudentGPA = 0;
                    HtmlTableCell tb7 = new HtmlTableCell();
                    try
                    {
                        StudentGPA = rdr.GetDecimal(rdr.GetOrdinal("GPA"));
                        tb7.InnerText = "" + StudentGPA;
                    }
                    catch (Exception e2)
                    {
                        tb7.InnerText = "-";
                    }

                    String StudentAddress = "-";
                    try
                    {
                        StudentAddress = rdr.GetString(rdr.GetOrdinal("address"));
                    }
                    catch (Exception e2)
                    {

                    }

                    String StudentEmail = "-";
                    try
                    {
                        StudentEmail = rdr.GetString(rdr.GetOrdinal("email"));
                    }
                    catch (Exception e2)
                    {

                    }

                    HtmlTableRow tRow = new HtmlTableRow();
                    HtmlTableCell tb = new HtmlTableCell();
                    tb.InnerText = "" + StudentId;
                    HtmlTableCell tb2 = new HtmlTableCell();
                    tb2.InnerText = StudentFirstName;
                    HtmlTableCell tb3 = new HtmlTableCell();
                    tb3.InnerText = StudentLastName;
                    HtmlTableCell tb4 = new HtmlTableCell();
                    tb4.InnerText = type;
                    HtmlTableCell tb5 = new HtmlTableCell();
                    tb5.InnerText = StudentFaculty;
                    HtmlTableCell tb6 = new HtmlTableCell();
                    tb6.InnerText = StudentAddress;
                    HtmlTableCell tb8 = new HtmlTableCell();
                    tb8.InnerText = StudentEmail;

                    tRow.Controls.Add(tb);
                    tRow.Controls.Add(tb2);
                    tRow.Controls.Add(tb3);
                    tRow.Controls.Add(tb4);
                    tRow.Controls.Add(tb5);
                    tRow.Controls.Add(tb6);
                    tRow.Controls.Add(tb7);
                    tRow.Controls.Add(tb8);

                    if (Session["gucian"].Equals(1))
                    {
                        Int32 undergradId = 0;
                        HtmlTableCell tb9 = new HtmlTableCell();
                        try
                        {
                            undergradId = rdr.GetInt32(rdr.GetOrdinal("undergradID"));
                            tb9.InnerText = "" + undergradId;
                        }
                        catch (Exception e2)
                        {

                            tb9.InnerText = "-";

                        }

                        tRow.Controls.Add(tb9);
                    }

                    studentProfileTable.Rows.Add(tRow);
                }

                SqlCommand viewMyMobileNumbersProc = new SqlCommand("viewMyMobileNumbers", conn);
                viewMyMobileNumbersProc.CommandType = CommandType.StoredProcedure;

                viewMyMobileNumbersProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));
                conn.Close();
                conn.Open();
                rdr = viewMyMobileNumbersProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    HtmlTableCell tb = new HtmlTableCell();
                    try
                    {
                        Int32 phone = rdr.GetInt32(rdr.GetOrdinal("phone"));
                        tb.InnerText = "0" + phone;
                    }
                    catch (Exception e2)
                    {
                        tb.InnerText = "-";
                    }

                    HtmlTableRow tRow = new HtmlTableRow();

                    tRow.Controls.Add(tb);

                    studentPhoneNumbersTable.Rows.Add(tRow);
                }
                conn.Close();
            }

        }

        protected void studentViewThesesClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = true;
            studentProfileTable.Visible = false;
            studentCoursesTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentViewTheses.Enabled = false;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;
            studentTableLabel.InnerText = "Theses Information";

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand viewAllMyThesisProc = new SqlCommand("viewAllMyThesis", conn);
            viewAllMyThesisProc.CommandType = CommandType.StoredProcedure;
            viewAllMyThesisProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = viewAllMyThesisProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {

                Int32 thesisSerial = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));

                String thesisType = "-";
                try
                {
                    thesisType = rdr.GetString(rdr.GetOrdinal("type"));
                }
                catch (Exception e2)
                {

                }

                String thesisTitle = "-";
                try
                {
                    thesisTitle = rdr.GetString(rdr.GetOrdinal("title"));
                }
                catch (Exception e2)
                {

                }

                String field = "-";
                try
                {
                    field = rdr.GetString(rdr.GetOrdinal("field"));
                }
                catch (Exception e5)
                {

                }

                DateTime startDate = new DateTime();
                HtmlTableCell tb5 = new HtmlTableCell();
                try
                {
                    startDate = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                    tb5.InnerText = "" + startDate;
                }
                catch (Exception e2)
                {

                    tb5.InnerText = "-";

                }

                DateTime endDate = new DateTime();
                HtmlTableCell tb6 = new HtmlTableCell();
                try
                {
                    endDate = rdr.GetDateTime(rdr.GetOrdinal("endDate"));
                    tb6.InnerText = "" + endDate;
                }
                catch (Exception e2)
                {

                    tb6.InnerText = "-";

                }

                DateTime defenseDate = new DateTime();
                HtmlTableCell tb7 = new HtmlTableCell();
                try
                {
                    defenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                    tb7.InnerText = "" + defenseDate;
                }
                catch (Exception e6)
                {
                    tb7.InnerText = "-";
                }

                Int32 years = 0;
                HtmlTableCell tb8 = new HtmlTableCell();
                try
                {
                    years = rdr.GetInt32(rdr.GetOrdinal("years"));
                    tb8.InnerText = "" + years;
                }
                catch (Exception e2)
                {
                    tb8.InnerText = "-";
                }

                Decimal grade = 0;
                HtmlTableCell tb9 = new HtmlTableCell();
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                    tb9.InnerText = "" + grade;
                }
                catch (Exception e7)
                {
                    tb9.InnerText = "-";
                }

                Int32 paymentId = 0;
                HtmlTableCell tb10 = new HtmlTableCell();
                try
                {
                    paymentId = rdr.GetInt32(rdr.GetOrdinal("payment_id"));
                    tb10.InnerText = "" + paymentId;
                }
                catch (Exception e7)
                {
                    tb10.InnerText = "-";
                }

                Int32 noOfExtensions = 0;
                HtmlTableCell tb11 = new HtmlTableCell();
                try
                {
                    noOfExtensions = rdr.GetInt32(rdr.GetOrdinal("noOfExtensions"));
                    tb11.InnerText = "" + noOfExtensions;
                }
                catch (Exception e7)
                {
                    tb11.InnerText = "-";
                }

                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + thesisSerial;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = field;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = thesisType;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = thesisTitle;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                tRow.Controls.Add(tb7);
                tRow.Controls.Add(tb8);
                tRow.Controls.Add(tb9);
                tRow.Controls.Add(tb10);
                tRow.Controls.Add(tb11);

                studentThesesTable.Rows.Add(tRow);
            }
            conn.Close();

        }
        protected void studentViewProfileClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = false;
            studentCoursesTable.Visible = false;
            studentProfileTable.Visible = true;
            studentAddMobileNumber.Visible = true;
            studentPhoneNumbersTable.Visible = true;
            studentProfileTableBreak.Visible = true;
            studentPhoneNumbersTableBreak.Visible = true;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = false;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;
            studentTableLabel.InnerText = "Profile Information";
        }
        protected void studentAddPublicationClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = false;
            studentProfileTable.Visible = false;
            studentCoursesTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = false;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = true;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;
            studentTableLabel.InnerText = "Add a Publication";

        }

        protected void studentAddPublicationAddButtonClick(object sender, EventArgs e)
        {
            if (studentAddPublicationTitle.Text.Replace(" ", "") == "" || studentAddPublicationHost.Text.Replace(" ", "") == "" || studentAddPublicationPlace.Text.Replace(" ", "") == "")
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "All fields must be not empty";
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand addPublicationProc = new SqlCommand("addPublication", conn);
                addPublicationProc.CommandType = CommandType.StoredProcedure;

                String title = studentAddPublicationTitle.Text;
                addPublicationProc.Parameters.Add(new SqlParameter("@title", title));
                DateTime date = studentAddPublicationDate.SelectedDate;
                addPublicationProc.Parameters.Add(new SqlParameter("@pubDate", date));
                String host = studentAddPublicationHost.Text;
                addPublicationProc.Parameters.Add(new SqlParameter("@host", host));
                String place = studentAddPublicationPlace.Text;
                addPublicationProc.Parameters.Add(new SqlParameter("@place", place));
                int accepted = 0;
                if (studentAddPublicationAcceptedCheckBox.Checked)
                {
                    accepted = 1;
                }
                addPublicationProc.Parameters.Add(new SqlParameter("@accepted", accepted));

                conn.Open();
                addPublicationProc.ExecuteNonQuery();
                conn.Close();

                studentHomePageErrorMessagesLabel.Visible = false;
                studentHomePageSuccessMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.InnerText = "Publication Added Successfully";
            }
        }

        protected void studentLinkPublicationClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = true;
            studentProfileTable.Visible = false;
            studentCoursesTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = false;
            studentThesesTableBreak.Visible = true;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = true;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            if (sender.Equals("Error"))
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
            }
            else if (sender.Equals("Success"))
            {
                studentHomePageErrorMessagesLabel.Visible = false;
                studentHomePageSuccessMessagesLabel.Visible = true;
            }
            else
            {
                studentHomePageErrorMessagesLabel.Visible = false;
                studentHomePageSuccessMessagesLabel.Visible = false;
            }
            studentTableLabel.InnerText = "Ongoing Theses Information";

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand viewAllMyOngoingThesisProc = new SqlCommand("viewAllMyOngoingThesis", conn);
            viewAllMyOngoingThesisProc.CommandType = CommandType.StoredProcedure;
            viewAllMyOngoingThesisProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = viewAllMyOngoingThesisProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 thesisSerial = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));

                String thesisType = "-";
                try
                {
                    thesisType = rdr.GetString(rdr.GetOrdinal("type"));
                }
                catch (Exception e2)
                {

                }

                String thesisTitle = "-";
                try
                {
                    thesisTitle = rdr.GetString(rdr.GetOrdinal("title"));
                }
                catch (Exception e2)
                {

                }

                String field = "-";
                try
                {
                    field = rdr.GetString(rdr.GetOrdinal("field"));
                }
                catch (Exception e5)
                {

                }

                DateTime startDate = new DateTime();
                HtmlTableCell tb5 = new HtmlTableCell();
                try
                {
                    startDate = rdr.GetDateTime(rdr.GetOrdinal("startDate"));
                    tb5.InnerText = "" + startDate;
                }
                catch (Exception e2)
                {

                    tb5.InnerText = "-";

                }

                DateTime endDate = new DateTime();
                HtmlTableCell tb6 = new HtmlTableCell();
                try
                {
                    endDate = rdr.GetDateTime(rdr.GetOrdinal("endDate"));
                    tb6.InnerText = "" + endDate;
                }
                catch (Exception e2)
                {

                    tb6.InnerText = "-";

                }

                DateTime defenseDate = new DateTime();
                HtmlTableCell tb7 = new HtmlTableCell();
                try
                {
                    defenseDate = rdr.GetDateTime(rdr.GetOrdinal("defenseDate"));
                    tb7.InnerText = "" + defenseDate;
                }
                catch (Exception e6)
                {
                    tb7.InnerText = "-";
                }

                Int32 years = 0;
                HtmlTableCell tb8 = new HtmlTableCell();
                try
                {
                    years = rdr.GetInt32(rdr.GetOrdinal("years"));
                    tb8.InnerText = "" + years;
                }
                catch (Exception e2)
                {
                    tb8.InnerText = "-";
                }

                Decimal grade = 0;
                HtmlTableCell tb9 = new HtmlTableCell();
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                    tb9.InnerText = "" + grade;
                }
                catch (Exception e7)
                {
                    tb9.InnerText = "-";
                }

                Int32 paymentId = 0;
                HtmlTableCell tb10 = new HtmlTableCell();
                try
                {
                    paymentId = rdr.GetInt32(rdr.GetOrdinal("payment_id"));
                    tb10.InnerText = "" + paymentId;
                }
                catch (Exception e7)
                {
                    tb10.InnerText = "-";
                }

                Int32 noOfExtensions = 0;
                HtmlTableCell tb11 = new HtmlTableCell();
                try
                {
                    noOfExtensions = rdr.GetInt32(rdr.GetOrdinal("noOfExtensions"));
                    tb11.InnerText = "" + noOfExtensions;
                }
                catch (Exception e7)
                {
                    tb11.InnerText = "-";
                }

                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + thesisSerial;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = field;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = thesisType;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = thesisTitle;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                tRow.Controls.Add(tb7);
                tRow.Controls.Add(tb8);
                tRow.Controls.Add(tb9);
                tRow.Controls.Add(tb10);
                tRow.Controls.Add(tb11);

                studentThesesTable.Rows.Add(tRow);
            }

            SqlCommand viewAllPublicationsProc = new SqlCommand("viewAllPublications", conn);
            viewAllPublicationsProc.CommandType = CommandType.StoredProcedure;

            conn.Close();
            conn.Open();
            rdr = viewAllPublicationsProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 id = rdr.GetInt32(rdr.GetOrdinal("id"));

                String title = "-";
                try
                {
                    title = rdr.GetString(rdr.GetOrdinal("title"));
                }
                catch (Exception e2)
                {

                }

                DateTime dateOfPublication = new DateTime();
                HtmlTableCell tb3 = new HtmlTableCell();
                try
                {
                    dateOfPublication = rdr.GetDateTime(rdr.GetOrdinal("dateOfPublication"));
                    tb3.InnerText = "" + dateOfPublication;
                }
                catch (Exception e5)
                {
                    tb3.InnerText = "-";
                }

                String place = "-";
                try
                {
                    place = rdr.GetString(rdr.GetOrdinal("place"));
                }
                catch (Exception e6)
                {

                }

                String host = "-";
                try
                {
                    host = rdr.GetString(rdr.GetOrdinal("host"));
                }
                catch (Exception e7)
                {

                }

                String accepted = "";
                HtmlTableCell tb5 = new HtmlTableCell();
                try
                {
                    accepted = rdr.GetBoolean(rdr.GetOrdinal("accepted")).ToString();
                    tb5.InnerText = "" + accepted;
                }
                catch (Exception e7)
                {
                    tb5.InnerText = "-";
                }


                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + id;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = title;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = place;
                HtmlTableCell tb6 = new HtmlTableCell();
                tb6.InnerText = host;


                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);

                studentLinkPublicationTable.Rows.Add(tRow);
            }

            SqlCommand ViewMyLinkedPublicationsProc = new SqlCommand("ViewMyLinkedPublications", conn);
            ViewMyLinkedPublicationsProc.CommandType = CommandType.StoredProcedure;
            ViewMyLinkedPublicationsProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Close();
            conn.Open();
            rdr = ViewMyLinkedPublicationsProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 thesisSerial = rdr.GetInt32(rdr.GetOrdinal("serialNO"));
                Int32 pubSerial = rdr.GetInt32(rdr.GetOrdinal("pubid"));

                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + thesisSerial;
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = "" + pubSerial;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);


                studentLinkPublicationTableLinked.Rows.Add(tRow);
            }
            conn.Close();

        }

        protected void studentLinkPublicationLinkButtonClick(object sender, EventArgs e)
        {
            if (studentLinkPublicationID.Text.Replace(" ", "") == "" || studentLinkPublicationThesisID.Text.Replace(" ", "") == "")
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "All fields must be not empty";
                studentLinkPublicationClick("Error", EventArgs.Empty);
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                int studentLinkPublicationThesisIDint = int.Parse(studentLinkPublicationThesisID.Text.Replace(" ", ""));

                SqlCommand checkOngoingThesisBelongsProc = new SqlCommand("checkOngoingThesisBelongs", conn);
                checkOngoingThesisBelongsProc.CommandType = CommandType.StoredProcedure;

                checkOngoingThesisBelongsProc.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                checkOngoingThesisBelongsProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentLinkPublicationThesisIDint));

                SqlParameter success = checkOngoingThesisBelongsProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkOngoingThesisBelongsProc.ExecuteNonQuery();
                conn.Close();
                if (!success.Value.ToString().Equals("True"))
                {
                    studentHomePageErrorMessagesLabel.Visible = true;
                    studentHomePageSuccessMessagesLabel.Visible = false;
                    studentHomePageErrorMessagesLabel.InnerText = "Invalid Thesis Serial Number";
                    studentLinkPublicationClick("Error", EventArgs.Empty);
                }
                else
                {

                    SqlCommand linkPubThesisProc = new SqlCommand("linkPubThesis", conn);
                    linkPubThesisProc.CommandType = CommandType.StoredProcedure;

                    int studentLinkPublicationIDint = int.Parse(studentLinkPublicationID.Text.Replace(" ", ""));
                    linkPubThesisProc.Parameters.Add(new SqlParameter("@PubID", studentLinkPublicationIDint));
                    linkPubThesisProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentLinkPublicationThesisIDint));

                    conn.Open();
                    try
                    {
                        linkPubThesisProc.ExecuteNonQuery();
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = false;
                        studentHomePageSuccessMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.InnerText = "Publication Linked Successfully";
                        studentLinkPublicationClick("Success", EventArgs.Empty);
                    }
                    catch (Exception e3)
                    {
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.Visible = false;
                        studentHomePageErrorMessagesLabel.InnerText = "Invalid Publication ID or Duplicate Entry";
                        studentLinkPublicationClick("Error", EventArgs.Empty);
                    }

                }

            }
        }

        protected void studentAddProgressReportClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = false;
            studentProfileTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentCoursesTable.Visible = false;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = true;
            studentAddProgressReport.Enabled = false;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;
            studentTableLabel.InnerText = "Add a Progress Report";
        }
        protected void studentAddProgressReportAddButtonClick(object sender, EventArgs e)
        {
            if (studentAddProgressReportNoTextBox.Text.Replace(" ", "") == "" || studentAddProgressReportThesisID.Text.Replace(" ", "") == "")
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "All Fields Must Be Not Empty";
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                int studentAddProgressReportThesis = int.Parse(studentAddProgressReportThesisID.Text.Replace(" ", ""));
                int studentAddProgressReportNo = int.Parse(studentAddProgressReportNoTextBox.Text.Replace(" ", ""));

                SqlCommand checkThesisBelongsProc = new SqlCommand("checkThesisBelongs", conn);
                checkThesisBelongsProc.CommandType = CommandType.StoredProcedure;

                checkThesisBelongsProc.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                checkThesisBelongsProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentAddProgressReportThesis));

                SqlParameter success = checkThesisBelongsProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkThesisBelongsProc.ExecuteNonQuery();
                conn.Close();
                if (!success.Value.ToString().Equals("True"))
                {
                    studentHomePageErrorMessagesLabel.Visible = true;
                    studentHomePageSuccessMessagesLabel.Visible = false;
                    studentHomePageErrorMessagesLabel.InnerText = "Invalid Thesis Serial Number";
                }
                else
                {
                    SqlCommand AddProgressReportProc = new SqlCommand("AddProgressReport", conn);
                    AddProgressReportProc.CommandType = CommandType.StoredProcedure;

                    AddProgressReportProc.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                    AddProgressReportProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentAddProgressReportThesis));
                    AddProgressReportProc.Parameters.Add(new SqlParameter("@progressReportDate", studentAddProgressReportCalendar.SelectedDate));
                    AddProgressReportProc.Parameters.Add(new SqlParameter("@progressReportNo", studentAddProgressReportNo));

                    conn.Open();
                    try
                    {
                        AddProgressReportProc.ExecuteNonQuery();
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = false;
                        studentHomePageSuccessMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.InnerText = "Progress Report Added Successfully";
                    }
                    catch (Exception e2)
                    {
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.Visible = false;
                        studentHomePageErrorMessagesLabel.InnerText = "Progress Report Number Already Exists";
                    }

                }

            }
        }

        protected void studentFillProgressReportClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = false;
            studentProfileTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentCoursesTable.Visible = false;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = true;
            studentFillProgressReport.Enabled = false;
            if (sender.Equals("Error"))
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
            }
            else if (sender.Equals("Success"))
            {
                studentHomePageErrorMessagesLabel.Visible = false;
                studentHomePageSuccessMessagesLabel.Visible = true;
            }
            else
            {
                studentHomePageErrorMessagesLabel.Visible = false;
                studentHomePageSuccessMessagesLabel.Visible = false;
            }
            studentTableLabel.InnerText = "Fill a Progress Report";

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand ViewMyProgressReportsProc = new SqlCommand("ViewMyProgressReports", conn);
            ViewMyProgressReportsProc.CommandType = CommandType.StoredProcedure;
            ViewMyProgressReportsProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = ViewMyProgressReportsProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 progressNo = rdr.GetInt32(rdr.GetOrdinal("no"));

                DateTime progressDate = new DateTime();
                HtmlTableCell tb2 = new HtmlTableCell();
                try
                {
                    progressDate = rdr.GetDateTime(rdr.GetOrdinal("date"));
                    tb2.InnerText = "" + progressDate;
                }
                catch (Exception e2)
                {
                    tb2.InnerText = "-";
                }

                Int32 progressEval = 0;
                HtmlTableCell tb3 = new HtmlTableCell();
                try
                {
                    progressEval = rdr.GetInt32(rdr.GetOrdinal("eval"));
                    tb3.InnerText = "" + progressEval;
                }
                catch (Exception e1)
                {
                    tb3.InnerText = "-";
                }

                Int32 progressState = 0;
                HtmlTableCell tb4 = new HtmlTableCell();
                try
                {
                    progressState = rdr.GetInt32(rdr.GetOrdinal("state"));
                    tb4.InnerText = "" + progressState;
                }
                catch (Exception e1)
                {
                    tb4.InnerText = "-";
                }

                String progressDescription = "-";
                try
                {
                    progressDescription = rdr.GetString(rdr.GetOrdinal("description"));
                }
                catch (Exception e1)
                {

                }

                Int32 progressThesisSerial = 0;
                HtmlTableCell tb5 = new HtmlTableCell();
                try
                {
                    progressThesisSerial = rdr.GetInt32(rdr.GetOrdinal("thesisSerialNumber"));
                    tb5.InnerText = "" + progressThesisSerial;
                }
                catch (Exception e2)
                {
                    tb5.InnerText = "-";
                }

                Int32 progressSupId = 0;
                HtmlTableCell tb6 = new HtmlTableCell();
                try
                {
                    progressSupId = rdr.GetInt32(rdr.GetOrdinal("supid"));
                    tb6.InnerText = "" + progressSupId;
                }
                catch (Exception e1)
                {
                    tb6.InnerText = "-";
                }

                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = "" + progressNo;
                HtmlTableCell tb7 = new HtmlTableCell();
                tb7.InnerText = "" + progressDescription;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);
                tRow.Controls.Add(tb6);
                tRow.Controls.Add(tb7);

                studentFillProgressReportTable.Rows.Add(tRow);
            }
            conn.Close();
        }

        protected void studentFillProgressReportFillButtonClick(object sender, EventArgs e)
        {
            if (studentFillProgressReportNo.Text.Replace(" ", "") == "" || studentFillProgressReportThesis.Text.Replace(" ", "") == "" || studentFillProgressReportState.Text=="")
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "Report No., Thesis No. and State Must be Not Empty";
                studentFillProgressReportClick("Error", EventArgs.Empty);
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                int studentFillProgressReportNoInt = int.Parse(studentFillProgressReportNo.Text.Replace(" ", ""));
                int studentFillProgressReportThesisInt = int.Parse(studentFillProgressReportThesis.Text.Replace(" ", ""));
                int studentFillProgressReportStateInt = int.Parse(studentFillProgressReportState.Text.Replace(" ", ""));
                

                SqlCommand checkThesisReportStudentExistProc = new SqlCommand("checkThesisReportStudentExist", conn);
                checkThesisReportStudentExistProc.CommandType = CommandType.StoredProcedure;

                checkThesisReportStudentExistProc.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                checkThesisReportStudentExistProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentFillProgressReportThesisInt));
                checkThesisReportStudentExistProc.Parameters.Add(new SqlParameter("@no", studentFillProgressReportNoInt));

                SqlParameter success = checkThesisReportStudentExistProc.Parameters.Add("@success", SqlDbType.Bit);
                success.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                checkThesisReportStudentExistProc.ExecuteNonQuery();
                conn.Close();
                if (!success.Value.ToString().Equals("True"))
                {
                    studentHomePageErrorMessagesLabel.Visible = true;
                    studentHomePageSuccessMessagesLabel.Visible = false;
                    studentHomePageErrorMessagesLabel.InnerText = "Invalid Thesis Or Progress Number";
                    studentFillProgressReportClick("Error", EventArgs.Empty);
                }
                else
                {
                    SqlCommand FillProgressReportProc = new SqlCommand("FillProgressReport", conn);
                    FillProgressReportProc.CommandType = CommandType.StoredProcedure;

                    FillProgressReportProc.Parameters.Add(new SqlParameter("@studentID", Session["user"]));
                    FillProgressReportProc.Parameters.Add(new SqlParameter("@thesisSerialNo", studentFillProgressReportThesisInt));
                    FillProgressReportProc.Parameters.Add(new SqlParameter("@progressReportNo", studentFillProgressReportNoInt));
                    FillProgressReportProc.Parameters.Add(new SqlParameter("@state", studentFillProgressReportStateInt));
                    FillProgressReportProc.Parameters.Add(new SqlParameter("@description", studentFillProgressReportDescription.InnerText));

                    conn.Open();
                    try
                    {
                        FillProgressReportProc.ExecuteNonQuery();
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = false;
                        studentHomePageSuccessMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.InnerText = "Report Filled Successfully";
                        studentFillProgressReportClick("Success", EventArgs.Empty);
                    }
                    catch (Exception e3)
                    {
                        conn.Close();
                        studentHomePageErrorMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.Visible = false;
                        studentHomePageErrorMessagesLabel.InnerText = "Invalid Progress Report Number or Duplicate Entry";
                        studentFillProgressReportClick("Error", EventArgs.Empty);
                    }

                }

            }
        }

        protected void studentViewMyCoursesClick(object sender, EventArgs e)
        {
            studentThesesTable.Visible = false;
            studentProfileTable.Visible = false;
            studentAddMobileNumber.Visible = false;
            studentPhoneNumbersTable.Visible = false;
            studentProfileTableBreak.Visible = false;
            studentPhoneNumbersTableBreak.Visible = false;
            studentCoursesTable.Visible = true;
            studentViewTheses.Enabled = true;
            studentViewProfile.Enabled = true;
            studentViewMyCourses.Enabled = false;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentThesesTableBreak.Visible = false;
            studentAddPublicationDiv.Visible = false;
            studentLinkPublicationDiv.Visible = false;
            studentAddProgressReportDiv.Visible = false;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReportDiv.Visible = false;
            studentFillProgressReport.Enabled = true;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;
            studentTableLabel.InnerText = "Courses Information";

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand ViewCoursesInfoProc = new SqlCommand("ViewCoursesInfo", conn);
            ViewCoursesInfoProc.CommandType = CommandType.StoredProcedure;
            ViewCoursesInfoProc.Parameters.Add(new SqlParameter("@studentId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = ViewCoursesInfoProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 creditHours = 0;
                HtmlTableCell tb2 = new HtmlTableCell();
                try
                {
                    creditHours = rdr.GetInt32(rdr.GetOrdinal("creditHours"));
                    tb2.InnerText = "" + creditHours;
                }
                catch (Exception e1)
                {
                    tb2.InnerText = "-";
                }

                String code = "-";
                try
                {
                    code = rdr.GetString(rdr.GetOrdinal("code"));
                }
                catch (Exception e1)
                {

                }

                Decimal grade = 0;
                HtmlTableCell tb3 = new HtmlTableCell();
                try
                {
                    grade = rdr.GetDecimal(rdr.GetOrdinal("grade"));
                    tb3.InnerText = "" + grade;
                }
                catch (Exception e1)
                {
                    tb3.InnerText = "-";
                }

                HtmlTableRow tRow = new HtmlTableRow();
                HtmlTableCell tb = new HtmlTableCell();
                tb.InnerText = code;

                tRow.Controls.Add(tb);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);

                studentCoursesTable.Rows.Add(tRow);
            }
            conn.Close();

        }

        protected void studentAddMobileNumberClick(object sender, EventArgs e)
        {
            studentAddMobileNumber.Visible = false;
            studentAddMobileNumberAdd.Visible = true;
            studentAddMobileNumberCancel.Visible = true;
            studentAddMobileNumberDiv.Visible = true;
            studentAddMobileNumberDivBreak.Visible = true;

            studentViewTheses.Enabled = false;
            studentAddPublication.Enabled = false;
            studentLinkPublication.Enabled = false;
            studentAddProgressReport.Enabled = false;
            studentFillProgressReport.Enabled = false;
            studentViewMyCourses.Enabled = false;
        }

        protected void studentAddMobileNumberCancelClick(object sender, EventArgs e)
        {
            studentAddMobileNumber.Visible = true;
            studentAddMobileNumberAdd.Visible = false;
            studentAddMobileNumberCancel.Visible = false;
            studentAddMobileNumberDiv.Visible = false;
            studentAddMobileNumberDivBreak.Visible = false;
            studentHomePageErrorMessagesLabel.Visible = false;
            studentHomePageSuccessMessagesLabel.Visible = false;

            studentViewTheses.Enabled = true;
            studentAddPublication.Enabled = true;
            studentLinkPublication.Enabled = true;
            studentAddProgressReport.Enabled = true;
            studentFillProgressReport.Enabled = true;
            studentViewMyCourses.Enabled = true;
        }

        protected void studentAddMobileNumberAddClick(object sender, EventArgs e)
        {

            if (studentMobileNumberTextBox.Text.ToString().Replace(" ", "") == "")
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "Mobile Number Field Must Be Not Empty";
            }
            else if (studentMobileNumberTextBox.Text.ToString().Replace(" ", "").Length != 11)
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "Invalid Mobile Number, Must be 11 Numbers Long";
            }
            else if ((studentMobileNumberTextBox.Text.ToString().Replace(" ", ""))[0] != '0')
            {
                studentHomePageErrorMessagesLabel.Visible = true;
                studentHomePageSuccessMessagesLabel.Visible = false;
                studentHomePageErrorMessagesLabel.InnerText = "Invalid Mobile Number, First digit must be a zero";
            }
            else
            {
                try
                {
                    int testCharacters = int.Parse(studentMobileNumberTextBox.Text);
                    String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                    SqlConnection conn = new SqlConnection(connStr);

                    SqlCommand addMobileProc = new SqlCommand("addMobile", conn);
                    addMobileProc.CommandType = CommandType.StoredProcedure;

                    addMobileProc.Parameters.Add(new SqlParameter("@id", Session["user"]));
                    addMobileProc.Parameters.Add(new SqlParameter("@mobile_number", testCharacters));

                    conn.Open();
                    try
                    {
                        addMobileProc.ExecuteNonQuery();
                        studentAddMobileNumber.Visible = true;
                        studentAddMobileNumberAdd.Visible = false;
                        studentAddMobileNumberCancel.Visible = false;
                        studentAddMobileNumberDiv.Visible = false;
                        studentAddMobileNumberDivBreak.Visible = false;

                        studentViewTheses.Enabled = true;
                        studentAddPublication.Enabled = true;
                        studentLinkPublication.Enabled = true;
                        studentAddProgressReport.Enabled = true;
                        studentFillProgressReport.Enabled = true;
                        studentViewMyCourses.Enabled = true;

                        Response.Redirect("StudentHomePage.aspx");

                        studentHomePageErrorMessagesLabel.Visible = false;
                        studentHomePageSuccessMessagesLabel.Visible = true;
                        studentHomePageErrorMessagesLabel.InnerText = "Mobile Number Added Successfully";
                    }
                    catch (Exception e2)
                    {
                        studentHomePageErrorMessagesLabel.Visible = true;
                        studentHomePageSuccessMessagesLabel.Visible = false;
                        studentHomePageErrorMessagesLabel.InnerText = "Mobile Number Already Exists";
                    }
                    conn.Close();
                }
                catch (Exception e2)
                {
                    studentHomePageErrorMessagesLabel.Visible = true;
                    studentHomePageSuccessMessagesLabel.Visible = false;
                    studentHomePageErrorMessagesLabel.InnerText = "Invalid Mobile Number, Can only contain digits";
                }

            }

        }

        protected void studentHomePageLogOutButtonClick(object sender, EventArgs e)
        {
            Session["user"] = null;
            Session["type"] = null;
            Session["gucian"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}