
using System;
using System.Collections.Generic;

namespace Task5Improved
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Exam Management System";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== Welcome to the Exam Management System ===");
            Console.ResetColor();

            Subject subject = CreateSubject();

            Exam exam = null;

            bool running = true;
            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n--- Main Menu ---");
                Console.ResetColor();
                Console.WriteLine("1. Create Final Exam");
                Console.WriteLine("2. Create Practical Exam");
                Console.WriteLine("3. View Exam Details");
                Console.WriteLine("4. Start Exam");
                Console.WriteLine("5. Exit");

                int choice = ReadInt("Choose an option (1-5): ", 1, 5);

                switch (choice)
                {
                    case 1:
                        exam = CreateExam(subject, true);
                        break;
                    case 2:
                        exam = CreateExam(subject, false);
                        break;
                    case 3:
                        if (exam != null)
                            exam.ShowExam();
                        else
                            ShowError("No exam created yet.");
                        break;
                    case 4:
                        if (exam != null)
                            exam.StartExam();
                        else
                            ShowError("No exam created yet.");
                        break;
                    case 5:
                        running = false;
                        Console.WriteLine("Exiting... Goodbye!");
                        break;
                }
            }
        }

        static Subject CreateSubject()
        {
            Console.WriteLine("\nEnter Subject Details:");
            Console.Write("Subject Name: ");
            string name = Console.ReadLine();
            Console.Write("Subject Code: ");
            string code = Console.ReadLine();

            return new Subject(name, code);
        }

        static Exam CreateExam(Subject subject, bool isFinal)
        {
            Console.WriteLine($"\nCreating {(isFinal ? "Final" : "Practical")} Exam:");
            int time = ReadInt("Enter exam duration (minutes): ", 1, 300);
            int numQuestions = ReadInt("Enter number of questions: ", 1, 50);

            Exam exam = isFinal ? new FinalExam(time, numQuestions, subject)
                                : new PracticalExam(time, numQuestions, subject);

            for (int i = 0; i < numQuestions; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n--- Question {i + 1} ---");
                Console.ResetColor();

                Question question;
                if (isFinal)
                {
                    Console.WriteLine("1. MCQ");
                    Console.WriteLine("2. True/False");
                    int qType = ReadInt("Choose question type: ", 1, 2);
                    question = (qType == 1) ? CreateMCQ() : CreateTrueFalse();
                }
                else
                {
                    question = CreateMCQ();
                }

                exam.AddQuestion(question);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Exam created successfully!");
            Console.ResetColor();

            return exam;
        }

        static MCQQuestion CreateMCQ()
        {
            Console.Write("Question Text: ");
            string text = Console.ReadLine();
            List<string> options = new List<string>();

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Option {i + 1}: ");
                options.Add(Console.ReadLine());
            }

            int correct = ReadInt("Enter correct option number (1-3): ", 1, 3);
            return new MCQQuestion(text, options, correct);
        }

        static TrueOrFalseQuestion CreateTrueFalse()
        {
            Console.Write("Question Text: ");
            string text = Console.ReadLine();
            int correct = ReadInt("Enter correct answer (1=True, 2=False): ", 1, 2);
            return new TrueOrFalseQuestion(text, correct == 1);
        }

        static int ReadInt(string prompt, int min, int max)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    return value;
                ShowError($"Please enter a number between {min} and {max}.");
            }
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    class Subject
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Subject(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }

    abstract class Exam
    {
        public int Time { get; set; }
        public int NumQuestions { get; set; }
        public Subject Subject { get; set; }
        protected List<Question> Questions { get; set; }

        public Exam(int time, int numQuestions, Subject subject)
        {
            Time = time;
            NumQuestions = numQuestions;
            Subject = subject;
            Questions = new List<Question>();
        }

        public void AddQuestion(Question q) => Questions.Add(q);

        public virtual void ShowExam()
        {
            Console.WriteLine($"\nExam for: {Subject.Name} ({Subject.Code})");
            Console.WriteLine($"Duration: {Time} minutes");
            Console.WriteLine($"Number of Questions: {NumQuestions}");
        }

        public virtual void StartExam()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n--- Starting Exam ---");
            Console.ResetColor();

            int score = 0;
            for (int i = 0; i < Questions.Count; i++)
            {
                var q = Questions[i];
                Console.WriteLine($"\nQ{i + 1}: {q.Text}");
                q.DisplayOptions();
                int answer = Program.ReadInt("Your answer: ", 1, q.GetOptionCount());

                if (q.CheckAnswer(answer))
                {
                    score++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong!");
                }
                Console.ResetColor();
            }

            Console.WriteLine($"\nYour score: {score}/{Questions.Count}");
        }
    }

    class FinalExam : Exam
    {
        public FinalExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }
    }

    class PracticalExam : Exam
    {
        public PracticalExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }
    }

    abstract class Question
    {
        public string Text { get; set; }
        public Question(string text) => Text = text;
        public abstract void DisplayOptions();
        public abstract int GetOptionCount();
        public abstract bool CheckAnswer(int answer);
    }

    class MCQQuestion : Question
    {
        private List<string> Options { get; set; }
        private int CorrectOption { get; set; }

        public MCQQuestion(string text, List<string> options, int correctOption) : base(text)
        {
            Options = options;
            CorrectOption = correctOption;
        }

        public override void DisplayOptions()
        {
            for (int i = 0; i < Options.Count; i++)
                Console.WriteLine($"{i + 1}. {Options[i]}");
        }

        public override int GetOptionCount() => Options.Count;

        public override bool CheckAnswer(int answer) => answer == CorrectOption;
    }

    class TrueOrFalseQuestion : Question
    {
        private bool CorrectAnswer { get; set; }

        public TrueOrFalseQuestion(string text, bool correct) : base(text)
        {
            CorrectAnswer = correct;
        }

        public override void DisplayOptions()
        {
            Console.WriteLine("1. True");
            Console.WriteLine("2. False");
        }

        public override int GetOptionCount() => 2;

        public override bool CheckAnswer(int answer) => (answer == 1) == CorrectAnswer;
    }
}
