using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmbizoMilestone3
{
    public partial class SupervisorHomePage : System.Web.UI.Page
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

        }

        protected void ViewStudentssButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewStudentssButtonPage.aspx");
        }

        protected void ViewStudentPublicationsButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewStudentPublicationsButtonPage.aspx");
        }


 

        protected void AddThesisDefenseButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AddThesisDefenseButtonPage.aspx");
        }


        protected void AddExaminerDefenseButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AddExaminerDefenseButtonPage.aspx");
        }

        protected void EvaluateProgressReportButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("EvaluateProgressReportButtonPage.aspx");
        }



        protected void CancelThesisButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("CancelThesisButtonPage.aspx");
        }

        protected void LogoutButtonClick(object sender, EventArgs e)
        {
            Session["user"] = null;
            Session["type"] = null;
            Session["gucian"] = null;
            Response.Redirect("Login.aspx");


        }
    }
}