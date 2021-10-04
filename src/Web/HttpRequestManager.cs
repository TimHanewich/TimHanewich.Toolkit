using System;
using System.Net.Http;
using System.Net;
using TimHanewich.Toolkit;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace TimHanewich.Toolkit.Web
{
    public class HttpRequestManager
    {
        //Instance of self
        public static HttpRequestManager Instance = new HttpRequestManager();

        //User agent to use for requests
        public string UserAgent {get; set;}

        //Events
        public event Notification StatusChanged; //Status was updated
        public event Action Throttled; //We were throttled

        public TimeSpan RequestDelay {get; set;} //The time that will be waited before all requests
        public TimeSpan TimeoutDelay {get; set;} //The time that will be waited after we get a throttling warning
        public ushort MaxTryCount {get; set;} //The maximum number of trys that will occur before giving up (if it keeps getting throttle warnings)

        public HttpRequestManager()
        {
            RequestDelay = new TimeSpan(0, 0, 0, 0, 250); //250 milliseconds (quarter of a second)
            TimeoutDelay = new TimeSpan(0, 0, 2); //2 seconds
            MaxTryCount = 10;
        }

        public HttpRequestManager(TimeSpan request_delay, TimeSpan timeout_delay, ushort max_trys)
        {
            RequestDelay = request_delay;
            TimeoutDelay = timeout_delay;
            MaxTryCount = max_trys;
        }

        public async Task<string> GetAsync(string url)
        {
            string ToReturn = null;

            //Setup
            HttpClient hc = new HttpClient();
            int havetried = 0;

            while (ToReturn == null && havetried < MaxTryCount)
            {
                //Prepare the request
                TryUpdateStatus("Preparing request...");
                HttpRequestMessage req = PrepareHttpRequestMessage();
                req.RequestUri = new Uri(url);
                req.Method = HttpMethod.Get;

                //Take the request delay timeout first
                TryUpdateStatus("Taking request delay...");
                await Task.Delay(RequestDelay);
                
                //Make the call
                TryUpdateStatus("Attempting call...");
                HttpResponseMessage resp = await hc.SendAsync(req);
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    ToReturn = await resp.Content.ReadAsStringAsync();
                }
                else if (resp.StatusCode == HttpStatusCode.Forbidden) //Code 403 (throttled)
                {
                    if (Throttled != null) //Raise the throttled event
                    {
                        Throttled.Invoke();
                    }
                    TryUpdateStatus("Request was denied due to throttling. Waiting for timeout...");
                    await Task.Delay(TimeoutDelay);
                    havetried = havetried + 1;
                    TryUpdateStatus("Try count incremented and will try again.");
                }
            }

            //If the have tried is what caused it (it is over the limit), throw an exception
            if (havetried >= MaxTryCount)
            {
                throw new Exception("Unable to get data for URL '" + url + "'. Surpassed maximum try count of " + MaxTryCount.ToString());
            }

            return ToReturn;
        }

        public async Task<Stream> GetStreamAsync(string url)
        {
            Stream ToReturn = null;

            //Setup
            HttpClient hc = new HttpClient();
            int havetried = 0;

            while (ToReturn == null && havetried < MaxTryCount)
            {
                //Prepare the request
                TryUpdateStatus("Preparing request...");
                HttpRequestMessage req = PrepareHttpRequestMessage();
                req.RequestUri = new Uri(url);
                req.Method = HttpMethod.Get;

                //Take the request delay timeout first
                TryUpdateStatus("Taking request delay...");
                await Task.Delay(RequestDelay);
                
                //Make the call
                TryUpdateStatus("Attempting call...");
                HttpResponseMessage resp = await hc.SendAsync(req);
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    ToReturn = await resp.Content.ReadAsStreamAsync();
                }
                else if (resp.StatusCode == HttpStatusCode.Forbidden) //Code 403 (throttled)
                {
                    if (Throttled != null) //Raise the throttled event
                    {
                        Throttled.Invoke();
                    }
                    TryUpdateStatus("Request was denied due to throttling. Waiting for timeout...");
                    await Task.Delay(TimeoutDelay);
                    havetried = havetried + 1;
                    TryUpdateStatus("Try count incremented and will try again.");
                }
            }

            //If the have tried is what caused it (it is over the limit), throw an exception
            if (havetried >= MaxTryCount)
            {
                throw new Exception("Unable to get data for URL '" + url + "'. Surpassed maximum try count of " + MaxTryCount.ToString());
            }

            return ToReturn;
        }

        private void TryUpdateStatus(string msg)
        {
            if (StatusChanged != null)
            {
                StatusChanged.Invoke(msg);
            }
        }
    
        public HttpRequestMessage PrepareHttpRequestMessage()
        {
            HttpRequestMessage req = new HttpRequestMessage();
            req.Method = HttpMethod.Get;

            //Add user agent header
            if (UserAgent == null) //If they did not specify it, make it default
            {
                req.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.114 Safari/537.36 Edg/89.0.774.75"); //This identifies the request as coming from a browser. If we do not provide info, the SEC will flag this as coming from an undeclared tool.
            }
            else //If they did specify, make it the one they specify
            {
                req.Headers.Add("User-Agent", UserAgent);
            }
            
            req.Headers.Add("Accept", "*/*");
            return req;
        }
    }
}