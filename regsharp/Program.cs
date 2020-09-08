using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Security.Permissions;

namespace regsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathlist = new List<string>();
            RegistryKey x;
            x = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Services");
            // The name of the key must include a valid root.
            const string userRoot = "HKEY_LOCAL_MACHINE";
            const string subkey = "SYSTEM\\ControlSet001\\Services";
            const string keyName = userRoot + "\\" + subkey;
            // Console.WriteLine(x.SubKeyCount.ToString());
            foreach (string subKeyName in x.GetSubKeyNames())
            {
                using (RegistryKey
                    tempKey = x.OpenSubKey(subKeyName))
                {

                    try
                    {

                        //Console.WriteLine(tempKey.GetValue("ImagePath").ToString());
                        pathlist.Add((tempKey.GetValue("ImagePath").ToString()));
                    }
                    catch
                    {

                    }
                }


            }

            foreach (string path in pathlist)
            {
                if (path.Contains(" "))
                {
                    if (!path.Contains("\""))
                    {
                        if (!path.Contains("-"))
                        {


                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(path + " Is vulnerable");
                        }
                    }
                }
            }
            Console.ReadLine();
        }
    }
}