using System;

namespace Exam02.Classes
{
    public class MCQQuestion : Question
    {
        #region Constructors
        public MCQQuestion() : base("Choose One Answer Question")
        {
            Console.WriteLine("The Choices of Question");
            Answers = new Answer[3];
            string _Answer;
            for (int i = 0; i < 3; i++)
            {
                do
                {
                    Console.Write($"Please Enter The Choice Number {i + 1}: ");
                    _Answer = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(_Answer));
                Answers[i] = new Answer(i + 1, _Answer);
            }
            int RightIndex;
            do
            {
                Console.Write("Please Enter The Index of Right Answer (1-3): ");
            } while (!int.TryParse(Console.ReadLine(), out RightIndex) || RightIndex < 1 || RightIndex > 3);
            RightAnswerIndex = RightIndex - 1;
        }

        public MCQQuestion(string _Header, string _Body, int _Mark, Answer[] _Answers, int _RightIndex)
            : base(_Header, _Body, _Mark)
        {
            Answers = (Answer[])_Answers.Clone();
            RightAnswerIndex = _RightIndex;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += $"\n {i + 1}. {Answers[i].AnswerText}";
            }
            return base.ToString() + $"{s}";
        }

        public override object Clone()
        {
            return new MCQQuestion(Header, Body, Mark, Answers, RightAnswerIndex);
        }
        #endregion
    }
}
