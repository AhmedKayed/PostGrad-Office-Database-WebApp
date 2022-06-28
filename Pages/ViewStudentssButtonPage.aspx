﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewStudentssButtonPage.aspx.cs" Inherits="AmbizoMilestone3.ViewStudentssButtonPage" %>

<!DOCTYPE html>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Students</title>
    <style type="text/css">
       
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
        padding: 10px 20px;
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
        .auto-style1 {
            position: absolute;
            top: 148px;
            left: 868px;
        }
    </style>
</head>
<body>

   
    <form id="form1" runat="server">
                <div>
       <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Supervisor Home Page</h1> 
            </center>
                
       </div>
        <center>
            <asp:Label ID="Label1" runat="server" Text="Your current students" style="font-style:italic"></asp:Label>
            </center>
        <br />
        <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">
            
                    
  

            
            
                    
  

            <center>
                <b>
                <asp:Label ID="Label2" runat="server" Text="There is currently no students under your supervision"  style="font-size:x-large"></asp:Label>
               </b>
            </center>
                    <table id="Table1" runat="server">
 <tr>
     <th>Student ID</th>
    <th>First Name</th>
    <th>Last Name</th>
    <th>Years spent on Thesis</th>

  </tr>
  
</table>
            <center>
                <br />
                <br />
            <asp:Button ID="BackToASupervisorHomePageButton" runat="server" CssClass=button2 OnClick="BackToASupervisorHomePageButtonClick" Text="Back to Home Page"  Width="344px"/>

                <br />


            <br />
               </center> 

        </div>
    </form>
    </div>


</body>
</html>
