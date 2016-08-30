using System;
using CSCore.CoreAudioAPI;

namespace DeviceNotification
{
    public static class Program
    {
        public static void Main()
        {
            var notificationClient = new MMNotificationClient();
            notificationClient.DeviceAdded += OnDeviceAdded;
            notificationClient.DeviceRemoved += OnDeviceRemoved;
            notificationClient.DeviceStateChanged += OnDeviceStateChanged;
            notificationClient.DefaultDeviceChanged += OnDefaultDeviceChanged;
            notificationClient.DevicePropertyChanged += OnDevicePropertyChanged;

            Console.WriteLine("-- Sound devices ------------------");
            using (var enumerator = new MMDeviceEnumerator())
            {
                foreach (var mmDevice in enumerator.EnumAudioEndpoints(DataFlow.All, DeviceState.All))
                {
                    Console.WriteLine($"{mmDevice.FriendlyName}");
                }
            }
            Console.WriteLine("-----------------------------------\n");

            Console.WriteLine("-- Device notifications -----------");
            Console.ReadKey();
        }

        private static void OnDefaultDeviceChanged(object sender, DefaultDeviceChangedEventArgs e)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var mmDevice = enumerator.GetDevice(e.DeviceId))
            {
                Console.WriteLine($"{mmDevice.FriendlyName} became default {e.Role} {e.DataFlow} device");
            }
        }

        private static void OnDeviceStateChanged(object sender, DeviceStateChangedEventArgs e)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var mmDevice = enumerator.GetDevice(e.DeviceId))
            {
                Console.WriteLine($"{mmDevice.FriendlyName} state changed to {e.DeviceState}");
            }
        }

        private static void OnDeviceRemoved(object sender, DeviceNotificationEventArgs e)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var mmDevice = enumerator.GetDevice(e.DeviceId))
            {
                Console.WriteLine($"{mmDevice.FriendlyName} removed");
            }
        }

        private static void OnDeviceAdded(object sender, DeviceNotificationEventArgs e)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var mmDevice = enumerator.GetDevice(e.DeviceId))
            {
                Console.WriteLine($"{mmDevice.FriendlyName} added");
            }
        }

        private static void OnDevicePropertyChanged(object sender, DevicePropertyChangedEventArgs e)
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var mmDevice = enumerator.GetDevice(e.DeviceId))
            {
                Console.WriteLine($"{mmDevice.FriendlyName} property changed");
            }
        }
    }
}