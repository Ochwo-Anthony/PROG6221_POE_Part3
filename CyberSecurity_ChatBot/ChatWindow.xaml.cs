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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {

        private string userName;

        private TaskManager taskManager = new TaskManager();
        private string pendingTaskTitle = "";
        private bool awaitingReminder = false;

        private CyberQuiz quiz = new CyberQuiz();

        private NlpProcessor nlpProcessor = new NlpProcessor();

        private ActivityLog activityLog = new ActivityLog();


        public ChatWindow(string name)
        {
            InitializeComponent();
            userName = name;
            FadeInWindow();

            Loaded += async (s, e) =>
            {
                await TypeBotMessage($"Hello {userName}, welcome to CyberBot!");
                await Task.Delay(500); // Optional pause between messages
                await TypeBotMessage("Ask me about passwords, phishing, privacy, 2FA, safe browsing, antivirus, or cloud.");
                await Task.Delay(500); // Optional pause between messages
                await TypeBotMessage("Type 'exit' to end the chat.");
            };

        }

        private async Task TypeBotMessage(string message)
        {
            Border botBubble = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(58, 58, 58)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300
            };

            TextBlock botText = new TextBlock
            {
                Text = "", // Start empty
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            botBubble.Child = botText;
            ChatStack.Children.Add(botBubble);
            ChatScroll.ScrollToEnd();

            // Typing animation: show one character at a time
            foreach (char c in message)
            {
                botText.Text += c;
                await Task.Delay(50); // Typing speed (50ms per character)
                ChatScroll.ScrollToEnd(); // Keep scroll at the bottom as text grows
            }
        }


        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            AddUserMessage(input);
            txtUserInput.Clear();

            // Show typing indicator
            Border typingIndicator = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(58, 58, 58)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300
            };

            TextBlock typingText = new TextBlock
            {
                Text = "Typing...",
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            typingIndicator.Child = typingText;
            ChatStack.Children.Add(typingIndicator);
            ChatScroll.ScrollToEnd();

            await Task.Delay(1000);
            ChatStack.Children.Remove(typingIndicator);

            string response = "";

            // NLP SIMULATION: Get intent and details
            var nlpResult = nlpProcessor.ProcessInput(input);

            // ========================== NLP ROUTING ==========================
            if (nlpResult.Intent == "add_task")
            {
                string taskTitle = nlpResult.Detail;
                pendingTaskTitle = taskTitle;
                response = $"Task '{taskTitle}' added. Would you like a reminder?";
                awaitingReminder = true;

                activityLog.AddEntry($"Task added: '{taskTitle}'");
            }
            else if (nlpResult.Intent == "start_quiz")
            {
                response = quiz.StartQuiz();
                activityLog.AddEntry("Quiz started.");
            }
            else if (quiz.IsQuizInProgress())
            {
                response = quiz.ProcessAnswer(input);
            }
            else if (nlpResult.Intent == "show_history" || input.ToLower().Contains("show activity log"))
            {
                response = activityLog.GetRecentLog();
            }
            else if (nlpResult.Intent == "view_tasks")
            {
                response = taskManager.ViewTasks();
            }
            else if (nlpResult.Intent == "complete_task")
            {
                string taskTitle = nlpResult.Detail;
                response = taskManager.CompleteTask(taskTitle);
                activityLog.AddEntry($"Task completed: '{taskTitle}'");
            }
            else if (nlpResult.Intent == "delete_task")
            {
                string taskTitle = nlpResult.Detail;
                response = taskManager.DeleteTask(taskTitle);
                activityLog.AddEntry($"Task deleted: '{taskTitle}'");
            }
            // =================== REMINDER PROCESSING ===================
            else if (awaitingReminder)
            {
                if (input.ToLower().Contains("no"))
                {
                    response = taskManager.AddTask(pendingTaskTitle, $"Remember to {pendingTaskTitle}.");
                    response += "\nTask added without a reminder.";
                    awaitingReminder = false;

                    activityLog.AddEntry($"Task added without reminder: '{pendingTaskTitle}'");

                    pendingTaskTitle = "";
                }
                else
                {
                    int days = ExtractDaysFromInput(input);
                    if (days > 0)
                    {
                        DateTime reminderDate = DateTime.Now.AddDays(days);
                        response = taskManager.AddTask(pendingTaskTitle, $"Remember to {pendingTaskTitle}.", reminderDate);
                        response += $"\nReminder set for {reminderDate.ToShortDateString()}.";

                        activityLog.AddEntry($"Reminder set for '{pendingTaskTitle}' on {reminderDate.ToShortDateString()}");

                        awaitingReminder = false;
                        pendingTaskTitle = "";
                    }
                    else
                    {
                        response = "I couldn't understand the reminder time. Please specify like 'remind me in 3 days'.";
                    }
                }
            }
            // =================== DEFAULT CHATBOT RESPONSE ===================
            else
            {
                response = ResponseSystem.GetResponse(input, userName);
            }

            await TypeBotMessage($"CyberBOT: {response}");
            ChatScroll.ScrollToEnd();
        }




        private int ExtractDaysFromInput(string input)
        {
            var parts = input.ToLower().Split(' ');
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int days))
                    return days;
            }
            return 0;
        }


        private void AddUserMessage(string message)
        {
            Border userBubble = new Border
            {
                Background = Brushes.LimeGreen,
                CornerRadius = new CornerRadius(15), // ✅ Valid here
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 300
            };

            TextBlock userText = new TextBlock
            {
                Text = $"You: {message}",
                TextWrapping = TextWrapping.Wrap,               
            };

            userBubble.Child = userText;

            ChatStack.Children.Add(userBubble);
            ChatScroll.ScrollToEnd();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Thank you, {userName}! Stay safe online.");
            this.Close();
        }

        private void FadeInWindow()
        {
            this.Opacity = 0;
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);
        }
    }
}
