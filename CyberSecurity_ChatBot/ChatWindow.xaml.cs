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
        public ChatWindow()
        {
            InitializeComponent();
            userName = name;
            FadeInWindow();

            AddBotMessage($"Hello {userName}, welcome to CyberBot!");
            AddBotMessage("Ask me about passwords, phishing, privacy, 2FA, safe browsing, antivirus, or cloud.");
            AddBotMessage("Type 'exit' to end the chat.");
        }

        private async void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtUserInput.Text.Trim();

            if (string.IsNullOrEmpty(input))
                return;

            AddUserMessage(input);

            if (input.ToLower() == "exit")
            {
                MessageBox.Show($"Thank you, {userName}! Stay safe online.");
                this.Close();
                return;
            }

            txtUserInput.Clear();

            AddBotMessage("Typing...");

            await Task.Delay(1000); // Simulated typing delay

            // Remove "Typing..." message
            ChatStack.Children.RemoveAt(ChatStack.Children.Count - 1);

            string response = ResponseSystem.GetResponse(input, userName);
            AddBotMessage(response);

            ChatScroll.ScrollToEnd();
        }

        private void AddUserMessage(string message)
        {
            TextBlock userBubble = new TextBlock
            {
                Text = $"You: {message}",
                Background = System.Windows.Media.Brushes.LightBlue,
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Right
            };
            ChatStack.Children.Add(userBubble);
            ChatScroll.ScrollToEnd();
        }

        private void AddBotMessage(string message)
        {
            TextBlock botBubble = new TextBlock
            {
                Text = $"Bot: {message}",
                Background = System.Windows.Media.Brushes.LightGray,
                Padding = new Thickness(10),
                Margin = new Thickness(10),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            ChatStack.Children.Add(botBubble);
            ChatScroll.ScrollToEnd();
        }

        private void FadeInWindow()
        {
            this.Opacity = 0;
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);
        }
    }
}
