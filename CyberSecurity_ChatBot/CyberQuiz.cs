using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    /// <summary>
    /// CyberQuiz class manages the cybersecurity quiz logic, including questions, answers, and scoring.
    /// </summary>
    public class CyberQuiz
    {
        /// <summary>
        /// Represents a single quiz question with options, correct answer, and explanation.
        /// </summary>
        public class QuizQuestion
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public int CorrectOptionIndex { get; set; }
            public string Explanation { get; set; }
        }

        private List<QuizQuestion> questions = new List<QuizQuestion>(); // List to store quiz questions
        private int currentQuestionIndex = 0; // Tracks the current question index
        private int score = 0; // Stores the user's quiz score
        private bool quizInProgress = false; // Tracks whether the quiz is currently active

        /// <summary>
        /// Constructor that initializes the quiz and loads the questions.
        /// </summary>
        public CyberQuiz()
        {
            LoadQuestions();
        }

        /// <summary>
        /// Loads all the quiz questions into the quiz.
        /// </summary>
        private void LoadQuestions()
        {
            // Each question includes the question text, options, correct answer index, and an explanation.

            questions.Add(new QuizQuestion
            {
                Question = "What should you do if you receive an email asking for your password?",
                Options = new string[] { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" },
                CorrectOptionIndex = 2,
                Explanation = "Correct! Reporting phishing emails helps prevent scams."
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Using the same password everywhere is safe.",
                Options = new string[] { "A) True", "B) False" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Using unique passwords helps protect your accounts."
            });

            questions.Add(new QuizQuestion
            {
                Question = "Which of the following is the most secure password?",
                Options = new string[] { "A) password123", "B) MyBirthday2020", "C) $tr0ng!Passw0rd", "D) admin" },
                CorrectOptionIndex = 2,
                Explanation = "Correct! Strong passwords use symbols, numbers, and are hard to guess."
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: HTTPS websites are safer than HTTP websites.",
                Options = new string[] { "A) True", "B) False" },
                CorrectOptionIndex = 0,
                Explanation = "Correct! HTTPS encrypts your connection."
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is two-factor authentication (2FA)?",
                Options = new string[] { "A) A password reset tool", "B) An extra layer of login security", "C) A backup system", "D) A malware scanner" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! 2FA adds extra security to your accounts."
            });

            questions.Add(new QuizQuestion
            {
                Question = "What is phishing?",
                Options = new string[] { "A) A form of secure browsing", "B) Attempting to steal information via fake emails", "C) A firewall update", "D) A virus removal process" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Phishing tries to trick you into giving personal info."
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Antivirus software should be updated regularly.",
                Options = new string[] { "A) True", "B) False" },
                CorrectOptionIndex = 0,
                Explanation = "Correct! Antivirus updates keep you protected against new threats."
            });

            questions.Add(new QuizQuestion
            {
                Question = "Which of the following is NOT a good security practice?",
                Options = new string[] { "A) Sharing your password", "B) Using strong passwords", "C) Keeping software updated", "D) Using 2FA" },
                CorrectOptionIndex = 0,
                Explanation = "Correct! Never share your password."
            });

            questions.Add(new QuizQuestion
            {
                Question = "What does a firewall do?",
                Options = new string[] { "A) Deletes files", "B) Blocks unauthorized access", "C) Stores backups", "D) Speeds up your computer" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Firewalls block unauthorized access."
            });

            questions.Add(new QuizQuestion
            {
                Question = "True or False: Clicking on pop-up ads is always safe.",
                Options = new string[] { "A) True", "B) False" },
                CorrectOptionIndex = 1,
                Explanation = "Correct! Pop-up ads may lead to malicious sites."
            });
        }

        /// <summary>
        /// Checks if the quiz is currently active.
        /// </summary>
        public bool IsQuizInProgress()
        {
            return quizInProgress;
        }

        /// <summary>
        /// Starts the quiz and returns the first question.
        /// </summary>
        public string StartQuiz()
        {
            quizInProgress = true;
            currentQuestionIndex = 0;
            score = 0;
            return GetCurrentQuestion();
        }

        /// <summary>
        /// Processes the user's answer and provides feedback.
        /// </summary>
        public string ProcessAnswer(string userAnswer)
        {
            string answer = userAnswer.Trim().ToLower();

            // Validate input: only A, B, C, or D are accepted.
            if (answer != "a" && answer != "b" && answer != "c" && answer != "d")
            {
                return "Please answer with only A, B, C, or D.";
            }

            var question = questions[currentQuestionIndex];
            bool isCorrect = false;

            // Check if the answer is correct by comparing to the correct option.
            if (answer == question.Options[question.CorrectOptionIndex].Substring(0, 1).ToLower())
            {
                isCorrect = true;
                score++;
            }

            string feedback = isCorrect ? question.Explanation : $"Incorrect. {question.Explanation}";

            currentQuestionIndex++;

            // If the quiz is complete, return the final score and feedback.
            if (currentQuestionIndex >= questions.Count)
            {
                quizInProgress = false;
                return $"{feedback}\n\nQuiz Complete!\nYour Score: {score}/{questions.Count}\n{GetFinalFeedback()}";
            }
            else
            {
                return $"{feedback}\n\nNext Question:\n{GetCurrentQuestion()}";
            }
        }

        /// <summary>
        /// Returns the current question text and options.
        /// </summary>
        private string GetCurrentQuestion()
        {
            var question = questions[currentQuestionIndex];
            string options = string.Join("\n", question.Options);
            return $"{question.Question}\n{options}\nPlease answer with A, B, C, or D.";
        }

        /// <summary>
        /// Provides final feedback based on the user's score.
        /// </summary>
        private string GetFinalFeedback()
        {
            if (score >= 8)
                return "Excellent! You’re a cybersecurity pro!";
            else if (score >= 5)
                return "Good effort! Keep learning to strengthen your skills.";
            else
                return "Don't worry, keep learning and practicing cybersecurity safety.";
        }

        /// <summary>
        /// Returns the total number of questions in the quiz.
        /// </summary>
        public int GetQuestionCount()
        {
            return questions.Count;
        }

        /// <summary>
        /// Retrieves a specific question by index.
        /// </summary>
        public CyberQuiz.QuizQuestion GetQuestion(int index)
        {
            return questions[index];
        }

        /// <summary>
        /// Checks if the selected answer is correct for a specific question.
        /// </summary>
        public bool IsAnswerCorrect(int index, int selectedOption)
        {
            return questions[index].CorrectOptionIndex == selectedOption;
        }

        /// <summary>
        /// Provides final feedback based on a given score (for use in other windows).
        /// </summary>
        public string GetFinalFeedback(int score)
        {
            if (score >= 8)
                return "Excellent! You're a cybersecurity pro!";
            else if (score >= 5)
                return "Good effort! Keep learning to strengthen your skills.";
            else
                return "Don't worry, keep learning and practicing cybersecurity safety.";
        }
    }
}
