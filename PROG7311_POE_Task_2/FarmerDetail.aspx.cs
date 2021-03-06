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
        string selectCommand = "SELECT [Name], [Quantity], [Value], [Image], [imageContentType] FROM [Product] WHERE ([Owner] = @Owner)";

        // sorting command
        public string sortCommand = "";

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

            // fetch farmer data
            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    User farmer = this.entities.Users.SingleOrDefault(user => user.ID == farmerID);

                    FarmerName.Text = $"{farmer.FirstName} {farmer.LastName}";
                    FarmerEmail.Text = $"{farmer.Email}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            FarmerProductsRepeater.DataBind();
        }

        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect($"AddProduct?id={farmerID}");
        }

        protected void filterName_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Name] DESC";
            PriceIcon.Visible = false;
            QuantityIcon.Visible = false;
            NameIcon.Visible = true;
            sortDataSource();
        }

        protected void filterPrice_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Value] DESC";
            PriceIcon.Visible = true;
            QuantityIcon.Visible = false;
            NameIcon.Visible = false;
            sortDataSource();
        }

        protected void filterQuantity_Click(object sender, EventArgs e)
        {
            sortCommand = "ORDER BY [Quantity] DESC";
            PriceIcon.Visible = false;
            QuantityIcon.Visible = true;
            NameIcon.Visible = false;
            sortDataSource();
        }

        protected string GetImageString64(byte[] Image)
        {
            return Convert.ToBase64String(Image, 0, Image.Length);
        }

        private void sortDataSource()
        {
            string sortedSelect = $"{selectCommand} {sortCommand}";
            FarmerProductsDataSource.SelectCommand = sortedSelect;
            FarmerProductsDataSource.Select(DataSourceSelectArguments.Empty);
            FarmerProductsDataSource.DataBind();
            FarmerProductsRepeater.DataBind();
        }
    }
}