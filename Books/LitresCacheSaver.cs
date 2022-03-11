// LitresCacheSaver

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Windows.Storage;

using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Serialization;

//using PocketApi;
using PocketApi.Models;


namespace NfcSample
{

    public class LitresCacheSaver
    {
        private StorageFolder _storageFolder;

        public LitresCacheSaver(StorageFolder storageFolder)
        {
            _storageFolder = storageFolder;
        }

        public async Task<bool> SaveCacheAsync(LitresCache litresCache)
        {
            StorageFile itemsStorageFile = 
                await _storageFolder.CreateFileAsync
                ("PocketItems.json", CreationCollisionOption.ReplaceExisting);
            
            StorageFile lastSyncDateStorageFile = 
                await _storageFolder.CreateFileAsync
                ("SyncDate.json", CreationCollisionOption.ReplaceExisting);

            try
            {
                string itemsString = JsonConvert.SerializeObject(litresCache.LitresItems);
                string lastSyncDateString = JsonConvert.SerializeObject(litresCache.LastSyncDateTime);
                File.WriteAllText(itemsStorageFile.Path, itemsString);
                File.WriteAllText(lastSyncDateStorageFile.Path, lastSyncDateString);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> LoadCacheAsync(LitresCache litresCache)
        {
            try
            {
                StorageFile itemsStorageFile = await _storageFolder.GetFileAsync("PocketItems.json");
                string itemsString = File.ReadAllText(itemsStorageFile.Path);

                ObservableCollection<LitresItem> pocketItems
                    = JsonConvert.DeserializeObject<ObservableCollection<LitresItem>>(itemsString);

                StorageFile lastSyncDateStorageFile = await _storageFolder.GetFileAsync("SyncDate.json");

                string lastSyncDateString = File.ReadAllText(lastSyncDateStorageFile.Path);

                DateTime dateTime = JsonConvert.DeserializeObject<DateTime>(lastSyncDateString);

                litresCache.SetCacheContent(dateTime, pocketItems);

                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw (e);
            }
        }//

    }//class end
}
