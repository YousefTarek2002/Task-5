using System;
using System.Collections.Generic;
using System.Text;

namespace Exam02.Classes
{
    public class FinalExam : Exam
    {
        #region Properties
        public Question[] Questions { get; set; }
        public int[] Answers { get; set; } 
        #endregion

        #region Constructors
        public FinalExam(int time) : base()
        {
            Questions = new Question[NumberOfQuestions];
            Answers = new int[NumberOfQuestions];
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                int QuesType;
                do
                {
                    Console.Write($"Please Choose The Type of the {i + 1}th Question (1 for True | False, 2 for MCQ): ");
                    int.TryParse(Console.ReadLine(), out QuesType);
                } while (QuesType != 1 && QuesType != 2);

                Questions[i] = (QuesType == 1) ? new TrueOrFalseQuestion() : new MCQQuestion();
                TotalMarks += Questions[i].Mark;
                Answers[i] = -1;
            }
        }

        public FinalExam()
        {
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

                Console.WriteLine(Questions[i]);
                ShowTimerHint(end);
                Console.WriteLine("---------------------------------------------------");
                int _Answer;
                do
                {
                    int.TryParse(Console.ReadLine(), out _Answer);
                } while (_Answer <= 0 || _Answer > Questions[i].Answers.Length);

                Answers[i] = _Answer - 1;
                if (Questions[i].RightAnswerIndex == Answers[i])
                {
                    Grade += Questions[i].Mark;
                }
            }

            // Build review
            var reviews = new List<QuestionReview>();
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                var q = Questions[i];
                string studentAns = (Answers[i] >= 0 && Answers[i] < q.Answers.Length)
                    ? q.Answers[Answers[i]].AnswerText
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
                ExamType = nameof(FinalExam),
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
            for (int i = 0; i < NumberOfQuestions; i++)
            {
                var q = Questions[i];
                string studentAns = (Answers[i] >= 0 && Answers[i] < q.Answers.Length)
                    ? q.Answers[Answers[i]].AnswerText
                    : "No Answer";
                sb.AppendLine($"Q{i + 1}. {q.Body} : {studentAns}");
            }
            return sb.ToString();
        }
        #endregion
    }
}
