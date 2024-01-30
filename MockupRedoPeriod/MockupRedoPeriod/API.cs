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
    public class DataFile
    {
        public string Hej { get; set; }
    }
    internal class API
    {
        private string _uriBase;
        public API(string environment)
        {
            _uriBase = environment;

        }

        public async Task Get(string endpoint)
        {
            string uriCall = "/abetsgivardeklaration/hanteraredovisningsperiod/enkeltest/v1";
            uriCall = "/enkeltesttjanst/abetsgivardeklaration/hanteraredovisningsperiod/v1";

            var options = new RestClientOptions(_uriBase + uriCall);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint);

            request.AddHeader("Accept", "application/json");
            //request.AddHeader("client_id", "");
            //request.AddHeader("client_secret", "");
            //request.AddHeader("skv_client_correlation_id", "0002aa29-49f2-4baf-be51-c7c39c9824b4");
            //request.AddHeader("Authorization", "Bearer aToken");

            try
            {
                var response = await client.GetAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<int> Post(string endpoint, string file)
        {
            string uriCall = "/enkeltesttjanst/abetsgivardeklaration/inlamning/v1";
            var options = new RestClientOptions(_uriBase + uriCall);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Post);

            
            //request.AddParameter("application/xml", file, ParameterType.RequestBody);
            request.AddHeader("content-type", "application/xml");
            request.AddHeader("client_id", "");
            request.AddHeader("client_secret", "");
            //request.AddHeader("skv_client_correlation_id", "adda9696-e6ed-11e7-80c1-9a214cf093ae");

            request.RequestFormat = DataFormat.Xml;

            try
            {
                var response = await client.ExecuteAsync(request);
                return int.Parse(response.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public async Task GetGrundData(string arbetsgivarregistrerad, string redovisningsperiod)
        {
            string endpoint = $"/arbetsgivare/{arbetsgivarregistrerad}/redovisningsperioder/{redovisningsperiod}/grunddata";

            await Get(endpoint);
        }

        public async Task PostInlamning()
        {
            string endpoint = "/underlag";

            string file = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
                            <Skatteverket omrade=""Arbetsgivardeklaration"" xmlns=""http://xmls.skatteverket.se/se/skatteverket/da/instans/schema/1.1"" xmlns:agd=""http://xmls.skatteverket.se/se/skatteverket/da/komponent/schema/1.1"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://xmls.skatteverket.se/se/skatteverket/da/instans/schema/1.1 http://xmls.skatteverket.se/se/skatteverket/da/arbetsgivardeklaration/arbetsgivardeklaration_1.1.xsd"">
                              <agd:Avsandare>
                                <agd:Programnamn>Programutvecklarna AB</agd:Programnamn>
                                <agd:Organisationsnummer>190002039006</agd:Organisationsnummer>
                                <agd:TekniskKontaktperson>
                                  <agd:Namn>Jonas Persson</agd:Namn>
                                  <agd:Telefon>23-2-4-244454</agd:Telefon>
                                  <agd:Epostadress>jonas.persson@programutvecklarna.se</agd:Epostadress>
                                  <agd:Utdelningsadress1>Artillerigatan 11</agd:Utdelningsadress1>
                                  <agd:Postnummer>62145</agd:Postnummer>
                                  <agd:Postort>Visby</agd:Postort>
                                </agd:TekniskKontaktperson>
                                <agd:Skapad>2019-01-28T07:42:25</agd:Skapad>
                              </agd:Avsandare>
                              <agd:Blankettgemensamt>
                                <agd:Arbetsgivare>
                                  <agd:AgRegistreradId>165560143041</agd:AgRegistreradId>
                                  <agd:Kontaktperson>
                                    <agd:Namn>Löneadministrationen</agd:Namn>
                                    <agd:Telefon>23-2-244000</agd:Telefon>
                                    <agd:Epostadress>loneavdelningen@ninnasgarnhandel.se</agd:Epostadress>
                                  </agd:Kontaktperson>
                                </agd:Arbetsgivare>
                              </agd:Blankettgemensamt>
                              <!--HU -->
                              <agd:Blankett>
                                <agd:Arendeinformation>
                                  <agd:Arendeagare>165560143041</agd:Arendeagare>
                                  <agd:Period>201901</agd:Period>
                                </agd:Arendeinformation>
                                <agd:Blankettinnehall>
                                  <agd:HU>
                                    <agd:ArbetsgivareHUGROUP>
                                      <agd:AgRegistreradId faltkod=""201"">165560143041</agd:AgRegistreradId>
                                    </agd:ArbetsgivareHUGROUP>
                                    <agd:RedovisningsPeriod faltkod=""006"">201901</agd:RedovisningsPeriod>
                                    <agd:SummaArbAvgSlf faltkod=""487"">52601</agd:SummaArbAvgSlf>
		                            <agd:SummaSkatteavdr faltkod=""497"">74780</agd:SummaSkatteavdr>
                                  </agd:HU>
                                </agd:Blankettinnehall>
                              </agd:Blankett>
                              <!-- IU -->
                              <agd:Blankett>
                                <agd:Arendeinformation>
                                  <agd:Arendeagare>165560143041</agd:Arendeagare>
                                  <agd:Period>201901</agd:Period>
                                </agd:Arendeinformation>
                                <agd:Blankettinnehall>
                                  <agd:IU>
                                    <agd:ArbetsgivareIUGROUP>
                                      <agd:AgRegistreradId faltkod=""201"">165560143041</agd:AgRegistreradId>
                                    </agd:ArbetsgivareIUGROUP>
                                    <agd:BetalningsmottagareIUGROUP>
                                      <agd:BetalningsmottagareIDChoice>
                                        <agd:BetalningsmottagarId faltkod=""215"">198202252386</agd:BetalningsmottagarId>
                                      </agd:BetalningsmottagareIDChoice>
                                    </agd:BetalningsmottagareIUGROUP>
                                    <agd:RedovisningsPeriod faltkod=""006"">201901</agd:RedovisningsPeriod>
                                    <agd:Specifikationsnummer faltkod=""570"">001</agd:Specifikationsnummer>
                                    <agd:KontantErsattningUlagAG faltkod=""011"">28500</agd:KontantErsattningUlagAG>
                                    <agd:SkatteplBilformanUlagAG faltkod=""013"">2500</agd:SkatteplBilformanUlagAG>
                                    <agd:AvdrPrelSkatt faltkod=""001"">9300</agd:AvdrPrelSkatt>
                                  </agd:IU>
                                </agd:Blankettinnehall>
                              </agd:Blankett>
                            </Skatteverket>";

            await Post(endpoint, file); 
        }
    }
}
