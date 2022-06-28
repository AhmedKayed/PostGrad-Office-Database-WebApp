<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddExaminerDefenseButtonPage.aspx.cs" Inherits="AmbizoMilestone3.AddExaminerDefenseButtonPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Examiner to Defense</title>
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
            top: 848px;
            left: 870px;
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

                       <div>   <center>
                                        <asp:Label ID="Label5" runat="server"  Text="Add an Examiner to a Defense" style="font-style:italic"></asp:Label></center>
        <br />
                           </div>
             <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">
                                    <center><div>
                                
                    <center>
         
                

            <br />
        
        
            <asp:Label ID="Label1" runat="server" Text="Examiner ID :"></asp:Label>
        
                        <br />
        
        <asp:TextBox ID="SupervisorAddExaminerID" Type="number" runat="server"></asp:TextBox>
                        
                        <br />
                        
  

            <br />
        
        
            <asp:Label ID="Label3" runat="server" Text="Thesis Serial No. :"></asp:Label>
        
                        <br />
        
        <asp:TextBox ID="SupervisorAddExaminerThesisNo" Type="number" runat="server"></asp:TextBox>
                        
                        <br />
                        
            <br />
        
        
            <asp:Label ID="Label2" runat="server" Text="Defense Date :"></asp:Label>
        
                        <br />
        
                        <asp:Calendar ID="SupervisorAddExaminerDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="186px" SelectedDate="12/25/2021 03:17:55" Width="334px">
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                        </asp:Calendar>
                        <br />
                        <br />
                        <asp:Button ID="SupervisorAddExaminertoDefense" runat="server" Cssclass="button2" Text="Add Examiner to Defense" OnClick="SupervisorAddExaminertoDefense_Click" />
                        <br />
                        <asp:Label ID="Label6" runat="server"  Text=""></asp:Label>
                        <br />
                        <br />
         
                
                    <asp:Button ID="BackToSupervisorHomePageViewSupervisorButton" cssclass="button2" runat="server" OnClick="BackToAdminHomePageViewSupervisorButtonClick" Text="Back to Home Page" Width="344px"/>
                        
  

                        <br />
                        
  

                        <br />

                        </center>
            </div>
    </form>
</body>
</html>
