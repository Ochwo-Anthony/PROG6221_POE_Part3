using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    public class ActivityLog
    {
        private List<string> logEntries = new List<string>();
        private int maxEntries = 10;

        public void AddEntry(string action)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            string entry = $"{timestamp} - {action}";
            logEntries.Add(entry);

            // Keep log to max entries
            if (logEntries.Count > maxEntries)
                logEntries.RemoveAt(0); // Remove oldest entry
        }

        public string GetRecentLog()
        {
            if (logEntries.Count == 0)
                return "No recent actions recorded.";

            StringBuilder logSummary = new StringBuilder();
            logSummary.AppendLine("Here’s a summary of recent actions:");
            int count = 1;

            foreach (var entry in logEntries)
            {
                logSummary.AppendLine($"{count}. {entry}");
                count++;
            }

            return logSummary.ToString();
        }
    }
}
