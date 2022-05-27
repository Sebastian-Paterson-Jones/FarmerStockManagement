<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PROG7311_POE_Task_2.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder> 
    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    
    <style>
        .login {
            height: 70%;
            max-height: 900px;
            min-height: 600px;
            width: 60%;
            max-width: 600px;
            min-width: 300px;
        }
    </style>
</head>
<body class="flex-container centered">
    <div class="container login">
        <h2>Login</h2>
        <asp:Label
            ID="message"
            ForeColor="Red"
            runat="server">
        </asp:Label>
        <form id="login" class="form" runat="server">
            <div class="flex-container-vertical spaced-around">
                <div class="formField">
                    <label for="UserEmail" class="form-label">Email address</label>
                    <asp:TextBox
                        AutoCompleteType="Email"
                        ID="UserEmail"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="UsernameRequiredValidator"   
                        ControlToValidate="UserEmail"  
                        Display="Dynamic"   
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="UserPassword" class="form-label">Password</label>
                    <asp:TextBox
                        ID="UserPassword"
                        TextMode="Password"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1"   
                        ControlToValidate="UserPassword"  
                        Display="Dynamic"   
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="flex-container horizontal-center">
                    <asp:Button 
                        CssClass="btn btn-primary mb-3"
                        OnClick="login_click"
                        Text="Log In"
                        runat="server"/>
                    <div class="form-check">
                        <asp:CheckBox
                            ID="ChboxStayLoggedIn"
                            CssClass="form-check-input"
                            runat="server"/>
                        <label
                            class="form-check-label"
                            for="ChBoxStayLoggedIn">
                            Stay logged in
                        </label>
                    </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
