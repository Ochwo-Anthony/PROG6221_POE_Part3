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

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// This window handles user name entry and provides animated transitions to the chat window.
    /// </summary>
    public partial class NameEntryWindow : Window
    {
        /// <summary>
        /// Initializes the NameEntryWindow and applies a fade-in animation when the window loads.
        /// </summary>
        public NameEntryWindow()
        {
            InitializeComponent();
            FadeInWindow(); // Smooth fade-in animation when the window opens
        }

        /// <summary>
        /// Handles the click event when the user presses the "Enter" button.
        /// Validates that a name is entered, then opens the ChatWindow.
        /// </summary>
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim(); // Get and trim the entered name

            if (!string.IsNullOrEmpty(userName))
            {
                // Open the chat window and pass the entered name
                ChatWindow chatWindow = new ChatWindow(userName);
                FadeOutAndClose(chatWindow); // Smoothly fade out and transition to the chat window
            }
            else
            {
                // Show warning if the name field is empty
                MessageBox.Show("Please enter your name to continue.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Animates the window with a fade-in effect on load.
        /// </summary>
        private void FadeInWindow()
        {
            this.Opacity = 0; // Start with window fully transparent
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5))); // 0.5 second fade-in
            this.BeginAnimation(Window.OpacityProperty, fadeIn); // Apply the animation to the window's opacity
        }

        /// <summary>
        /// Fades out the current window, then opens the next window (ChatWindow).
        /// </summary>
        /// <param name="nextWindow">The window to open after the fade-out completes.</param>
        private void FadeOutAndClose(Window nextWindow)
        {
            var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.5))); // 0.5 second fade-out

            // When fade-out is complete, open the next window and close this one
            fadeOut.Completed += (s, e) =>
            {
                nextWindow.Show(); // Show the chat window
                this.Close(); // Close the name entry window
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOut); // Apply the fade-out animation
        }
    }
}
