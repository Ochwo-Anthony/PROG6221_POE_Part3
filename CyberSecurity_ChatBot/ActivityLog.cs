using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// The ActivityLog class keeps track of significant user actions throughout the chatbot session.
    /// It records actions such as adding tasks, setting reminders, starting quizzes, etc.
    /// </summary>
    public class ActivityLog
    {
        // List to store log entries
        private List<string> logEntries = new List<string>();

        // Maximum number of entries to keep in the log
        private int maxEntries = 10;

        /// <summary>
        /// Adds a new action to the activity log with a timestamp.
        /// </summary>
        /// <param name="action">A description of the user action to log.</param>
        public void AddEntry(string action)
        {
            // Record the current time for the log entry
            string timestamp = DateTime.Now.ToString("HH:mm:ss");

            // Format the log entry with timestamp and action description
            string entry = $"{timestamp} - {action}";

            // Add the new entry to the log list
            logEntries.Add(entry);

            // Ensure the log does not exceed the maximum allowed entries
            if (logEntries.Count > maxEntries)
                logEntries.RemoveAt(0); // Remove the oldest entry if over the limit
        }

        /// <summary>
        /// Retrieves the most recent activity log as a formatted string.
        /// </summary>
        /// <returns>A string summarizing the recent actions or a message if the log is empty.</returns>
        public string GetRecentLog()
        {
            // If no actions have been recorded, return a friendly message
            if (logEntries.Count == 0)
                return "No recent actions recorded.";

            // Prepare the log summary using StringBuilder for efficient string concatenation
            StringBuilder logSummary = new StringBuilder();
            logSummary.AppendLine("Here’s a summary of recent actions:");
            int count = 1;

            // Add each log entry to the summary with numbering
            foreach (var entry in logEntries)
            {
                logSummary.AppendLine($"{count}. {entry}");
                count++;
            }

            // Return the complete log summary
            return logSummary.ToString();
        }
    }
}
