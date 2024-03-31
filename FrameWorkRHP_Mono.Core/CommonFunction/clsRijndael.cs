using System.Security.Cryptography;
using System.Text;

namespace FrameWorkRHP_Mono.Core.CommonFunction
{
    public class clsRijndael
    {  
        public static string Encrypt(string stringInputText)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(clsGlobalConstant.EncryptionKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(clsGlobalConstant.EncryptionKey, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(CipherBytes);
        }
         
        public static string Encrypt(string stringInputText, string stringKey)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(stringKey, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(CipherBytes);
        }
         
        public static string Decrypt(string stringInputText)
        {
            using RijndaelManaged RijndaelCipher = new();
            byte[] EncryptedData = Convert.FromBase64String(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(clsGlobalConstant.EncryptionKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(clsGlobalConstant.EncryptionKey, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            using MemoryStream memoryStream = new(EncryptedData);
            using CryptoStream cryptoStream = new(memoryStream, Decryptor, CryptoStreamMode.Read);
            //byte[] PlainText = new byte[EncryptedData.Length];
            //int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            using MemoryStream plainTextStream = new();
            cryptoStream.CopyTo(plainTextStream);
            byte[] plainTextBytes = plainTextStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            plainTextStream.Close();
            return Encoding.Unicode.GetString(plainTextBytes, 0, plainTextBytes.Length);
        }
         
        public static string Decrypt(string stringInputText, string stringKey)
        {
            using RijndaelManaged RijndaelCipher = new();
            byte[] EncryptedData = Convert.FromBase64String(stringInputText);
            byte[] Salt = Encoding.ASCII.GetBytes(stringKey.Length.ToString());
            PasswordDeriveBytes SecretKey = new(stringKey, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            using MemoryStream memoryStream = new(EncryptedData);
            using CryptoStream cryptoStream = new(memoryStream, Decryptor, CryptoStreamMode.Read);
            //byte[] PlainText = new byte[EncryptedData.Length];
            //int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            using MemoryStream plainTextStream = new();
            cryptoStream.CopyTo(plainTextStream);
            byte[] plainTextBytes = plainTextStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            plainTextStream.Close();
            return Encoding.Unicode.GetString(plainTextBytes, 0, plainTextBytes.Length);
        }

    }

}
