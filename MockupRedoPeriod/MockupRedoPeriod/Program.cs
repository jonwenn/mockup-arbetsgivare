using MockupRedoPeriod;
using System.Diagnostics;

string environment = "https://api.test.skatteverket.se";
//_uriBase = "https://apis.test-dmz.rsv.se";
//_uriBase = "http://mocking.com";

var api = new API(environment);

//api.GetGrundData("160101010101", "201005");

api.PostInlamning();