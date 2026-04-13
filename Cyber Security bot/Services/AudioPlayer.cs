using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;
using System.Runtime.InteropServices;

namespace CyberSecurityAwarenessBot.Services
{
    public static class AudioPlayer
    {
        // Windows Media Player COM object for wider format support
        private static dynamic _mediaPlayer;

        static AudioPlayer()
        {
            try
            {
                // Initialize Windows Media Player for better format support
                Type mediaPlayerType = Type.GetTypeFromProgID("WMPlayer.OCX");
                if (mediaPlayerType != null)
                {
                    _mediaPlayer = Activator.CreateInstance(mediaPlayerType);
                }
            }
            catch
            {
                _mediaPlayer = null;
            }
        }

        public static void PlayGreeting(string filePath)
        {
            try
            {
                // Check if file path is null or empty
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    Console.WriteLine("[Error: No audio file path provided.]");
                    return;
                }

                // Resolve full path
                string fullPath = ResolveFilePath(filePath);

                // Check if file exists
                if (!File.Exists(fullPath))
                {
                    Console.WriteLine($"[Audio file not found: {fullPath}]");
                    Console.WriteLine("[Checking common locations...]");

                    // Try alternative locations
                    List<string> alternativePaths = GetAlternativePaths(filePath);
                    bool found = false;

                    foreach (string altPath in alternativePaths)
                    {
                        if (File.Exists(altPath))
                        {
                            Console.WriteLine($"[Found audio at: {altPath}]");
                            fullPath = altPath;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("[No audio file found in any location. Continuing without voice greeting.]");
                        return;
                    }
                }

                // Check file extension and play accordingly
                string extension = Path.GetExtension(fullPath).ToLower();

                Console.WriteLine($"[Playing audio: {Path.GetFileName(fullPath)}]");

                switch (extension)
                {
                    case ".wav":
                        PlayWavFile(fullPath);
                        break;

                    case ".mp3":
                    case ".wma":
                    case ".m4a":
                        PlayMediaFile(fullPath);
                        break;

                    default:
                        Console.WriteLine($"[Unsupported audio format: {extension}. Supported formats: .wav, .mp3, .wma, .m4a]");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Could not play audio: {ex.Message}]");
                Console.WriteLine($"[Error details: {ex.GetType().Name}]");
            }
        }

        private static string ResolveFilePath(string filePath)
        {
            // If it's already a full path, just return it
            if (Path.IsPathRooted(filePath))
            {
                return filePath;
            }

            // Try multiple locations
            string[] possibleDirectories = {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets"),
                Directory.GetCurrentDirectory(),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CyberSecurityBot", "Audio"),
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
            };

            foreach (string dir in possibleDirectories)
            {
                if (dir == null) continue;

                string fullPath = Path.Combine(dir, Path.GetFileName(filePath));
                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            // Return original path if not found
            return filePath;
        }

        private static List<string> GetAlternativePaths(string originalPath)
        {
            List<string> alternatives = new List<string>();
            string fileName = Path.GetFileName(originalPath);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(originalPath);

            // Try different extensions
            string[] extensions = { ".wav", ".mp3", ".wma", ".m4a" };
            foreach (string ext in extensions)
            {
                string altFileName = nameWithoutExt + ext;
                foreach (string dir in GetSearchDirectories())
                {
                    string altPath = Path.Combine(dir, altFileName);
                    if (File.Exists(altPath))
                    {
                        alternatives.Add(altPath);
                    }
                }
            }

            return alternatives.Distinct().ToList();
        }

        private static string[] GetSearchDirectories()
        {
            return new[]
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"),
                Directory.GetCurrentDirectory(),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "CyberSecurityBot", "Audio")
            };
        }

        private static void PlayWavFile(string filePath)
        {
            try
            {
                using (SoundPlayer player = new SoundPlayer(filePath))
                {
                    player.PlaySync();
                    Console.WriteLine("[Audio played successfully]");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error playing WAV file: {ex.Message}]");
                throw;
            }
        }

        private static void PlayMediaFile(string filePath)
        {
            try
            {
                if (_mediaPlayer != null)
                {
                    _mediaPlayer.URL = filePath;
                    _mediaPlayer.controls.play();

                    // Wait for playback to finish (simple approach)
                    System.Threading.Thread.Sleep(100);
                    while (_mediaPlayer.playState != 1 && _mediaPlayer.playState != 12) // 1=Stopped, 12=MediaEnded
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    Console.WriteLine("[Audio played successfully]");
                }
                else
                {
                    // Fallback for MP3 files using System.Media.SoundPlayer (only works for WAV)
                    Console.WriteLine("[Windows Media Player not available. MP3 playback not supported without external library.]");
                    Console.WriteLine("[Consider converting your audio to WAV format for better compatibility.]");

                    // Alternative: Use NAudio or other libraries (commented out as it requires additional NuGet packages)
                    // PlayMp3WithNAudio(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error playing media file: {ex.Message}]");
            }
        }

        // Alternative method using NAudio library (requires NuGet package: Install-Package NAudio)
        /*
        private static void PlayMp3WithNAudio(string filePath)
        {
            try
            {
                using (var reader = new NAudio.Wave.Mp3FileReader(filePath))
                using (var waveOut = new NAudio.Wave.WaveOutEvent())
                {
                    waveOut.Init(reader);
                    waveOut.Play();
                    
                    while (waveOut.PlaybackState == NAudio.Wave.PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error playing MP3: {ex.Message}]");
            }
        }
        */

        public static void PlayBeep()
        {
            try
            {
                Console.Beep(800, 200);
            }
            catch
            {
                // Ignore beep errors
            }
        }

        public static void PlaySuccessSound()
        {
            try
            {
                Console.Beep(1000, 150);
                System.Threading.Thread.Sleep(50);
                Console.Beep(1200, 200);
            }
            catch
            {
                // Ignore beep errors
            }
        }

        public static void PlayErrorSound()
        {
            try
            {
                Console.Beep(400, 300);
                System.Threading.Thread.Sleep(100);
                Console.Beep(300, 300);
            }
            catch
            {
                // Ignore beep errors
            }
        }

        public static bool TestAudioFile(string filePath)
        {
            string fullPath = ResolveFilePath(filePath);

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"[Audio file test failed: File not found at {fullPath}]");
                return false;
            }

            Console.WriteLine($"[Audio file found: {fullPath}]");
            Console.WriteLine($"[File size: {new FileInfo(fullPath).Length} bytes]");
            Console.WriteLine($"[File extension: {Path.GetExtension(fullPath)}]");

            // Try to read file to verify it's not corrupted
            try
            {
                byte[] fileData = File.ReadAllBytes(fullPath);
                Console.WriteLine($"[File read successfully: {fileData.Length} bytes]");

                if (fileData.Length == 0)
                {
                    Console.WriteLine("[Warning: File is empty!]");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error reading file: {ex.Message}]");
                return false;
            }
        }

        public static void ListAvailableAudioFiles()
        {
            Console.WriteLine("[Searching for audio files...]");
            Console.WriteLine("========================================");

            string[] searchPatterns = { "*.wav", "*.mp3", "*.wma", "*.m4a" };
            var foundFiles = new List<string>();

            foreach (string dir in GetSearchDirectories())
            {
                if (!Directory.Exists(dir)) continue;

                foreach (string pattern in searchPatterns)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(dir, pattern);
                        foreach (string file in files)
                        {
                            foundFiles.Add(file);
                        }
                    }
                    catch
                    {
                        // Skip directories we can't access
                    }
                }
            }

            if (foundFiles.Any())
            {
                Console.WriteLine("[Audio files found:]");
                foreach (string file in foundFiles.Distinct())
                {
                    Console.WriteLine($"  • {file}");
                }
            }
            else
            {
                Console.WriteLine("[No audio files found in any search location]");
                Console.WriteLine("[Search locations:]");
                foreach (string dir in GetSearchDirectories())
                {
                    Console.WriteLine($"  • {dir}");
                }
            }
            Console.WriteLine("========================================");
        }
    }
}