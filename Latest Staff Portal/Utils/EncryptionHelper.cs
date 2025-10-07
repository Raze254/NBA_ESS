using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{
    // Encrypt the document number
    public static string EncryptDocumentNo(string docNo)
    {
        return Encrypt(docNo, docNo);
    }

    // Decrypt the document number
    public static string DecryptDocumentNo(string encryptedDocNo)
    {
        return Decrypt(encryptedDocNo, encryptedDocNo);
    }

    // Encryption logic
    private static string Encrypt(string plainText, string docNo)
    {
        byte[] keyBytes = GenerateKeyFromDocNo(docNo, 32);
        byte[] ivBytes = GenerateKeyFromDocNo(docNo, 16);

        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = ivBytes;
        aes.Padding = PaddingMode.PKCS7;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

        byte[] encryptedBytes;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
                encryptedBytes = ms.ToArray();
            }
        }

        return Convert.ToBase64String(encryptedBytes);
    }

    // Decryption logic
    private static string Decrypt(string encryptedText, string docNo)
    {
        byte[] keyBytes = GenerateKeyFromDocNo(docNo, 32);
        byte[] ivBytes = GenerateKeyFromDocNo(docNo, 16);
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using Aes aes = Aes.Create();
        aes.Key = keyBytes;
        aes.IV = ivBytes;
        aes.Padding = PaddingMode.PKCS7;

        using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var ms = new MemoryStream(encryptedBytes);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var resultStream = new MemoryStream();
        cs.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }

    // Key generation logic
    private static byte[] GenerateKeyFromDocNo(string docNo, int length)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] docNoBytes = Encoding.UTF8.GetBytes(docNo);
        byte[] hashBytes = sha256.ComputeHash(docNoBytes);

        byte[] keyBytes = new byte[length];
        Array.Copy(hashBytes, keyBytes, Math.Min(length, hashBytes.Length));
        return keyBytes;
    }
}

