using System;

namespace Exam02.Classes
{
    public class Answer : ICloneable
    {
        #region Properties
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        #endregion

        #region Constructors
        public Answer(int _AnswerID, string _AnswerText)
        {
            AnswerID = _AnswerID;
            AnswerText = _AnswerText;
        }

        public Answer(Answer answer)
        {
            AnswerID = answer.AnswerID;
            AnswerText = answer.AnswerText;
        }
        #endregion

        #region Methods
        public object Clone()
        {
            return new Answer(this);
        }

        public override string ToString()
        {
            return $"Answer ID: {AnswerID}, Answer Text: {AnswerText}";
        }
        #endregion
    }
}
