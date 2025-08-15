using System;
using System.Collections.Generic;

namespace Exam02.Classes
{
    public class Subject
    {
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Exam> Exams { get; set; }
        public List<Result> Results { get; set; }
        #endregion

        #region Constructors
        public Subject(int _ID, string _Name)
        {
            ID = _ID;
            Name = _Name;
            Exams = new List<Exam>();
            Results = new List<Result>();
        }
        #endregion

        #region Methods
        public void CreateExam()
        {

            int ExamType;
            do
            {
                Console.Write($"Please Enter The Exam type (1 for Practical, 2 for Final): ");
                int.TryParse(Console.ReadLine(), out ExamType);
            } while (ExamType != 1 && ExamType != 2);

            Exam newExam = (ExamType == 1) ? new PracticalExam() : new FinalExam();
            Exams.Add(newExam);
            Console.WriteLine(" Exam added successfully!");
        }

        public void ShowAvailableExams()
        {

            if (Exams.Count == 0)
            {
                Console.WriteLine(" No exams available yet.");
                return;
            }

            Console.WriteLine("\nAvailable Exams:");
            for (int i = 0; i < Exams.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Exams[i].SubjectName} ({Exams[i].GetType().Name}) - {Exams[i].NumberOfQuestions} Questions");
            }
        }

        public void ShowResultsSummary()
        {
            if (Results.Count == 0)
            {
                Console.WriteLine(" No results yet.");
                return;
            }

            Console.WriteLine("\n--- Results Summary ---");
            for (int i = 0; i < Results.Count; i++)
            {
                var r = Results[i];
                Console.WriteLine($"{i + 1}. {r.TakenAt:g} | {r.StudentName} | {r.SubjectName} | {r.ExamType} | {r.Grade}/{r.TotalMarks} ({r.Percentage:F1}%) {(r.Passed ? "✅" : "❌")}");
            }
        }

        public void PrintResultSummary(Result r, bool showReview)
        {
            Console.WriteLine($"\n==== Result for {r.StudentName} ====");
            Console.WriteLine($"Subject: {r.SubjectName}");
            Console.WriteLine($"Exam: {r.ExamType}");
            Console.WriteLine($"When: {r.TakenAt:g}");
            Console.WriteLine($"Grade: {r.Grade} / {r.TotalMarks}  => {r.Percentage:F1}%  {(r.Passed ? " Passed" : " Failed")}");

            if (showReview && r.Reviews is not null && r.Reviews.Count > 0)
            {
                Console.WriteLine("\n--- Review ---");
                int q = 1;
                foreach (var rev in r.Reviews)
                {
                    Console.WriteLine($"Q{q++}. {rev.QuestionText}");
                    Console.WriteLine($"   Your answer : {rev.StudentAnswer}");
                    Console.WriteLine($"   Correct     : {rev.CorrectAnswer} {(rev.IsCorrect ? "Passed" : "Failed")}");
                }
            }
        }
        #endregion
    }
}
