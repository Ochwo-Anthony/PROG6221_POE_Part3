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
    /// This is the introduction window that appears when the application starts.
    /// It provides a smooth fade-in effect and allows the user to proceed to the name entry window.
    /// </summary>
    public partial class IntroWindow : Window
    {
        /// <summary>
        /// Constructor that initializes the IntroWindow and starts the fade-in animation.
        /// </summary>
        public IntroWindow()
        {
            InitializeComponent();
            FadeInWindow(); // Smooth fade-in when window loads
        }

        /// <summary>
        /// Event handler for the Start button click.
        /// It opens the NameEntryWindow and fades out the IntroWindow.
        /// </summary>
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NameEntryWindow nameEntryWindow = new NameEntryWindow(); // Create a new NameEntryWindow
            FadeOutAndClose(nameEntryWindow); // Fade out the intro and transition to the name entry window
        }

        /// <summary>
        /// Applies a fade-in animation when the IntroWindow loads.
        /// </summary>
        private void FadeInWindow()
        {
            this.Opacity = 0; // Start fully transparent
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5))); // Animate to fully visible over 0.5 seconds
            this.BeginAnimation(Window.OpacityProperty, fadeIn); // Apply the animation to the window's opacity
        }

        /// <summary>
        /// Applies a fade-out animation and opens the next window when the animation completes.
        /// </summary>
        /// <param name="nextWindow">The window to open after fade-out.</param>
        private void FadeOutAndClose(Window nextWindow)
        {
            var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.5))); // Animate to fully transparent over 0.5 seconds

            // When fade-out completes, show the next window and close the intro window
            fadeOut.Completed += (s, e) =>
            {
                nextWindow.Show(); // Show the next window
                this.Close(); // Close the intro window
            };

            this.BeginAnimation(Window.OpacityProperty, fadeOut); // Apply the fade-out animation
        }
    }
}
