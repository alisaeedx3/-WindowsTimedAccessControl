using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace UTIL
{
    public static class clsUtil
    {
        private static readonly string Key = "0123456789123456";
        private static readonly string IV = "0123456789123456";
        private static readonly string FileName = "LockSettings.txt";

        public static string SettingFile => Path.Combine(Application.StartupPath, FileName);

        public static void SaveData(Data data)
        {
            File.WriteAllText(SettingFile, EncryptDecrypt(JsonConvert.SerializeObject(data)));
        }

        public static Data ReadData()
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, FileName)))
                return null;

            return JsonConvert.DeserializeObject<Data>(EncryptDecrypt(File.ReadAllText(SettingFile), encrypt: false));
        }

        private static string EncryptDecrypt(string data, bool encrypt = true)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    ICryptoTransform transform = encrypt ? aes.CreateEncryptor(aes.Key, aes.IV) : aes.CreateDecryptor(aes.Key, aes.IV);
                    using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        if (encrypt)
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(data);
                            }
                        }
                        else
                        {
                            // Correct decryption flow: Read encrypted data, convert back to string
                            byte[] cipherBytes = Convert.FromBase64String(data);
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.FlushFinalBlock();
                        }
                    }

                    if (encrypt)
                    {
                        return Convert.ToBase64String(ms.ToArray());
                    }
                    else
                    {
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }

    public class Data
    {
        public DateTime LockStartTime { get; set; }
        public DateTime LockEndTime { get; set; }
        public string LockPass { get; set; }
        public string UnlockPass { get; set; }
        public bool IsAllUsers { get; set; }
        public string UserName { get; set; }
    }
}
