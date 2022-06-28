using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AmbizoMilestone3
{
    public partial class AdminHomePage : System.Web.UI.Page
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

        }

        protected void ViewSupervisorsButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewSupervisorsButtonPage.aspx");

        }

        protected void ViewThesisButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("ViewThesisButtonPage.aspx");

        }

        protected void IssuePaymentButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("IssuePaymentButtonPage.aspx");
        }

        protected void IssueInstallmentButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("IssueInstallmentButtonPage.aspx");
        }

        protected void AddExtensionButtonClick(object sender, EventArgs e)
        {
            Response.Redirect("AddExtensionButtonPage.aspx");
        }

        protected void LogoutClick(object sender, EventArgs e)
        {
            Session["user"] = null;
            Session["type"] = null;
            Session["gucian"] = null;
            Response.Redirect("Login.aspx");

        }
    }
}