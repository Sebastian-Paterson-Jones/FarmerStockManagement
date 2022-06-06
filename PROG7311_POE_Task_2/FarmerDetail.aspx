<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmerDetail.aspx.cs" Inherits="PROG7311_POE_Task_2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="FarmerProductsRepeater" runat="server" DataSourceID="FarmerProductsDataSource">
        <ItemTemplate>  
                <div class="card">  
                    <p>  
                        <%#DataBinder.Eval(Container,"DataItem.ID")%>  
                    </p>  
                    <p>  
                        <%#DataBinder.Eval(Container,"DataItem.Name")%>  
                    </p>  
                    <p>  
                        <%#DataBinder.Eval(Container,"DataItem.Quantity")%>  
                    </p>
                    <p>
                        <%#DataBinder.Eval(Container,"DataItem.Value")%>  
                    </p>
                </div>  
            </ItemTemplate>
    </asp:Repeater>
    <asp:SqlDataSource ID="FarmerProductsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:stockManagementDataSource %>" SelectCommand="SELECT [ID], [Name], [Value], [Quantity] FROM [Product] WHERE ([Owner] = @Owner)">
        <SelectParameters>
            <asp:QueryStringParameter Name="Owner" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
