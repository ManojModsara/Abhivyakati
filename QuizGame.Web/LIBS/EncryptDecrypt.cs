using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// Summary description for EncryptDecrypt
/// </summary>
public static class EncryptDecrypt
{

    public static String Encrypt(string source)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] Key = { 12, 13, 14, 15, 16, 17, 18, 19 };
        byte[] IV = { 12, 13, 14, 15, 16, 17, 18, 19 };

        ICryptoTransform encryptor = des.CreateEncryptor(Key, IV);

        try
        {
            byte[] IDToBytes = ASCIIEncoding.ASCII.GetBytes(source);
            byte[] encryptedID = encryptor.TransformFinalBlock(IDToBytes, 0, IDToBytes.Length);
            return Convert.ToBase64String(encryptedID);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static string Decrypt(string encrypted)
    {
        byte[] Key = { 12, 13, 14, 15, 16, 17, 18, 19 };
        byte[] IV = { 12, 13, 14, 15, 16, 17, 18, 19 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        ICryptoTransform decryptor = des.CreateDecryptor(Key, IV);

        try
        {
            byte[] encryptedIDToBytes = Convert.FromBase64String(encrypted.Replace(" ", "+"));
            byte[] IDToBytes = decryptor.TransformFinalBlock(encryptedIDToBytes, 0, encryptedIDToBytes.Length);
            return ASCIIEncoding.ASCII.GetString(IDToBytes);
        }
        catch (FormatException)
        {
            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public static string GetUniqueKey()
    {
        int maxSize = 10;
        int minSize = 7;
        char[] chars = new char[104];
        string a;
        a = "1234567890!@$%^*()_ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^*()_abcdefghijklmnopqrstuvwxyz";
        chars = a.ToCharArray();
        int size = minSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {

            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }

    public static string GetUniqueKeypass()
    {
        int maxSize = 6;
        int minSize = 6;
        char[] chars = new char[104];
        string a;
        a = "1234567890";
        chars = a.ToCharArray();
        int size = minSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {

            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }

    public static string GetUniqueKeyPin()
    {
        int maxSize = 5;
        int minSize = 5;
        char[] chars = new char[104];
        string a;
        a = "123456789";
        chars = a.ToCharArray();
        int size = minSize;
        byte[] data = new byte[1];
        RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        crypto.GetNonZeroBytes(data);
        size = maxSize;
        data = new byte[size];
        crypto.GetNonZeroBytes(data);
        StringBuilder result = new StringBuilder(size);
        foreach (byte b in data)
        {

            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }
}