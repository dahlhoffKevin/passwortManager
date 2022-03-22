using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using PasswordManagerLogger;
using System.Linq;

namespace EncryptionHelper
{
	public class EncryptionHelper
	{
		#pragma warning disable SYSLIB0023
		static RNGCryptoServiceProvider provider = new();
		public static string get_master_password()
        {
			#pragma warning disable CS8602
			string masterpassword_file = Path.GetPathRoot(Environment.GetEnvironmentVariable("WINDIR")) +
									 ConfigurationManager.AppSettings["pwd_manager_folder"].ToString() +
									 ConfigurationManager.AppSettings["userdata_folder_path"].ToString() +
									 ConfigurationManager.AppSettings["master_password_file"].ToString() +
									 ConfigurationManager.AppSettings["pwd_file_ending"].ToString();

			string encrypted_master_password = "";
			try
			{
				encrypted_master_password = File.ReadAllText(masterpassword_file);

			}
			catch (Exception ex)
			{
				Logger.WriteLog(ex.ToString(), "ERROR");
			}

			string decrypted_master_password = "";
			try
			{
				decrypted_master_password = Decrypt(encrypted_master_password,  true);
			}
			catch (Exception ex)
			{
				Logger.WriteLog(ex.ToString(), "ERROR");
			}
			return decrypted_master_password;
		}

		// String Generator
		private static char GenerateChar(string availableChars)
		{
			var byteArray = new byte[1];
			char c;
			do
			{
				provider.GetBytes(byteArray);
				c = (char)byteArray[0];

			} while (!availableChars.Any(x => x == c));
			return c;
		}
		public static string generated_string()
        {
			string CapitalLetters = ConfigurationManager.AppSettings["capital_letters"].ToString();
			string SmallLetters = ConfigurationManager.AppSettings["small_letters"].ToString();
			string Digits = ConfigurationManager.AppSettings["digits"].ToString();
			string AllChar = CapitalLetters + SmallLetters + Digits;
			int string_length = 32;
			string pwd;

			StringBuilder sb = new StringBuilder();
			for (int n = 0; n < string_length; n++)
			{
				sb = sb.Append(GenerateChar(AllChar));
			}

			pwd = sb.ToString();

			return pwd;
		}

		// Verschlüsselung
		public static string Encrypt(string text, bool master)
		{
			var b = Encoding.UTF8.GetBytes(text);
			var encrypted = new byte[16];
			if (master)
            {
				encrypted = getAesMaster().CreateEncryptor().TransformFinalBlock(b, 0, b.Length);
			}
            else
            {
				encrypted = getAes().CreateEncryptor().TransformFinalBlock(b, 0, b.Length);
			}
			return Convert.ToBase64String(encrypted);
		}

		// Entschlüsselung
		public static string Decrypt(string encrypted, bool master)
		{
			var b = Convert.FromBase64String(encrypted);
			var decrypted = new byte[16];
			if (master)
            {
				decrypted = getAesMaster().CreateDecryptor().TransformFinalBlock(b, 0, b.Length);
			}
            else
            {
				decrypted = getAes().CreateDecryptor().TransformFinalBlock(b, 0, b.Length);
			}
			return Encoding.UTF8.GetString(decrypted);
		}

		// Key generierung
		static Aes getAes()
		{
			var keyBytes = new byte[16];
			var skeyBytes = Encoding.UTF8.GetBytes("A2CDb7Oc3E0eZ85THaS1JW6Y9hlLIUI4");
			Array.Copy(skeyBytes, keyBytes, Math.Min(keyBytes.Length, skeyBytes.Length));

			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			aes.KeySize = 256;
			aes.Key = keyBytes;
			aes.IV = keyBytes;

			return aes;
		}
		static Aes getAesMaster()
		{
			string master_password = get_master_password();
			var keyBytes = new byte[16];
			var skeyBytes = Encoding.UTF8.GetBytes(master_password + "A2CDb7Oc3E0eZ85THaS1JW6Y9hlLIUI4");
			Array.Copy(skeyBytes, keyBytes, Math.Min(keyBytes.Length, skeyBytes.Length));

			Aes aes = Aes.Create();
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			aes.KeySize = 256;
			aes.Key = keyBytes;
			aes.IV = keyBytes;

			return aes;
		}
	}
}