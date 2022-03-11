// LitresItemsAsync

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using Newtonsoft.Json; //using System.Text.Json;
using System.Diagnostics;
using System.Xml;

namespace PocketApi
{
   

    public partial class LitresClient
    {
        private static Uri _getUri =
            new Uri($"https://robot.litres.ru/pages/");
       
        public async Task<List<LitresItem>> LitresItemsAsync(DateTime lastSyncDateTime)
        {
            double lastSyncUnixTimestamp =
                Converters.UnixTimestampConverter.ToUnixtimestamp(lastSyncDateTime);

            string response = "";

            //TODO

            try
            {
                response = await ApiPostAsync2
                (
                    _getUri,
                    new LitresItemsBody()
                    {
                        //Login = this._accessData.Login,
                        //Password = this._accessData.Password,
                        //DetailType = "complete",
                        //Since = lastSyncUnixTimestamp,
                        //State = PocketItemState.All,
                        //Sort = PocketItemSort.Oldest
                        Sid = this._accessData.Sid
                    }
                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] ApiPostAsync2 Exception: " + ex.Message);
            }


            //List<LitresItem> litresItems = ConvertJsonToLitresItems(apiResponse);

            List<LitresItem> litresItems = new List<LitresItem>() { };

            // ----------------------------------------------------------------------------------
            // XML Parse

            // create xml doc
            XmlDocument xDoc = new XmlDocument();

            // load xml from html response
            xDoc.LoadXml(response); 
           
            // get root element
            XmlElement xRoot = xDoc.DocumentElement;

            // get sid attr
            //XmlNode sidAttr = xRoot.Attributes.GetNamedItem("sid");

           
            // get user-id attr
            //XmlNode useridAttr = xRoot.Attributes.GetNamedItem("user-id");

            //Debug.WriteLine(sidAttr.Value);

            //token.Sid = sidAttr.Value;
            //token.UserId = useridAttr.Value;

            
            if (xRoot != null)
            {
                Debug.WriteLine("--start--");


                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут hub_id
                    XmlNode attrHubId = xnode.Attributes.GetNamedItem("hub_id");
                    
                    Debug.WriteLine(attrHubId?.Value);


                    // получаем атрибут Filename
                    XmlNode attrFilneame = xnode.Attributes.GetNamedItem("filename");

                    Debug.WriteLine(attrFilneame?.Value);

                    // получаем атрибут Cover
                    XmlNode attrCover = xnode.Attributes.GetNamedItem("cover");

                    Debug.WriteLine(attrCover?.Value);


                    // получаем атрибут Url
                    XmlNode attrUrl = xnode.Attributes.GetNamedItem("url");

                    Debug.WriteLine(attrUrl?.Value);

                    

                    string attrTitle = "Test title";

                    
                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {

                        // если узел - company
                        if (childnode.Name == "book-title")
                        {
                            Debug.WriteLine($"book-title: {childnode.InnerText}");
                            attrTitle = childnode.InnerText;
                        }

                        // если узел - company
                        if (childnode.Name == "sid")
                        {
                            Debug.WriteLine($"sid: {childnode.InnerText}");
                        }

                        // если узел age
                        if (childnode.Name == "catalit-authorization-ok")
                        {
                            Debug.WriteLine($"catalit-authorization-ok: {childnode.InnerText}");

                            //token.Sid = childnode.InnerText;
                        }


                    }//for 
                    

                    Debug.WriteLine("-- end --");

                    litresItems.Add(new LitresItem()
                    {
                        Id = attrHubId.Value,
                        GivenTitle = attrTitle,
                        ResolvedId = attrHubId.Value, // temp
                        ResolvedTitle = "Test title", // temp
                        GivenUrl = attrUrl.Value,
                        TopImageUrl = attrCover.Value,
                        Status = "1",
                    });
                }
            }
            

            // ----------------------------------------------------------------------------------


            //litresItems.Add(new LitresItem() { Id = "1111" });

            return litresItems;
        }

        public async Task<List<LitresItem>> LitresItemsAsync()
        {
            List<LitresItem> litresItems = await this.LitresItemsAsync(new DateTime(1970, 1, 1));
            return litresItems;
        }

        private List<LitresItem> ConvertJsonToLitresItems(string json)
        {
            List<LitresItem> litresItems = new List<LitresItem>();

            //JsonDocument apiJsonDocument = JsonDocument.Parse(json);
            JToken apiJsonDocument = JToken.Parse(json);

            //JsonElement jsonElement;
            JToken jsonElement;
            string statusString = "";

            //(apiJsonDocument.Root.GetProperty("status", out jsonElement))
            jsonElement = apiJsonDocument.Root.SelectToken("status");

            if (jsonElement != null)
                statusString = jsonElement.ToString();

            if (statusString == "1")
            {
                IJEnumerable<JToken> Llist = apiJsonDocument.Root.SelectToken("list").AsJEnumerable();

                // RnD: JsonProperty
                foreach (JToken litresItemJsonProperty in Llist)
                {
                    /*                  
                    string s = pocketItemJsonProperty.ToString();
                    int found = s.IndexOf(":");
                    s = s.Substring(found + 2);                    

                    //RnD
                    //found = s.LastIndexOf("}");                    
                    //s = s.Substring(0, s.Length - 2);           


                    PocketItem pocketItem = JsonConvert.DeserializeObject<PocketItem>(s);
                    */

                    LitresItem pocketItem = Converters.LitresConverter.ConvertJsonToLitresItem(
                        litresItemJsonProperty);

                    litresItems.Add(pocketItem);

                }
            }

            return litresItems;

        }//ConvertJsonToLitresItems end

    } // class end

}//
