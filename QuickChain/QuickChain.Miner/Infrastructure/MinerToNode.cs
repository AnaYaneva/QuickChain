namespace QuickChain.Miner.Infrastructure
{
    using System;
    using System.IO;
    using System.Net;

    public static class MinerToNode
    {
        public static string GetRequestToNode()
        {
            WebResponse response = null;
            HttpStatusCode statusCode = HttpStatusCode.RequestTimeout;

            do
            {
                try
                {
                    statusCode = HttpStatusCode.RequestTimeout;

                    // Create a request to Node         //TODO nodeIP   And                          minerAddres
                    WebRequest request = WebRequest.Create("nodeIpAddress" + "/mining/get-block/" + "minerAddress"); 
                    request.Method = "GET";
                    request.Timeout = 3000;
                    request.ContentType = "application/json; charset=utf-8";

                    response = request.GetResponse();
                    statusCode = ((HttpWebResponse)response).StatusCode;
                }
                catch (WebException e)
                {
                    Console.WriteLine("WebException raised!");
                    Console.WriteLine("{0}\n", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception raised!");
                    Console.WriteLine("Source : {0}", e.Source);
                    Console.WriteLine("Message : {0}\n", e.Message);
                }
            } while (statusCode != HttpStatusCode.OK);


            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            string responseFromNode = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromNode;
        }
    }
}
