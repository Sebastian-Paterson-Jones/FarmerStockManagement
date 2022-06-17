<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FarmerDetail.aspx.cs" Inherits="PROG7311_POE_Task_2.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>
    <style>
        .user-image {
            height: 100px;
            border-radius: 100vw;
            border: 5px solid #ebebeb;
            margin-right: 25px;
        }
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
        .dates {
            margin-left: 25px;
        }
        .date-input {
            margin:0 5px;
        }
        .card {
            position: relative;
            width: 100%;
            height: 350px;
            max-width: 250px;
            padding: 10px 5px;
            margin: 25px;
            overflow-x: auto;
        }
        .btn-del {
            position: absolute;
            top: 15px;
            right: 40px;
            width: 25px;
            height: 25px;
            padding: 0;
        }
        .btn-edit {
            position: absolute;
            top: 15px;
            right: 10px;
            width: 25px;
            height: 25px;
            padding: 0;
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
            height: max-content;
            margin-top: 50px;
            padding: 5px;
            background: #f8f9fa;
            border-radius: 5px;
            flex-wrap: wrap;
            justify-content: space-between;
        }
        .filters {
            flex-wrap: wrap;
            height: initial;
            margin: 10px 0;
        }
        h5 ~ i {
            margin-left: 10px;
        }
        h5 {
            color: var(--primary);
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
        <div class="flex-container" style="height: max-content;">
            <asp:Image ID="userImage" runat="server" ImageUrl="" CssClass="user-image" />
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
        </div>
    <div class="flex-container controls">
            <div>
                <div class="flex-container filters">
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
                    <div class="filter flex-container centered dates">
                        <h5>From</h5>
                        <asp:TextBox ID="FromDate" runat="server" type="date" CssClass="date-input form-control"></asp:TextBox>
                        <h5>To</h5>
                        <asp:TextBox ID="ToDate" runat="server" type="date" CssClass="date-input form-control"></asp:TextBox>
                        <asp:Button ID="SearchDates" runat="server" OnClick="SearchDates_Click" CssClass="btn btn-primary dateSearch" Text="search" />
                    </div>
                </div>
            </div>
            <div class="search-container">
                <div class="flex-container centered">
                    <asp:TextBox ID="searchBox" placeholder="search by type" CssClass="col form-control" runat="server"></asp:TextBox>
                    <asp:LinkButton runat="server" OnClick="Search_Click" ID="btnSearch" CssClass="icon-container">
                        <i aria-hidden="true" class="icon fa fa-search"></i>
                    </asp:LinkButton>
                    <div class="col-1" ></div>
                    <asp:Button ID="AddNew" Text="Add" type="button" CssClass="col-2 btn btn-primary" OnClick="AddNew_Click" runat="server" />
                </div>
            </div>
        </div>
    <div class="products-container">
        <asp:Repeater ID="FarmerProductsRepeater" runat="server" DataSourceID="FarmerProductsDataSource">
        <ItemTemplate>  
                <div class="card flex-container-vertical">
                    <asp:Image ID="productImage" runat="server" CssClass="product-image" ImageUrl='<%#String.IsNullOrEmpty(DataBinder.Eval(Container, "DataItem.Image").ToString())?"https://www.pngitem.com/pimgs/m/325-3256269_packaging-white-icon-png-png-download-packaging-black.png": $"data:{DataBinder.Eval(Container, "DataItem.ImageContentType")};base64,{GetImageString64((byte[])DataBinder.Eval(Container, "DataItem.Image"))}" %>' />
                    <table>
                        <tr>
                            <th>Name:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.Name")%></td>
                        </tr>
                        <tr>
                            <th>Type:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.Type")%></td>
                        </tr>
                        <tr>
                            <th>Quantity:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.Quantity")%></td>
                        </tr>
                        <tr>
                            <th>Price:</th>
                            <td>R <%#DataBinder.Eval(Container,"DataItem.Value")%></td>
                        </tr>
                        <tr>
                            <th>Date of entry:</th>
                            <td><%#DataBinder.Eval(Container,"DataItem.DateOfEntry", "{0:d/M/yyyy}")%></td>
                        </tr>
                    </table>
                    <asp:LinkButton runat="server" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' OnClick="BtnDeleteProduct_Click" ID="BtnDeleteProduct" CssClass="btn btn-danger btn-del">
                        <i aria-hidden="true" class="icon fa fa-trash"></i>
                    </asp:LinkButton>
                    <asp:LinkButton runat="server" CommandArgument='<%#DataBinder.Eval(Container,"DataItem.ID")%>' OnClick="BtnEditProduct_Click" ID="BtnEditProduct" CssClass="btn btn-primary btn-edit">
                        <i aria-hidden="true" class="icon fa fa-edit"></i>
                    </asp:LinkButton>
                </div>  
            </ItemTemplate>
    </asp:Repeater>
    <asp:SqlDataSource 
        ID="FarmerProductsDataSource" 
        runat="server"
        ConnectionString="<%$ ConnectionStrings:stockManagementDataSource %>" 
        SelectCommand="SELECT [ID], [Name], [Quantity], [Value], [Image], [imageContentType], [Type], [DateOfEntry] FROM [Product] WHERE ([Owner] = @Owner) ORDER BY [Name] DESC"
        FilterExpression="Type='{0}'">
        <SelectParameters>
            <asp:QueryStringParameter Name="Owner" QueryStringField="id" Type="Int32" />
        </SelectParameters>
        <FilterParameters>
            <asp:ControlParameter Name="Type" ControlID="searchBox" PropertyName="text" />
        </FilterParameters>
    </asp:SqlDataSource>
    </div>
    </div>
</asp:Content>
