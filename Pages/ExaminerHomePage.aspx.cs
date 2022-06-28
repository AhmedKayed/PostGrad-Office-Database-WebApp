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
    public partial class ExaminerHomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null && Session["type"].Equals("Examiner"))
            {
                AddToGrade.Visible = false;
                AddYourGrade.Visible = false;
                GradeLabel.Visible = false;
                GradeTextBox.Visible = false;
                TitleTable.Visible = false;
                SuperTable.Visible = false;
                StudTable.Visible = false;
                ViewAllLabel.Visible = false;
                AllThesisTable.Visible = false;
                SearchButton.Visible = false;
                AllThesisLabel.Visible = false;
                SearchButton.Visible = false;
                SearchTextBox.Visible = false;

                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                SqlCommand viewExaminerInfoProc = new SqlCommand("viewExaminerInfo", conn);
                viewExaminerInfoProc.CommandType = CommandType.StoredProcedure;

                viewExaminerInfoProc.Parameters.Add(new SqlParameter("@id", Session["user"]));

                conn.Open();
                SqlDataReader rdr = viewExaminerInfoProc.ExecuteReader(CommandBehavior.CloseConnection);
                while (rdr.Read())
                {
                    Int32 ExaminerId = rdr.GetInt32(rdr.GetOrdinal("id"));

                    String ExaminerName = "-";
                    try
                    {
                        ExaminerName = rdr.GetString(rdr.GetOrdinal("name"));
                    }
                    catch (Exception e2)
                    {

                    }

                    String ExfieldOfWork = "-";
                    try
                    {
                        ExfieldOfWork = rdr.GetString(rdr.GetOrdinal("fieldOfWork"));
                    }
                    catch (Exception e2)
                    {

                    }

                    HtmlTableCell tb4 = new HtmlTableCell();
                    String ExisNational = "-";
                    try
                    {
                        ExisNational = rdr.GetBoolean(rdr.GetOrdinal("isNational")).ToString();
                        tb4.InnerText = "" + ExisNational;
                    }
                    catch (Exception e2)
                    {
                        tb4.InnerText = "";
                    }


                    HtmlTableRow tRow = new HtmlTableRow();
                    HtmlTableCell tb1 = new HtmlTableCell();
                    tb1.InnerText = "" + ExaminerId;
                    HtmlTableCell tb2 = new HtmlTableCell();
                    tb2.InnerText = ExaminerName;
                    HtmlTableCell tb3 = new HtmlTableCell();
                    tb3.InnerText = ExfieldOfWork;

                    tRow.Controls.Add(tb1);
                    tRow.Controls.Add(tb2);
                    tRow.Controls.Add(tb3);
                    tRow.Controls.Add(tb4);

                    examinerProfileTable.Rows.Add(tRow);
                    Table2.Visible = false;
                }
            }
            else
            {
                Session["user"] = null;
                Session["type"] = null;
                Session["gucian"] = null;
                Response.Redirect("Login.aspx");
            }
            

        }


        protected void SubmitOnClick(object sender, EventArgs e)
        {

            if (NameTextBox.Text.Replace(" ", "") == "" || FieldOfWorkTextBox.Text.Replace(" ", "") == "")
            {
                Response.Write("<span style= 'color:red'>All Fields Must Not Be Empty </span>");
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand editExaminerinfoProc = new SqlCommand("editExaminerinfo", conn);
                editExaminerinfoProc.CommandType = CommandType.StoredProcedure;

                editExaminerinfoProc.Parameters.Add(new SqlParameter("@id", Session["user"]));
                editExaminerinfoProc.Parameters.Add(new SqlParameter("@name", NameTextBox.Text));
                editExaminerinfoProc.Parameters.Add(new SqlParameter("@fieldOfWork", FieldOfWorkTextBox.Text));

                String name = NameTextBox.Text;
                String fieldofwork = FieldOfWorkTextBox.Text;

                conn.Open();
                editExaminerinfoProc.ExecuteNonQuery();
                conn.Close();
                Response.Redirect("ExaminerHomePage.aspx");
            }

        }
        protected void AddCommentOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            AddToGrade.Visible = false;
            AddYourGrade.Visible = false;
            GradeLabel.Visible = false;
            GradeTextBox.Visible = false;
            Table2.Visible = true;
            SerialLabel.Visible = true;
            SerialTextBox.Visible = true;
            CommentLabel.Visible = true;
            CommentTextBox.Visible = true;
            AddToComments.Visible = true;
            examinerTableCommentsLabel.Visible = true;
            AddYourComments.Visible = true;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand getExmainerDefense1Proc = new SqlCommand("getExmainerDefense1", conn);
            getExmainerDefense1Proc.CommandType = CommandType.StoredProcedure;


            getExmainerDefense1Proc.Parameters.Add(new SqlParameter("@examinerId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = getExmainerDefense1Proc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 DeExaminerId = rdr.GetInt32(rdr.GetOrdinal("examinerId"));
                Int32 DeSerialNo = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));
                //Decimal DeGrade = rdr.GetDecimal(rdr.GetOrdinal("grade"));

                DateTime DeDate = new DateTime();
                HtmlTableCell tb1 = new HtmlTableCell();
                try
                {
                    DeDate = rdr.GetDateTime(rdr.GetOrdinal("date"));
                    tb1.InnerText = "" + DeDate;
                }
                catch (Exception e2)
                {

                    tb1.InnerText = "-";

                }


                /* String DeSerialNo = "-";
                  try
                  {
                      DeSerialNo = rdr.GetString(rdr.GetOrdinal("serialNumber"));
                  }
                  catch (Exception e2)
                  {

                  }*/
                String DeComment = "-";
                try
                {
                    DeComment = rdr.GetString(rdr.GetOrdinal("comment"));
                }
                catch (Exception e2)
                {

                }
                String DeLocation = "-";
                try
                {
                    DeLocation = rdr.GetString(rdr.GetOrdinal("location"));
                }
                catch (Exception e2)
                {

                }
                String DeGrade = "-";
                try
                {
                    DeGrade = rdr.GetDecimal(rdr.GetOrdinal("grade")).ToString();
                }
                catch (Exception e2)
                {

                }


                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = "" + DeSerialNo;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = "" + DeExaminerId;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = DeComment;
                HtmlTableCell tb5 = new HtmlTableCell();
                tb5.InnerText = "" + DeGrade;

                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);


                Table2.Rows.Add(tRow);



            }
        }
        protected void EditInfoOnClick(object sender, EventArgs e)
        {

            EditYourInfo.Visible = true;
            NameTextBox.Visible = true;
            FieldOfWorkTextBox.Visible = true;
            SubmitButton.Visible = true;
            FieldOfWorkLabel.Visible = true;
            NameLabel.Visible = true;
            examinerTableLabel.Visible = true;
            examinerProfileTable.Visible = true;
            Table2.Visible = false;
            SerialLabel.Visible = false;
            SerialTextBox.Visible = false;
            CommentLabel.Visible = false;
            CommentTextBox.Visible = false;
            AddToComments.Visible = false;
            examinerTableCommentsLabel.Visible = false;
            AddYourComments.Visible = false;
            GradeTextBox.Visible = false;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;

        }

        protected void AddtoCommentOnClick(object sender, EventArgs e)
        {

            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            GradeTextBox.Visible = false;
            Table2.Visible = true;
            SerialLabel.Visible = true;
            SerialTextBox.Visible = true;
            CommentLabel.Visible = true;
            CommentTextBox.Visible = true;
            AddToComments.Visible = true;
            examinerTableCommentsLabel.Visible = true;
            AddYourComments.Visible = true;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;

            if (CommentTextBox.Text.Replace(" ", "") == "" || SerialTextBox.Text.Replace(" ", "") == "")
            {
                Response.Write("<span style= 'color:red'>All Fields Must Be Not Empty</span>");
                AddCommentOnClick(null, EventArgs.Empty);
            }
            else
            {
                String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                SqlConnection conn = new SqlConnection(connStr);

                SqlCommand AddCommentsGrade1Proc = new SqlCommand("AddCommentsGrade1", conn);
                AddCommentsGrade1Proc.CommandType = CommandType.StoredProcedure;

                AddCommentsGrade1Proc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SerialTextBox.Text.Replace(" ", "")));
                AddCommentsGrade1Proc.Parameters.Add(new SqlParameter("@comments", CommentTextBox.Text));

                conn.Open();

                AddCommentsGrade1Proc.ExecuteNonQuery();
                conn.Close();
                AddCommentOnClick(null, EventArgs.Empty);
            }

        }

        protected void AddGradeOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            Table2.Visible = true;
            SerialLabel.Visible = true;
            SerialTextBox.Visible = true;
            CommentLabel.Visible = false;
            GradeTextBox.Visible = true;
            GradeLabel.Visible = true;
            CommentTextBox.Visible = false;
            AddToComments.Visible = false;
            AddToGrade.Visible = true;
            examinerTableCommentsLabel.Visible = true;
            AddYourComments.Visible = false;
            AddYourGrade.Visible = true;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand getExmainerDefense1Proc = new SqlCommand("getExmainerDefense1", conn);
            getExmainerDefense1Proc.CommandType = CommandType.StoredProcedure;


            getExmainerDefense1Proc.Parameters.Add(new SqlParameter("@examinerId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = getExmainerDefense1Proc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {
                Int32 DeExaminerId = rdr.GetInt32(rdr.GetOrdinal("examinerId"));
                Int32 DeSerialNo = rdr.GetInt32(rdr.GetOrdinal("serialNumber"));
                // Decimal DeGrade = rdr.GetDecimal(rdr.GetOrdinal("grade"));

                DateTime DeDate = new DateTime();
                HtmlTableCell tb1 = new HtmlTableCell();
                try
                {
                    DeDate = rdr.GetDateTime(rdr.GetOrdinal("date"));
                    tb1.InnerText = "" + DeDate;
                }
                catch (Exception e2)
                {

                    tb1.InnerText = "-";

                }

                String DeComment = "-";
                try
                {
                    DeComment = rdr.GetString(rdr.GetOrdinal("comment"));
                }
                catch (Exception e2)
                {

                }
                String DeLocation = "-";
                try
                {
                    DeLocation = rdr.GetString(rdr.GetOrdinal("location"));
                }
                catch (Exception e2)
                {

                }
                String DeGrade = "-";
                try
                {
                    DeGrade = rdr.GetDecimal(rdr.GetOrdinal("grade")).ToString();
                }
                catch (Exception e2)
                {

                }



                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = "" + DeSerialNo;
                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = "" + DeExaminerId;
                HtmlTableCell tb4 = new HtmlTableCell();
                tb4.InnerText = DeComment;
                HtmlTableCell tb5 = new HtmlTableCell();
                tb5.InnerText = "" + DeGrade;

                tRow.Controls.Add(tb1);
                tRow.Controls.Add(tb2);
                tRow.Controls.Add(tb3);
                tRow.Controls.Add(tb4);
                tRow.Controls.Add(tb5);


                Table2.Rows.Add(tRow);

            }
        }
        protected void AddtoGradeOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            Table2.Visible = true;
            SerialLabel.Visible = true;
            SerialTextBox.Visible = true;
            CommentLabel.Visible = false;
            GradeLabel.Visible = true;
            CommentTextBox.Visible = true;
            AddToComments.Visible = false;
            AddToGrade.Visible = true;
            examinerTableCommentsLabel.Visible = true;
            AddYourComments.Visible = false;
            AddYourGrade.Visible = true;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;

            if (GradeTextBox.Text.Replace(" ", "") == "" || SerialTextBox.Text.Replace(" ", "") == "")
            {
                Response.Write("<span style= 'color:red'>All Fields Must Be Not Empty</span>");
                AddGradeOnClick(null, EventArgs.Empty);
            }
            else
            {
                try
                {
                    int testGrade = int.Parse(GradeTextBox.Text);
                    String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                    SqlConnection conn = new SqlConnection(connStr);

                    SqlCommand AddaGradeProc = new SqlCommand("AddaGrade", conn);
                    AddaGradeProc.CommandType = CommandType.StoredProcedure;

                    AddaGradeProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SerialTextBox.Text.Replace(" ", "")));
                    AddaGradeProc.Parameters.Add(new SqlParameter("@grade", GradeTextBox.Text));

                    conn.Open();
                    AddaGradeProc.ExecuteNonQuery();
                    conn.Close();
                    AddGradeOnClick(null, EventArgs.Empty);
                }
                catch
                {
                    Response.Write("<span style= 'color:red'>Please Enter Numbers Only</span>");
                    AddGradeOnClick(null, EventArgs.Empty);
                }

                /* String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
                 SqlConnection conn = new SqlConnection(connStr);

                 SqlCommand AddaGradeProc = new SqlCommand("AddaGrade", conn);
                 AddaGradeProc.CommandType = CommandType.StoredProcedure;

                 AddaGradeProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", SerialTextBox.Text.Replace(" ", "")));
                 AddaGradeProc.Parameters.Add(new SqlParameter("@grade", GradeTextBox.Text));

                 conn.Open();
                 AddaGradeProc.ExecuteNonQuery();
                 conn.Close();
                 AddGradeOnClick(null, EventArgs.Empty);*/
            }

        }

        protected void ViewAllMyOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            Table2.Visible = false;
            SerialLabel.Visible = false;
            SerialTextBox.Visible = false;
            CommentLabel.Visible = false;
            GradeTextBox.Visible = false;
            GradeLabel.Visible = false;
            CommentTextBox.Visible = false;
            AddToComments.Visible = false;
            AddToGrade.Visible = false;
            examinerTableCommentsLabel.Visible = false;
            AddYourComments.Visible = false;
            AddYourGrade.Visible = false;
            AllThesisTable.Visible = false;
            SearchButton.Visible = false;
            TitleTable.Visible = true;
            SuperTable.Visible = true;
            StudTable.Visible = true;
            ViewAllLabel.Visible = true;
            AllThesisLabel.Visible = false;
            SearchButton.Visible = false;
            SearchTextBox.Visible = false;


            List<String> supervisors = new List<String>();

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


            SqlCommand examinerViewStudentThesisSupervisorProc = new SqlCommand("examinerViewStudentThesisSupervisor", conn);
            examinerViewStudentThesisSupervisorProc.CommandType = CommandType.StoredProcedure;

            examinerViewStudentThesisSupervisorProc.Parameters.Add(new SqlParameter("@examinerId", Session["user"]));

            conn.Open();
            SqlDataReader rdr = examinerViewStudentThesisSupervisorProc.ExecuteReader(CommandBehavior.CloseConnection);
            while (rdr.Read())
            {

                String TheTitle = "-";
                try
                {
                    TheTitle = rdr.GetString(rdr.GetOrdinal("title"));
                }
                catch (Exception e2)
                {

                }
                try
                {
                    if (!supervisors.Contains(rdr.GetString(rdr.GetOrdinal("supervisorName"))))
                    {
                        supervisors.Add(rdr.GetString(rdr.GetOrdinal("supervisorName")));
                    }

                }
                catch (Exception e2)
                {

                }
                String TheStud = "-";
                try
                {
                    TheStud = rdr.GetString(rdr.GetOrdinal("studentName"));
                }
                catch (Exception e2)
                {

                }
                HtmlTableRow tRow = new HtmlTableRow();

                HtmlTableRow t3Row = new HtmlTableRow();

                HtmlTableCell tb1 = new HtmlTableCell();
                tb1.InnerText = "" + TheTitle;

                HtmlTableCell tb3 = new HtmlTableCell();
                tb3.InnerText = TheStud;

                tRow.Controls.Add(tb1);

                t3Row.Controls.Add(tb3);

                TitleTable.Rows.Add(tRow);

                StudTable.Rows.Add(t3Row);
            }
            foreach (String sup in supervisors)
            {

                HtmlTableRow t2Row = new HtmlTableRow();
                HtmlTableCell tb2 = new HtmlTableCell();
                tb2.InnerText = sup;
                t2Row.Controls.Add(tb2);
                SuperTable.Rows.Add(t2Row);
            }

        }

        protected void ViewSearchOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            Table2.Visible = false;
            SerialLabel.Visible = false;
            SerialTextBox.Visible = false;
            CommentLabel.Visible = false;
            GradeTextBox.Visible = false;
            GradeLabel.Visible = false;
            CommentTextBox.Visible = false;
            AddToComments.Visible = false;
            AddToGrade.Visible = false;
            examinerTableCommentsLabel.Visible = false;
            AddYourComments.Visible = false;
            AddYourGrade.Visible = false;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = true;
            SearchButton.Visible = true;
            AllThesisLabel.Visible = true;
            SearchButton.Visible = true;
            SearchTextBox.Visible = true;

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand AdminViewAllThesesProc = new SqlCommand("AdminViewAllTheses", conn);
            AdminViewAllThesesProc.CommandType = CommandType.StoredProcedure;


            conn.Open();
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

                AllThesisTable.Rows.Add(tRow);



            }
        }

        protected void SearchOnClick(object sender, EventArgs e)
        {
            EditYourInfo.Visible = false;
            NameTextBox.Visible = false;
            FieldOfWorkTextBox.Visible = false;
            SubmitButton.Visible = false;
            FieldOfWorkLabel.Visible = false;
            NameLabel.Visible = false;
            examinerProfileTable.Visible = false;
            examinerTableLabel.Visible = false;
            Table2.Visible = false;
            SerialLabel.Visible = false;
            SerialTextBox.Visible = false;
            CommentLabel.Visible = false;
            GradeTextBox.Visible = false;
            GradeLabel.Visible = false;
            CommentTextBox.Visible = false;
            AddToComments.Visible = false;
            AddToGrade.Visible = false;
            examinerTableCommentsLabel.Visible = false;
            AddYourComments.Visible = false;
            AddYourGrade.Visible = false;
            TitleTable.Visible = false;
            SuperTable.Visible = false;
            StudTable.Visible = false;
            ViewAllLabel.Visible = false;
            AllThesisTable.Visible = true;
            SearchButton.Visible = true;
            AllThesisLabel.Visible = true;
            SearchButton.Visible = true;
            SearchTextBox.Visible = true;

            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);

            SqlCommand ViewAllThesisThatProc = new SqlCommand("ViewAllThesisThat", conn);
            ViewAllThesisThatProc.CommandType = CommandType.StoredProcedure;

            ViewAllThesisThatProc.Parameters.Add(new SqlParameter("@keyword", SearchTextBox.Text));


            conn.Open();
            SqlDataReader rdr = ViewAllThesisThatProc.ExecuteReader(CommandBehavior.CloseConnection);
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

                AllThesisTable.Rows.Add(tRow);
            }
        }
        protected void LogOutOnClick(object sender, EventArgs e)
        {
            Session["user"] = null;
            Session["type"] = null;
            Session["gucian"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}


