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
                // do auth
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
            // check validity of email
            if(email.Equals(String.Empty) || email.Equals(null)) {
                System.Diagnostics.Trace.WriteLine("[ValidatingUser] validation of email failed.");
                return null;
            }
            // check validity of password
            if (password.Equals(String.Empty) || password.Equals(null)) {
                System.Diagnostics.Trace.WriteLine("[ValidatingUser] validation of password failed.");
                return null;
            }

            try
            {
                using (this.entities = new StockManagementEntities())
                {
                    User targetUser = this.entities.Users.SingleOrDefault(user => user.Email == email);

                    if(targetUser != null)
                    {
                        if(validPassword(password, targetUser.Password))
                        {
                            return targetUser;
                        }
                        else
                        {
                            System.Diagnostics.Trace.WriteLine("[ValidatingUser] Invalid password.");
                            return null;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidatingUser] Invalid user.");
                        return null;
                    }
                }
            } 
            catch(Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
                return null;
            }
        }

        private bool validPassword(String EnteredPassword, String targetPassword)
        {
            return (targetPassword == ComputeHash(EnteredPassword));
        }

        private string ComputeHash(string password)
        {
            HashAlgorithm sha256 = SHA256CryptoServiceProvider.Create();

            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            Byte[] hashedBytes = sha256.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}