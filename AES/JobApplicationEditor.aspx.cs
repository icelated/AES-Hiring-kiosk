using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;
using JobPositionSystem;

namespace AES
{
    public partial class JobApplicationEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if("POST" == Request.RequestType)
            {
                NameValueCollection coll;
                JobApplication app = Session["JobApplication"] as JobApplication;
                if (null == app)
                {
                    JobPositionSystemDAL Database = new JobPositionSystemDAL();
                  //  app = Database.OpenJobOpeningApplication(1);
                    app = Database.OpenJobApplication(1);
                    Session["JobApplication"] = app;
                }

                //Load Form variables into NameValueCollection variable.
                coll = Request.Form;
                // Get names of all forms into a string array.
                String[] arr1 = coll.AllKeys;
                int fieldID = 0;

                //deprecated 
                
                foreach(JobApplicationField tmpField in app.Fields)
                    tmpField.Deprecated = true;
                JobApplicationField appField = new JobApplicationField();

                foreach(string key in Request.Form.AllKeys)
                {
                    string value = coll[key];
                    //Response.Write(String.Format("<!-- {0}:{1} -->", key, value));
                    if (key.Contains("Text") || key.Contains("CorrectAnswer") || key.Contains("AnswerRequired") || key.Contains("CorrectAnswerRequired"))
                    {
                        char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                        int index = key.IndexOfAny(numbers);
                        int id = Int32.Parse(key.Substring(index));
                        if (id != fieldID && 0 != fieldID)
                        {
                            app.AddField(appField);
                            appField = new JobApplicationField();                            
                        }

                        if (id < 65536) //update existing fields
                            appField.FieldID = id;

                        fieldID = id;

                        if (key.Contains("Text"))
                            appField.FieldText = value;
                        else if(key.Contains("AnswerRequired"))
                            appField.AnswerRequired = (value == "Yes");
                        else if(key.Contains("CorrectAnswerRequired"))
                            appField.HasCorrectAnswer = (value == "Yes");
                        else if(key.Contains("CorrectAnswer"))
                            appField.CorrectAnswer = value;
                    }
                }

                if (0 != fieldID)
                {
                    app.AddField(appField);
                }

                app.Save();
                
            }

            CreateDataTable();
        }

        void CreateDataTable()
        {
            System.Data.DataTable table = new System.Data.DataTable();
            JobApplication app = Session["JobApplication"] as JobApplication;
            if (app == null)
            {
                JobPositionSystemDAL Database = new JobPositionSystemDAL();
               // app = Database.OpenJobOpeningApplication(1);
                app = Database.OpenJobApplication(1);
                Session["JobApplication"] = app;
            }

            DataColumn column;
            column = new DataColumn("ID");
            table.Columns.Add(column);
            column = new DataColumn("Name");
            table.Columns.Add(column);
            column = new DataColumn("FieldType");
            table.Columns.Add(column);
            column = new DataColumn("HasAnswer");
            table.Columns.Add(column);
            column = new DataColumn("Answer");
            table.Columns.Add(column);
            column = new DataColumn("Required");
            table.Columns.Add(column);

            
            foreach (JobApplicationField field in app.Fields)
            {
                DataRow row = table.NewRow();
                row["ID"] = field.FieldID;
                row["Name"] = field.FieldText;
                row["FieldType"] = "Text";
                row["HasAnswer"] = field.HasCorrectAnswer ? "Yes" : "No";
                row["Answer"] = field.CorrectAnswer;
                row["Required"] = field.AnswerRequired ? "Yes" : "No";
                table.Rows.Add(row);
            }

            this.Repeater1.DataSource = table;
            this.Repeater1.DataBind();
        }
    }
}