using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class EditUser : System.Web.UI.Page
    {
        // entity framework instance for db interactions
        private StockManagementEntities entities;

        // id of farmer
        int userID;

        // prev page
        private string prevPage = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // set the postback
            if (!IsPostBack)
            {
                try
                {
                    prevPage = Request.UrlReferrer.ToString();
                } catch (Exception err)
                {
                    prevPage = "/Default";
                }
            }
            // set farmer id
            try
            {
                userID = int.Parse(Request.QueryString["id"]);
            }
            catch
            {
                Response.Redirect("/404");
            }

            // check authority
            if (Utils.Auth.getUserCookieData()[1] != "admin")
            {
                if (!Utils.Auth.isUserIdAcceptable(userID.ToString()))
                {
                    Response.Redirect("/401");
                }
            }

            // fetch product data
            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    User user = this.entities.Users.SingleOrDefault(u => u.ID == userID);

                    if (user == null)
                    {
                        Response.Redirect("/404");
                    }
                    else
                    {
                        if (user.image != null)
                        {
                            this.userImage.ImageUrl = $"data:{user.imageContentType};base64,{Convert.ToBase64String(user.image, 0, user.image.Length)}";
                        }
                        else
                        {
                            this.userImage.ImageUrl = "https://www.unitedway.ca/wp-content/uploads/2017/06/TempProfile.jpg";
                        }

                        this.FirstName.Text = user.FirstName;
                        this.LastName.Text = user.LastName;
                        this.UserEmail.Text = user.Email;
                        this.UserRole.Text = user.Role;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            // reset error message box
            errorMessageBox.Text = "";

            // set text fields
            string userFirstName = FirstName.Text;
            string userLastName = LastName.Text;
            string userEmail = UserEmail.Text;
            string userRole = UserRole.SelectedValue;
            string userPassword = UserPassword.Text;

            if (!Utils.Auth.IsValidEmail(userEmail))
            {
                EmailRequiredValidator.ErrorMessage = "Email is invalid email";
                errorMessageBox.Text = "Email is invalid email";
            }
            else
            {
                try
                {
                    using (this.entities = new StockManagementEntities())
                    {
                        User newUser = entities.Users.FirstOrDefault(user => user.ID == userID);
                        newUser.Email = userEmail;
                        newUser.FirstName = userFirstName;
                        newUser.LastName = userLastName;
                        newUser.Role = userRole;

                        if(!String.IsNullOrEmpty(userPassword))
                        {
                            newUser.Password = Utils.Auth.ComputeHash(userPassword);
                        }

                        // Save image if uploaded 
                        if (imageUplodaBox.HasFile && imageUplodaBox.PostedFile != null)
                        {
                            byte[] imageBytes;
                            BinaryReader br = new System.IO.BinaryReader(imageUplodaBox.PostedFile.InputStream);
                            imageBytes = br.ReadBytes((Int32)imageUplodaBox.PostedFile.InputStream.Length);
                            newUser.image = imageBytes;
                            newUser.imageContentType = imageUplodaBox.PostedFile.ContentType;
                        }

                        this.entities.SaveChanges();
                    }
                    Response.Redirect(prevPage);
                }
                catch (Exception err)
                {
                    System.Diagnostics.Trace.WriteLine(err);
                    errorMessageBox.Text = "An unexpected error occured when creating user, please rety";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(prevPage);
        }
    }
}