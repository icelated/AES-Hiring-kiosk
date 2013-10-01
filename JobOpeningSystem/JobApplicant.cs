using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobPositionSystem;
using System.Data;
namespace JobOpeningSystem
{
    public class JobApplicant
    {
        List<JobApplicationField> _fields = new List<JobApplicationField>();
        List<String> _answers = new List<String>();
        JobApplicantDAL _dal = new JobApplicantDAL();
        public List<JobApplicationField> Fields
        {
            get { return _fields; }
        }
        public List<String> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }
        public void AddField(JobApplicationField field)
        {
            _fields.Add(field);
        }
        public void AddAnswer(String answer)
        {
            _answers.Add(answer);
        }
        public void PopulateFromDatabase()
        {
            DataSet data = _dal.GetGenericQuestions();
            foreach (DataRow row in data.Tables[0].Rows)
            {
                JobApplicationField field = new JobApplicationField(Convert.ToInt32(row["QUESTIONID"]), row["QUESTIONTEXT"].ToString(), Convert.ToBoolean(row["HASCORRECTANSWER"]), row["CORRECTANSWER"].ToString(), Convert.ToBoolean(row["ISREQUIRED"]));
                AddField(field);
            }
        }
        public bool Verify()
        {
            if (_fields.Count != Answers.Count)
                throw new Exception("Number of answers != number of fields");
            for(int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].HasCorrectAnswer == true && _fields[i].CorrectAnswer != _answers[i])
                    return false;
            }
            return true;
        }
    }
}
