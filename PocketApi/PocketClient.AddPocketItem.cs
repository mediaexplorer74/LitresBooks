using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
//using System.ComponentModel.Design.Serialization;
using System.Text;
//using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PocketApi
{
    /*
    public partial class PocketClient
    {
        private static Uri _addUri = new Uri($"https://getpocket.com/v3/add");

        public async Task<PocketItem> AddPocketItemAsync(Uri uri)
        {
            PocketItem addedPocketItem = new PocketItem();

            string apiResponse = await ApiPostAsync(
                _addUri,
                new AddPocketItemBody()
                {
                    ConsumerKey = _accessToken.ConsumerKey,
                    AccessToken = _accessToken.Token,
                    Uri = uri
                });

            //JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponse);
            var apiJsonDocument = JToken.Parse(apiResponse);

            //RnD 
            //foreach (JsonProperty property in apiJsonDocument.RootElement.EnumerateObject())
            //{
            //    if (property.Name == "item")
            //        addedPocketItem = Converters.PocketConverter.ConvertJsonToPocketItem(property);
            //}

            return addedPocketItem;
        }

    }//class end
    */

    // 
    public partial class LitresClient
    {
        private static Uri _addUri = new Uri($"https://getpocket.com/v3/add");

        public async Task<LitresItem> AddLitresItemAsync(Uri uri)
        {
            LitresItem addedPocketItem = new LitresItem();

            var A = new ObtainAccessTokenBody()//AddPocketItemBody()
            {
                Login = _accessData.Login,
                Pwd = _accessData.Password,
            };

            string apiResponse = await ApiPostAsync
            (
                _addUri,
                 A
            );

            //JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponse);
            var apiJsonDocument = JToken.Parse(apiResponse);

            //RnD 
            //foreach (JsonProperty property in apiJsonDocument.RootElement.EnumerateObject())
            //{
            //    if (property.Name == "item")
            //        addedPocketItem = Converters.PocketConverter.ConvertJsonToPocketItem(property);
            //}

            return addedPocketItem;
        }

    }//class end

}
