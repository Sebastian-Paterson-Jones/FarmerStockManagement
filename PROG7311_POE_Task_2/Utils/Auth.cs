using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace PROG7311_POE_Task_2.Utils
{
    public class Auth
    {
        public static string ComputeHash(string password)
        {
            HashAlgorithm sha256 = SHA256CryptoServiceProvider.Create();

            Byte[] inputBytes = Encoding.UTF8.GetBytes(password);

            Byte[] hashedBytes = sha256.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                // normalise format
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // convert unicode types
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e);
                return false;
            }
        }

        public static string[] getUserCookieData()
        {
            HttpCookie authCookie = System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (!string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    return ticket.UserData.Split(',');
                }
            }
            return null;
        }

        public static bool isUserIdAcceptable(string userID)
        {
            String activeUserID = getUserCookieData()[0];
            return userID == activeUserID;
        }

        public static void signOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}