using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace PocketApi.Converters
{
    internal class LitresConverter
    {
        // public static PocketItem ConvertJsonToPocketItem(JsonProperty itemObject)
        public static LitresItem ConvertJsonToLitresItem(JToken itemObject)
        {
            string goodObject = itemObject.ToString();
            int found = goodObject.IndexOf(":");
            goodObject = goodObject.Substring(found + 2);

            //RnD
            //found = s.LastIndexOf("}");                    
            //s = s.Substring(0, s.Length - 2);
            LitresItem pocketItem = JsonConvert.DeserializeObject<LitresItem>(goodObject);

            // ************************
            JToken sObject = JsonConvert.SerializeObject(goodObject);

            //JToken authorsJsonElement = itemObject.Root.SelectToken("authors", false);
            JToken authorsJsonElement = sObject.Root.SelectToken("authors");

            if (authorsJsonElement != null)
            {
                pocketItem.Authors = ConvertJsonToAuthors
                    (sObject.Root.SelectToken("authors"));
            }
            else
            {
                pocketItem.Authors = new List<LitresAuthor>();
            }


            //JToken imagesJsonElement = (itemObject.Root.SelectToken("images"));
            JToken imagesJsonElement = sObject.Root.SelectToken("image");

            if (imagesJsonElement != null)
                pocketItem.Images = ConvertJsonToImages(sObject.Root.SelectToken("images"));
            else
                pocketItem.Images = new List<LitresImage>();

            // ************************

            /*
            // RnD
            //JToken itemJsonDocument = itemObject.Root.SelectToken("list", false);
            JToken itemJsonDocument = JValue.Parse(itemObject.Root.ToString());//JsonDocument.Parse(itemObject.Value.ToString());
            PocketItem pocketItem = JsonConvert.DeserializeObject<PocketItem>(itemJsonDocument.SelectToken("list").ToString());


            //JsonElement 
            JToken authorsJsonElement;

            //if (itemJsonDocument.Root.SelectToken("authors", out authorsJsonElement))...
            authorsJsonElement = itemJsonDocument.Root.SelectToken("authors", false);            
            if (authorsJsonElement != null)
            {
                pocketItem.Authors = ConvertJsonToAuthors
                    (itemJsonDocument.Root.SelectToken("authors"));
            }
            else
            {
                pocketItem.Authors = new List<PocketAuthor>();
            }

            
            JToken imagesJsonElement = (itemJsonDocument.Root.SelectToken("images"));

            if (imagesJsonElement != null)
                pocketItem.Images = ConvertJsonToImages(itemJsonDocument.Root.SelectToken("images"));
            else
                pocketItem.Images = new List<PocketImage>();

            
            */

            return pocketItem;

        }//ConvertJsonToPocketItem



        // JsonElement
        private static List<LitresAuthor> ConvertJsonToAuthors(JToken json)
        {
            List<LitresAuthor> authors = new List<LitresAuthor>();

            foreach (var itemObject in json.AsJEnumerable())//.EnumerateObject())
            {
                JToken itemJsonDocument = JToken.Parse(itemObject.ToString());
                LitresAuthor author = JsonConvert.DeserializeObject<LitresAuthor>
                    (itemJsonDocument.Root.ToString());

                authors.Add(author);
            }

            return authors;

        }//ConvertJsonToAuthors


        // ConvertJsonToImages
        private static List<LitresImage> ConvertJsonToImages(JToken json)
        {
            List<LitresImage> images = new List<LitresImage>();

            foreach (var itemObject in json.AsJEnumerable())//.EnumerateObject())
            {
                var itemJsonDocument = JToken.Parse(itemObject.ToString());

                LitresImage image = JsonConvert.DeserializeObject<LitresImage>(
                    itemJsonDocument.Root.ToString());
                images.Add(image);
            }

            return images;

        }//ConvertJsonToImages
    }
}
