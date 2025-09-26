using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Core.Helpers
{
    public static class EmailHelper
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            if (email.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
