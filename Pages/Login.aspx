<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AmbizoMilestone3.Login" %>

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
      input[type=email], select {
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
    <title>Login Page</title>
</head>

<body>
    <form id="form1" runat="server">
        <div>

            <center style="background-color: rgba(135, 206, 250, 0.3); width: 80%; height: 51px; margin-left: auto; margin-right: auto">
                <h1>Login Page</h1>
            </center>

            <br />

            <div style="background-color:rgba(135, 206, 250, 0.02);border-color: rgba(135, 206, 250, 0.3); border-width: 5px; border-style: groove; width: 80%; margin-left: auto; margin-right: auto">

                <div>
                    <center>

                        <br />
                        <br />

                        <label>Email:</label>
                        <asp:TextBox Type="email" ID="emailid" runat="server" MaxLength="50" Style="margin-left: 17px" Width="250px"></asp:TextBox>

                        <br />
                        <br />

                        <label>Password:</label>
                        <asp:TextBox type="password" ID="password" runat="server" MaxLength="20" Width="250px"></asp:TextBox>

                        <br />
                        <br />

                        <asp:Button ID="signin" CssClass="button" runat="server" OnClick="login" Text="Log in" Width="100px" />

                        <br />
                        <br />

                        <center>
                            <label id="loginPageErrorMessagesLabel" runat="server" style="color: red" visible="false"></label>
                        </center>

                        <br />
                        <br />

                        <center>
                            <label style="font-size: 20px">Register as:</label>
                            <br />
                            <asp:Button ID="studentreg" CssClass="button2" OnClick="studentregClick" runat="server" Style="margin-right: 10px" Text="Student" />
                            <asp:Button ID="supervisorreg" CssClass="button2" OnClick="supervisorregClick" runat="server" Style="margin-right: 10px" Text="Supervisor" />
                            <asp:Button ID="examinerreg" CssClass="button2" OnClick="examinerregClick" runat="server" Text="Examiner" />
                        </center>
                    </center>
                </div>

            </div>

        </div>
    </form>
</body>
</html>
