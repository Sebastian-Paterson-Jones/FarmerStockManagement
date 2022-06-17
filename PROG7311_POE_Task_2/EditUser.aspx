<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="PROG7311_POE_Task_2.EditUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .edit-container {
            display: flex;
            flex-direction: column;
            max-height: 900px;
            min-height: max-content;
            width: 60%;
            max-width: 500px;
            min-width: 300px;
            overflow-y: auto;
        }
        .edit-container .title {
            text-align: center;
        }
        .edit-container .messageBox {
            display: block;
            margin-top: 5rem;
            height: 1rem;
            text-align: center;
        }
        .image{
            width: 80%;
            border-radius: 10px;
        }
    </style>
    <div class="container edit-container">
        <h2 class="title">Add user</h2>
        <asp:Label
            ID="errorMessageBox"
            ForeColor="Red"
            CssClass="messageBox"
            runat="server">
        </asp:Label>
            <div class="flex-container-vertical centered">
                <asp:Image ID="userImage" runat="server" CssClass="image"/>
                <div class="formField">
                    <label for="imageUploadBox" class="form-label">Ùser Image (Optional)</label>
                    <asp:FileUpload ID="imageUplodaBox" runat="server" accept=".png,.jpg,.jpeg,.gif"/>
                </div>
                <div class="formField">
                    <label for="FirstName" class="form-label">User first name</label>
                    <asp:TextBox
                        AutoCompleteType="FirstName"
                        ID="FirstName"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="FirstNameRequiredFieldValidator"   
                        ControlToValidate="FirstName"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="LastName" class="form-label">User last name</label>
                    <asp:TextBox
                        AutoCompleteType="LastName"
                        ID="LastName"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="LastNameRequiredFieldValidator"   
                        ControlToValidate="LastName"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="UserEmail" class="form-label">Email address</label>
                    <asp:TextBox
                        AutoCompleteType="Email"
                        ID="UserEmail"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="EmailRequiredValidator"   
                        ControlToValidate="UserEmail"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="UserRole" class="form-label">Users role</label>
                    <asp:DropDownList ID="UserRole" CssClass="form-control dropdown" runat="server">
                        <asp:ListItem Selected="True">Farmer</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="formField">
                    <label for="UserPassword" class="form-label">Users password</label>
                    <asp:TextBox
                        ID="UserPassword"
                        TextMode="Password"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                </div>
                <div class="formField flex-container-vertical centered" style="height: max-content; margin-top: 5rem">
                    <asp:Button ID="btnUpdateUser"
                        CssClass="btn btn-primary"
                        OnClick="btnUpdateUser_Click"
                        Text="Submit"
                        style="width: 100%"
                        runat="server" />
                    <asp:Button ID="btnCancel"
                        CssClass="btn btn-danger"
                        OnClick="btnCancel_Click"
                        Text="Cancel"
                        style="width: 100%"
                        runat="server" />
                </div>
            </div>
    </div>
</asp:Content>
