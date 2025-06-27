using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// Chat window logic for CyberSecurity ChatBot.
    /// Handles chat input, NLP processing, task management, reminders, quiz interaction, and navigation.
    /// </summary>
    public partial class ChatWindow : Window
    {
        private string userName;                     // Stores user's name
        private TaskManager taskManager = new TaskManager(); // Manages tasks
        private string pendingTaskTitle = "";        // Stores task waiting for a reminder confirmation
        private bool awaitingReminder = false;       // Tracks if the bot is waiting for a reminder setup

        private CyberQuiz quiz = new CyberQuiz();    // Quiz manager
        private NlpProcessor nlpProcessor = new NlpProcessor(); // NLP processor
        private ActivityLog activityLog = new ActivityLog();    // Logs user actions

        /// <summary>
        /// Constructor: Initializes the chat window and displays a welcome message.
        /// </summary>
        public ChatWindow(string name)
        {
            InitializeComponent();
            userName = name;
            FadeInWindow(); // Applies fade-in animation on window load

            // Display initial bot messages when the window loads
            Loaded += async (s, e) =>
            {
                await TypeBotMessage($"Hello {userName}, welcome to CyberBot!");
                await Task.Delay(500);
                await TypeBotMessage("Ask me about passwords, phishing, privacy, 2FA, safe browsing, antivirus, or cloud.");
            };
        }

        /// <summary>
        /// Handles send button click: processes user input, triggers NLP, and returns chatbot responses.
        /// </summary>
        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            AddUserMessage(input); // Show user message in chat
            txtUserInput.Clear();

            // Show typing animation
            Border typingIndicator = CreateTypingIndicator();
            ChatStack.Children.Add(typingIndicator);
            ChatScroll.ScrollToEnd();

            await Task.Delay(1000); // Simulate bot thinking time
            ChatStack.Children.Remove(typingIndicator);

            string response = "";
            var nlpResult = nlpProcessor.ProcessInput(input); // Process user input with NLP

            // Route based on detected intent
            switch (nlpResult.Intent)
            {
                case "add_task":
                    pendingTaskTitle = nlpResult.Detail;
                    response = $"Task '{pendingTaskTitle}' added. Would you like a reminder?";
                    awaitingReminder = true;
                    activityLog.AddEntry($"Task added: '{pendingTaskTitle}'");
                    break;

                case "start_quiz":
                    response = quiz.StartQuiz();
                    activityLog.AddEntry("Quiz started.");
                    break;

                case "show_history":
                    response = activityLog.GetRecentLog();
                    break;

                case "view_tasks":
                    response = taskManager.ViewTasks();
                    break;

                case "complete_task":
                    response = taskManager.CompleteTask(nlpResult.Detail);
                    activityLog.AddEntry($"Task completed: '{nlpResult.Detail}'");
                    break;

                case "delete_task":
                    response = taskManager.DeleteTask(nlpResult.Detail);
                    activityLog.AddEntry($"Task deleted: '{nlpResult.Detail}'");
                    break;

                case "general_chat":
                    response = ResponseSystem.GetResponse(input, userName);
                    break;

                default:
                    response = "I'm not sure what you mean. Please try rephrasing your request.";
                    break;
            }

            // Handle quiz answers if quiz is in progress
            if (quiz.IsQuizInProgress() && nlpResult.Intent != "start_quiz")
            {
                response = quiz.ProcessAnswer(input);
            }

            // Handle reminder confirmation
            if (awaitingReminder)
            {
                if (input.ToLower().Contains("no"))
                {
                    response = taskManager.AddTask(pendingTaskTitle, $"Remember to {pendingTaskTitle}.");
                    response += "\nTask added without a reminder.";
                    activityLog.AddEntry($"Task added without reminder: '{pendingTaskTitle}'");
                    awaitingReminder = false;
                    pendingTaskTitle = "";
                }
                else
                {
                    int days = ExtractDaysFromInput(input);
                    if (days >= 0)
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

            Console.WriteLine($"Detected Intent: {nlpResult.Intent}, Detail: {nlpResult.Detail}");

            await TypeBotMessage($"CyberBOT: {response}");
            ChatScroll.ScrollToEnd();
        }

        // ================= Helper Methods =================

        /// <summary>
        /// Creates a "Typing..." bubble to simulate chatbot typing.
        /// </summary>
        private Border CreateTypingIndicator()
        {
            return new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(58, 58, 58)),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 300,
                Child = new TextBlock
                {
                    Text = "Typing...",
                    Foreground = Brushes.White,
                    TextWrapping = TextWrapping.Wrap
                }
            };
        }

        /// <summary>
        /// Extracts the number of days for a reminder from user input.
        /// </summary>
        private int ExtractDaysFromInput(string input)
        {
            input = input.ToLower();

            if (input.Contains("today"))
                return 0;

            if (input.Contains("tomorrow"))
                return 1;

            if (input.Contains("day after tomorrow"))
                return 2;

            // Try to find any number in input
            var parts = input.Split(' ');
            foreach (var part in parts)
            {
                if (int.TryParse(part, out int days))
                    return days;
            }
            return -1; // No valid number found
        }

        /// <summary>
        /// Displays the bot's response with typing animation.
        /// </summary>
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
                Text = "",
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap
            };

            botBubble.Child = botText;
            ChatStack.Children.Add(botBubble);
            ChatScroll.ScrollToEnd();

            // Animate text typing
            foreach (char c in message)
            {
                botText.Text += c;
                await Task.Delay(50);
                ChatScroll.ScrollToEnd();
            }
        }

        /// <summary>
        /// Displays the user's message in a chat bubble.
        /// </summary>
        private void AddUserMessage(string message)
        {
            Border userBubble = new Border
            {
                Background = Brushes.LimeGreen,
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 300
            };

            TextBlock userText = new TextBlock
            {
                Text = $"You: {message}",
                TextWrapping = TextWrapping.Wrap
            };

            userBubble.Child = userText;

            ChatStack.Children.Add(userBubble);
            ChatScroll.ScrollToEnd();
        }

        /// <summary>
        /// Handles exit button click: shows a goodbye message and closes the window.
        /// </summary>
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Thank you, {userName}! Stay safe online.");
            this.Close();
        }

        /// <summary>
        /// Applies a fade-in animation to the chat window.
        /// </summary>
        private void FadeInWindow()
        {
            this.Opacity = 0;
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);
        }

        // ================= Navigation Handling =================

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
                        // Already in chat window
                        break;

                    case "Tasks":
                        TaskManagementWindow taskWindow = new TaskManagementWindow(taskManager, quiz, activityLog, userName);
                        taskWindow.Show();
                        this.Close();
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
    }
}
