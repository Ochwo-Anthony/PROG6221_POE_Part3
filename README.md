# PROG6221-POE-Part3


Project Name: Cybersecurity Awareness Bot (GUI)

Project Description:
The Cybersecurity Awareness Bot is an educational chatbot application built with C# WPF (Windows Presentation Foundation). It helps users learn about cybersecurity topics such as password safety, phishing, safe browsing, and privacy settings. The bot integrates an interactive chat, a task assistant with reminders, a cybersecurity quiz, an NLP-based conversation system, and an activity log.

The project builds upon the console-based chatbot from Part 1 and Part 2, now expanded into a full graphical user interface (GUI) application.

GITHUB LINK:
https://github.com/Ochwo-Anthony/PROG6221_POE_Part3

UNLISTED YOUTUBE VIDEO LINK:
https://www.youtube.com/watch?v=yIKaLkx-rHQ


Project Structure:
The project has the following structure:
CyberSecurity_ChatBot/
	.vs/ (Visual Studio configuration files)
	bin/Debug/net9.0-windows/ (Build output files)
	obj/ (Temporary build files)
	ActivityLog.cs (Activity Log tracking system)
	App.xaml (WPF App configuration)
	App.xaml.cs
	AssemblyInfo.cs
	ChatWindow.xaml (Main chat window UI)
	ChatWindow.xaml.cs
	CyberQuiz.cs (Cybersecurity Quiz logic)
	CyberSecurity_ChatBot.csproj (Project configuration file)
	CyberSecurity_ChatBot.sln (Visual Studio solution file)
	IntroWindow.xaml (Introductory screen UI)
	IntroWindow.xaml.cs
	MainWindow.xaml (Startup window)
	MainWindow.xaml.cs
	NameEntryWindow.xaml (Name entry screen UI)
	NameEntryWindow.xaml.cs
	NlpProcessor.cs (Natural Language Processing logic)
	ResponseSystem.cs (Chatbot response handling)
	TaskItem.cs (Task management logic)
	README.md (Project documentation)



Prerequisites:
Before running the application, ensure that you have the following:

- .NET SDK (version 9 or later) installed on your machine.
  - You can download it from: https://dotnet.microsoft.com/download
- GitHub account for accessing the project repository and running CI/CD pipelines.
- A Windows system that supports WPF desktop applications
Setup and Installation:
1. Clone the repository:

	git clone https://github.com/Ochwo-Anthony/PROG6221_POE_Part3.git

2. Open the project in Visual Studio 2022 or later.

3. Ensure that the target framework is set to .NET 9.0.

4. Build the solution:

	Go to Build > Build Solution or press Ctrl + Shift + B.

5. Run the application:

	Press F5 to start the WPF app.

Usage:

Features and User Guide

Task Assistant with Reminders

	Add cybersecurity tasks like "Enable two-factor authentication" or "Review privacy settings."
	Specify a reminder date or timeframe (e.g., "Remind me in 3 days").
	View, complete, and delete tasks.
	Fully integrated with chatbot conversation flow and GUI.

Cybersecurity Quiz (Mini-Game)

Accessible via the navigation menu.

	Contains 10 cybersecurity questions (mix of multiple-choice and true/false).
	Provides immediate feedback after each answer.
	Displays final score and performance feedback at the end of the quiz.

Natural Language Processing (NLP) Simulation

Recognizes varied phrasing of user commands like:

	"Add a task to update my password"
	"Set a reminder to enable 2FA"
	Uses regex and keyword detection to interpret user intent.
	Handles flexible, natural language input to add tasks, start quizzes, and more.

Activity Log

Records all significant user actions:

	Tasks added, completed, or deleted
	Reminders set
	Quiz attempts
	NLP-triggered actions
	Accessible via chat command or GUI.
	Displays the last 5-10 recent actions in a clear list.

Example Interactions

Task Example:

	User:Add task to review privacy settings.Chatbot:Task added with the description "Review account privacy settings to ensure your data is 	protected." Would you like a reminder?
	User:Yes, remind me in 3 days.Chatbot:Got it! I'll remind you in 3 days.

Quiz Example:

	User:Start quiz.Chatbot:Question: What should you do if you receive an email asking for your password?A) Reply with your passwordB) Delete the 	emailC) Report the email as phishingD) Ignore it
	User:CChatbot:Correct! Reporting phishing emails helps prevent scams.

NLP Example:

	User:Can you remind me to update my password tomorrow?
	Chatbot:Reminder set for 'Update my password' on tomorrow's date.

Activity Log Example:

	User:Show activity log.
	Chatbot:Here’s a summary of recent actions:
	Task added: 'Enable two-factor authentication' (Reminder set for 5 days from now).
	Quiz started - 5 questions answered.
	Reminder set: 'Review privacy settings' on [specific date].

CI/CD Pipeline:
This project is integrated with GitHub Actions for Continuous Integration/Continuous Deployment (CI/CD). The configuration is in the .github/workflows/main.yml file. Upon pushing changes to the repository, the workflow automatically builds and tests the project.

Release Notes

v2.0.0 — 2025-06-27

- Integrated Task Management with GUI
- Added Reminders with Flexible Timeframes (e.g., "tomorrow", "in 3 days")
- Developed Cybersecurity Quiz with GUI and Score Tracking
- Implemented NLP Simulation with Regex-based Keyword Recognition
- Added Activity Log Feature with Recent Action Tracking

v1.1.0 — 2025-05-26:
New Features & Enhancements

Sentiment Detection & Empathy

- Added advanced sentiment detection to recognize emotional tones like worried, curious, and frustrated.
- The chatbot now provides tailored, empathetic responses based on the detected mood to improve user experience.

Dynamic Conversation Flow

- Introduced support for contextual follow-ups using triggers like "more info" or "anything else", allowing users to explore topics in depth.
- Sentiment-based responses are automatically cleared when a new topic is detected, ensuring relevant tone adaptation.

Personalized Interaction

- Tracks favorite topics based on user queries and delivers proactive tips after every few messages to reinforce learning.

Structured & Maintainable Design

- Refactored response handling using dictionaries and modular functions for better scalability and easier maintenance.

Intelligent Proactive Prompts

- Periodically provides helpful tips tailored to the user’s most engaged topic to encourage safer online habits.

Input Validation & Robust Handling

- Improved user input checks to prevent empty questions and handle unrecognized input gracefully with friendly fallback suggestions.

v1.0.0 — 2025-04-17:

Initial release of Cybersecurity Awareness Bot.

Basic chatbot features covering password safety, phishing, privacy, and other cybersecurity topics.

Included audio and ASCII art greetings.

Implemented simple user input handling and random response selection.

    REFERENCES & CONTRIBUTIONS
 ----------------------------------------------------------------------------

REFERENCES

Troelsen, A., & Japikse, P. (2022). *Pro C# 10 with .NET 6: Foundational principles and practices in programming* (11th ed.). Apress.

CONTRIBUTIONS

    1. ASCII Art Generator:
        - Source : https://patorjk.com/software/taag
        - Use    : For generating styled welcome ASCII art in the AsciiArt class.

    2. Console Colors and Styling:
        - Concept : https://learn.microsoft.com/en-us/dotnet/api/system.console.foregroundcolor
        - Use    : For improving user interface with color-coded responses and prompts.

    3. Exception Handling and User Input Validation:
        - Tutorials : Microsoft C# Documentfation, YouTube (freeCodeCamp C# tutorials)
        - Use    : Try-catch block logic in `TextGreeting` and `ResponseSystem`.

    4. Audio Playback in Console:
        - Idea   : Adapted from community suggestions on StackOverflow:
                  https://stackoverflow.com/questions/221925/playing-audio-file-in-c-sharp
        - Use    : Implemented in `AudioPlayer` class with `System.Media.SoundPlayer`.

    5. Chatbot Conversation Logic:
        - Inspiration : Basic decision tree structure from beginner chatbot models.
        - Use    : Implemented in `ResponseSystem.GetResponse()` using `string.Contains`.

    6. General Formatting Tips & Visual Prompts:
        - Community Snippets & Visual Enhancements: ChatGPT-assisted refinements.
        - Use    : Enhanced user prompts.

Contact:
For more information or to report issues, please contact:
- Author: Anthony Ochwo
- Student Number: ST10395938
- Email: st10395938@imconnect.edu.za
