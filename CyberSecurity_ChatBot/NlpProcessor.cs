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

            // Handle flexible task addition
            if (input.Contains("add task") || input.Contains("create task") || input.Contains("set task") || input.Contains("remind me to") || input.Contains("add a reminder"))
            {
                string detail = ExtractTaskDetail(input);
                ActionHistory.Add($"Task added: '{detail}'");
                return ("add_task", detail);
            }

            // Start quiz
            if (input.Contains("start quiz") || input.Contains("launch quiz") || input.Contains("begin quiz"))
            {
                ActionHistory.Add("Quiz started.");
                return ("start_quiz", "");
            }

            // Show history
            if (input.Contains("what have you done") || input.Contains("show history") || input.Contains("what have you done for me"))
            {
                return ("show_history", "");
            }

            // View tasks
            if (input.Contains("show tasks") || input.Contains("view tasks") || input.Contains("display tasks"))
            {
                return ("view_tasks", "");
            }

            // Complete task
            if (input.Contains("complete task") || input.Contains("mark task"))
            {
                string detail = ExtractTaskDetail(input);
                return ("complete_task", detail);
            }

            // Delete task
            if (input.Contains("delete task") || input.Contains("remove task"))
            {
                string detail = ExtractTaskDetail(input);
                return ("delete_task", detail);
            }

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
                if (input.Contains(trigger))
                {
                    return input.Replace(trigger, "").Trim();
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
