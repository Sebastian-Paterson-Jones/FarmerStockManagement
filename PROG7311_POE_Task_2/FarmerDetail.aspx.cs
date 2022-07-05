using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        // entity framework instance for db interactions
        private StockManagementEntities entities;

        // id of farmer
        int farmerID;

        // default select command of dataSource
        string selectCommand = "SELECT [ID], [Name], [Quantity], [Value], [Image], [imageContentType], [Type], [DateOfEntry] FROM [Product] WHERE ([Owner] = @Owner)";

        // sorting command
        public string sortCommand = "";

        // date command
        public string dateCommand = "";

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

            // check authority
            if (Utils.Auth.getUserCookieData()[1] != "admin")
            {
                if (!Utils.Auth.isUserIdAcceptable(farmerID.ToString()))
                {
                    Response.Redirect("/401");
                }
            }

            // fetch farmer data
            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    User farmer = this.entities.Users.SingleOrDefault(user => user.ID == farmerID);

                    FarmerName.Text = $"{farmer.FirstName} {farmer.LastName}";
                    FarmerEmail.Text = $"{farmer.Email}";
                    if (farmer.image != null)
                    {
                        userImage.ImageUrl = $"data:{farmer.imageContentType};base64,{Convert.ToBase64String(farmer.image, 0, farmer.image.Length)}";
                    }
                    else
                    {
                        userImage.ImageUrl = "https://www.unitedway.ca/wp-content/uploads/2017/06/TempProfile.jpg";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void Search_Click(object sender, EventArgs e)
        {
            FarmerProductsRepeater.DataBind();
        }

        /// <summary>
        /// redirect to add product
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AddProduct?id={farmerID}");
        }

        /// <summary>
        /// Filter products by name
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void filterName_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Name] DESC";
            PriceIcon.Visible = false;
            QuantityIcon.Visible = false;
            NameIcon.Visible = true;
            sortDataSource();
        }

        /// <summary>
        /// Filter products by price
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void filterPrice_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Value] DESC";
            PriceIcon.Visible = true;
            QuantityIcon.Visible = false;
            NameIcon.Visible = false;
            sortDataSource();
        }

        /// <summary>
        /// filter product by quantity
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void filterQuantity_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Quantity] DESC";
            PriceIcon.Visible = false;
            QuantityIcon.Visible = true;
            NameIcon.Visible = false;
            sortDataSource();
        }

        /// <summary>
        /// convert byte array to base 64 strings
        /// </summary>
        /// <param name="Image">byte array</param>
        /// <returns>base 64 image string</returns>
        protected string GetImageString64(byte[] Image)
        {
            return Convert.ToBase64String(Image, 0, Image.Length);
        }

        /// <summary>
        /// sort product list items
        /// </summary>
        private void sortDataSource()
        {
            string sortedSelect = $"{selectCommand} {dateCommand} {sortCommand}";
            FarmerProductsDataSource.SelectCommand = sortedSelect;
            FarmerProductsDataSource.Select(DataSourceSelectArguments.Empty);
            FarmerProductsDataSource.DataBind();
            FarmerProductsRepeater.DataBind();
        }

        /// <summary>
        /// redirect to edit product
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void BtnEditProduct_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect($"/EditProduct?farmerID={farmerID}&productID={btn.CommandArgument.ToString()}");
        }

        /// <summary>
        /// Redirect to product item delete
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int productID = Convert.ToInt32(btn.CommandArgument);
            
            try
            {
                using (entities = new StockManagementEntities())
                {
                    Product product = entities.Products.Where(item => item.ID == productID).FirstOrDefault();
                    entities.Products.Remove(product);
                    entities.SaveChanges();
                    FarmerProductsRepeater.DataBind();
                }
            } catch (Exception err)
            {
            }
        }

        /// <summary>
        /// List product by date
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void SearchDates_Click(object sender, EventArgs e)
        {
            string fromDate = FromDate.Text;
            string toDate = ToDate.Text;

            if(String.IsNullOrEmpty(fromDate) || String.IsNullOrEmpty(toDate))
            {
                dateCommand = "";
                sortDataSource();
            } 
            else
            {
                dateCommand = $"AND ([DateOfEntry] BETWEEN '{Convert.ToDateTime(fromDate)}' AND '{Convert.ToDateTime(toDate)}')";
                sortDataSource();
            }
        }
        }

        protected void BtnEditProduct_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect($"/EditProduct?farmerID={farmerID}&productID={btn.CommandArgument.ToString()}");
        }

        protected void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int productID = Convert.ToInt32(btn.CommandArgument);
            
            try
            {
                using (entities = new StockManagementEntities())
                {
                    Product product = entities.Products.Where(item => item.ID == productID).FirstOrDefault();
                    entities.Products.Remove(product);
                    entities.SaveChanges();
                    FarmerProductsRepeater.DataBind();
                }
            } catch (Exception err)
            {
            }
        }

        protected void SearchDates_Click(object sender, EventArgs e)
        {
            string fromDate = FromDate.Text;
            string toDate = ToDate.Text;

            if(String.IsNullOrEmpty(fromDate) || String.IsNullOrEmpty(toDate))
            {
                dateCommand = "";
                sortDataSource();
            } 
            else
            {
                dateCommand = $"AND ([DateOfEntry] BETWEEN '{Convert.ToDateTime(fromDate)}' AND '{Convert.ToDateTime(toDate)}')";
                sortDataSource();
            }
        }
    }
}