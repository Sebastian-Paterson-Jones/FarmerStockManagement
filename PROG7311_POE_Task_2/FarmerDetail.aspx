<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmerDetail.aspx.cs" Inherits="PROG7311_POE_Task_2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .products-container {
            display: flex;
            flex-wrap: wrap;
            height: 100%;
            padding: 25px;
            margin-right: -15px;
            margin-left: -15px;
            border-bottom-left-radius: 15px;
            border-bottom-right-radius: 15px;
            border-left: 2px solid #ebebeb;
            border-right: 2px solid #ebebeb;
            border-bottom: 2px solid #ebebeb;
            overflow-y: auto;
        }
        .card {
            position: relative;
            width: 100%;
            height: 300px;
            max-width: 250px;
            padding: 10px 5px;
            margin: 25px;
            overflow-x: auto;
        }
        .icon-container {
            display: flex;
            justify-content: center;
            align-items: center;
            width: max-content;
            height: calc(1.5em + 0.75rem + 2px);
            padding: 0 15px;
        }
        .controls {
            margin-top: 50px;
            padding: 5px;
            background: #f8f9fa;
            border-radius: 5px;
        }
        h5 ~ i {
            margin-left: 10px;
        }
        .product-image {
            max-width: 100%;
            max-height: 100%;
            height: 200px;
            display: block;
            border-radius: 5px; 
        }
    </style>
    <div class="flex-container-vertical">
        <h3>Product details</h3>
    <table>
        <tr>
            <th>Farmer:</th>
            <td><asp:Label ID="FarmerName" class="col value" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <th>Email:</th>
            <td><asp:Label ID="FarmerEmail" class="col value" runat="server"></asp:Label></td>
        </tr>
    </table>
    <div class="row controls">
            <div class="col">
                <div class="flex-container">
                    <asp:LinkButton runat="server" OnClick="filterName_Click" ID="filterName" CssClass="filter icon-container">
                        <h5>Name </h5>
                        <i ID="NameIcon" aria-hidden="true" class="icon fa fa-arrow-down" runat="server"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" OnClick="filterQuantity_Click" ID="filterQuantity" CssClass="filter icon-container">
                        <h5>Quantity </h5>
                        <i ID="QuantityIcon" visible="false" aria-hidden="true" class="icon fa fa-arrow-down" runat="server"></i>
                    </asp:LinkButton>
                     <asp:LinkButton runat="server" OnClick="filterPrice_Click" ID="filterPrice" CssClass="filter icon-container">
                         <h5>Price </h5>
                        <i ID="PriceIcon" visible="false" aria-hidden="true" class="icon fa fa-arrow-down" runat="server"></i>
                     </asp:LinkButton>
                </div>
            </div>
            <div class="col-4 search-container">
                <div class="row">
                    <asp:TextBox ID="searchBox" placeholder="search" CssClass="col form-control" runat="server"></asp:TextBox>
                    <asp:LinkButton runat="server" OnClick="Search_Click" ID="btnSearch" CssClass="icon-container">
                        <i aria-hidden="true" class="icon fa fa-search"></i>
                    </asp:LinkButton>
                    <div class="col-1" ></div>
                    <asp:Button ID="AddNew" Text="Add" CssClass="col-2 btn btn-primary" OnClick="AddNew_Click" runat="server" />
                </div>
            </div>
        </div>
    <div class="products-container">
        <asp:Repeater ID="FarmerProductsRepeater" runat="server" DataSourceID="FarmerProductsDataSource">
        <ItemTemplate>  
                <div class="card flex-container-vertical">
                    <asp:Image ID="productImage" runat="server" CssClass="product-image" ImageUrl='<%#String.IsNullOrEmpty(DataBinder.Eval(Container, "DataItem.Image").ToString())?"https://www.pngitem.com/pimgs/m/325-3256269_packaging-white-icon-png-png-download-packaging-black.png": $"data:{DataBinder.Eval(Container, "DataItem.Image")};base64,{GetImageString64((byte[])DataBinder.Eval(Container, "DataItem.Image"))}" %>' />
                    <table>
                        <tr>
                            <th>Name:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.Name")%></td>
                        </tr>
                        <tr>
                            <th>Quantity:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.Quantity")%></td>
                        </tr>
                        <tr>
                            <th>Price:</th>
                            <td>R <%#DataBinder.Eval(Container,"DataItem.Value")%></td>
                        </tr>
                    </table>
                </div>  
            </ItemTemplate>
    </asp:Repeater>
    <asp:SqlDataSource 
        ID="FarmerProductsDataSource" 
        runat="server"
        ConnectionString="<%$ ConnectionStrings:stockManagementDataSource %>" 
        SelectCommand="SELECT [Name], [Quantity], [Value], [Image], [imageContentType] FROM [Product] WHERE ([Owner] = @Owner) ORDER BY [Name] DESC"
        FilterExpression="Name='{0}'">
        <SelectParameters>
            <asp:QueryStringParameter Name="Owner" QueryStringField="id" Type="Int32" />
        </SelectParameters>
        <FilterParameters>
            <asp:ControlParameter Name="Name" ControlID="searchBox" PropertyName="text" />
        </FilterParameters>
    </asp:SqlDataSource>
    </div>
    </div>
</asp:Content>
