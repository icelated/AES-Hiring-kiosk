using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace JobPositionSystem
{
    public class JobApplication
    {
        private int _jobID = -1;
        public JobApplication(int jobID)
        {
            _jobID = jobID;
        }
        List<String> _answers = new List<String>();
        List<JobApplicationField> _fields = new List<JobApplicationField>();
        public List<JobApplicationField> Fields
        {
            get { return _fields; }
        }
        public int JobID()
        {
            return _jobID;
        }
        public List<String> Answers
        {
            get { return _answers; }
            set { _answers = value; }
        }
        public void AddAnswer(String answer)
        {
            _answers.Add(answer);
        }
        public void AddField(JobApplicationField field)
        {
            if (field.HasFieldID)
            {
                for (int i = 0; i < _fields.Count(); i++)
                {
                    if (_fields[i].FieldID == field.FieldID)
                    {
                        _fields[i] = field;
                        return;
                    }
                }
                
            }

            _fields.Add(field);
        }
        public void PopulateFromDatabase()
        {
            JobPositionSystemDAL dal = new JobPositionSystemDAL();
            DataSet data = dal.OpenJobOpeningByID(_jobID);
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
            for (int i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].HasCorrectAnswer == true && _fields[i].CorrectAnswer != _answers[i])
                    return false;
            }
            return true;
        }

        public void Save()
        {
            JobPositionSystemDAL database = new JobPositionSystemDAL();
            
            foreach(JobApplicationField field in _fields)
            {
                field.Save(_jobID, ref database);
            }

            //reload to get the Field/question ID for any new fields
            Reload();
        }

        public void Reload()
        {
            _fields = new List<JobApplicationField>();
            Load(_jobID);
        }

        public void Load(int jobID)
        {
            _jobID = jobID;
            DataSet ds = DB.OpenJobOpeningByID(jobID);
            DataTable table = ds.Tables[0];

            foreach (DataRow row in table.Rows)
            {
                int questionID = 0;
                object obj = row["QUESTIONID"];
                Int32.TryParse(obj.ToString(), out questionID);
                //int jobApplicationID = Int32.Parse(row["JOBAPPLICATIONID"] as String);
                string questionText = row["QUESTIONTEXT"] as String;
                int type = Int32.Parse(row["TYPEID"].ToString());
                bool hasCorrectAnswer = (row["HASCORRECTANSWER"] as String == "0") ? false : true;
                string correctAnswer = row["CORRECTANSWER"] as String;
                bool required = ((row["ISREQUIRED"] as String) == "0") ? false : true;

                JobApplicationField field = new JobApplicationField(questionID, questionText, hasCorrectAnswer, correctAnswer, required);
                AddField(field);
            }
        }
        private JobPositionSystemDAL _db;
        private JobPositionSystemDAL DB 
        {
            get
            {
                if (null == _db)
                    _db = new JobPositionSystemDAL();
                return _db;
            }
        }
    }
}
