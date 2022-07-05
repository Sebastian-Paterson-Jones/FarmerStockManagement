using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class EditProduct : System.Web.UI.Page
    {
        // entity framework instance for db interactions
        private StockManagementEntities entities;

        // id of farmer
        int farmerID;

        // id of product
        int productID;

        // store prev page
        private string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // set the postback
            if (!IsPostBack)
            {
                try
                {
                    prevPage = Request.UrlReferrer.ToString();
                }
                catch (Exception err)
                {
                    prevPage = "/Default";
                }
            }

            // set product id and farmer id
            try
            {
                productID = int.Parse(Request.QueryString["productID"]);
                farmerID = int.Parse(Request.QueryString["farmerID"]);
            }
            catch
            {
                Response.Redirect("/404");
            }

            // check authority
            if (Utils.Auth.getUserCookieData()[1] != "admin")
            {
                if(!Utils.Auth.isUserIdAcceptable(farmerID.ToString()))
                {
                    Response.Redirect("/401");
                }
            }

            // fetch product data
            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    Product product = this.entities.Products.SingleOrDefault(prod => prod.ID == productID);

                    if(product == null)
                    {
                        Response.Redirect("/404");
                    } else
                    {
                        if(product.Image != null)
                        {
                            this.productImage.ImageUrl = $"data:{product.imageContentType};base64,{Convert.ToBase64String(product.Image, 0, product.Image.Length)}";
                        } else
                        {
                            this.productImage.ImageUrl = "https://www.pngitem.com/pimgs/m/325-3256269_packaging-white-icon-png-png-download-packaging-black.png";
                        }

                        this.ProductName.Text = product.Name;
                        this.ProductType.Text = product.Type;
                        this.ProductQuantity.Text = product.Quantity.ToString();
                        this.ProductValue.Text = product.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        /// <summary>
        /// update product in database
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            errorMessageBox.Text = "";

            string productName = this.ProductName.Text;
            string productType = this.ProductType.Text;
            int produtQuantity;
            decimal productPrice;
            try
            {
                produtQuantity = int.Parse(this.ProductQuantity.Text);
            }
            catch
            {
                ProductQuantityRequiredFieldValidator.ErrorMessage = "Must be a valid integer";
                errorMessageBox.Text = "Please correct the relevant fields";
                return;
            }
            try
            {
                productPrice = decimal.Parse(this.ProductValue.Text);
            }
            catch
            {
                ProductValueRequiredValidator.ErrorMessage = "Must be a valid decimal";
                errorMessageBox.Text = "Please correct the relevant fields";
                return;
            }
            try
            {
                using (entities = new StockManagementEntities())
                {
                    Product product = entities.Products.Find(productID);

                    product.Name = productName;
                    product.Type = productType;
                    product.Value = productPrice;
                    product.Quantity = produtQuantity;

                    // Save image if uploaded 
                    if (imageUplodaBox.HasFile && imageUplodaBox.PostedFile != null)
                    {
                        byte[] imageBytes;
                        BinaryReader br = new System.IO.BinaryReader(imageUplodaBox.PostedFile.InputStream);
                        imageBytes = br.ReadBytes((Int32)imageUplodaBox.PostedFile.InputStream.Length);
                        product.Image = imageBytes;
                        product.imageContentType = imageUplodaBox.PostedFile.ContentType;
                    }
                    
                    entities.SaveChanges();
                    Response.Redirect(prevPage);
                }
            }
            catch (Exception err)
            {
                errorMessageBox.Text = "An unexpected error occured, please retry";
            }
}
        /// <summary>
        /// Cancel edit product and redurect back
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}