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
        static Dictionary<string, Dictionary<string, string>> responses = new Dictionary<string, Dictionary<string, string>>()
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

        static void Main()
        {
            // Play voice greeting
            PlayWelcomeSound();

            // Display ASCII art
            DisplayLogo();

            // Get user name
            string userName = GetUserName();

            // Main chat loop
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
                Console.WriteLine("[Audio file missing]");
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
            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid name. Please try again:");
                name = Console.ReadLine();
            }

            return name;
        }

        static void RunChatLoop(string userName)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Welcome {userName}! Ask about:");
            Console.WriteLine("- Passwords\n- Phishing\n- Browsing\n(Type 'exit' to quit)");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\nYou: ");
                string input = Console.ReadLine().ToLower().Trim();

                if (input == "exit")
                {
                    Console.WriteLine("Goodbye! Stay secure!");
                    break;
                }

                HandleTopicSelection(input);
            }
        }

        static void HandleTopicSelection(string input)
        {
            if (responses.ContainsKey(input))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nBot: Which {input} topic?");
                Console.WriteLine("Options: " + string.Join(", ", responses[input].Keys));
                Console.ResetColor();

                Console.Write("\nYou: ");
                string subTopic = Console.ReadLine().ToLower().Trim();

                if (responses[input].TryGetValue(subTopic, out string response))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nBot: {response}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nBot: Invalid {input} subtopic. Try again.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBot: Main topics are: passwords, phishing, browsing");
                Console.ResetColor();
            }
        }
    }
}
