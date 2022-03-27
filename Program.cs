using System.Text;

public class Program{
    private static string UrlSoap = "https://www.banguat.gob.gt/variables/ws/TipoCambio.asmx";
    private static string SoapAction = "http://www.banguat.gob.gt/variables/ws/TipoCambioDia";
    public static async Task Main(string[] args){
        try{
            var respuesta = await WSBancoGuatemala();

            Console.WriteLine(respuesta);
            Console.ReadKey();
        }catch(Exception e){
            Console.WriteLine(e.StackTrace);
            Console.ReadKey();
        }
    }

    private static string XmlSoapRequest()
    {
        return @"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                <soap:Body>
                    <TipoCambioDia xmlns=""http://www.banguat.gob.gt/variables/ws/"" />
                </soap:Body>
                </soap:Envelope>";
    }

    private static async Task<string> WSBancoGuatemala(){
        HttpClient clientWS = new HttpClient();
        HttpContent contentRequest = new StringContent(XmlSoapRequest(), Encoding.UTF8, "text/xml");

        using(HttpRequestMessage requestWS = new HttpRequestMessage(HttpMethod.Post, UrlSoap)){
            requestWS.Headers.Add("SoapAction", SoapAction);
            requestWS.Content = contentRequest;

            using(HttpResponseMessage responseWS = await clientWS.SendAsync(requestWS, HttpCompletionOption.ResponseHeadersRead)){
                var contenido = await responseWS.Content.ReadAsStringAsync();
                return contenido;
            }
        }
    }
}
