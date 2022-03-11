// LitresCache

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PocketApi;
using PocketApi.Models;

namespace NfcSample
{
    // LitresCache
    public class LitresCache
    {
        private LitresClient _litresClient;

        public bool CurrentlySyncing { get; set; }
        public ObservableCollection<LitresItem> LitresItems { get; private set; } 
            = new ObservableCollection<LitresItem>();
        public DateTime LastSyncDateTime { get; private set; } = new DateTime(1970, 1, 1);

        public LitresCache(LitresClient litresClient)
        {
            _litresClient = litresClient;
        }

        public async Task SyncArticlesAsync()
        {
            CurrentlySyncing = true;

            try
            {
                DateTime newSyncDateTime = DateTime.UtcNow;
                IEnumerable<LitresItem> newLitresItems =
                    await _litresClient.LitresItemsAsync(LastSyncDateTime);

                foreach (LitresItem litresItem in newLitresItems)
                {
                    switch (litresItem.Status)
                    {
                        case "0":
                            if (!LitresItems.Any(pi => pi.Id == litresItem.Id))
                                LitresItems.Insert(0, litresItem);
                            break;
                        case "1":
                            if (LitresItems.Any(pi => pi.Id == litresItem.Id))
                                LitresItems.First(pi => pi.Id == litresItem.Id).Status = "1";
                            else
                                LitresItems.Insert(0, litresItem);
                            break;
                        case "2":
                            if (LitresItems.Any(pi => pi.Id == litresItem.Id))
                                LitresItems.Remove(LitresItems.First(pi => pi.Id == litresItem.Id));
                            break;
                    }
                }

                LastSyncDateTime = newSyncDateTime;
                CurrentlySyncing = false;
            }
            catch (Exception e)
            {
                CurrentlySyncing = false;
                throw (e);
            }
        }

        // SetCacheContent
        public void SetCacheContent(DateTime newLastSyncDateTime, 
            ObservableCollection<LitresItem> newLitresItems)
        {
            LastSyncDateTime = newLastSyncDateTime;
            LitresItems = newLitresItems;
        }

        // AddArticleAsync
        public async Task AddArticleAsync(Uri uri)
        {
            LitresItem litresItem = await _litresClient.AddLitresItemAsync(uri);
            await SyncArticlesAsync();
        }

        // DeleteArticleAsync
        public async Task DeleteArticleAsync(LitresItem litresItem)
        {
            await _litresClient.DeleteLitresItemAsync(litresItem);
            await SyncArticlesAsync();
        }

    }//class end

}//namespace end
