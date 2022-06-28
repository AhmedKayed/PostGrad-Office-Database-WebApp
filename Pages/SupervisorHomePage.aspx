<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupervisorHomePage.aspx.cs" Inherits="AmbizoMilestone3.SupervisorHomePage" %>

<!DOCTYPE html>
<style>
    
    .button {
        display: inline-block;
        font-size: 24px;
        cursor: pointer;
        text-align: center;
        text-decoration: none;
        outline: none;
        color: black;
        background-color: rgba(135, 206, 250, 0.2);
        border: none;
        border-radius: 15px;
        box-shadow: 0 9px #999;
    }

        .button:hover {
            background-color: rgba(135, 206, 250, 0.4);
        }

        .button:active {
            background-color: rgba(135, 206, 250, 0.6);
            box-shadow: 0 5px #666;
            transform: translateY(4px);
        }

    .button2 {
        border: none;
        color: white;
        padding: 5px 5px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        transition-duration: 0.4s;
        cursor: pointer;
        background-color: white;
        color: black;
        border: 2px solid rgba(135, 206, 250, 0.4);
    }

        .button2:hover {
            background-color: rgba(135, 206, 250, 0.6);
        }
        .button2:disabled{
              opacity: 0.6;
               cursor: not-allowed;
        }

    input[type=number], select {
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    input[type=password], select {
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supervisor Home Page</title>
</head>
<body>
    <form id="form1" runat="server">
               <div>
                    <center>

                        <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Supervisor Home Page</h1> 
            </center>

                        <br />
           <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

               <br />

            <asp:Button ID="ViewStudentssButton" runat="server" OnClick="ViewStudentssButtonClick" Text="View your Students" CssClass="button2" Width="344px"  />

            <br />

            <br />

            <asp:Button ID="ViewStudentPublicationsButton" runat="server" OnClick="ViewStudentPublicationsButtonClick" CssClass="button2" Text="View a Student Publication(s)"   Width="344px"  />

            <br />

            <br />

              <asp:Button ID="AddThesisDefenseButton" runat="server" OnClick="AddThesisDefenseButtonClick" CssClass="button2" Text="Add a Defense to a thesis"  Width="344px" />

            <br />
                        <br />

              <asp:Button ID="AddExaminerDefenseButton" runat="server" OnClick="AddExaminerDefenseButtonClick"  CssClass="button2" Text="Add Examiner to a Defense"   Width="344px" />

            <br />
                        <br />

              <asp:Button ID="EvaluateProgressReportButton" runat="server" OnClick="EvaluateProgressReportButtonClick" CssClass="button2" Text="Evaluate a Progress Report" Width="344px"  />

                        <br />
                        <br />

              <asp:Button ID="CancelThesisButton" runat="server" OnClick="CancelThesisButtonClick" CssClass="button2" Text="Cancel a Thesis"   Width="344px" />

                        <br />
                        <br />

              <asp:Button ID="LogoutButton" runat="server" OnClick="LogoutButtonClick" Text="Logout" CssClass="button2"  Width="344px" />

                        <br />
                        <br />

            <br />
</center>   
        </div>
    </form>
</body>
</html>
