// ObtainRequestToken

using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Xml;

// PocketApi namespace
namespace PocketApi
{
    // LitresClient
    public partial class LitresClient
    {
        private static Uri _obtainRequestTokenUri =
            new Uri(
                $"https://robot.litres.ru/pages/catalit_authorise/"
                );

        //new Uri($"https://getpocket.com/v3/oauth/request");


        // ObtainRequestTokenAsync
        // gets sid and user-id and puts at token
        public async Task<RequestToken> ObtainRequestTokenAsync(Uri CallBackUri)
        {
            string response = await ApiPostAsync( 
               _obtainRequestTokenUri,
                new ObtainAccessTokenBody()
                {
                    Login = _accessData.Login,//_userKey,//ConsumerKey = _consumerKey,
                    Pwd = _accessData.Password,//CallBackUri.ToString()
                }
            );

            //
            RequestToken token = new RequestToken();

            // ----------------------------------------------------------------------------------
            // XML Parse

            // create xml doc
            XmlDocument xDoc = new XmlDocument();

            // load xml from html response
            xDoc.LoadXml(response);// as XmlDocument;//xDoc.Load("people.xml");
            
            // get root element
            XmlElement xRoot = xDoc.DocumentElement;

            // get sid attr
            XmlNode sidAttr = xRoot.Attributes.GetNamedItem("sid");

            // get user-id attr
            XmlNode useridAttr = xRoot.Attributes.GetNamedItem("user-id");

            //Debug.WriteLine(sidAttr.Value);

            try
            {
                token.Sid = sidAttr.Value;
                token.UserId = useridAttr.Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] GetNamedItem(sid/user-id) Exception: " + ex.Message);
            }

            /*
            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement xnode in xRoot)
                {
                    // получаем атрибут name
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
                    
                    Debug.WriteLine(attr?.Value);

                    // обходим все дочерние узлы элемента user
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        // если узел - company
                        if (childnode.Name == "sid")
                        {
                            Debug.WriteLine($"Company: {childnode.InnerText}");
                        }
                        // если узел age
                        if (childnode.Name == "catalit-authorization-ok")
                        {
                            Debug.WriteLine($"catalit-authorization-ok: {childnode.InnerText}");

                            token.Sid = childnode.InnerText;
                        }
                    }

                    Debug.WriteLine("---");
                }
            }
            */

            // ----------------------------------------------------------------------------------


            /*
            var apiJsonDocument = JToken.Parse(response);
            IJEnumerable<JToken> Lsid = apiJsonDocument.Root.SelectToken("sid").AsJEnumerable();

            // Newtonsoft.Json.Serialization.JsonProperty
            foreach ( JToken property in apiJsonDocument.Root.AsJEnumerable())//apiJsonDocument.RootElement.EnumerateObject()
            {
                
                
                if (property.Name == "sid")
                {
                    if (property.Value.GetDouble() != 1)
                    {
                        //return false;
                    }
                    else
                    {
                        //return true;
                    }
                }
                
                Debug.WriteLine(property.ToString());
            }
            */


            return token;
        }
    }//class end

}
