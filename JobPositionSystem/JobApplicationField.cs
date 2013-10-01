using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPositionSystem
{
    public class JobApplicationField
    {
        public JobApplicationField()
        { }

        public JobApplicationField(string fieldText, bool hasCorrectAnswer, string correctAnswer, bool answerRequired)
        {
            _hasFieldID = false;
            FieldText = fieldText;
            HasCorrectAnswer = hasCorrectAnswer;
            CorrectAnswer = correctAnswer;
            AnswerRequired = answerRequired;
        }

        public JobApplicationField(int fieldID, string fieldText, bool hasCorrectAnswer, string correctAnswer, bool answerRequired)
        {
            _hasFieldID = true;
            FieldID = fieldID;
            FieldText = fieldText;
            HasCorrectAnswer = hasCorrectAnswer;
            CorrectAnswer = correctAnswer;
            AnswerRequired = answerRequired;
        }
        private bool _deprecated = false;
        public bool Deprecated
        {
            get { return _deprecated; }
            set { _deprecated = value; }
        }

        private bool _hasFieldID = false;
        public bool HasFieldID
        {
            get { return _hasFieldID; }
        }

        private int _fieldID;
        public int FieldID
        {
            get { return _fieldID; }
            set { _fieldID = value; 
                _hasFieldID = true;
            }
        }

        private string _fieldText;
        public string FieldText
        {
            get { return _fieldText; }
            set { _fieldText = value; }
        }

        private bool _hasCorrectAnswer = false;
        public bool HasCorrectAnswer
        {
            get { return _hasCorrectAnswer; }
            set { _hasCorrectAnswer = value; }
        }

        private string _correctAnswer;
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }

        private bool _answerRequired = false;
        public bool AnswerRequired
        {
            get { return _answerRequired; }
            set { _answerRequired = value; }
        }

        private int _fieldType = 1;
        public int FieldType
        {
            get { return _fieldType; }
        }


        public void Save(int jobID, ref JobPositionSystemDAL db)
        {
            //if it has a field ID it is a new field and needs a field ID
            int hasCorrectAnswer = HasCorrectAnswer ? 1 : 0;
            int answerRequired = AnswerRequired ? 1 : 0;
            if (HasFieldID)
            {
                db.UpdateJobApplicationField(FieldID, jobID, FieldText, FieldType, hasCorrectAnswer, CorrectAnswer, answerRequired);
            }
            else
            {
                db.AddField(jobID, FieldText, FieldType, hasCorrectAnswer, CorrectAnswer, answerRequired);
            }
        }
    }
}
