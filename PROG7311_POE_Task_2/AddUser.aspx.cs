using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class AddUser : System.Web.UI.Page
    {

        // entity framework instance for db interactions
        private StockManagementEntities entities;


        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
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
            } else
            {
                try
                {
                    using (this.entities = new StockManagementEntities())
                    {
                        User newUser = new User();
                        newUser.Email = userEmail;
                        newUser.Password = Utils.Auth.ComputeHash(userPassword);
                        newUser.FirstName = userFirstName;
                        newUser.LastName = userLastName;
                        newUser.Role = userRole;

                        this.entities.Users.Add(newUser);
                        this.entities.SaveChanges();
                    }
                    Response.Redirect("AdminDefault.aspx");
                } 
                catch (Exception err)
                {
                    System.Diagnostics.Trace.WriteLine(err);
                    errorMessageBox.Text = "An unexpected error occured when creating user, please rety";
                }
            }
        }
    }
}