// PocketClientApiPost 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json; 
using System.Threading.Tasks;
using System.Text;
using PocketApi.RestApiRequestModels;


// namespace
namespace PocketApi
{
   
    public partial class LitresClient
    {
        // _http Client 
        private static HttpClient _httpClient = null;
        

        // LitresClient
        public LitresClient()
        {
            if (_httpClient == null)
            {
                HttpClientHandler hhandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                _httpClient = new HttpClient(hhandler);
            }

        }//LitresClient end


        // InitializeHttpClient
        private void InitializeHttpClient()
        {
            //_httpClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
            if (_httpClient == null)
            {
                HttpClientHandler hhandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };

                _httpClient = new HttpClient(hhandler);

                _httpClient.DefaultRequestHeaders.Add("X-Accept", "application/json");
            }

        }// InitializeHttpClient end


        // case 1 - get token

        // ApiPostAsync
        private async Task<string> ApiPostAsync(Uri requestUri, ObtainAccessTokenBody body)
        {
            HttpRequestMessage request = new HttpRequestMessage();

            string jsonBody = JsonConvert.SerializeObject(body);
            
            StringContent content = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json");

           
            request.RequestUri = 
                new Uri($"https://robot.litres.ru/pages/catalit_authorise/?login=" +
                body.Login +
                $"&pwd=" +
                body.Pwd);
            
            request.Method = HttpMethod.Get;
            

            HttpResponseMessage response = await _httpClient.SendAsync(request);
           
            response.EnsureSuccessStatusCode();
            
            string responseContent = await response.Content.ReadAsStringAsync();
            
            return responseContent;

        }//ApiPostAsync end


        // case 2 - get book collection 

        // ApiPostAsync
        private async Task<string> ApiPostAsync2(Uri requestUri, LitresItemsBody body)
        {
            HttpRequestMessage request = new HttpRequestMessage();

            string jsonBody = JsonConvert.SerializeObject(body);

            StringContent content = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json");

            request.RequestUri =               
                new Uri
                (
                    $"https://robot.litres.ru/pages/catalit_browser/?sid=" +
                    body.Sid
                    + "&my=1"
                    + "&limit=5"
                );

            request.Method = HttpMethod.Get;


            HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;

        }//ApiPostAsync2 end

    }//LibresClient end

}//namespace end
