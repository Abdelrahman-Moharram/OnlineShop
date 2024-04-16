using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShop.Infrastructure.Helper
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string str)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(str);
            if (match.Success)
                return true;
            return false;
        }
    }
}
