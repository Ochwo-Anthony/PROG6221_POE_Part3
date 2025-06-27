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

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Code-behind for ActivityLogWindow.xaml
    /// Handles navigation and activity log display logic.
    /// </summary>
    public partial class ActivityLogWindow : Window
    {
        // Fields to hold shared application objects
        private TaskManager taskManager;    // Reference to the task management system
        private ActivityLog activityLog;    // Reference to the activity log
        private string userName;            // Stores the user's name
        private CyberQuiz quiz;             // Reference to the quiz system

        /// <summary>
        /// Constructor: Initializes the window and sets up references to shared data.
        /// </summary>
        /// <param name="manager">Task manager object</param>
        /// <param name="quizInstance">CyberQuiz object</param>
        /// <param name="log">ActivityLog object</param>
        /// <param name="name">User's name</param>
        public ActivityLogWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent(); // Initializes XAML components
            taskManager = manager; // Link the task manager
            quiz = quizInstance;   // Link the quiz instance
            activityLog = log;     // Link the activity log
            userName = name;       // Set the user name

            // Display the recent activity log on the screen
            txtActivityLog.Text = log.GetRecentLog();
        }

        /// <summary>
        /// Handles navigation dropdown selection changes.
        /// Opens the selected window and closes the current one.
        /// </summary>
        private void NavigationDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NavigationDropdown.SelectedItem is ComboBoxItem selectedItem)
            {
                string selection = selectedItem.Content.ToString();

                switch (selection)
                {
                    case "Chat":
                        // Navigate to ChatWindow
                        ChatWindow chatWindow = new ChatWindow("User"); // Pass username if available
                        chatWindow.Show();
                        this.Close(); // Close current window
                        break;

                    case "Tasks":
                        // Navigate to TaskManagementWindow if not already there
                        if (!(this is TaskManagementWindow))
                        {
                            TaskManagementWindow taskWindow = new TaskManagementWindow(taskManager, quiz, activityLog, userName);
                            taskWindow.Show();
                            this.Close(); // Close current window
                        }
                        break;

                    case "Quiz":
                        // Navigate to QuizWindow
                        QuizWindow quizWindow = new QuizWindow(taskManager, quiz, activityLog, userName);
                        quizWindow.Show();
                        NavigationDropdown.SelectedIndex = 0; // Reset dropdown selection to Chat
                        break;

                    case "Activity Log":
                        // Refresh or reopen the Activity Log window
                        ActivityLogWindow logWindow = new ActivityLogWindow(taskManager, quiz, activityLog, userName);
                        logWindow.Show();
                        this.Close(); // Close current window
                        break;
                }
            }
        }
    }
}
