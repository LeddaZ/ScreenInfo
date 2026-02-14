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
            Console.ReadKey();
        }
    }
}
