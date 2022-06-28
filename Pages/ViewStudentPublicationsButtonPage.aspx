<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewStudentPublicationsButtonPage.aspx.cs" Inherits="AmbizoMilestone3.ViewStudentPublicationsButtonPage" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Student's Publication(s)</title>
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

            <center>
                <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                    <h1>Supervisor Home Page</h1>
                </center>
        </div>
        <center>
            <asp:Label ID="Label5" runat="server" Text="Viewing a Student's Publication(s)" Style="font-style: italic"></asp:Label></center>
        <br />

        <center>
            <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">



                <br />


                <asp:Label ID="Label1" runat="server" Text="Student ID :"></asp:Label>

                <br />

                <asp:TextBox ID="ViewStudentPublicationStudentId" Type="number" runat="server"></asp:TextBox>

                <br />


                <br />
                <asp:Button ID="ViewStudentPublicationGetPublicationButton" runat="server" CssClass="button2" Text="Get Publication(s)" OnClick="ViewStudentPublicationGetPublicationButtonClick" />
                <br />
                <center>
                    <b></b>
                    <center>
                        <b>
                            <asp:Label ID="Label6" runat="server" Style="font-style: italic" Text="Publication(s)"></asp:Label>
                            <asp:Label ID="Label2" runat="server" Style="font-size: x-large" Text="There is currently no Publications for this student"></asp:Label>
                        </b>
                        <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                    </center>
                    <br />
                    <table id="Table1" runat="server">
                        <tr>
                            <th>Publication ID</th>
                            <th>Title</th>
                            <th>Date Of Publication</th>
                            <th>Place</th>
                            <th>Accepted</th>
                            <th>Host</th>

                        </tr>

                    </table>

                    <br />


                    <asp:Button ID="BackToASupervisorHomePageButton" runat="server" CssClass="button2" OnClick="BackToASupervisorHomePageButtonClick" Text="Back to Home Page" Width="344px" />



                    <br />
                    <br />

                </center>
            </div>
    </form>
</body>
</html>
