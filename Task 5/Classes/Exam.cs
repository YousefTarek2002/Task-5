using System;
using System.Collections.Generic;

namespace Exam02.Classes
{
    public abstract class Exam
    {
        #region Properties
        public int TimeOfExam { get; set; }         
        public int NumberOfQuestions { get; set; }
        public int Grade { get; set; }
        public int TotalMarks { get; set; }
        public string SubjectName { get; set; }

        #endregion

        #region Constructors
        public Exam()
        {
            int _Time;
            do
            {
                Console.Write("Please Enter The Time of Exam (minutes): ");
                int.TryParse(Console.ReadLine(), out _Time);
            } while (_Time <= 0);
            TimeOfExam = _Time;

            int _NumberOfQuestions;
            do
            {
                Console.Write("Please Enter The Number of Questions You Want to create: ");
                int.TryParse(Console.ReadLine(), out _NumberOfQuestions);
            } while (_NumberOfQuestions <= 0);
            NumberOfQuestions = _NumberOfQuestions;

            Grade = 0;
            TotalMarks = 0;
        }

        public Exam(int timeOfExam, int numberOfQuestions , string subjectName)
        {
            TimeOfExam = timeOfExam;
            NumberOfQuestions = numberOfQuestions;
            Grade = 0;
            TotalMarks = 0;
            SubjectName = subjectName;

        }
        #endregion

        #region Methods
        protected bool TimeLeft(DateTime end) => DateTime.Now <= end;

        protected void ShowTimerHint(DateTime end)
        {
            var remaining = end - DateTime.Now;
            if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;
            Console.WriteLine($" Time left: {(int)remaining.TotalMinutes}m {remaining.Seconds}s");
        }

        public abstract Result Conduct(string subjectName, string studentName);

        public override string ToString()
        {
            return $"Time of the Exam: {TimeOfExam} min, Number of Questions: {NumberOfQuestions}";
        }
        #endregion
    }
}
