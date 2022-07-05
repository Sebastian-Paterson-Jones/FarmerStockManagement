using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class AdminDefault : System.Web.UI.Page
    {
        StockManagementEntities entities;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utils.Auth.getUserCookieData()[1] != "admin")
            {
                Response.Redirect("/401");
            }

            // seperate header and body elements on table
            FarmerGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

        /// <summary>
        /// delete user from database
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e">event arguments</param>
        protected void FarmerGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userID = Convert.ToInt32(FarmerGridView.Rows[e.RowIndex].Cells[0].Text);

            using (entities = new StockManagementEntities())
            {
                User farmer = entities.Users.Where(user => user.ID == userID).FirstOrDefault();
                entities.Users.Remove(farmer);
                entities.SaveChanges();
                FarmerGridView.DataBind();
            }
        }

        /// <summary>
        /// convert byte array to base 64 string
        /// </summary>
        /// <param name="Image">image byte array</param>
        /// <returns>base 64 image</returns>
        protected string GetImageString64(byte[] Image)
        {
            return Convert.ToBase64String(Image, 0, Image.Length);
        }
    }
}