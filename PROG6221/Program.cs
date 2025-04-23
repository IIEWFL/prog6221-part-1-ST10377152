using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace PROG6221
{
    class Program
    {
        // Cybersecurity topic responses with subtopics
        static Dictionary<string, Dictionary<string, string>> topicResponses = new Dictionary<string, Dictionary<string, string>>()
        {
            {
                "password", new Dictionary<string, string>()
                {
                    { "strength", "Strong passwords should have 12+ characters with mix of uppercase, numbers, and symbols!" },
                    { "manager", "Use a password manager like LastPass or Bitwarden to store passwords securely." },
                    { "reset", "Reset passwords immediately after a data breach or suspicious activity." }
                }
            },
            {
                "phishing", new Dictionary<string, string>()
                {
                    { "email", "Check sender addresses carefully - scammers often use lookalike domains like 'amaz0n.com'." },
                    { "link", "Hover over links to see real URLs before clicking. Look for HTTPS and valid certificates." },
                    { "report", "Forward phishing emails to report@phishing.gov.za and delete them." }
                }
            },
            {
                "browsing", new Dictionary<string, string>()
                {
                    { "vpn", "Use a VPN on public Wi-Fi to encrypt your internet traffic." },
                    { "https", "Always check for HTTPS padlock icon before entering sensitive information." },
                    { "cookies", "Regularly clear browser cookies and avoid 'accept all' on cookie banners." }
                }
            }
        };

        // General conversation responses
        static Dictionary<string, string> generalResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "how are you", "I'm a bot, so I don't feel emotions, but I'm here to help! 🛡️" },
            { "purpose", "I teach cybersecurity basics like password safety, phishing identification, and safe browsing practices." },
            { "what can i ask", "You can ask me about:\n- Password security\n- Phishing detection\n- Safe browsing\nOr type 'exit' to quit" }
        };

        static void Main()
        {
            PlayWelcomeSound();
            DisplayLogo();
            string userName = GetUserName();
            RunChatLoop(userName);
        }

        static void PlayWelcomeSound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("welcome.wav");
                player.Play();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("[Audio greeting not found]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Audio error: {ex.Message}]");
            }
        }

        static void DisplayLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            try
            {
                string[] logoLines = File.ReadAllLines("logo.txt");
                foreach (string line in logoLines)
                    Console.WriteLine(line);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("=== CYBERSECURITY AWARENESS BOT ===");
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        static string GetUserName()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine().Trim();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid name. Please try again:");
                Console.ResetColor();
                name = Console.ReadLine().Trim();
            }

            return name;
        }

        static void RunChatLoop(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Welcome {userName}! I'm your Cybersecurity Assistant.(Type exit to leave)");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\nYou: ");
                Console.ResetColor();

                string input = Console.ReadLine().Trim().ToLower();

                if (input == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Goodbye! Remember to stay vigilant online!");
                    Console.ResetColor();
                    break;
                }

                ProcessInput(input);
            }
        }

        static void ProcessInput(string input)
        {
            if (generalResponses.TryGetValue(input, out string generalResponse))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nBot: {generalResponse}");
                Console.ResetColor();
                return;
            }

            if (topicResponses.ContainsKey(input))
            {
                HandleSubtopicSelection(input);
                return;
            }

            ShowErrorMessage();
        }

        static void HandleSubtopicSelection(string mainTopic)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nBot: Which {mainTopic} topic?");
            Console.WriteLine($"Options: {string.Join(", ", topicResponses[mainTopic].Keys)}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nYou: ");
            Console.ResetColor();

            string subTopic = Console.ReadLine().Trim().ToLower();

            if (topicResponses[mainTopic].TryGetValue(subTopic, out string response))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nBot: {response}");
                Console.ResetColor();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nBot: Invalid {mainTopic} subtopic. Try: {string.Join(", ", topicResponses[mainTopic].Keys)}");
                Console.ResetColor();
            }
        }

        static void ShowErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nBot: I didn't understand that. Try these commands:");
            Console.WriteLine("- General questions: how are you, purpose, what can i ask");
            Console.WriteLine("- Main topics: password, phishing, browsing");
            Console.WriteLine("- Type 'exit' to quit");
            Console.ResetColor();
        }
    }
}
