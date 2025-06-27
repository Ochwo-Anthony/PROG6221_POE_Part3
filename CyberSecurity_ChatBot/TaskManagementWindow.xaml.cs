using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Interaction logic for TaskManagementWindow.xaml
    /// This window handles task management: adding, viewing, completing, and deleting tasks.
    /// </summary>
    public partial class TaskManagementWindow : Window
    {
        private TaskManager taskManager; // Manages task storage and operations
        private CyberQuiz quiz; // Quiz system (passed for navigation)
        private ActivityLog activityLog; // Tracks user actions (passed for navigation)
        private string userName; // Stores current user’s name

        /// <summary>
        /// Constructor: initializes task window with task manager, quiz, activity log, and user name.
        /// </summary>
        public TaskManagementWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent();
            taskManager = manager;
            quiz = quizInstance;
            activityLog = log;
            userName = name;
            LoadTasks(); // Display existing tasks on load
        }

        /// <summary>
        /// Handles navigation between Chat, Task, Quiz, and Activity Log windows.
        /// </summary>
        private void NavigationDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                string selection = selectedItem.Content.ToString();

                switch (selection)
                {
                    case "Chat":
                        ChatWindow chatWindow = new ChatWindow("User"); // You can replace "User" with userName for personalization
                        chatWindow.Show();
                        this.Close();
                        break;

                    case "Tasks":
                        // Already in task window - do nothing
                        break;

                    case "Quiz":
                        QuizWindow quizWindow = new QuizWindow(taskManager, quiz, activityLog, userName);
                        quizWindow.Show();
                        this.Close();
                        break;

                    case "Activity Log":
                        ActivityLogWindow logWindow = new ActivityLogWindow(taskManager, quiz, activityLog, userName);
                        logWindow.Show();
                        this.Close();
                        break;
                }
            }
        }

        /// <summary>
        /// Loads tasks from the TaskManager and displays them in the ListBox.
        /// </summary>
        private void LoadTasks()
        {
            TaskListBox.Items.Clear(); // Clear existing items
            foreach (var task in taskManager.GetTasks()) // Load all current tasks
            {
                TaskListBox.Items.Add(task);
            }
        }

        /// <summary>
        /// Adds a new task with an optional reminder date.
        /// </summary>
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            string taskTitle = txtTaskTitle.Text.Trim();
            DateTime? reminderDate = null;

            // Check if a reminder date is selected
            if (datePickerReminder.SelectedDate.HasValue)
            {
                reminderDate = datePickerReminder.SelectedDate.Value;
            }

            if (!string.IsNullOrEmpty(taskTitle))
            {
                // Add task with or without a reminder
                if (reminderDate.HasValue)
                {
                    taskManager.AddTask(taskTitle, $"Remember to {taskTitle}.", reminderDate.Value);
                }
                else
                {
                    taskManager.AddTask(taskTitle, $"Remember to {taskTitle}.");
                }

                // Clear input fields after adding
                txtTaskTitle.Clear();
                datePickerReminder.SelectedDate = null;

                LoadTasks(); // Refresh task list
            }
        }

        /// <summary>
        /// Marks the selected task as completed.
        /// </summary>
        private void BtnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var task = TaskListBox.SelectedItem as TaskItem;
                taskManager.CompleteTask(task.Title);
                LoadTasks(); // Refresh task list
            }
        }

        /// <summary>
        /// Reloads and displays the task list.
        /// </summary>
        private void BtnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }

        /// <summary>
        /// Deletes the selected task from the list.
        /// </summary>
        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var task = TaskListBox.SelectedItem as TaskItem;
                taskManager.DeleteTask(task.Title);
                LoadTasks(); // Refresh task list
            }
        }
    }
}
