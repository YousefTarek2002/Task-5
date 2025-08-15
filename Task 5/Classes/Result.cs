using System;
using System.Collections.Generic;

namespace Exam02.Classes
{
    public class Result
    {
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public string ExamType { get; set; }
        public DateTime TakenAt { get; set; }
        public int Grade { get; set; }
        public int TotalMarks { get; set; }
        public bool Passed { get; set; }
        public double Percentage => TotalMarks > 0 ? (Grade * 100.0 / TotalMarks) : 0.0;
        public List<QuestionReview> Reviews { get; set; } = new List<QuestionReview>();
    }

    public class QuestionReview
    {
        public string QuestionText { get; set; }
        public string StudentAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
    }
}
