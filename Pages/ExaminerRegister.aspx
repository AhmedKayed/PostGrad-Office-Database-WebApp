<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExaminerRegister.aspx.cs" Inherits="AmbizoMilestone3.ExaminerRegister" %>

<!DOCTYPE html>
<style>
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
    input[type=email], select {
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
    <title>Examiner Register Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Examiner Register</h1>
            </center>

            <br />

            <div style="background-color:rgba(135, 206, 250, 0.02);border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

                <div>
                    <center>

                        <br />
                        <br />

                        <label>First Name:</label>
                        <asp:TextBox ID="ExaminerFirstName" runat="server" Style="margin-left: 50px" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Last Name:</label>
                        <asp:TextBox ID="ExaminerLastName" runat="server" Style="margin-left: 50px" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <label>Field Of Work:</label>
                        <asp:TextBox ID="ExaminerFeildOfWork" runat="server" Style="margin-left: 28px" MaxLength="100"></asp:TextBox>

                        <br />
                        <br />

                        <label>Email:</label>
                        <asp:TextBox type="email" ID="ExaminerEmail" runat="server" Style="margin-left: 81px" MaxLength="50"></asp:TextBox>

                        <br />
                        <br />

                        <label>Password:</label>
                        <asp:TextBox ID="ExaminerPassword" runat="server" Style="margin-left: 50px" MaxLength="20"></asp:TextBox>

                        <br />
                        <br />

                        <asp:CheckBox ID="ExaminerCheckBox" runat="server" Style="margin-left: 42px" Text="National" />

                        <br />
                        <br />

                        <center>
                            <label id="examinerRegisterErrorMessagesLabel" runat="server" style="color: red" visible="false"></label>
                            <label id="examinerRegisterSuccessMessagesLabel" runat="server" style="color: green" visible="false"></label>
                        </center>

                        <br />
                        <br />

                        <center>
                            <asp:Button ID="examinerRegisterButton" CssClass="button2" runat="server" OnClick="examinerRegisterButtonClick" Text="Register" Style="margin-right:10px" />

                            <asp:Button ID="examinerBackTologinButton" CssClass="button2" runat="server" OnClick="examinerBackTologinButtonClick" Text="Back To Login" />
                        </center>
                    </center>
                </div>

            </div>

        </div>
    </form>
</body>
</html>
