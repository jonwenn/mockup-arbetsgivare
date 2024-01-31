using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MockupRedoPeriod
{
    internal class API
    {
        private string _uriBase;
        private RestResponse response = new();
        public API(string environment)
        {
            _uriBase = environment;

        }

        public void Get(string endpoint)
        {
            var options = new RestClientOptions(_uriBase);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("client_id", "\"\"");
            request.AddHeader("client_secret", "\"\"");
            request.AddHeader("skv_client_correlation_id", "0002aa29-49f2-4baf-be51-c7c39c9824b4");
            request.AddHeader("Authorization", "Bearer aToken");

            response = client.ExecuteGet(request);
        }

        public void Post(string endpoint, string file)
        {
            string uriCall = "/enkeltesttjanst/arbetsgivardeklaration/inlamning/v1";
            var options = new RestClientOptions(_uriBase + uriCall);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Post);

            
            request.AddParameter("application/xml", file, ParameterType.RequestBody);
            request.AddHeader("content-type", "application/xml");
            request.AddHeader("client_id", "\"\"");
            request.AddHeader("client_secret", "\"\"");
            request.AddHeader("skv_client_correlation_id", "adda9696-e6ed-11e7-80c1-9a214cf093ae");

            request.RequestFormat = DataFormat.Xml;

            response = client.ExecuteGet(request);
        }

        public void GetGrundData(string arbetsgivarregistrerad, string redovisningsperiod)
        {
            string call = "arbetsgivardeklaration/hanteraredovisningsperiod/enkeltest/v1";
            string endpoint = call + $"/arbetsgivare/{arbetsgivarregistrerad}/redovisningsperioder/{redovisningsperiod}/grunddata";

            Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
            }
        }

        public void PostInlamning()
        {
            string endpoint = "/underlag";

            string file = "";

            Post(endpoint, file); 
        }
    }
}
