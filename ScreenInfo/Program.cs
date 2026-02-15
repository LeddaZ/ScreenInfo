using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace ScreenInfo
{
    public class Program
    {
        public static void Main()
        {
            List<MonitorHelper.MonitorDetails> monitors = MonitorHelper.GetAllMonitors();

            foreach (MonitorHelper.MonitorDetails monitor in monitors)
            {
                Console.WriteLine($"\nMonitor: {monitor.DeviceName}");
                Console.WriteLine($"Scaled Resolution: {monitor.ScaledWidth}x{monitor.ScaledHeight}");
                Console.WriteLine($"Physical Resolution: {monitor.PhysicalWidth}x{monitor.PhysicalHeight}");
                Console.WriteLine($"DPI: {monitor.DpiX}x{monitor.DpiY}");
                Console.WriteLine($"Scaling Factor: {monitor.ScalingFactor:P0}"); // Shows as percentage
                Console.WriteLine($"Position: ({monitor.Left}, {monitor.Top})");
            }
            Console.WriteLine();

            string registryPath = @"SYSTEM\CurrentControlSet\Enum\DISPLAY";

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                if (key != null)
                {
                    foreach (string subKeyName in key.GetSubKeyNames())
                    {
                        using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                        {
                            if (subKey != null)
                            {
                                foreach (string deviceKeyName in subKey.GetSubKeyNames())
                                {
                                    using (RegistryKey deviceKey = subKey.OpenSubKey(deviceKeyName + @"\Device Parameters"))
                                    {
                                        if (deviceKey != null)
                                        {
                                            if (deviceKey.GetValue("EDID") is byte[] edid && edid.Length >= 128)
                                            {
                                                string monitorName = MonitorHelper.GetMonitorName(edid);
                                                Console.WriteLine($"Monitor: {monitorName}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
