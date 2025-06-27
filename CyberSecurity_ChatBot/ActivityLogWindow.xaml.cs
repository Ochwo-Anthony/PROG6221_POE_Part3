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
    /// Interaction logic for ActivityLogWindow.xaml
    /// </summary>
    public partial class ActivityLogWindow : Window
    {
        private TaskManager taskManager;
        private ActivityLog activityLog;
        private string userName;
        private CyberQuiz quiz;

        public ActivityLogWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent();
            taskManager = manager;
            quiz = quizInstance;
            activityLog = log;
            userName = name;
            txtActivityLog.Text = log.GetRecentLog();
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
                        if (!(this is TaskManagementWindow))
                        {
                            TaskManagementWindow taskWindow = new TaskManagementWindow(taskManager, quiz, activityLog, userName);
                            taskWindow.Show();
                            this.Close();
                        }
                        break;

                    case "Quiz":
                        QuizWindow quizWindow = new QuizWindow(taskManager, quiz, activityLog, userName);
                        quizWindow.Show();
                        NavigationDropdown.SelectedIndex = 0; // Reset to Chat
                        break;

                    case "Activity Log":
                        ActivityLogWindow logWindow = new ActivityLogWindow(taskManager, quiz, activityLog, userName);
                        logWindow.Show();
                        this.Close();
                        break;
                }
            }
        }
    }
}
