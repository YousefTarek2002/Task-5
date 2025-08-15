using System;

namespace Exam02.Classes
{
    public class TrueOrFalseQuestion : Question
    {
        #region Constructors
        public TrueOrFalseQuestion() : base("True | False Question")
        {
            Answers = new Answer[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };

            int _Answer;
            do
            {
                Console.Write("Please Enter The Right Answer of Question (1 for True and 2 for False): ");
            } while (!int.TryParse(Console.ReadLine(), out _Answer) || (_Answer != 1 && _Answer != 2));
            RightAnswerIndex = _Answer - 1;
        }

        public TrueOrFalseQuestion(string _Header, string _Body, int _Mark, int _RightAnswer)
            : base(_Header, _Body, _Mark)
        {
            Answers = new Answer[]
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
            RightAnswerIndex = _RightAnswer - 1;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return base.ToString() + $"\n1. True\n2. False";
        }

        public override object Clone()
        {
            return new TrueOrFalseQuestion(Header, Body, Mark, RightAnswerIndex + 1);
        }
        #endregion
    }
}
