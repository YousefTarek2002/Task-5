using System;
using System.Collections.Generic;
using System.Text;

namespace Exam02.Classes
{
    public class PracticalExam : Exam
    {
        #region Properties
        public MCQQuestion[] MCQQuestions { get; set; }
        public int[] MCQAnswers { get; set; }
        #endregion

        #region Constructors
        public PracticalExam() : base()
        {
            MCQQuestions = new MCQQuestion[NumberOfQuestions];
            MCQAnswers = new int[NumberOfQuestions];
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                MCQQuestions[i] = new MCQQuestion();
                TotalMarks += MCQQuestions[i].Mark;
            }
        }
        #endregion

        #region Methods
        public override Result Conduct(string subjectName, string studentName)
        {
            Grade = 0;
            DateTime start = DateTime.Now;
            DateTime end = start.AddMinutes(TimeOfExam);

            for (int i = 0; i < NumberOfQuestions; i++)
            {
                if (!TimeLeft(end))
                {
                    Console.WriteLine("\n⏰ Time is up!");
                    break;
                }

                Console.WriteLine(MCQQuestions[i]);
                ShowTimerHint(end);
                Console.WriteLine("---------------------------------------------------");

                int _Answer;
                do
                {
                    int.TryParse(Console.ReadLine(), out _Answer);
                } while (_Answer <= 0 || _Answer > 3);

                MCQAnswers[i] = _Answer - 1;
                if (MCQQuestions[i].RightAnswerIndex == MCQAnswers[i])
                {
                    Grade += MCQQuestions[i].Mark;
                }
            }

            // Build review
            var reviews = new List<QuestionReview>();
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                var q = MCQQuestions[i];
                string studentAns = (i < MCQAnswers.Length && MCQAnswers[i] >= 0 && MCQAnswers[i] < 3)
                    ? q.Answers[MCQAnswers[i]].AnswerText
                    : "No Answer";
                string correct = q.Answers[q.RightAnswerIndex].AnswerText;

                reviews.Add(new QuestionReview
                {
                    QuestionText = q.Body,
                    StudentAnswer = studentAns,
                    CorrectAnswer = correct,
                    IsCorrect = (studentAns == correct)
                });
            }

            var result = new Result
            {
                StudentName = studentName,
                SubjectName = subjectName,
                ExamType = nameof(PracticalExam),
                TakenAt = DateTime.Now,
                Grade = Grade,
                TotalMarks = TotalMarks,
                Passed = Grade >= (TotalMarks / 2.0),
                Reviews = reviews
            };

            Console.WriteLine(ToString());
            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Right Answers: ");
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                sb.AppendLine($"Q{i + 1}. {MCQQuestions[i].Answers[MCQQuestions[i].RightAnswerIndex].AnswerText}");
            }
            return sb.ToString();
        }
        #endregion
    }
}
