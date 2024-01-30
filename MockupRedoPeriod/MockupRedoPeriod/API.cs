using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MockupRedoPeriod
{
    public class DataFile
    {
        public string Hej { get; set; }
    }
    internal class API
    {
        private string _uriBase = "https://api.test.skatteverket.se/enkeltesttjanst";
        public API()
        {
        }

        public async Task Query(string endpoint)
        {
            var options = new RestClientOptions(_uriBase + "/abetsgivardeklaration/hanteraredovisningsperiod/v1/")
            { };
            var client = new RestClient(options);
            var request = new RestRequest(endpoint);
            request.AddHeader("accept", "application/json");

            try
            {
                var response = await client.GetAsync(request);
                string content = response.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task Post(string endpoint)
        {
            var options = new RestClientOptions(_uriBase + "/abetsgivardeklaration/inlamning/v1")
            { };
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Post);

            var file = new DataFile { Hej = "Tjena" };
            request.AddParameter("application/xml", file, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Xml;

            try
            {
                var response = await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task GetGrundData(string arbetsgivarregistrerad, string redovisningsperiod)
        {
            string endpoint = $"/arbetsgivare/{arbetsgivarregistrerad}/redovisningsperioder/{redovisningsperiod}/grunddata";
            endpoint = "/arbetsgivare/165560269986/redovisningsperioder/202201/grunddata";

            await Query(endpoint);
        }
    }
}
