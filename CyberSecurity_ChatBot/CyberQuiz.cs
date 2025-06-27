using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurity_ChatBot
{
    class CyberQuiz
    {
        public class QuizQuestion
        {
            public string Question { get; set; }
            public string[] Options { get; set; }
            public int CorrectOptionIndex { get; set; }
            public string Explanation { get; set; }
        }

        private List<QuizQuestion> questions = new List<QuizQuestion>();
        private int currentQuestionIndex = 0;
        private int score = 0;
        private bool quizInProgress = false;

        public CyberQuiz()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
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

        public bool IsQuizInProgress()
        {
            return quizInProgress;
        }

        public string StartQuiz()
        {
            quizInProgress = true;
            currentQuestionIndex = 0;
            score = 0;
            return GetCurrentQuestion();
        }

        public string ProcessAnswer(string userAnswer)
        {
            var question = questions[currentQuestionIndex];

            bool isCorrect = false;

            if (userAnswer.Trim().ToLower() == question.Options[question.CorrectOptionIndex].Substring(0, 1).ToLower())
            {
                isCorrect = true;
                score++;
            }

            string feedback = isCorrect ? question.Explanation : $"Incorrect. {question.Explanation}";

            currentQuestionIndex++;

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

        private string GetCurrentQuestion()
        {
            var question = questions[currentQuestionIndex];
            string options = string.Join("\n", question.Options);
            return $"{question.Question}\n{options}\nPlease answer with A, B, C, or D.";
        }

        private string GetFinalFeedback()
        {
            if (score >= 8)
                return "Excellent! You’re a cybersecurity pro!";
            else if (score >= 5)
                return "Good effort! Keep learning to strengthen your skills.";
            else
                return "Don't worry, keep learning and practicing cybersecurity safety.";
        }
    }
}
