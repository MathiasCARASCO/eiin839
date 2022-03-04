using System;
using System.Collections.Generic.Dictionary;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace BasicWebServerUrlParser

public class MyMethods
{

    HttpListenerRequest req;
    public static Dictionary<string, string> routeMap = new Dictionary<string, string>()
        {
            { "homepage", "HomePage" },
            { "page1", "PageOne" }
        };

    public MyMethods()
    {

    }

	public static routeTo(HttpListenerRequest request)
    {

        string uri = request.Uri.ToString();
        req = request;
       
		MyMethods myMethods = new MyMethods();

        HttpListenerResponse response = context.Response;

        string responseString = "";
        // Construct a response.
        if (!routeMap.containsKey(uri)
        {
            return NotFound404();
        }
        string responseString = myMethods.GetType().GetMethod(routeMap[uri]).Invoke(myMethods);

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
        return "<HTML><BODY> Hello world! <br>"
            + HttpUtility.UrlDecode(request.ToString()) + "<br>"
            + HttpUtility.ParseQueryString(request.Url.Query) + "<br>"
            + HttpUtility.ParseQueryString(request.Url.Query).Get("param1") + "<br>"
            + "</BODY></HTML>";
    }

    public static string NotFound404()
    {
        return "<html><head>
              + " <title> 404 Not Found</title>"
              + "</head >< body >"
              + "<h1> Not Found </h1>"
              + "<p> The requested URL "+ req.Uri.ToString() +" was not found on this server.</p>"
              + "</body></html>";
    }
}
