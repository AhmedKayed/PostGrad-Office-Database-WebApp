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
    public partial class AddThesisDefenseButtonPage : System.Web.UI.Page
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

        protected void AddDefense_Click(object sender, EventArgs e)
        {




            String connStr = WebConfigurationManager.ConnectionStrings["PostGradOffice"].ToString();
            SqlConnection conn = new SqlConnection(connStr);


           // try
            {
                int serialNo = Int16.Parse(SupervisorAddDefenseThesisNo.Text);
                DateTime defensedate = SupervisorAddDefenseDate.SelectedDate;
                String location = SupervisorAddDefenseLocation.Text;
                if (location.Replace(" ", "").Equals(""))
                {
                    Label6.Text = "<span style= 'color:red'> Enter a Defense Location </span>";

                }
                else
                {

                    SqlCommand getSidofThesisProc = new SqlCommand("getSidofThesis2", conn);
                    getSidofThesisProc.CommandType = CommandType.StoredProcedure;

                    getSidofThesisProc.Parameters.Add(new SqlParameter("@TheisSerialNo", serialNo));

                    SqlParameter success = getSidofThesisProc.Parameters.Add("@succes", SqlDbType.Int);
                    success.Direction = System.Data.ParameterDirection.Output;
                    SqlParameter sid = getSidofThesisProc.Parameters.Add("@sid", SqlDbType.Int);
                    sid.Direction = System.Data.ParameterDirection.Output;
                   


                    SqlCommand DefenseExistsProc = new SqlCommand("DefenseExists", conn);
                    DefenseExistsProc.CommandType = CommandType.StoredProcedure;

                    DefenseExistsProc.Parameters.Add(new SqlParameter("@TheisSerialNo", serialNo));

                    SqlParameter success3 = DefenseExistsProc.Parameters.Add("@success", SqlDbType.Int);
                    success3.Direction = System.Data.ParameterDirection.Output;
              



                    SqlCommand AddDefenseGucianProc = new SqlCommand("AddDefenseGucian2", conn);
                    AddDefenseGucianProc.CommandType = CommandType.StoredProcedure;

                    AddDefenseGucianProc.Parameters.Add(new SqlParameter("@ThesisSerialNo", serialNo));
                    AddDefenseGucianProc.Parameters.Add(new SqlParameter("@DefenseDate", defensedate));
                    AddDefenseGucianProc.Parameters.Add(new SqlParameter("@DefenseLocation", location));

                    SqlCommand AddDefenseNonGucianProc = new SqlCommand("AddDefenseNonGucian2", conn);
                    AddDefenseNonGucianProc.CommandType = CommandType.StoredProcedure;

                    AddDefenseNonGucianProc.Parameters.Add(new SqlParameter("@TheisSerialNo", serialNo));
                    AddDefenseNonGucianProc.Parameters.Add(new SqlParameter("@DefenseDate", defensedate));
                    AddDefenseNonGucianProc.Parameters.Add(new SqlParameter("@DefenseLocation", location));







                    conn.Open();
                    getSidofThesisProc.ExecuteNonQuery();


                    SqlCommand checkgradesProc = new SqlCommand("checkgrades", conn);
                    checkgradesProc.CommandType = CommandType.StoredProcedure;


                    checkgradesProc.Parameters.Add(new SqlParameter("@sid", Int32.Parse(sid.Value.ToString())));
                    SqlParameter success2 = checkgradesProc.Parameters.Add("@success", SqlDbType.Bit);
                    success2.Direction = System.Data.ParameterDirection.Output;

                    if (success.Value.ToString().Equals("0"))
                    {
                        Label6.Text = "<span style= 'color:red'> Please enter a valid Thesis Serial Number </span>";

                    }
                    else
                    {
                        if (success.Value.ToString().Equals("3"))
                        {
                            Label6.Text = "<span style= 'color:red'> This Thesis is not linked to a student </span>";

                        }
                        else
                        {



                            if (success.Value.ToString().Equals("1"))
                            {
                                DefenseExistsProc.ExecuteNonQuery();
                                if (success3.Value.ToString().Equals("1"))
                                {
                                    if (defensedate.CompareTo(DateTime.Today) >= 0)
                                    {
                                        AddDefenseGucianProc.ExecuteNonQuery();
                                        Label6.Text = "<span style= 'color:green'>Success </span>";
                                    }
                                    else
                                    {
                                        Label6.Text = "<span style= 'color:red'>Please enter an upcoming date </span>";

                                    }

                                }
                                else
                                {
                                    Label6.Text = "<span style= 'color:red'>This Thesis already has a defense </span>";

                                }


                            }
                            else
                            {

                                checkgradesProc.ExecuteNonQuery();
                                if (success2.Value.ToString().Equals("False"))
                                {
                                    Label6.Text = "<span style= 'color:red'> The student belonging to the thesis has a failed course, Cannot add Defense </span>";

                                }
                                else
                                {
                                    DefenseExistsProc.ExecuteNonQuery();
                                    if (success3.Value.ToString().Equals("1"))
                                    {

                                        AddDefenseGucianProc.ExecuteNonQuery();
                                        Label6.Text = "<span style= 'color:green'>Success </span>";


                                    }
                                    else
                                    {
                                        Label6.Text = "<span style= 'color:red'>This Thesis already has a defense </span>";

                                    }
                                }



                            }




                        }
                    }

                    conn.Close();
                }
            }
           // catch (Exception e1)
            {
              //  Label6.Text = "<span style= 'color:red'> Thesis Number Field Must Be Not Empty </span> ";

            }


        }

        protected void BackToAdminHomePageViewSupervisorButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("SupervisorHomePage.aspx");

        }
    }
}