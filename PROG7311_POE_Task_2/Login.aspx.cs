using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PROG7311_POE_Task_2
{
    public partial class Login : System.Web.UI.Page
    {

        // entity framework instance for db interactions
        private StockManagementEntities entities;

        // form fields
        private TextBox emailBox;
        private TextBox passwordBox;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.emailBox = this.UserEmail;
            this.passwordBox = this.UserPassword;
        }

        protected void login_click(Object sender, EventArgs e)
        {
            User user = validateUser(this.emailBox.Text, this.passwordBox.Text);

            if(user != null)
            {
                FormsAuthenticationTicket ticket;
                string cookiestr;
                HttpCookie cookie;
                ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now,
                DateTime.Now.AddMinutes(30), this.ChboxStayLoggedIn.Checked, $"{user.ID.ToString()},{user.Role}");
                cookiestr = FormsAuthentication.Encrypt(ticket);
                cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                if (this.ChboxStayLoggedIn.Checked)
                    cookie.Expires = ticket.Expiration;
                cookie.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(cookie);

                string strRedirect;
                strRedirect = Request["ReturnUrl"];
                if (user.Role == "admin")
                    strRedirect = "AdminDefault.aspx";
                else
                {
                    strRedirect = "Default.aspx";
                }
                Response.Redirect(strRedirect, true);
            }
        }

        /// <summary>
        /// validate entered credentials
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns> returns User or null if invalid </returns>
        private User validateUser(String email, String password)
        {
            this.PasswordRequiredFieldValidator.Text = "";
            this.UsernameRequiredValidator.Text = "";
            this.errorMessageBox.Text = "";

            // check validity of email
            if (email.Equals(String.Empty) || email.Equals(null)) {
                System.Diagnostics.Trace.WriteLine("[ValidatingUser] validation of email failed.");
                this.UsernameRequiredValidator.Text = "Email cannot be empty";
                return null;
            }
            // check validity of password
            if (password.Equals(String.Empty) || password.Equals(null)) {
                System.Diagnostics.Trace.WriteLine("[ValidatingUser] validation of password failed.");
                this.PasswordRequiredFieldValidator.Text = "Password cannot be empty";
                return null;
            }

            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    User targetUser = this.entities.Users.SingleOrDefault(user => user.Email == email);

                    if(targetUser != null)
                    {

                        if (validPassword(password, targetUser.Password))
                        {
                            return targetUser;
                        }
                        else
                        {
                            System.Diagnostics.Trace.WriteLine("[ValidatingUser] Invalid password.");
                            this.errorMessageBox.Text = "Invalid email and/or password";
                            this.errorMessageBox.Visible = true;
                            return null;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidatingUser] Invalid user.");
                        this.errorMessageBox.Text = "Invalid email and/or password";
                        this.errorMessageBox.Visible = true;
                        return null;
                    }
                }
            } 
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                this.errorMessageBox.Text = ex.ToString();
                return null;
            }
        }

        private bool validPassword(String EnteredPassword, String targetPassword)
        {
            return targetPassword.Trim() == Utils.Auth.ComputeHash(EnteredPassword);
        }
    }
}