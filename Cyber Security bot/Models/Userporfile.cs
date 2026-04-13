using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Media;

namespace CyberSecurityAwarenessBot.Services
{
    public static class AudioPlaybackService
    {
        public static void PlayGreeting(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var player = GetPlayer(filePath);
                    player.PlaySync();
                }
                else
                {
                    Console.WriteLine("[Audio file not found. Continuing without voice greeting.]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Could not play audio: {ex.Message}]");
            }
        }

        private static SoundPlayer GetPlayer(string filePath)
        {
            return new SoundPlayer(filePath);
        }
    }
}