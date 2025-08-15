using System;

namespace Exam02.Classes
{
    public abstract class Question : ICloneable
    {
        #region Properties
        public string Header { get; set; }
        public string Body { get; set; }
        public int Mark { get; set; }
        public Answer[] Answers { get; set; }
        public int RightAnswerIndex { get; set; }  
        #endregion

        #region Constructors
        public Question(string _Header)
        {
            Header = _Header;
            do
            {
                Console.Write("Please Enter The Body Of The Question: ");
                Body = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(Body));

            int _Marks;
            do
            {
                Console.Write("Please Enter The Marks Of the Question: ");
            } while (!int.TryParse(Console.ReadLine(), out _Marks) || _Marks <= 0);
            Mark = _Marks;
        }

        public Question(string _Header, string _Body, int _Mark)
        {
            Header = _Header;
            Body = _Body;
            Mark = _Mark;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Header}      Mark({Mark}) \n{Body}?";
        }

        public abstract object Clone();
        #endregion
    }
}
