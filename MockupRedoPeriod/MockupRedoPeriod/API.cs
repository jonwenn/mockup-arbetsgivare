using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
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

        public void Get2(string endpoint)
        {
            var options = new RestClientOptions(_uriBase);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Get);

            request.AddHeader("Accept", "application/json");
            request.AddHeader("client_id", "\"\"");
            request.AddHeader("client_secret", "\"\"");
            request.AddHeader("SKV-client_correlationid", "0002aa29-49f2-4baf-be51-c7c39c9824b4");
            request.AddHeader("Authorization", "Bearer aToken");

            response = client.ExecuteGet(request);
        }

        public void Post(string endpoint, string file)
        {
            var options = new RestClientOptions(_uriBase);
            var client = new RestClient(options);
            var request = new RestRequest(endpoint, Method.Post);

            
            request.AddParameter("application/xml", file, ParameterType.RequestBody);
            request.AddHeader("content-type", "application/xml");
            request.AddHeader("client_id", "\"\"");
            request.AddHeader("client_secret", "\"\"");
            request.AddHeader("SKV-client_correlationid", "adda9696-e6ed-11e7-80c1-9a214cf093ae");

            request.RequestFormat = DataFormat.Xml;

            response = client.ExecutePost(request);
        }

        public void GetGrundData(string arbetsgivarregistrerad, string redovisningsperiod)
        {
            string call = "arbetsgivardeklaration/hanteraredovisningsperiod/enkeltest/v1";
            string endpoint = call + $"/arbetsgivare/{arbetsgivarregistrerad}/redovisningsperioder/{redovisningsperiod}/grunddata";

            Get(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                if (content != null)
                {

                }
            }
        }

        public void GetKontrollresultat(int id)
        {
            string uriCall = "/enkeltesttjanst/arbetsgivardeklaration/inlamning/v1";
            string endpoint = uriCall + $"/underlag/{id}/kontrollresultat";
            
            Get2(endpoint);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                if (content != null)
                {

                }
            }
        }

        public int PostInlamning()
        {

            string uriCall = "/enkeltesttjanst/arbetsgivardeklaration/inlamning/v1";
            string endpoint = uriCall + "/underlag";

            string file = XMLFile();

            Post(endpoint, file);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                if (content != null)
                {
                    InlamningSvar svar = JsonSerializer.Deserialize<InlamningSvar>(content);
                    return svar.InlamningId;
                }
            }
            return -1;
        }

        private string XMLFile()
        {
            string file = @"
                <?xml version=""1.0"" encoding=""UTF-8"" standalone=""no""?>
                <Skatteverket omrade=""Arbetsgivardeklaration"" xmlns=""http://xmls.skatteverket.se/se/skatteverket/da/instans/schema/1.1"" xmlns:agd=""http://xmls.skatteverket.se/se/skatteverket/da/komponent/schema/1.1"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://xmls.skatteverket.se/se/skatteverket/da/instans/schema/1.1 http://xmls.skatteverket.se/se/skatteverket/da/arbetsgivardeklaration/arbetsgivardeklaration_1.1.xsd"">
                  <agd:Avsandare>
                    <agd:Programnamn>Programmakarna AB</agd:Programnamn>
                    <agd:Organisationsnummer>190002039006</agd:Organisationsnummer>
                    <agd:TekniskKontaktperson>
                      <agd:Namn>Valle Vadman</agd:Namn>
                      <agd:Telefon>23-2-4-244454</agd:Telefon>
                      <agd:Epostadress>valle.vadman@programmakarna.se</agd:Epostadress>
                      <agd:Utdelningsadress1>Frihetsgatan 11</agd:Utdelningsadress1>
                      <agd:Utdelningsadress2>C/O Segemyhr</agd:Utdelningsadress2>
                      <agd:Postnummer>62145</agd:Postnummer>
                      <agd:Postort>Sångby</agd:Postort>
                    </agd:TekniskKontaktperson>
                    <agd:Skapad>2024-03-02T09:42:25</agd:Skapad>
                  </agd:Avsandare>
                  <agd:Blankettgemensamt>
                    <agd:Arbetsgivare>
                      <agd:AgRegistreradId>165560269986</agd:AgRegistreradId>
                      <agd:Kontaktperson>
                        <agd:Namn>Ville Vessla</agd:Namn>
                        <agd:Telefon>555-244454</agd:Telefon>
                        <agd:Epostadress>ville.vessla@foretaget.se</agd:Epostadress>
                        <agd:Sakomrade>skruv-avdelningens anställda</agd:Sakomrade>
                      </agd:Kontaktperson>
                      <agd:Kontaktperson>
                        <agd:Namn>Maria Olsson</agd:Namn>
                        <agd:Telefon>555-244121</agd:Telefon>
                        <agd:Epostadress>maria.olsson@foretaget.se</agd:Epostadress>
                        <agd:Sakomrade>mutter-avdelningens anställda</agd:Sakomrade>
                      </agd:Kontaktperson>
                    </agd:Arbetsgivare>
                  </agd:Blankettgemensamt>

                  <!-- Uppgift 1 HU -->
                  <agd:Blankett>
                    <agd:Arendeinformation>
                      <agd:Arendeagare>165560269986</agd:Arendeagare>
                      <agd:Period>202402</agd:Period>
                    </agd:Arendeinformation>
                    <agd:Blankettinnehall>
                      <agd:HU>
                        <agd:ArbetsgivareHUGROUP>
                          <agd:AgRegistreradId faltkod=""201"">165560269986</agd:AgRegistreradId>
                        </agd:ArbetsgivareHUGROUP>
                        <agd:RedovisningsPeriod faltkod=""006"">202402</agd:RedovisningsPeriod>
                        <agd:SummaArbAvgSlf faltkod=""487"">26094</agd:SummaArbAvgSlf>
                        <agd:SummaSkatteavdr faltkod=""497"">25140</agd:SummaSkatteavdr>
                      </agd:HU>
                    </agd:Blankettinnehall>
                  </agd:Blankett>

                  <!-- Uppgift 1 IU -->
                  <agd:Blankett>
                    <agd:Arendeinformation>
                      <agd:Arendeagare>165560269986</agd:Arendeagare>
                      <agd:Period>202402</agd:Period>
                    </agd:Arendeinformation>
                    <agd:Blankettinnehall>
                      <agd:IU>
                        <agd:ArbetsgivareIUGROUP>
                          <agd:AgRegistreradId faltkod=""201"">165560269986</agd:AgRegistreradId>
                        </agd:ArbetsgivareIUGROUP>
                        <agd:BetalningsmottagareIUGROUP>
                          <agd:BetalningsmottagareIDChoice>
                            <agd:BetalningsmottagarId faltkod=""215"">198202252386</agd:BetalningsmottagarId>
                          </agd:BetalningsmottagareIDChoice>
                        </agd:BetalningsmottagareIUGROUP>
                        <agd:RedovisningsPeriod faltkod=""006"">202402</agd:RedovisningsPeriod>
                        <agd:Specifikationsnummer faltkod=""570"">001</agd:Specifikationsnummer>
                        <agd:ArbetsplatsensGatuadress faltkod=""245"">Arbetsgatan 31</agd:ArbetsplatsensGatuadress>		
                        <agd:ArbetsplatsensOrt faltkod=""246"">Jobbköping</agd:ArbetsplatsensOrt>	
                        <agd:KontantErsattningUlagAG faltkod=""011"">28500</agd:KontantErsattningUlagAG>
                        <agd:SkatteplBilformanUlagAG faltkod=""013"">2500</agd:SkatteplBilformanUlagAG>
		                <agd:DrivmVidBilformanUlagAG faltkod=""018"">1200</agd:DrivmVidBilformanUlagAG>
		                <agd:BetForDrivmVidBilformanUlagAG faltkod=""098"">1000</agd:BetForDrivmVidBilformanUlagAG>
                        <agd:AvdrPrelSkatt faltkod=""001"">9300</agd:AvdrPrelSkatt>
                      </agd:IU>
                    </agd:Blankettinnehall>
                  </agd:Blankett>

                  <!-- Uppgift 2 IU -->
                  <agd:Blankett>
                    <agd:Arendeinformation>
                      <agd:Arendeagare>165560269986</agd:Arendeagare>
                      <agd:Period>202402</agd:Period>
                    </agd:Arendeinformation>
                    <agd:Blankettinnehall>
                      <agd:IU>
                        <agd:ArbetsgivareIUGROUP>
                          <agd:AgRegistreradId faltkod=""201"">165560269986</agd:AgRegistreradId>
                        </agd:ArbetsgivareIUGROUP>
                        <agd:BetalningsmottagareIUGROUP>
                          <agd:BetalningsmottagareIDChoice>
                            <agd:BetalningsmottagarId faltkod=""215"">198301302397</agd:BetalningsmottagarId>
                          </agd:BetalningsmottagareIDChoice>
                        </agd:BetalningsmottagareIUGROUP>
                        <agd:RedovisningsPeriod faltkod=""006"">202402</agd:RedovisningsPeriod>
                        <agd:Specifikationsnummer faltkod=""570"">002</agd:Specifikationsnummer>
                        <agd:ArbetsplatsensGatuadress faltkod=""245"">Arbetsgatan 21</agd:ArbetsplatsensGatuadress>		
                        <agd:ArbetsplatsensOrt faltkod=""246"">Jobbköping</agd:ArbetsplatsensOrt>	
                        <agd:KontantErsattningUlagAG faltkod=""011"">25350</agd:KontantErsattningUlagAG>
                        <agd:AvdrPrelSkatt faltkod=""001"">7590</agd:AvdrPrelSkatt>
                        <agd:Bilersattning faltkod=""050"">1</agd:Bilersattning>
                      </agd:IU>
                    </agd:Blankettinnehall>
                  </agd:Blankett>

                  <!-- Uppgift 3 IU -->
                  <agd:Blankett>
                    <agd:Arendeinformation>
                      <agd:Arendeagare>165560269986</agd:Arendeagare>
                      <agd:Period>202402</agd:Period>
                    </agd:Arendeinformation>
                    <agd:Blankettinnehall>
                      <agd:IU>
                        <agd:ArbetsgivareIUGROUP>
                          <agd:AgRegistreradId faltkod=""201"">165560269986</agd:AgRegistreradId>
                        </agd:ArbetsgivareIUGROUP>
                        <agd:BetalningsmottagareIUGROUP>
                          <agd:BetalningsmottagareIDChoice>
                            <agd:BetalningsmottagarId faltkod=""215"">198409102392</agd:BetalningsmottagarId>
                          </agd:BetalningsmottagareIDChoice>
                        </agd:BetalningsmottagareIUGROUP>
                        <agd:RedovisningsPeriod faltkod=""006"">202402</agd:RedovisningsPeriod>
                        <agd:Specifikationsnummer faltkod=""570"">003</agd:Specifikationsnummer>
                        <agd:ArbetsplatsensGatuadress faltkod=""245"">Arbetsgatan 21</agd:ArbetsplatsensGatuadress>		
                        <agd:ArbetsplatsensOrt faltkod=""246"">Jobbstaden</agd:ArbetsplatsensOrt>	
                        <agd:KontantErsattningUlagAG faltkod=""011"">26700</agd:KontantErsattningUlagAG>
                        <agd:AvdrPrelSkatt faltkod=""001"">8250</agd:AvdrPrelSkatt>
                        <agd:Traktamente faltkod=""051"">1</agd:Traktamente>
                      </agd:IU>
                    </agd:Blankettinnehall>
                  </agd:Blankett>

                </Skatteverket>
            ";
            return file;
        }
    }
}
