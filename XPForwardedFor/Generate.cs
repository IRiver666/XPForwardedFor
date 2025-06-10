namespace XPForwardedFor;

using System;
using System.Security.Cryptography;
using System.Text;

public static class GenerateXP
{
    private const int TagSize = 16; // 128 bits for GCM tag
    private const int NonceSize = 12; // 96 bits recommended for GCM

    private static string BytesToHex(byte[] bytes)
    {
        var sb = new StringBuilder(bytes.Length * 2);
        foreach (var b in bytes)
            sb.AppendFormat("{0:x2}", b);
        return sb.ToString();
    }

    private static byte[] DeriveAesKey(string mainKey, string guestId)
    {
        using SHA256 sha256 = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(mainKey + guestId);
        return sha256.ComputeHash(combined);
    }

    public static string Encrypt(string plainText, string mainKey, string guestId)
    {
        byte[] key = DeriveAesKey(mainKey, guestId);
        byte[] nonce = new byte[NonceSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(nonce);
        }
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] cipherBytes = new byte[plainBytes.Length];
        byte[] tag = new byte[TagSize];

        // FIX: Use the constructor that accepts tag size to avoid SYSLIB0053 warning
        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag);
        }

        // Concatenate nonce + ciphertext + tag
        byte[] result = new byte[nonce.Length + cipherBytes.Length + tag.Length];
        Buffer.BlockCopy(nonce, 0, result, 0, nonce.Length);
        Buffer.BlockCopy(cipherBytes, 0, result, nonce.Length, cipherBytes.Length);
        Buffer.BlockCopy(tag, 0, result, nonce.Length + cipherBytes.Length, tag.Length);

        return BytesToHex(result);
    }
     public static string Decrypt(string hexString, string mainKey, string guestId)
    {
        byte[] key = DeriveAesKey(mainKey, guestId);
        byte[] raw = HexToBytes(hexString);

        if (raw.Length < NonceSize + TagSize)
            throw new ArgumentException("Input data too short.");

        byte[] nonce = new byte[NonceSize];
        Buffer.BlockCopy(raw, 0, nonce, 0, NonceSize);

        int cipherTextLength = raw.Length - NonceSize - TagSize;
        byte[] cipherText = new byte[cipherTextLength];
        Buffer.BlockCopy(raw, NonceSize, cipherText, 0, cipherTextLength);

        byte[] tag = new byte[TagSize];
        Buffer.BlockCopy(raw, NonceSize + cipherTextLength, tag, 0, TagSize);

        byte[] plainBytes = new byte[cipherText.Length];
        using (var aesGcm = new AesGcm(key, TagSize))
        {
            aesGcm.Decrypt(nonce, cipherText, tag, plainBytes);
        }

        return Encoding.UTF8.GetString(plainBytes);
    }
}
