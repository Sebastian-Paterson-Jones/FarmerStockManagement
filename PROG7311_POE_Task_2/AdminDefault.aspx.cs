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
            // seperate header and body elements on table
            FarmerGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

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
    }
}