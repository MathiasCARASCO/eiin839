using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;
using System.Reflection;
using System.Diagnostics;

namespace BasicWebServer
{
    public class MyMethods
    {

        public static HttpListenerRequest req;
        public static int nbRequests = 0;

        public static Dictionary<string, string> routeMap = new Dictionary<string, string>()
        {
            { "/homepage/", "HomePage" },
            { "/page1/", "PageOne" },
            { "/stats/", "Stats" },
            { "/stats/reset/", "StatsReset" },
            { "/exec/", "Exec" }
        };

        public MyMethods()
        {

        }

        public static void routeTo(HttpListenerContext context, HttpListenerRequest request)
        {

            string uri = request.Url.LocalPath.ToString();
            if (!uri.EndsWith("/"))
                uri += "/";

            req = request;
            nbRequests++;

            MyMethods myMethods = new MyMethods();

            HttpListenerResponse response = context.Response;

            string responseString = "";
            // Construct a response.
            if (!routeMap.ContainsKey(uri))
                responseString = NotFound404();
            else
            {
                Type thisType = myMethods.GetType();
                MethodInfo theMethod = thisType.GetMethod(routeMap[uri]);
                responseString = (string)theMethod.Invoke(myMethods, null);
            }
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }
        public static string HomePage()
        {
            return "Welcome to my new website";
        }

        public static string PageOne()
        {
            return "<HTML><BODY> <h1>Hello world! </h1><br>"
                + HttpUtility.UrlDecode(req.Url.ToString()) + "<br>"
                + HttpUtility.ParseQueryString(req.Url.Query) + "<br>"
                + HttpUtility.ParseQueryString(req.Url.Query).Get("param1") + "<br>"
                + "</BODY></HTML>";
        }

        public static string Stats()
        {
            return "<HTML><BODY> <h1>Stats </h1><br>"
                + "Number of requests received: "
                + nbRequests.ToString() + "<br>"
                + "</BODY></HTML>";
        }

        public static string StatsReset()
        {
            nbRequests = 0;
            return "<HTML><BODY> <h1>Stats </h1><br>"
                + "Stats have been successfully reset !"
                + "</BODY></HTML>";
        }

        public static string Exec()
        {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"cmd.exe";
            startInfo.Arguments = @"/K echo " + HttpUtility.ParseQueryString(req.Url.Query).Get("word");
            p.StartInfo = startInfo;
            p.Start();
            //p.WaitForExit();
            return "<HTML><BODY> <h1>Execution in a cmd terminal</h1><br>"
                + HttpUtility.UrlDecode(req.Url.ToString()) + "<br>"
                + HttpUtility.ParseQueryString(req.Url.Query) + "<br>"
                + HttpUtility.ParseQueryString(req.Url.Query).Get("param1") + "<br>"
                + "</BODY></HTML>";
        }

        public static string NotFound404()
        {
            return "<html><head>"
                  + " <title> 404 Not Found</title>"
                  + "</head ><body>"
                  + "<h1> Not Found </h1>"
                  + "<p> The requested URL " + req.Url.LocalPath.ToString() + " was not found on this server.</p>"
                  + "</body></html>";
        }
    }
}
