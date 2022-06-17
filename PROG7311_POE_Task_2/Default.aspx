<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PROG7311_POE_Task_2._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Welcome to the farmer management prototype</h1>
        <p class="lead">This prototype enables farmers to manage their stock for any item they wish</p>
        <p>The application is intended to be an intuitive and well designed portal for any user it services</p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                Assuming an admin has created an account for you, you may now navigate through the various links in the navifation bar. Admins and farmer have
                different portal avaliable but the premise is the same. You may navigate to the products page to fully harness the functioanlity provided. Admins/Employees 
                will also have the ability to manage users.
            </p>
        </div>
        <div class="col-md-4">
            <h2>The navigation bar</h2>
            <p>
                As you can see above, there are various links at your disposal. The links are self explanitory, so i wont cover them. There is a dropdown with the user image to
                logout and edit the current users account. However, Editing is not currently functional. There is an issue with Entity Frameworks update commands that i couldnt
                resolve. The code is still provided as proof of work. 
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                If this application is to be deployed live, there are steps to be completed. First, the database will need to be migrated. Second, the application will need to be built
                and then deployed on the relevant architecture.
            </p>
        </div>
    </div>

</asp:Content>
