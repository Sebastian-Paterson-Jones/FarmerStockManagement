<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDefault.aspx.cs" Inherits="PROG7311_POE_Task_2.AdminDefault" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
		function isDelete()
		{
			return confirm("Do you want to delete this row ?");
		}
    </script>
    <style>
        .pager-container {
            position: relative;
            padding-top: 50px;
        }
        .btnAdd {
            position: absolute;
            top: 7px;
            right: 0;
        }
        .table-container {
            border-radius: 0.5rem;
            border: 1px solid #CDCDCD;
            overflow-x: auto;
        }
        .table {
            border: none;
            margin-bottom: 0;
        }
        td, th {
            min-width: 50px;
            vertical-align: middle !important;
        }
        .pager {
            position: absolute;
            top: 15px;
            left: 0;
            border-radius: 0.25rem;
            border: 1px solid #CDCDCD;
        }
        .pager td{
            background: white;
            padding: 0;
            border-right: 1px solid #CDCDCD;
        }
        .pager td:last-child {
            border-right: none;
        }
        .pager td > * {
            width: 100%;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <h3>Admin</h3>
    <div class="pager-container">
        <a class="btnAdd btn btn-primary" href="/AddUser.aspx">Add</a>
        <div class="table-container">
        <asp:GridView ID="FarmerGridView" runat="server" AllowPaging="True" AllowSorting="True" PageSize="4" AutoGenerateColumns="False" DataSourceID="stockManagementDataSource" DataKeyNames="ID"
                class="table table-striped table-borderless" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" RowStyle-CssClass="rows">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="#" SortExpression="ID" InsertVisible="False" ReadOnly="True" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="FarmerDetail.aspx?id={0}" Text="Select">
                    <ControlStyle CssClass="btn btn-primary"></ControlStyle>
                </asp:HyperLinkField>
                <asp:ButtonField CommandName="Delete" Text="Delete" ControlStyle-CssClass="btn btn-danger" />
            </Columns>
            <HeaderStyle CssClass=”thead-dark” />
            <PagerStyle CssClass="pager"></PagerStyle>
            <RowStyle CssClass="rows"></RowStyle>
        </asp:GridView>
        <asp:SqlDataSource 
            ID="stockManagementDataSource" 
            runat="server" 
            ConnectionString="<%$ ConnectionStrings:stockManagementDataSource %>" 
            SelectCommand="SELECT [ID], [FirstName], [LastName], [Email] FROM [User] WHERE ([Role] = @Role)" 
            DeleteCommand="DELETE FROM [User] WHERE ID = @ID">
            <SelectParameters>
                <asp:Parameter DefaultValue="farmer" Name="Role" Type="String" />
            </SelectParameters>
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
    </div>
    </div>
</asp:Content>
