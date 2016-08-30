using System;
using CSCore.CoreAudioAPI;

namespace SessionNotification
{
    public static class Program
    {
        public static void Main()
        {
            using (var enumerator = new MMDeviceEnumerator())
            using (var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia))
            using (var sessionManager2 = AudioSessionManager2.FromMMDevice(device))
            {
                Console.WriteLine("-- Sessions on default multimedia playback device --");
                foreach (var sessionControl in sessionManager2.GetSessionEnumerator())
                {
                    using (var sessionControl2 = sessionControl.QueryInterface<AudioSessionControl2>())
                    {
                        Console.WriteLine(sessionControl2.Process?.ProcessName ?? "System");
                    }
                    var sessionEvents = new AudioSessionEvents();
                    sessionEvents.SimpleVolumeChanged += OnSimpleVolumeChanged;
                    sessionControl.RegisterAudioSessionNotification(sessionEvents);
                }
                Console.WriteLine("----------------------------------------------------\n");

                Console.WriteLine("-- Volume notifications ----------------------------");
                Console.ReadKey();
            }
        }

        private static void OnSimpleVolumeChanged(object sender, AudioSessionSimpleVolumeChangedEventArgs e)
        {
            Console.WriteLine($"Volume or mute changed: new volume {e.NewVolume}, new mute state {e.IsMuted}");
        }
    }
}