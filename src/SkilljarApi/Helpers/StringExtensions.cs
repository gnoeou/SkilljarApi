using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Helpers
{
    internal static class StringExtensions
    {
        public static Uri FormatUri(this string pattern, params object[] args)
        {
            var uriString = string.Format(CultureInfo.InvariantCulture, pattern, args);
            return new Uri(uriString, UriKind.Relative);
        }
    }
}
