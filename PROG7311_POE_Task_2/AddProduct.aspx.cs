using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class AddProduct : System.Web.UI.Page
    {

        // entity framework instance for db interactions
        private StockManagementEntities entities;

        // farmer id for product
        private int farmerID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // set farmer id
            try
            {
                farmerID = int.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect("/404");
            }
        }

        /// <summary>
        /// add product to database
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            errorMessageBox.Text = "";

            string productName = this.ProductName.Text;
            string productType = this.ProductType.Text;
            int produtQuantity;
            decimal productPrice;
            try
            {
                produtQuantity = int.Parse(this.ProductQuantity.Text);
            } catch
            {
                ProductQuantityRequiredFieldValidator.ErrorMessage = "Must be a valid integer";
                errorMessageBox.Text = "Please correct the relevant fields";
                return;
            }
            try
            {
                productPrice = decimal.Parse(this.ProductValue.Text);
            } catch
            {
                ProductValueRequiredValidator.ErrorMessage = "Must be a valid decimal";
                errorMessageBox.Text = "Please correct the relevant fields";
                return;
            }
            try
            {
                using (entities = new StockManagementEntities())
                {
                    Product newProduct = new Product();
                    newProduct.Name = productName;
                    newProduct.Type = productType;
                    newProduct.Quantity = produtQuantity;
                    newProduct.Value = productPrice;
                    newProduct.Owner = farmerID;
                    newProduct.DateOfEntry = DateTime.Now;

                    // Save image if uploaded 
                    if (imageUplodaBox.HasFile && imageUplodaBox.PostedFile != null)
                    {
                        byte[] imageBytes;
                        BinaryReader br = new System.IO.BinaryReader(imageUplodaBox.PostedFile.InputStream);
                        imageBytes = br.ReadBytes((Int32)imageUplodaBox.PostedFile.InputStream.Length);
                        newProduct.Image = imageBytes;
                        newProduct.imageContentType = imageUplodaBox.PostedFile.ContentType;
                    }

                    entities.Products.Add(newProduct);
                    entities.SaveChanges();

                    Response.Redirect($"/FarmerDetail?id={farmerID}");
                }
            }
            catch (Exception err)
            {
                errorMessageBox.Text = "An unexpected error occured, please retry";
            }
        }
    }
}