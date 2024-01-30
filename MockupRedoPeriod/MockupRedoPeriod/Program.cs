using MockupRedoPeriod;
using System.Diagnostics;

string environment = "https://api.test.skatteverket.se";
//_uriBase = "https://apis.test-dmz.rsv.se";
//_uriBase = "http://mocking.com";

var api = new API(environment);

await api.GetGrundData("165560143041", "201903");

//await api.PostInlamning();