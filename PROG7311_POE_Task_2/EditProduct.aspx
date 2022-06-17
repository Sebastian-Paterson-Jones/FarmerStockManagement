<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="PROG7311_POE_Task_2.EditProduct" %>
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
        <h2 class="title">Add product</h2>
        <asp:Label
            ID="errorMessageBox"
            ForeColor="Red"
            CssClass="messageBox"
            runat="server">
        </asp:Label>
            <div class="flex-container-vertical centered">
                <asp:Image ID="productImage" runat="server" CssClass="image"/>
                <div class="formField">
                    <label for="imageUploadBox" class="form-label">Product Image (Optional)</label>
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
                    <label for="ProductType" class="form-label">Product type</label>
                    <asp:TextBox
                        ID="ProductType"
                        CssClass="form-control"
                        runat="server">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="ProductTypeRequiredFieldValidator"   
                        ControlToValidate="ProductType"  
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
                    <asp:RegularExpressionValidator runat="server" ErrorMessage="Value must be a comma seperated decimal" ID="ValueRegex"
                       ForeColor="Red" ControlToValidate="ProductValue"              
                       ValidationExpression="^\d+\,\d{0,2}$"></asp:RegularExpressionValidator>
                </div>
                <div class="formField flex-container-vertical centered" style="height: max-content; margin-top: 5rem">
                    <asp:Button ID="btnUpdateProduct"
                        CssClass="btn btn-primary"
                        OnClick="btnUpdateProduct_Click"
                        Text="Update"
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
