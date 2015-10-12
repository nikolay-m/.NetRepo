using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LimeSystems
{
    public static class Loger
    {
        public static void Log(HttpResponse Response, Exception ex)
        {
            string log = string.Format("Error. Source: {0}; Message: {1}", ex.Source, ex.Message);
            Response.AppendToLog(log);
            Response.Redirect("ErrorPage.aspx");
        }
    }

}