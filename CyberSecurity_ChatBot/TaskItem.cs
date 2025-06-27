using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Represents a task item with title, description, optional reminder, and completion status.
    /// </summary>
    public class TaskItem
    {
        public string Title { get; set; } // Task name
        public string Description { get; set; } // Task details
        public DateTime? ReminderDate { get; set; } // Optional reminder date
        public bool IsCompleted { get; set; } // Task completion status

        /// <summary>
        /// Returns a formatted string representation of the task for easy display.
        /// </summary>
        public override string ToString()
        {
            string status = IsCompleted ? "Completed" : "Pending"; // Show task status
            string reminder = ReminderDate.HasValue ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" : ""; // Show reminder if set
            return $"{Title}: {Description}{reminder} - {status}";
        }

        /// <summary>
        /// Returns a display-friendly task name with reminder date if available.
        /// </summary>
        public string DisplayText
        {
            get
            {
                if (ReminderDate.HasValue)
                    return $"{Title} (Reminder: {ReminderDate.Value.ToShortDateString()})";
                else
                    return Title;
            }
        }
    }

    /// <summary>
    /// Manages the list of tasks: add, view, complete, delete.
    /// </summary>
    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>(); // List to store tasks

        /// <summary>
        /// Adds a new task with optional reminder.
        /// </summary>
        public string AddTask(string title, string description, DateTime? reminderDate = null)
        {
            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminderDate,
                IsCompleted = false // Task starts as incomplete
            });

            return $"Task added: {title}. {description} {(reminderDate.HasValue ? $"I'll remind you on {reminderDate.Value.ToShortDateString()}." : "")}";
        }

        /// <summary>
        /// Returns a list of all tasks in string format.
        /// </summary>
        public string ViewTasks()
        {
            if (tasks.Count == 0)
                return "You have no tasks at the moment.";

            string taskList = "Here are your tasks:\n";
            foreach (var task in tasks)
            {
                taskList += $"- {task}\n"; // Use the ToString method of TaskItem
            }
            return taskList;
        }

        /// <summary>
        /// Marks a task as completed based on the title.
        /// </summary>
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

        /// <summary>
        /// Deletes a task based on the title.
        /// </summary>
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

        /// <summary>
        /// Returns the list of TaskItem objects for GUI display.
        /// </summary>
        public List<TaskItem> GetTasks()
        {
            return tasks;
        }
    }
}
