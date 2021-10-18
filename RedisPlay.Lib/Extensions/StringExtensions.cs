using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Lib.Extensions
{
    public static class StringExtensions
    {
        public static MemoryStream ToStream(this string str)
            => string.IsNullOrWhiteSpace(str)
                ? null
                    : new MemoryStream(Encoding.UTF8.GetBytes(str));
    }
}
