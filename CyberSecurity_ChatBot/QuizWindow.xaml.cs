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
    /// Handles the interaction logic for the QuizWindow.
    /// Provides quiz navigation, scoring, and displays the quiz questions and options.
    /// </summary>
    public partial class QuizWindow : Window
    {
        private CyberQuiz quiz; // Quiz logic handler
        private List<int> userAnswers; // Stores user's selected answers
        private int currentIndex; // Tracks the current question index
        private TaskManager taskManager; // Shared task manager instance for navigation
        private ActivityLog activityLog; // Shared activity log instance for navigation
        private string userName; // Current user's name

        /// <summary>
        /// Initializes the QuizWindow with shared instances and user details.
        /// </summary>
        public QuizWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent();
            quiz = quizInstance;
            userAnswers = new List<int>(new int[quiz.GetQuestionCount()]); // Pre-fill answers list
            currentIndex = 0;
            taskManager = manager;
            quiz = quizInstance;
            activityLog = log;
            userName = name;
            LoadQuestion(); // Load the first question
        }

        /// <summary>
        /// Handles navigation to other windows when selection changes in the dropdown.
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
                        ChatWindow chatWindow = new ChatWindow(userName); // Pass the correct username
                        chatWindow.Show();
                        this.Close();
                        break;

                    case "Tasks":
                        // Navigate to Task Management Window
                        if (!(this is TaskManagementWindow))
                        {
                            TaskManagementWindow taskWindow = new TaskManagementWindow(taskManager, quiz, activityLog, userName);
                            taskWindow.Show();
                            this.Close();
                        }
                        break;

                    case "Quiz":
                        // Current window is Quiz, no action needed
                        break;

                    case "Activity Log":
                        // Navigate to Activity Log Window
                        ActivityLogWindow logWindow = new ActivityLogWindow(taskManager, quiz, activityLog, userName);
                        logWindow.Show();
                        this.Close();
                        break;
                }
            }
        }

        /// <summary>
        /// Loads the current quiz question and displays the answer options.
        /// </summary>
        private void LoadQuestion()
        {
            var question = quiz.GetQuestion(currentIndex);
            txtQuestion.Text = question.Question; // Display question text

            OptionsPanel.Children.Clear(); // Clear previous options

            for (int i = 0; i < question.Options.Length; i++)
            {
                RadioButton optionButton = new RadioButton
                {
                    Content = question.Options[i],
                    GroupName = "Options", // Ensures only one option can be selected
                    Margin = new Thickness(5)
                };

                // If user already selected this answer, mark it as checked
                if (userAnswers[currentIndex] == i)
                    optionButton.IsChecked = true;

                // Update user's answer when an option is selected
                optionButton.Checked += (s, e) => { userAnswers[currentIndex] = i; };

                OptionsPanel.Children.Add(optionButton); // Add option to the UI
            }
        }

        /// <summary>
        /// Navigates to the previous quiz question.
        /// </summary>
        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                LoadQuestion();
            }
        }

        /// <summary>
        /// Navigates to the next quiz question.
        /// </summary>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < quiz.GetQuestionCount() - 1)
            {
                currentIndex++;
                LoadQuestion();
            }
        }

        /// <summary>
        /// Finishes the quiz, calculates the score, and shows the feedback.
        /// </summary>
        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;

            // Check each answer
            for (int i = 0; i < quiz.GetQuestionCount(); i++)
            {
                if (quiz.IsAnswerCorrect(i, userAnswers[i]))
                    score++;
            }

            string feedback = quiz.GetFinalFeedback(score); // Get feedback based on score

            // Show result to the user
            MessageBox.Show($"Quiz complete! Your score: {score}/{quiz.GetQuestionCount()}\n\n{feedback}");
            this.Close(); // Close quiz window after completion
        }
    }
}
