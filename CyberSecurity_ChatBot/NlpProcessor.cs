using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    class NlpProcessor
    {
        public List<string> ActionHistory { get; private set; } = new List<string>();

        // Simulated NLP processing
        public (string Intent, string Detail) ProcessInput(string input)
        {
            input = input.ToLower();

            // Add Task
            if (Regex.IsMatch(input, @"\b(add|create|set).*(task|reminder)\b|\bremind me to\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                ActionHistory.Add($"Task added: '{detail}'");
                return ("add_task", detail);
            }

            // Start Quiz
            if (Regex.IsMatch(input, @"\b(start|launch|begin)\s+quiz\b", RegexOptions.IgnoreCase))
            {
                ActionHistory.Add("Quiz started.");
                return ("start_quiz", "");
            }

            // Show History
            if (Regex.IsMatch(input, @"\b(show|view|display)\s+(history|actions|activity)\b|\bwhat have you done\b", RegexOptions.IgnoreCase))
            {
                return ("show_history", "");
            }

            // View Tasks
            if (Regex.IsMatch(input, @"\b(show|view|display)\s+(tasks|reminders)\b", RegexOptions.IgnoreCase))
            {
                return ("view_tasks", "");
            }

            // Complete Task
            if (Regex.IsMatch(input, @"\b(complete|mark|finish)\s+(task|reminder)\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                return ("complete_task", detail);
            }

            // Delete Task
            if (Regex.IsMatch(input, @"\b(delete|remove)\s+(task|reminder)\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                return ("delete_task", detail);
            }

            // If no pattern matched, return general chat
            return ("general_chat", "");
        }


        // Extracts the task or reminder detail from user input using simple regex
        private string ExtractTaskDetail(string input)
        {
            // Example: "Remind me to update my password tomorrow" → "update my password"
            var match = Regex.Match(input, @"(remind me to|add a task to|create task to|set task to|add task to|add a reminder to)\s+(.*)", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[2].Value.Trim();

            // Fall back: remove trigger words and return remaining text
            string[] triggers = { "remind me to", "add a task to", "create task to", "set task to", "add task to", "add a reminder to", "add task", "create task", "set task", "add reminder" };
            foreach (var trigger in triggers)
            {
                if (input.ToLower().Contains(trigger))
                {
                    return input.ToLower().Replace(trigger, "").Trim();
                }
            }

            return input; // Return original input if no specific extraction worked
        }

        public string GetActionHistory()
        {
            if (ActionHistory.Count == 0)
                return "No actions recorded yet.";

            string summary = "Here’s a summary of recent actions:\n";
            int count = 1;
            foreach (var action in ActionHistory)
            {
                summary += $"{count}. {action}\n";
                count++;
            }
            return summary;
        }
    }
}
