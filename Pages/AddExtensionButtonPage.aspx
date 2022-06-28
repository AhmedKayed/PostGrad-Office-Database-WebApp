<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExtensionButtonPage.aspx.cs" Inherits="AmbizoMilestone3.AddExtensionButtonPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Extension</title>
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
            top: 342px;
            left: 870px;
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
                       </div><center>
                             <asp:Label ID="Label5" runat="server"  Text="Adding Extension to a thesis" style="font-style:italic"></asp:Label></center>
        <br />
                                
                    <center>
                                            <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

                

                        <br />
        
        
            <asp:Label ID="Label1" runat="server" Text="Thesis Serial Number :"></asp:Label>
        
                        <br />
        
        <asp:TextBox ID="AdminAddExtensionThesisNo" Type="number" runat="server"></asp:TextBox>
                        
                        <br />
                        
                        
                        <br />
                        <asp:Button ID="AdminAddExtension" runat="server" CssClass="button2"  Text="Add Extension" OnClick="AdminAddExtension_Click" />
                                                <br />
                                                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
                                                <br />
                                                <br />

                
                    <asp:Button ID="BackToAdminHomePageViewSupervisorButton" CssClass="button2" runat="server" OnClick="BackToAdminHomePageViewSupervisorButtonClick" Text="Back to Home Page" Width="344px"/>
                        
  

                                                <br />
                        <br />

                        </center>
            </div>
    </form>
</body>
</html>
