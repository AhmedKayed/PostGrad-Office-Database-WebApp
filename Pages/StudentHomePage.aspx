<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentHomePage.aspx.cs" Inherits="AmbizoMilestone3.StudentHomePage" %>

<!DOCTYPE html>
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
        padding: 16px 32px;
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
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Home Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Student Home Page</h1>
            </center>

            <br />

            <div style="background-color:rgba(135, 206, 250, 0.02);border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

                <h2 id="studentTableLabel" runat="server">Profile Information</h2>

                <table id="studentProfileTable" runat="server">
                    <tr>
                        <th>ID:</th>
                        <th>First Name:</th>
                        <th>Last Name:</th>
                        <th>Type:</th>
                        <th>Faculty:</th>
                        <th>Address:</th>
                        <th>GPA:</th>
                        <th>Email:</th>
                        <th id="studentProfileTableUnderGradIdColumn" visible="false" runat="server">UnderGrad ID:</th>
                    </tr>
                </table>

                <br id="studentProfileTableBreak" runat="server" />

                <table style="width: 15%" id="studentPhoneNumbersTable" runat="server">
                    <tr>
                        <th>Mobile Numbers:</th>
                    </tr>
                </table>

                <br id="studentPhoneNumbersTableBreak" runat="server" />

                <div id="studentAddMobileNumberDiv" runat="server" visible="false">
                    <center>
                        <label>Mobile Number:</label>
                        <asp:TextBox type="number" ID="studentMobileNumberTextBox" runat="server" MaxLength="20"></asp:TextBox>
                    </center>
                </div>

                <br id="studentAddMobileNumberDivBreak" runat="server" visible="false" />

                <center id="studentAddMobileNumberCenter" runat="server">
                    <asp:Button ID="studentAddMobileNumber" CssClass="button2" runat="server" OnClick="studentAddMobileNumberClick" Text="Add Mobile Number" Style="margin-right: 10px" />
                    <asp:Button ID="studentAddMobileNumberAdd" CssClass="button2" runat="server" OnClick="studentAddMobileNumberAddClick" Visible="false" Text="Add" Style="margin-right: 10px" />
                    <asp:Button ID="studentAddMobileNumberCancel" CssClass="button2" runat="server" OnClick="studentAddMobileNumberCancelClick" Visible="false" Text="Cancel" />
                </center>

                <table id="studentThesesTable" visible="false" runat="server">
                    <tr>
                        <th>Serial Number:</th>
                        <th>Field:</th>
                        <th>Type:</th>
                        <th>Title:</th>
                        <th>Start Date:</th>
                        <th>End Date:</th>
                        <th>Defense Date:</th>
                        <th>Years:</th>
                        <th>Grade:</th>
                        <th>Payment ID:</th>
                        <th>Number Of Extensions:</th>

                    </tr>
                </table>

                <br id="studentThesesTableBreak" runat="server" visible="false" />

                <div id="studentLinkPublicationDiv" runat="server" visible="false">
                    <center>

                        <h2 id="studentLinkPublicationTableLabel" runat="server">Publications Information</h2>

                        <table id="studentLinkPublicationTable" runat="server">
                            <tr>
                                <th>ID:</th>
                                <th>Title:</th>
                                <th>Date:</th>
                                <th>Place:</th>
                                <th>Accepted:</th>
                                <th>Host:</th>
                            </tr>
                        </table>

                        <br />

                        <h2 id="studentLinkPublicationTableLinkedLabel" runat="server">Linked Publications</h2>

                        <table id="studentLinkPublicationTableLinked" runat="server">
                            <tr>
                                <th>Thesis ID:</th>
                                <th>Publication ID:</th>
                            </tr>
                        </table>

                        <br />
                        <br />

                        <label>Thesis ID:</label>
                        <asp:TextBox type="number" ID="studentLinkPublicationThesisID" runat="server" Style="margin-left: 30px" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Publication ID:</label>
                        <asp:TextBox type="number" ID="studentLinkPublicationID" runat="server" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <asp:Button ID="studentLinkPublicationLinkButton" CssClass="button2" runat="server" OnClick="studentLinkPublicationLinkButtonClick" Text="Link" />

                    </center>
                </div>

                <div id="studentAddPublicationDiv" runat="server" visible="false">
                    <center>
                        <label>Title:</label>
                        <asp:TextBox type="text" ID="studentAddPublicationTitle" runat="server" MaxLength="100" Width="400px"></asp:TextBox>

                        <br />
                        <br />

                        <label>Date:</label>

                        <br />

                        <asp:Calendar ID="studentAddPublicationDate" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="186px" SelectedDate="12/25/2021 03:17:55" Width="334px">
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

                        <label>Host: </label>
                        <asp:TextBox type="text" ID="studentAddPublicationHost" runat="server" MaxLength="100" Width="400px"></asp:TextBox>

                        <br />
                        <br />

                        <label>Place:</label>
                        <asp:TextBox type="text" ID="studentAddPublicationPlace" runat="server" MaxLength="100" Width="400px"></asp:TextBox>

                        <br />
                        <br />

                        <asp:CheckBox ID="studentAddPublicationAcceptedCheckBox" runat="server" Style="margin-left: 10px" Text="Accepted" />

                        <br />
                        <br />

                        <asp:Button ID="studentAddPublicationAddButton" CssClass="button2" runat="server" OnClick="studentAddPublicationAddButtonClick" Text="Add" />

                    </center>
                </div>

                <div id="studentAddProgressReportDiv" runat="server" visible="false">
                    <center>
                        <label>Thesis ID:</label>
                        <asp:TextBox type="number" ID="studentAddProgressReportThesisID" runat="server" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Progress Report No.:</label>
                        <asp:TextBox type="number" ID="studentAddProgressReportNoTextBox" runat="server" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Date:</label>

                        <br />

                        <asp:Calendar ID="studentAddProgressReportCalendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="186px" SelectedDate="12/25/2021 03:17:55" Width="334px">
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

                        <asp:Button ID="studentAddProgressReportAddButton" CssClass="button2" runat="server" OnClick="studentAddProgressReportAddButtonClick" Text="Add" />

                    </center>
                </div>

                <div id="studentFillProgressReportDiv" runat="server" visible="false">
                    <center>

                        <h2 id="studentFillProgressReportLabel" runat="server">Progress Reports Information</h2>

                        <table id="studentFillProgressReportTable" runat="server">
                            <tr>
                                <th>Progress Report No:</th>
                                <th>Date:</th>
                                <th>Evaluation:</th>
                                <th>State:</th>
                                <th>Thesis Serial Number:</th>
                                <th>Supervisor ID:</th>
                                <th>Description:</th>
                            </tr>
                        </table>

                        <br />
                        <br />

                        <label>Progress No:</label>
                        <asp:TextBox type="number" ID="studentFillProgressReportNo" runat="server" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>State:</label>
                        <asp:TextBox type="number" ID="studentFillProgressReportState" runat="server" Style="margin-left: 30px" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Thesis ID:</label>
                        <asp:TextBox type="number" ID="studentFillProgressReportThesis" runat="server" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Description:</label>
                        <br />
                        <textarea type="text" rows="4" cols="50" maxlength="200" id="studentFillProgressReportDescription" runat="server"> </textarea>

                        <br />
                        <br />

                        <asp:Button ID="studentFillProgressReportFillButton" CssClass="button2" runat="server" OnClick="studentFillProgressReportFillButtonClick" Text="Fill" />

                    </center>
                </div>

                <table id="studentCoursesTable" visible="false" runat="server">
                    <tr>
                        <th>Course Code:</th>
                        <th>Credit Hours:</th>
                        <th>Grade:</th>
                    </tr>
                </table>

                <br />

                <center>
                    <label id="studentHomePageErrorMessagesLabel" runat="server" style="color: red" visible="false"></label>
                    <label id="studentHomePageSuccessMessagesLabel" runat="server" style="color: green" visible="false"></label>
                </center>

                <br />
                <br />
                <br />

                <center>
                    <asp:Button ID="studentViewProfile" runat="server" CssClass="button2" Enabled="false" OnClick="studentViewProfileClick" Text="View My Profile" Style="margin-right: 10px" />
                    <asp:Button ID="studentViewTheses" runat="server" CssClass="button2" OnClick="studentViewThesesClick" Text="View My Theses" Style="margin-right: 10px" />

                    <asp:Button ID="studentAddPublication" runat="server" CssClass="button2" OnClick="studentAddPublicationClick" Text="Add Publication" Style="margin-right: 10px" />
                    <asp:Button ID="studentLinkPublication" runat="server" CssClass="button2" OnClick="studentLinkPublicationClick" Text="Link a Publication" Style="margin-right: 10px" />

                    <asp:Button ID="studentAddProgressReport" runat="server" CssClass="button2" OnClick="studentAddProgressReportClick" Text="Add Progress Report" Style="margin-right: 10px" />
                    <asp:Button ID="studentFillProgressReport" runat="server" CssClass="button2" OnClick="studentFillProgressReportClick" Text="Fill a Progress Report" Style="margin-right: 10px" />

                    <asp:Button ID="studentViewMyCourses" runat="server" CssClass="button2" OnClick="studentViewMyCoursesClick" Text="View My Courses" Style="margin-right: 10px" />

                    <asp:Button ID="studentHomePageLogOutButton" runat="server" CssClass="button2" OnClick="studentHomePageLogOutButtonClick" Text="Log Out" />
                </center>

            </div>
        </div>
    </form>
</body>
</html>
