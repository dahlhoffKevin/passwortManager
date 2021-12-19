using System;
using System.Text;
using System.Security.Cryptography;

namespace EncryptionHelper
{
	public class EncryptionHelper
	{
		public static string Encrypt(string text)
		{
			var b = Encoding.UTF8.GetBytes(text);
			var encrypted = getAes().CreateEncryptor().TransformFinalBlock(b, 0, b.Length);
			return Convert.ToBase64String(encrypted);
		}

		public static string Decrypt(string encrypted)
		{
			var b = Convert.FromBase64String(encrypted);
			var decrypted = getAes().CreateDecryptor().TransformFinalBlock(b, 0, b.Length);
			return Encoding.UTF8.GetString(decrypted);
		}

		static Aes getAes()
		{
			var keyBytes = new byte[16];
			var skeyBytes = Encoding.UTF8.GetBytes("A2CDb7Oc3E0eZ85THaS1JW6Y9hlLIUI4");
			Array.Copy(skeyBytes, keyBytes, Math.Min(keyBytes.Length, skeyBytes.Length));

			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			aes.KeySize = 128;
			aes.Key = keyBytes;
			aes.IV = keyBytes;

			return aes;
		}

	}
}