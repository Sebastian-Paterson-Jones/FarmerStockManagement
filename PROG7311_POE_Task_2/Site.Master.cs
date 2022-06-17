using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class SiteMaster : MasterPage
    {
        // entity for user
        StockManagementEntities entities;

        // userID
        private string userID;


        protected void Page_Load(object sender, EventArgs e)
        {
            if(Utils.Auth.getUserCookieData()[1] == "admin")
            {
                ProductsLink.Visible = false;
            } else
            {
                UsersLink.Visible = false;
            }

            // get user specific data
            using(entities = new StockManagementEntities())
            {
                userID = Utils.Auth.getUserCookieData()[0];
                User user = entities.Users.FirstOrDefault(u => u.ID.ToString() == userID);
                userProfileLabel.Text = $"{user.FirstName} {user.LastName}";
                if(user.image != null)
                {
                    userProfileImage.ImageUrl = $"data:{user.imageContentType};base64,{Convert.ToBase64String(user.image, 0, user.image.Length)}";
                } else
                {
                    userProfileImage.ImageUrl = "https://www.unitedway.ca/wp-content/uploads/2017/06/TempProfile.jpg";
                }
            }
        }

        protected void ProductsLink_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/FarmerDetail?id={Utils.Auth.getUserCookieData()[0]}");
        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            Utils.Auth.signOut();
            Response.Redirect("/Login");
        }

        protected void EditUserBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect($"/EditUser?id={userID}");
        }
    }
}