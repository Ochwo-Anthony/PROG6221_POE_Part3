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
    /// </summary>
    public partial class TaskManagementWindow : Window
    {
        private TaskManager taskManager;
        private CyberQuiz quiz;
        private ActivityLog activityLog;
        private string userName;

        public TaskManagementWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent();
            taskManager = manager;
            quiz = quizInstance;
            activityLog = log;
            userName = name;
            LoadTasks();
        }

        private void NavigationDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                string selection = selectedItem.Content.ToString();

                switch (selection)
                {
                    case "Chat":
                        ChatWindow chatWindow = new ChatWindow("User"); // Replace "User" with username if available
                        chatWindow.Show();
                        this.Close();
                        break;

                    case "Tasks":
                    
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


        private void LoadTasks()
        {
            TaskListBox.Items.Clear();
            foreach (var task in taskManager.GetTasks())
            {
                TaskListBox.Items.Add(task);
            }
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            string taskTitle = txtTaskTitle.Text.Trim();
            DateTime? reminderDate = null;

            if (datePickerReminder.SelectedDate.HasValue)
            {
                reminderDate = datePickerReminder.SelectedDate.Value;
            }

            if (!string.IsNullOrEmpty(taskTitle))
            {
                if (reminderDate.HasValue)
                {
                    taskManager.AddTask(taskTitle, $"Remember to {taskTitle}.", reminderDate.Value);
                }
                else
                {
                    taskManager.AddTask(taskTitle, $"Remember to {taskTitle}.");
                }

                txtTaskTitle.Clear();
                datePickerReminder.SelectedDate = null;
                LoadTasks();
            }
        }

        private void BtnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var task = TaskListBox.SelectedItem as TaskItem;
                taskManager.CompleteTask(task.Title);
                LoadTasks();
            }
        }

        private void BtnLoadTasks_Click(object sender, RoutedEventArgs e)
        {
            LoadTasks();
        }


        private void BtnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var task = TaskListBox.SelectedItem as TaskItem;
                taskManager.DeleteTask(task.Title);
                LoadTasks();
            }
        }
    }
}
