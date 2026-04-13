using System;

namespace CyberSecurityAwarenessBot.Services
{
    public class ResponseService
    {
        private const string BotName = "CipherX";

        public string GetResponse(string userInput, string userName)
        {
            string input = userInput.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                return $"I didn’t receive anything, {userName}. Type a question and {BotName} will assist you.";
            }

            if (input.Contains("how are you"))
            {
                return $"{BotName} is fully operational, {userName}. Ready to help you stay secure online.";
            }

            if (input.Contains("what's your purpose") || input.Contains("what is your purpose"))
            {
                return $"{BotName}'s mission is to help you understand cybersecurity risks and protect your digital life.";
            }

            if (input.Contains("what can i ask you about"))
            {
                return $"You can ask {BotName} about passwords, phishing, scams, suspicious links, and safe browsing habits.";
            }

            if (input.Contains("password"))
            {
                return $"{BotName} recommends using strong, unique passwords with a mix of characters. Never reuse passwords across sites.";
            }

            if (input.Contains("phishing"))
            {
                return $"{BotName} warning: Phishing attempts often mimic trusted sources. Always verify emails and avoid clicking unknown links.";
            }

            if (input.Contains("safe browsing") || input.Contains("browse safely") || input.Contains("browsing"))
            {
                return $"{BotName} tip: Use secure (HTTPS) websites, avoid downloads from unknown sources, and keep your browser updated.";
            }

            if (input.Contains("suspicious link") || input.Contains("link"))
            {
                return $"{BotName} advises: Hover over links before clicking. If the URL looks suspicious, do not open it.";
            }

            if (input.Contains("scam"))
            {
                return $"{BotName} alert: Scams often create urgency. Never share personal or financial details without verifying the source.";
            }

            if (input == "exit")
            {
                return $"Session ended. {BotName} wishes you a safe and secure online experience, {userName}.";
            }

            return $"{BotName} could not understand your request. Try asking about cybersecurity topics like phishing or passwords.";
        }
    }
}