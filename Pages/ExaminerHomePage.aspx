<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExaminerHomePage.aspx.cs" Inherits="AmbizoMilestone3.ExaminerHomePage" %>

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
      /*width: 80%;
        margin-left: auto;
        margin-right: auto;*/
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
    <title>Examiner Home Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Examiner Home Page</h1>
            </center>

            <br />

        </div>

            <div style="border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

                <h2 id="examinerTableLabel" runat="server">Profile Information</h2>
                <h2 id="examinerTableCommentsLabel" runat="server" visible="false">Defense Information</h2>
                <h2 id="ViewAllLabel" runat="server" visible="false">Thesis Titles, Supervisors, Students</h2>
                <asp:TextBox ID="SearchTextBox" runat="server" visible="false" style="float:right" ></asp:TextBox>
                <asp:Button ID="SearchButton" CssClass="button2" runat="server" Text="Search"  visible="false"  style="float:right" OnClick="SearchOnClick"/>
                <br/>
                <h2 id="AllThesisLabel" runat="server" visible="false">Search In All Thesis</h2>
                
                <center>
                <table id="examinerProfileTable" runat="server" width ="80%"  >
                    <tr>
                        <th>Examiner ID:</th>
                        <th>Name:</th>
                        <th>Field of work:</th>
                        <th>isNational:</th>
                        
                       
   
                    </tr>
                </table>
                <table id="Table2"  visible="false" runat="server" width ="80%" >
                    <tr>
                        <th>Date:</th>
                        <th>Serial Num:</th>
                        <th>Examiner ID:</th>
                        <th>Comment:</th>
                        <th>Grade:</th>
                        
   
                    </tr>
                </table>
                    </center>
                
                  <center>
                <table id="TitleTable"  visible="false" runat="server" style="display:inline-block;margin-right:20px">
                    <tr>
                        <th>Thesis Titles:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </th>              

                    </tr>
                </table>
                <table id="SuperTable"  visible="false" runat="server" style="display:inline-block ; margin-right:20px">
                    <tr>
                        <th>Supervisors:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </th>              
                    </tr>
                </table>
                <table id="StudTable"  visible="false" runat="server" style="display:inline-block" >
                    <tr>
                        <th>Students:&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        </th>              
                    </tr>
                </table>

                       <table id="AllThesisTable" runat="server">
                    <tr>
                             <th>Thesis Serial Number</th>
                             <th>Field</th>
                             <th>Type</th>
                             <th>Title</th>
                             <th>Start Date</th>
                             <th>End Date</th>
                             <th>Defense Date</th>
                             <th>Years</th>
                             <th>Grade</th>
                             <th>Payment ID</th>      
                             <th>No. of Extensions</th>
                       </tr>
  
                        </table>
                      </center>


                <center>

                    <br />

                    <asp:Label ID="EditYourInfo" runat="server" Text="Edit your information:" Font-Bold BorderColor="Black" BorderStyle="Solid" Height="40px" Width="200px"></asp:Label>
                    <asp:Label ID="AddYourComments" runat="server" Text="Add your Comments:" Font-Bold BorderColor="Black" BorderStyle="Solid" Height="40px" Width="200px" Visible="false"></asp:Label>
                    <asp:Label ID="AddYourGrade" runat="server" Text="Add a grade:" Font-Bold BorderColor="Black" BorderStyle="Solid" Height="40px" Width="200px" Visible="false"></asp:Label>
                     <br />
                      
                        <br />
                        <asp:Label ID="NameLabel" runat="server" Text="Name: "></asp:Label>
                        <asp:TextBox ID="NameTextBox" runat="server"  MaxLength="20"></asp:TextBox>
                        <asp:Label ID="SerialLabel" runat="server" Text="Serial Num: " visible="false"></asp:Label>
                        <asp:TextBox ID="SerialTextBox" type="number" runat="server"  MaxLength="20" visible="false"></asp:TextBox>

                        <br />
                        <br />

                        <asp:Label ID="FieldOfWorkLabel" runat="server" Text="Field of Work: "></asp:Label>
                        <asp:TextBox ID="FieldOfWorkTextBox" runat="server" MaxLength="20"></asp:TextBox>
                        <asp:Label ID="CommentLabel" runat="server" Text="Comment: " visible="false"></asp:Label>
                        <asp:Label ID="GradeLabel" runat="server" Text="Grade: " visible="false"></asp:Label>
                        <asp:TextBox ID="CommentTextBox" runat="server"  MaxLength="20" visible="false"></asp:TextBox>
                        <asp:TextBox ID="GradeTextBox" type="text" runat="server"  MaxLength="20" visible="false"></asp:TextBox>
                
                    <br />
                    
                <br id="examinerProfileTableBreak" runat="server" />
                

                    <asp:Button ID="SubmitButton" CssClass="button2" runat="server" Text="Submit" OnClick="SubmitOnClick" />
                    <asp:Button ID="AddToComments" CssClass="button2" runat="server" Text="Add The Comment"  visible="false" OnClick="AddtoCommentOnClick" />
                    <asp:Button ID="AddToGrade" CssClass="button2" runat="server" Text="Add The Grade"  visible="false" OnClick="AddtoGradeOnClick" />
                    <br />
                    <br />
                    <asp:Button ID="EditInfoButton" CssClass="button2" runat="server" Text="Edit Information"  OnClick="EditInfoOnClick"/>
                    <asp:Button ID="AddCommentButton" CssClass="button2" runat="server" Text="Add Comment" OnClick="AddCommentOnClick" />
                    <asp:Button ID="AddGradeButton" CssClass="button2" runat="server" Text="Add Grade" OnClick="AddGradeOnClick" />
                    <asp:Button ID="ViewAllMyButton"  CssClass="button2" runat="server" Text="View All My Thesis Titles, Supervisors, Students Names"  OnClick="ViewAllMyOnClick"/>
                    <asp:Button ID="ViewSearchButton" CssClass="button2" runat="server" Text="Search For A Thesis" OnClick="ViewSearchOnClick" />
                    <asp:Button ID="LogOutButton" CssClass="button2" runat="server" Text="Log Out"  visible="true" OnClick="LogOutOnClick" />

                </center>

                <br />
                
                 
            </div>

    </form>
</body>
</html>
