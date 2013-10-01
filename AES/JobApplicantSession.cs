using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using System.Data;
using JobOpeningSystem;
using JobPositionSystem;


namespace AES
{

    /// <summary>
    /// This is the session class for the job application
    /// This gets and sets the session.
    /// </summary>
    public class JobApplicantSession
    {

        public JobApplication ApplicationSession
        {
       
          get {if (HttpContext.Current.Session["Application"] != null)
                   return (JobApplication)HttpContext.Current.Session["Application"];
               return null; }

          set{ HttpContext.Current.Session["Application"] = value; }
        }
    

    }
}