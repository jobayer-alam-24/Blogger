using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogger.Helpers.CustomAttributes
{
    public static class Extensions
    {
        public static string GetBlogUrl(this string value)
        {
            return value.Replace(' ', '-').ToLower();
        }
    }
}