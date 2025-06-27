using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            string status = IsCompleted ? "Completed" : "Pending";
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" : "";
            return $"{Title}: {Description}{reminder} - {status}";
        }
    }

    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public string AddTask(string title, string description, DateTime? reminderDate = null)
        {
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false
            });

            return $"Task added: {title}. {description} {(reminderDate.HasValue ? $"I'll remind you on {reminderDate.Value.ToShortDateString()}." : "")}";
        }

        public string ViewTasks()
        {
            if (tasks.Count == 0)
                return "You have no tasks at the moment.";

            string taskList = "Here are your tasks:\n";
            foreach (var task in tasks)
            {
                taskList += $"- {task}\n";
            }
            return taskList;
        }

        public string CompleteTask(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                task.IsCompleted = true;
                return $"Task '{title}' marked as completed.";
            }
            return $"Task '{title}' not found.";
        }

        public string DeleteTask(string title)
        {
            var task = tasks.Find(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                tasks.Remove(task);
                return $"Task '{title}' deleted.";
            }
            return $"Task '{title}' not found.";
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }
    }
}
