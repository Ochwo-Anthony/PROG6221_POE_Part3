using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Processes user input to detect intent and extract task details using simple NLP patterns.
    /// </summary>
    class NlpProcessor
    {
        // Stores a history of actions performed based on user input
        public List<string> ActionHistory { get; private set; } = new List<string>();

        /// <summary>
        /// Main method to process the user's input and return the detected intent and any associated detail.
        /// </summary>
        /// <param name="input">User's input message</param>
        /// <returns>Tuple containing the detected intent and detail (if applicable)</returns>
        public (string Intent, string Detail) ProcessInput(string input)
        {
            input = input.ToLower(); // Convert input to lowercase for easier pattern matching

            // Check if the input is an Add Task command
            if (Regex.IsMatch(input, @"\b(add|create|set).*(task|reminder)\b|\bremind me to\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                ActionHistory.Add($"Task added: '{detail}'");
                return ("add_task", detail);
            }

            // Check if the input is a Start Quiz command
            if (Regex.IsMatch(input, @"\b(start|launch|begin)\s+quiz\b", RegexOptions.IgnoreCase))
            {
                ActionHistory.Add("Quiz started.");
                return ("start_quiz", "");
            }

            // Check if the user wants to view activity history
            if (Regex.IsMatch(input, @"\b(show|view|display)\s+(history|actions|activity)\b|\bwhat have you done\b", RegexOptions.IgnoreCase))
            {
                return ("show_history", "");
            }

            // Check if the user wants to view tasks
            if (Regex.IsMatch(input, @"\b(show|view|display)\s+(tasks|reminders)\b", RegexOptions.IgnoreCase))
            {
                return ("view_tasks", "");
            }

            // Check if the user wants to complete a task
            if (Regex.IsMatch(input, @"\b(complete|mark|finish)\s+(task|reminder)\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                return ("complete_task", detail);
            }

            // Check if the user wants to delete a task
            if (Regex.IsMatch(input, @"\b(delete|remove)\s+(task|reminder)\b", RegexOptions.IgnoreCase))
            {
                string detail = ExtractTaskDetail(input);
                return ("delete_task", detail);
            }

            // If no patterns matched, classify as general chat
            return ("general_chat", "");
        }

        /// <summary>
        /// Attempts to extract the task or reminder description from the user's input.
        /// </summary>
        /// <param name="input">User's input message</param>
        /// <returns>Extracted task description</returns>
        private string ExtractTaskDetail(string input)
        {
            // Regex pattern to extract task description after common trigger phrases
            var match = Regex.Match(input, @"(remind me to|add a task to|create task to|set task to|add task to|add a reminder to)\s+(.*)", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[2].Value.Trim();

            // If no regex match, attempt to clean input by removing common trigger words
            string[] triggers = { "remind me to", "add a task to", "create task to", "set task to", "add task to", "add a reminder to", "add task", "create task", "set task", "add reminder" };
            foreach (var trigger in triggers)
            {
                if (input.ToLower().Contains(trigger))
                {
                    return input.ToLower().Replace(trigger, "").Trim();
                }
            }

            // Fallback: return original input if no extraction worked
            return input;
        }

        /// <summary>
        /// Returns a formatted string summarizing all recorded user actions.
        /// </summary>
        /// <returns>Summary of action history</returns>
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
