using PocketApi.Models;
using PocketApi.RestApiRequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Serialization;

using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PocketApi
{
    /*
    public partial class PocketClient
    {
        private static Uri _modifyUri = new Uri($"https://getpocket.com/v3/send");

        public async Task<bool> DeletePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction pocketDeleteAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Delete,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(pocketDeleteAction);
        }

        public async Task<bool> ArchivePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction archiveModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Archive,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(archiveModifyAction);
        }

        public async Task<bool> ReAddPocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction reAddModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.ReAdd,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(reAddModifyAction);
        }

        public async Task<bool> MarkFavoritePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction favoriteModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.Favorite,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(favoriteModifyAction);
        }

        public async Task<bool> UnFavoritePocketItemAsync(PocketItem pocketItem)
        {
            PocketModifyAction unfavoriteModifyAction = new PocketModifyAction()
            {
                Action = PocketModifyActionType.UnFavorite,
                ItemId = pocketItem.Id
            };

            return await ModifyPocketItemAsync(unfavoriteModifyAction);
        }

        private async Task<bool> ModifyPocketItemAsync(PocketModifyAction pocketModifyAction)
        {
            string apiResponse = await ApiPostAsync(
                _modifyUri,
                new ModifyPocketItemBody()
                {
                    ConsumerKey = _accessToken.ConsumerKey,
                    AccessToken = _accessToken.Token,
                    Actions = new PocketModifyAction[]
                    {
                        pocketModifyAction
                    }
                });

            //JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponse);
            var apiJsonDocument = JToken.Parse(apiResponse);

            //RnD
            //foreach (JsonProperty property in apiJsonDocument.Root.AsJEnumerable())//apiJsonDocument.RootElement.EnumerateObject()
            //{
            //    if (property.Name == "status")
            //    {
            //        if (property.Value.GetDouble() != 1)
            //            return false;
            //        else
            //            return true;
            //    }
            //}

            return false;
        }
    }
    */

    public partial class LitresClient
    {
        private static Uri _modifyUri = new Uri($"https://robot.litres.ru/pages/catalit_browser/");//($"https://getpocket.com/v3/send");

        public async Task<bool> DeleteLitresItemAsync(LitresItem litresItem)
        {
            LitresModifyAction litresDeleteAction = new LitresModifyAction()
            {
                Action = LitresModifyActionType.Delete,
                ItemId = litresItem.Id
            };

            return await ModifyLitresItemAsync(litresDeleteAction);
        }

        public async Task<bool> ArchiveLitresItemAsync(LitresItem litresItem)
        {
            LitresModifyAction archiveModifyAction = new LitresModifyAction()
            {
                Action = LitresModifyActionType.Archive,
                ItemId = litresItem.Id
            };

            return await ModifyLitresItemAsync(archiveModifyAction);
        }

        public async Task<bool> ReAddLitresItemAsync(LitresItem litresItem)
        {
            LitresModifyAction reAddModifyAction = new LitresModifyAction()
            {
                Action = LitresModifyActionType.ReAdd,
                ItemId = litresItem.Id
            };

            return await ModifyLitresItemAsync(reAddModifyAction);
        }

        public async Task<bool> MarkFavoriteLitresItemAsync(LitresItem litresItem)
        {
            LitresModifyAction favoriteModifyAction = new LitresModifyAction()
            {
                Action = LitresModifyActionType.Favorite,
                ItemId = litresItem.Id
            };

            return await ModifyLitresItemAsync(favoriteModifyAction);
        }

        public async Task<bool> UnFavoriteLitresItemAsync(LitresItem litresItem)
        {
            LitresModifyAction unfavoriteModifyAction = new LitresModifyAction()
            {
                Action = LitresModifyActionType.UnFavorite,
                ItemId = litresItem.Id
            };

            return await ModifyLitresItemAsync(unfavoriteModifyAction);
        }

        private async Task<bool> ModifyLitresItemAsync(LitresModifyAction litresModifyAction)
        {
            string apiResponse = "";

            /*
            apiResponse = await ApiPostAsync(
                _modifyUri,
                new ModifyLitresItemBody()
                {
                    Sid = _accessData.Sid,
                    AccessToken = _accessData.Password,
                    Actions = new LitresModifyAction[]
                    {
                        litresModifyAction
                    }
                });
            */

            //JsonDocument apiJsonDocument = JsonDocument.Parse(apiResponse);
            var apiJsonDocument = JToken.Parse(apiResponse);

            //RnD
            //foreach (JsonProperty property in apiJsonDocument.Root.AsJEnumerable())//apiJsonDocument.RootElement.EnumerateObject()
            //{
            //    if (property.Name == "status")
            //    {
            //        if (property.Value.GetDouble() != 1)
            //            return false;
            //        else
            //            return true;
            //    }
            //}

            return false;
        }

    }//class end

}//namespace end
