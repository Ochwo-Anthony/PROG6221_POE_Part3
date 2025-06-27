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
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private CyberQuiz quiz;
        private List<int> userAnswers;
        private int currentIndex;
        private TaskManager taskManager;
        private ActivityLog activityLog;
        private string userName;

        public QuizWindow(TaskManager manager, CyberQuiz quizInstance, ActivityLog log, string name)
        {
            InitializeComponent();
            quiz = quizInstance;
            userAnswers = new List<int>(new int[quiz.GetQuestionCount()]);
            currentIndex = 0;
            taskManager = manager;
            quiz = quizInstance;
            activityLog = log;
            userName = name;
            LoadQuestion();
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
                       
                        break;

                    case "Activity Log":
                        ActivityLogWindow logWindow = new ActivityLogWindow(taskManager, quiz, activityLog, userName);
                        logWindow.Show();
                        this.Close();
                        break;
                }
            }
        }


        private void LoadQuestion()
        {
            var question = quiz.GetQuestion(currentIndex);
            txtQuestion.Text = question.Question;

            OptionsPanel.Children.Clear();
            for (int i = 0; i < question.Options.Length; i++)
            {
                RadioButton optionButton = new RadioButton
                {
                    Content = question.Options[i],
                    GroupName = "Options",
                    Margin = new Thickness(5)
                };

                if (userAnswers[currentIndex] == i)
                    optionButton.IsChecked = true;

                optionButton.Checked += (s, e) => { userAnswers[currentIndex] = i; };
                OptionsPanel.Children.Add(optionButton);
            }
        }

        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex > 0)
            {
                currentIndex--;
                LoadQuestion();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentIndex < quiz.GetQuestionCount() - 1)
            {
                currentIndex++;
                LoadQuestion();
            }
        }

        private void BtnFinish_Click(object sender, RoutedEventArgs e)
        {
            int score = 0;

            for (int i = 0; i < quiz.GetQuestionCount(); i++)
            {
                if (quiz.IsAnswerCorrect(i, userAnswers[i]))
                    score++;
            }

            string feedback = quiz.GetFinalFeedback(score);

            MessageBox.Show($"Quiz complete! Your score: {score}/{quiz.GetQuestionCount()}\n\n{feedback}");
            this.Close();
        }
    }
}
