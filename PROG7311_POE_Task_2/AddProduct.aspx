<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="PROG7311_POE_Task_2.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .add-container {
            display: flex;
            flex-direction: column;
            max-height: 900px;
            min-height: max-content;
            width: 60%;
            max-width: 500px;
            min-width: 300px;
            overflow-y: auto;
        }
        .add-container .title {
            text-align: center;
        }
        .add-container .messageBox {
            display: block;
            margin-top: 5rem;
            height: 1rem;
            text-align: center;
        }
    </style>
    <div class="container add-container">
        <h2 class="title">Add product</h2>
        <asp:Label
            ID="errorMessageBox"
            ForeColor="Red"
            CssClass="messageBox"
            runat="server">
        </asp:Label>
            <div class="flex-container-vertical centered">
                <div class="formField">
                    <asp:FileUpload ID="imageUplodaBox" runat="server" accept=".png,.jpg,.jpeg,.gif"/>
                </div>
                <div class="formField">
                    <label for="ProductName" class="form-label">Product name</label>
                    <asp:TextBox
                        AutoCompleteType="DisplayName"
                        ID="ProductName"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="ProductNameRequiredFieldValidator"   
                        ControlToValidate="ProductName"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="" class="form-label">Product quantity</label>
                    <asp:TextBox
                        ID="ProductQuantity"
                        CssClass="form-control"
                        type="number"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="ProductQuantityRequiredFieldValidator"   
                        ControlToValidate="ProductQuantity"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField">
                    <label for="ProductValue" class="form-label">Product value</label>
                    <asp:TextBox
                        ID="ProductValue"
                        CssClass="form-control"
                        type="number" 
                        min="1" 
                        step="any"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="ProductValueRequiredValidator"   
                        ControlToValidate="ProductValue"  
                        Display="Dynamic"
                        ForeColor="Red"
                        ErrorMessage="Cannot be empty."   
                        runat="server" />
                </div>
                <div class="formField flex-container-vertical centered" style="height: max-content; margin-top: 5rem">
                    <asp:Button ID="btnAddProduct"
                        CssClass="btn btn-primary"
                        OnClick="btnAddProduct_Click"
                        Text="Submit"
                        style="width: 100%"
                        runat="server" />
                </div>
            </div>
    </div>
</asp:Content>
