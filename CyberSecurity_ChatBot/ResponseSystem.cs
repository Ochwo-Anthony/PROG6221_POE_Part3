using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// This class handles chatbot responses, topic tracking, sentiment detection, and proactive tips.
    /// </summary>
    class ResponseSystem
    {
        private static string favoriteTopic = ""; // Most frequently asked topic
        private static Dictionary<string, int> topicCounts = new Dictionary<string, int>(); // Tracks how many times each topic was asked
        private static int messageCount = 0; // Tracks the number of messages exchanged
        private static int proactivePromptThreshold = 5; // Every 5 messages, give proactive advice
        private static string lastTopicAnswered = ""; // Tracks the last topic discussed
        private static HashSet<int> usedTipIndexes = new HashSet<int>(); // Avoids repeating the same tips
        private static string currentMood = ""; // User's detected mood

        // Keywords that signal the user wants more information
        private static readonly string[] moreInfoTriggers = new string[]
        {
            "anything else", "more info", "tell me more",
            "more information", "what else", "can you elaborate"
        };

        private static Random rand = new Random(); // Random number generator for selecting responses

        // Main cybersecurity topics with multiple tips for each
        private static readonly Dictionary<string, string[]> topicResponses = new Dictionary<string, string[]>
        {
            ["password"] = new string[]
            {
                "Use strong passwords with a mix of letters, numbers, and symbols.",
                "Avoid reusing the same password across multiple sites.",
                "Consider using a trusted password manager.",
                "Don't include personal info like your birthday in your passwords."
            },
            ["phishing"] = new string[]
            {
                "Phishing is a scam to steal your info. Don’t click suspicious links.",
                "Be cautious of emails asking for personal data.",
                "Check the sender's email address for authenticity.",
                "Never give out login details via email."
            },
            ["privacy"] = new string[]
            {
                "Lock down your social media privacy settings.",
                "Avoid posting personal details publicly.",
                "Regularly review app permissions.",
                "Don’t overshare personal moments online."
            },
            ["2fa"] = new string[]
            {
                "2FA adds an extra layer of protection to your accounts.",
                "Even with your password stolen, 2FA keeps hackers out.",
                "Use apps like Google Authenticator for 2FA."
            },
            ["safe browsing"] = new string[]
            {
                "Use browsers that support secure connections.",
                "Avoid clicking on pop-ups or unknown ads.",
                "Look for 'https://' in the web address."
            },
            ["antivirus"] = new string[]
            {
                "Firewalls help block unauthorized access to your computer.",
                "Keep your antivirus software up-to-date.",
                "Run regular scans to check for malware."
            },
            ["cloud"] = new string[]
            {
                "Enable 2FA on your cloud accounts.",
                "Don’t store sensitive info in unencrypted form.",
                "Choose trusted cloud providers."
            }
        };

        // Responses based on detected user sentiment
        private static readonly Dictionary<string, string> sentimentResponses = new Dictionary<string, string>()
        {
            ["worried"] = "It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe.",
            ["curious"] = "That's great! Curiosity is the first step to staying safe online. Let me provide some useful info.",
            ["frustrated"] = "I know cybersecurity can feel overwhelming at times. I'm here to help make it simpler for you."
        };

        /// <summary>
        /// Main method to process user input and return an appropriate response.
        /// </summary>
        public static string GetResponse(string input, string userName)
        {
            messageCount++;
            input = input.ToLower();

            // Sentiment detection
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    currentMood = sentiment;
                    return sentimentResponses[sentiment];
                }
            }

            // If user asks for more information on the last topic
            if (moreInfoTriggers.Any(trigger => input.Contains(trigger)) && !string.IsNullOrEmpty(lastTopicAnswered))
            {
                var tips = topicResponses[lastTopicAnswered];

                if (tips != null && tips.Length > 0)
                {
                    // Select unused tip
                    var availableIndexes = Enumerable.Range(0, tips.Length)
                        .Where(i => !usedTipIndexes.Contains(i)).ToList();

                    if (availableIndexes.Count == 0)
                    {
                        usedTipIndexes.Clear(); // Reset if all tips were used
                        availableIndexes = Enumerable.Range(0, tips.Length).ToList();
                    }

                    int selectedIndex = availableIndexes[rand.Next(availableIndexes.Count)];
                    usedTipIndexes.Add(selectedIndex);

                    return AdaptResponseToMood(tips[selectedIndex], currentMood);
                }
                else
                {
                    return $"I've shared all I know about {lastTopicAnswered}, but feel free to ask about other topics!";
                }
            }

            // General greetings
            if (input.Contains("how are you"))
                return $"I'm doing well, {userName}! Ready to help you stay secure online.";

            if (input.Contains("your purpose"))
                return "I'm your cybersecurity buddy — here to educate and protect you online.";

            if (input.Contains("what can i ask") || input.Contains("help"))
                return "You can ask me about:\n- Password safety\n- Phishing attacks\n- 2FA (Two-Factor Authentication)\n- Social media privacy\n- Safe browsing habits\n- Antivirus and firewalls";

            // Topic matching: check if user asked about a known topic
            foreach (var topic in topicResponses.Keys)
            {
                if (input.Contains(topic))
                {
                    UpdateTopicCount(topic);
                    lastTopicAnswered = topic;
                    usedTipIndexes.Clear();
                    currentMood = "";

                    var responses = topicResponses[topic];
                    string selectedResponse = responses[rand.Next(responses.Length)];

                    return AdaptResponseToMood(selectedResponse, currentMood);
                }
            }

            // Track user interests
            if (input.Contains("i'm interested in"))
            {
                string[] words = input.Split(' ');
                string interest = words.Last();
                UpdateTopicCount(interest);
                return $"Great! I'll remember that you're interested in {interest}. It's a crucial part of staying safe online.";
            }

            // Fallback response when input is not recognized
            string[] fallbackResponses = {
                $"Hmm, I'm not sure about that, {userName}. Try asking something like 'What is phishing?'",
                $"That's an interesting question! I'll try to learn more about that.",
                $"Cybersecurity is broad! Try narrowing your question to passwords, scams, or privacy.",
                $"I may need an update to answer that — try asking about 2FA, antivirus, or phishing."
            };

            string response = fallbackResponses[rand.Next(fallbackResponses.Length)];

            // Proactive advice based on user's favorite topic every 5 messages
            if (messageCount % proactivePromptThreshold == 0 && !string.IsNullOrEmpty(favoriteTopic))
            {
                var proactiveTips = topicResponses[favoriteTopic];
                if (proactiveTips.Length > 0)
                {
                    response += $"\nBy the way, since you're interested in {favoriteTopic}: {proactiveTips[rand.Next(proactiveTips.Length)]}";
                }
            }

            return response;
        }

        /// <summary>
        /// Updates the topic tracking to find user's favorite topic.
        /// </summary>
        private static void UpdateTopicCount(string topic)
        {
            if (topicCounts.ContainsKey(topic))
                topicCounts[topic]++;
            else
                topicCounts[topic] = 1;

            // Favorite topic is the one asked about most
            favoriteTopic = topicCounts.OrderByDescending(kv => kv.Value).First().Key;
        }

        /// <summary>
        /// Adjusts the response based on detected user mood.
        /// </summary>
        private static string AdaptResponseToMood(string baseResponse, string mood)
        {
            switch (mood)
            {
                case "worried":
                    return $"{baseResponse} Don't worry — you're not alone in this. These steps can really help protect you.";
                case "curious":
                    return $"{baseResponse} There's so much more to learn — you're on the right track exploring this topic!";
                case "frustrated":
                    return $"{baseResponse} It can feel complicated, but you're making progress. I'm here to guide you.";
                default:
                    return baseResponse;
            }
        }
    }
}
