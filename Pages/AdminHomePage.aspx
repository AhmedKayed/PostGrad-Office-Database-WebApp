<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminHomePage.aspx.cs" Inherits="AmbizoMilestone3.AdminHomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Home Page</title>
    <style>
        
    h1 {
        text-align: center;
    }

    h2 {
        text-align: center;
    }

    table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 80%;
        margin-left: auto;
        margin-right: auto;
    }

    td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 8px;
    }

    tr:nth-child(even) {
        background-color: #dddddd;
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

        .button2:disabled {
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

    input[type=text], select {
        padding: 12px 20px;
        margin: 8px 0;
        display: inline-block;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
        <div>
                    <center>

                        <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Admin Home Page</h1> 
            </center>

                        <br />
           <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">


               <br />


            <asp:Button ID="ViewSupervisorsButton" runat="server" CssClass="button2" OnClick="ViewSupervisorsButtonClick" Text="View all Supervisors"  Width="344px"  />

            <br />

            <br />

            <asp:Button ID="ViewThesisButton" runat="server" CssClass="button2" OnClick="ViewThesisButtonClick" Text="View all Theses"  Width="344px" />

            <br />

            <br />

              <asp:Button ID="IssuePaymentButton" runat="server" Cssclass="button2" OnClick="IssuePaymentButtonClick" Text="Issue a Thesis Payment"  Width="344px"  />

            <br />
                        <br />

              <asp:Button ID="IssueInstallmentButton" runat="server" CssClass="button2" OnClick="IssueInstallmentButtonClick" Text="Issue Installments for a Payment" Width="344px"  />

            <br />
                        <br />

              <asp:Button ID="AddExtensionButton" runat="server" CssClass="button2" OnClick="AddExtensionButtonClick" Text="Extend a Thesis"  Width="344px"  />

                        <br />
                        <br />

              <asp:Button ID="Logout" runat="server" CssClass="button2" OnClick="LogoutClick" Text="Log Out"  Width="344px"  />

               <br />

            <br />
               </div>
</center>   
        </div>
    </form>
    </body>
</html>
