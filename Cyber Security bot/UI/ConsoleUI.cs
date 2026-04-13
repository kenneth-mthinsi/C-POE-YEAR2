using System;
using System.Threading;

namespace CyberSecurityAwarenessBot.UI
{
    public static class ConsoleUI
    {
        // ─── ASCII Art Banner ────────────────────────────────────────────────────
        private static readonly string[] AsciiLogo = new[]
        {
            @"  ██████╗██╗   ██╗██████╗ ███████╗██████╗ ██████╗  ██████╗ ████████╗",
            @" ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔═══██╗╚══██╔══╝",
            @" ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██████╔╝██║   ██║   ██║   ",
            @" ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██╗██║   ██║   ██║   ",
            @" ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██████╔╝╚██████╔╝   ██║   ",
            @"  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═════╝  ╚═════╝   ╚═╝   ",
        };

        private static readonly ConsoleColor[] RainbowColors = new[]
        {
            ConsoleColor.Cyan,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
            ConsoleColor.DarkCyan,
            ConsoleColor.DarkBlue,
        };

        // ─── Boot Sequence ───────────────────────────────────────────────────────
        public static void PlayBootSequence()
        {
            try { Console.Clear(); } catch { }
            Console.CursorVisible = false;

            // Matrix rain effect (short burst)
            MatrixRain(28);

            try { Console.Clear(); } catch { }

            // Animated logo reveal
            DisplayAnimatedLogo();

            Thread.Sleep(400);

            // Scanning bar
            DisplayScanBar("INITIALIZING SECURITY PROTOCOLS");
            Thread.Sleep(200);
            DisplayScanBar("LOADING THREAT DATABASE");
            Thread.Sleep(200);
            DisplayScanBar("ENCRYPTING COMMUNICATION CHANNEL");
            Thread.Sleep(200);
            DisplayScanBar("SYSTEM READY");

            Thread.Sleep(500);
            Console.CursorVisible = true;
        }

        // ─── Header ──────────────────────────────────────────────────────────────
        public static void DisplayHeader()
        {
            try { Console.Clear(); } catch { }

            int maxWidth = 80;
            int width = Math.Min(Console.WindowWidth > 0 ? Console.WindowWidth : maxWidth, maxWidth);
            int innerWidth = Math.Max(20, width - 2);

            string title = "CYBERSECURITY AWARENESS BOT";
            string subtitle = "Stay alert. Stay secure. Stay informed.";

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔" + new string('═', innerWidth) + "╗");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("║" + CenterText(title, innerWidth) + "║");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║" + CenterText(subtitle, innerWidth) + "║");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╚" + new string('═', innerWidth) + "╝");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            TypeText("  Protecting your digital life — one tip at a time.", 12);
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine();
        }

        // ─── Main Menu ───────────────────────────────────────────────────────────
        public static void DisplayMainMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ┌─────────────────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  │              MAIN MENU                          │");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ├─────────────────────────────────────────────────┤");
            Console.ResetColor();

            WriteMenuItem("1", "Learn Cybersecurity Topics", ConsoleColor.Green);
            WriteMenuItem("2", "Cybersecurity Tips of the Day", ConsoleColor.Yellow);
            WriteMenuItem("3", "Interactive Quiz", ConsoleColor.Magenta);
            WriteMenuItem("4", "Ask the Bot", ConsoleColor.Cyan);
            WriteMenuItem("5", "Threat Alert Dashboard", ConsoleColor.Red);
            WriteMenuItem("6", "Exit", ConsoleColor.DarkGray);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  └─────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void WriteMenuItem(string key, string label, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  │  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = color;
            Console.Write(key);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = color;
            Console.Write(label.PadRight(42));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("│");
            Console.ResetColor();
        }

        // ─── Bot / User Messages ─────────────────────────────────────────────────
        public static void WriteBotMessage(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  ╔══ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("BOT");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" ══╗");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  ║  ");
            Console.ResetColor();

            TypeText(message, 18);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ╚══════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteBotMessageFast(string message)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  ╔══ ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("BOT");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(" ══╗");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("  ║  ");
            Console.ResetColor();
            Console.WriteLine(message);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  ╚══════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteUserPrompt(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  " + message);
            Console.ResetColor();
        }

        public static void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  [!] " + message);
            Console.ResetColor();
        }

        public static void WriteSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  [✓] " + message);
            Console.ResetColor();
        }

        public static void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  [⚠] " + message);
            Console.ResetColor();
        }

        public static void WriteInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("  [i] " + message);
            Console.ResetColor();
        }

        // ─── Section Header ──────────────────────────────────────────────────────
        public static void WriteSectionHeader(string title, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.WriteLine();
            Console.ForegroundColor = color;
            string bar = new string('─', 50);
            Console.WriteLine("  ┌" + bar + "┐");
            Console.WriteLine("  │  " + title.PadRight(49) + "│");
            Console.WriteLine("  └" + bar + "┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ─── Tip Box ─────────────────────────────────────────────────────────────
        public static void WriteTipBox(string tip, ConsoleColor color = ConsoleColor.Yellow)
        {
            int boxWidth = 60;
            Console.WriteLine();
            Console.ForegroundColor = color;
            Console.WriteLine("  ╭" + new string('─', boxWidth) + "╮");

            // Word-wrap the tip
            var words = tip.Split(' ');
            string line = "";
            foreach (var word in words)
            {
                if ((line + word).Length > boxWidth - 4)
                {
                    Console.WriteLine("  │  " + line.TrimEnd().PadRight(boxWidth - 2) + "│");
                    line = "";
                }
                line += word + " ";
            }
            if (!string.IsNullOrWhiteSpace(line))
                Console.WriteLine("  │  " + line.TrimEnd().PadRight(boxWidth - 2) + "│");

            Console.WriteLine("  ╰" + new string('─', boxWidth) + "╯");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ─── Quiz Helpers ────────────────────────────────────────────────────────
        public static void WriteQuestion(int number, string question)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"  Q{number}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(question);
            Console.ResetColor();
        }

        public static void WriteQuizOption(char letter, string option)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"     [{letter}] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(option);
            Console.ResetColor();
        }

        public static void WriteQuizResult(bool correct, string explanation)
        {
            if (correct)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  ✔  CORRECT!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  ✘  INCORRECT.");
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("     " + explanation);
            Console.ResetColor();
        }

        public static void WriteScoreboard(int score, int total)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╔══════════════════════════════╗");
            Console.WriteLine("  ║         QUIZ RESULTS         ║");
            Console.WriteLine("  ╠══════════════════════════════╣");

            double pct = (double)score / total * 100;
            ConsoleColor grade = pct >= 80 ? ConsoleColor.Green : pct >= 50 ? ConsoleColor.Yellow : ConsoleColor.Red;

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("  ║  Score: ");
            Console.ForegroundColor = grade;
            Console.Write($"{score}/{total}  ({pct:F0}%)");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("".PadRight(14) + "║");

            string rank = pct >= 90 ? "Security Expert!" : pct >= 70 ? "Cyber Aware!" : pct >= 50 ? "Keep Learning!" : "Needs Practice";
            Console.Write("  ║  Rank:  ");
            Console.ForegroundColor = grade;
            Console.Write(rank.PadRight(21));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("║");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  ╚══════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ─── Threat Dashboard ────────────────────────────────────────────────────
        public static void WriteThreatDashboard()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ╔══════════════════════════════════════════════════╗");
            Console.WriteLine("  ║         ⚠  LIVE THREAT ALERT DASHBOARD  ⚠       ║");
            Console.WriteLine("  ╠══════════════════════════════════════════════════╣");
            Console.ResetColor();

            WriteThreatRow("PHISHING ATTACKS", "CRITICAL", ConsoleColor.Red, "████████████████████ 98%");
            WriteThreatRow("RANSOMWARE", "HIGH", ConsoleColor.Red, "████████████████░░░░ 82%");
            WriteThreatRow("SOCIAL ENGINEERING", "HIGH", ConsoleColor.Yellow, "███████████████░░░░░ 76%");
            WriteThreatRow("WEAK PASSWORDS", "MEDIUM", ConsoleColor.Yellow, "████████████░░░░░░░░ 61%");
            WriteThreatRow("UNPATCHED SOFTWARE", "MEDIUM", ConsoleColor.Yellow, "██████████░░░░░░░░░░ 53%");
            WriteThreatRow("PUBLIC WIFI RISKS", "LOW", ConsoleColor.Green, "███████░░░░░░░░░░░░░ 34%");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  ╚══════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        private static void WriteThreatRow(string threat, string level, ConsoleColor color, string bar)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  ║  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(threat.PadRight(22));
            Console.ForegroundColor = color;
            Console.Write(level.PadRight(10));
            Console.ForegroundColor = color;
            Console.Write(bar.PadRight(22));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("║");
            Console.ResetColor();
        }

        // ─── Progress / Scan Bar ─────────────────────────────────────────────────
        public static void DisplayScanBar(string label)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"  [{label}] ");
            Console.ForegroundColor = ConsoleColor.Green;

            int barLen = 30;
            Console.Write("[");
            for (int i = 0; i < barLen; i++)
            {
                Console.Write("█");
                Thread.Sleep(18);
            }
            Console.Write("]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" DONE");
            Console.ResetColor();
        }

        // ─── Animated Logo ───────────────────────────────────────────────────────
        private static void DisplayAnimatedLogo()
        {
            Console.WriteLine();
            for (int i = 0; i < AsciiLogo.Length; i++)
            {
                Console.ForegroundColor = RainbowColors[i % RainbowColors.Length];
                Console.WriteLine("  " + AsciiLogo[i]);
                Thread.Sleep(80);
            }
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            TypeText("  ╔══════════════════════════════════════════════════════════════════╗", 2);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText("  ║          CYBERSECURITY AWARENESS BOT  v2.0                       ║", 2);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            TypeText("  ╚══════════════════════════════════════════════════════════════════╝", 2);
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine();
        }

        // ─── Matrix Rain ─────────────────────────────────────────────────────────
        private static void MatrixRain(int frames)
        {
            var rng = new Random();
            int cols = Math.Min(Console.WindowWidth > 0 ? Console.WindowWidth : 80, 80);
            int rows = Math.Min(Console.WindowHeight > 0 ? Console.WindowHeight : 24, 24);
            char[] chars = "01アイウエオカキクケコサシスセソタチツテトナニヌネノ".ToCharArray();

            for (int f = 0; f < frames; f++)
            {
                try { Console.SetCursorPosition(0, 0); } catch { }
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols - 1; c++)
                    {
                        if (rng.Next(10) < 3)
                        {
                            Console.ForegroundColor = rng.Next(4) == 0
                                ? ConsoleColor.White
                                : ConsoleColor.DarkGreen;
                            Console.Write(chars[rng.Next(chars.Length)]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(rng.Next(2) == 0 ? '0' : '1');
                        }
                    }
                    Console.WriteLine();
                }
                Thread.Sleep(40);
            }
            Console.ResetColor();
        }

        // ─── Helpers ─────────────────────────────────────────────────────────────
        public static void TypeText(string text, int delay = 20)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
        }

        public static string CenterText(string text, int width)
        {
            if (string.IsNullOrEmpty(text)) return new string(' ', width);
            if (text.Length >= width) return text.Substring(0, width);
            int left = (width - text.Length) / 2;
            int right = width - text.Length - left;
            return new string(' ', left) + text + new string(' ', right);
        }

        public static void PressAnyKey()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("  Press any key to continue...");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.WriteLine();
        }

        public static void DrawDivider(ConsoleColor color = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("  " + new string('─', 54));
            Console.ResetColor();
        }
    }
}
