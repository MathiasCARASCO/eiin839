# EIIN839 - ECUE Service oriented computing/WS

## Student: Mathias CARASCO

In this forked repo, you will find the lab exercises I have completed.

# PART I - Session 2
### Instructions to run the program and play with it:
Open the solution `BasicExamplesTP2.sln` \
Run `BasicWebServerUrlParser` \
Open your favorite Browser. \
1) Go to: [http://localhost:8080/homepage](http://localhost:8080/homepage)
2) Go to: [http://localhost:8080/hfozefhzo](http://localhost:8080/hfozefhzo) \
As this page does not exist, you should have a 404 error message. You can try with any other url not listed in this section.
3) Go to: [http://localhost:8080/page1](http://localhost:8080/page1) \
It should return a "Hello world!" message and the full URL you entered.
4) Go to: [http://localhost:8080/page1?arg1=bonjour&arg2=au%20revoir](http://localhost:8080/page1?arg1=bonjour&arg2=au%20revoir) \
It should return a "Hello world!" message and the full URL and also the URI.
5) Go to: [http://localhost:8080/page1?param1=hello](http://localhost:8080/page1?param1=hello) \
It should return a "Hello world!" message, the URL, the URI and the value of param1 ("hello"). \
6) Go to: [http://localhost:8080/exec/?word=Bonjour](http://localhost:8080/exec/?word=Bonjour) \
It should open a CMD console printing "Bonjour" and return the URL and the set parameter on the webpage.

### How is it all handled?
I decided to create a MyMethods class with Map containing the uri associated to the method to call.
This Map is defined as follows:
```
{
            { "/homepage/", "HomePage" },
            { "/page1/", "PageOne" },
            { "/stats/", "Stats" },
            { "/stats/reset/", "StatsReset" },
            { "/exec/", "Exec" }
};
```
And in a `routeTo(string url)` method, the program invokes the corresponding method associated with the url set as parameter.

This is it for this lab. Thanks for looking at it ;)