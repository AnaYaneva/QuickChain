namespace QuickChain.Miner.Infrastructure
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

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

        public static void PostRequestToNode(string blockHash, string timestamp, long nonce)
        {
           // var nodeUrltest = Config["Miner: nodeUrl"];

            // TODO - read from config
           // string nodeUrl = "http://quickchain.azurewebsites.net";
           // string address = "SOME-ADDRESS-FROM-THE-Wallet";

            Config.Conf();
            var nodeUrl = Config.Configuration["nodeUrl"];
            var address = Config.Configuration["address"];

            JObject obj = JObject.FromObject(new
            {
                nonce = nonce.ToString(),
                dateCreated = timestamp,
                blockHash = blockHash
            });

            byte[] blockFoundData = Encoding.UTF8.GetBytes(obj.ToString());
            int retries = 0;

            HttpStatusCode statusCode = HttpStatusCode.OK;

            do
            {
                try
                {
                    statusCode = HttpStatusCode.RequestTimeout;

                    //TODO nodeIP   And                          minerAddres
                    string requestUrl = string.Format("{0}/api/MiningJobs/request/{1}", nodeUrl, address);
                    WebRequest request = WebRequest.Create(requestUrl);
                    request.Method = "POST";
                    request.Timeout = 3000;
                    request.ContentType = "application/json; charset=utf-8";

                    var dataStream = request.GetRequestStream();
                    dataStream.Write(blockFoundData, 0, blockFoundData.Length);
                    dataStream.Close();

                    WebResponse response = request.GetResponse();
                    statusCode = ((HttpWebResponse)response).StatusCode;

                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    response.Close();
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

                System.Threading.Thread.Sleep(1000);
            } while (statusCode != HttpStatusCode.OK && retries++ < 3);
        }
    }
}
