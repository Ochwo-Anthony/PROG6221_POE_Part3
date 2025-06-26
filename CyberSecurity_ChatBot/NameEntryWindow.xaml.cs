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
    /// Interaction logic for NameEntryWindow.xaml
    /// </summary>
    public partial class NameEntryWindow : Window
    {
        public NameEntryWindow()
        {
            InitializeComponent();
            FadeInWindow();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUserName.Text.Trim();

            if (!string.IsNullOrEmpty(userName))
            {
                ChatWindow chatWindow = new ChatWindow(userName);
                FadeOutAndClose(chatWindow);
            }
            else
            {
                MessageBox.Show("Please enter your name to continue.", "Input Required", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FadeInWindow()
        {
            this.Opacity = 0;
            var fadeIn = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.5)));
            this.BeginAnimation(Window.OpacityProperty, fadeIn);
        }

        private void FadeOutAndClose(Window nextWindow)
        {
            var fadeOut = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.5)));
            fadeOut.Completed += (s, e) =>
            {
                nextWindow.Show();
                this.Close();
            };
            this.BeginAnimation(Window.OpacityProperty, fadeOut);
        }
    }
}
