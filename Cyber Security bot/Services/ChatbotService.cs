using CyberSecurityAwarenessBot.UI;
using CyberSecurityAwarenessBot.Models;

namespace CyberSecurityAwarenessBot.Services
{
    public class ChatbotService
    {
        private readonly ResponseService _responseService;

        public ChatbotService()
        {
            _responseService = new ResponseService();
        }

        public void StartChat(UserProfile user)
        {
            while (true)
            {
                ConsoleUI.WriteUserPrompt($"{user.Name}: ");
                string? userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    ConsoleUI.WriteError("Invalid input. Please type a valid question.");
                    continue;
                }

                // Check for exit command first
                if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                // Get response using the user's name directly
                string response = _responseService.GetResponse(userInput, user.Name);
                ConsoleUI.WriteBotMessage(response);
            }
        }
    }
}