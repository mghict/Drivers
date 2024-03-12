using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions;

public static class StringExtensions
{
    public static string Hash(this string input)
    {
        string resultAsHash = string.Empty;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashValue1 = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            resultAsHash = Convert.ToHexString(hashValue1);
        }
        return resultAsHash;

    }

    public static long GuidToLong(this Guid id)
    {
        byte[] gb = id.ToByteArray();
        return BitConverter.ToInt64(gb, 0);
    }

    public static int GuidToInt(this Guid id)
    {
        byte[] gb = id.ToByteArray();
        return BitConverter.ToInt32(gb, 0);
    }

    public static decimal ToDecimal(this string? input)
    {
        if(string.IsNullOrWhiteSpace(input)) return 0;
        try
        {
            return Convert.ToDecimal(input);
        }
        catch
        {
            return 0;
        }
    }

    public static string ToBase64(this byte[]? input)
    {
        if (input is null)
            return string.Empty;

        try
        {
            return Convert.ToBase64String(input);
        }
        catch
        {
            return string.Empty;
        }
    }
}

