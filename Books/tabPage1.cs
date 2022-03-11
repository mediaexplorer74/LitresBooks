using PocketApi;
using PocketApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI.Popups;
using Newtonsoft.Json; //using System.Text.Json;
using Newtonsoft.Json.Serialization;

// MultiPocket.UWP
namespace MultiPocket.UWP
{

    public sealed partial class MainPage : Page
    {
        
        private PocketClient pocketClient;
        
        private PocketCache pocketCache;

        private PocketCacheSaver cacheSaver = 
            new PocketCacheSaver(Windows.Storage.ApplicationData.Current.LocalFolder);

        public SecretsClass Secrets = new SecretsClass();

        // MainPage
        public MainPage()
        {
            this.InitializeComponent();

            Secrets.PocketAPIConsumerKey = new PocketApi.Models.AccessToken();
            Secrets.PocketAPIConsumerKey.Token = "";

            // Desktop case (universal, but Capcha exists...)
            //Secrets.PocketAPIConsumerKey.ConsumerKey = "xxxxx";// consumerkey 

            // Mobile case (No capcha, but only for auth via Social Net...)
            Secrets.PocketAPIConsumerKey.ConsumerKey = "xxxxx"; // consumerkey 

            Secrets.PocketAPIConsumerKey.Username = "xxx"; // username

            this.InitializePocketCache();

        }//MainPage


        // InitializePocketCache
        private async void InitializePocketCache()
        {
            try
            {
                await this.AuthPocketAsync();
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog m =
                    new Windows.UI.Popups.MessageDialog("Error on getting app permissions: " + ex.Message);

                m.ShowAsync();

                return;
            }

            pocketCache = new PocketCache(pocketClient);
            await cacheSaver.LoadCacheAsync(pocketCache);

            try
            {
                await SyncPocketCacheAsync();
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog m = 
                    new Windows.UI.Popups.MessageDialog("Error on app pocket sync: " + ex.Message);

                m.ShowAsync();
            }

        }//InitializePocketCache


        // SyncPocketCacheAsync
        private async Task SyncPocketCacheAsync()
        {
            await pocketCache.SyncArticlesAsync();

            await cacheSaver.SaveCacheAsync(pocketCache);
            
            Bindings.Update();

        }// SyncPocketCacheAsync


        // AuthPocketAsync
        private async Task AuthPocketAsync()
        {
            if (!AuthPocketViaSavedAccessToken())
            {
                pocketClient = new PocketClient(Secrets.PocketAPIConsumerKey);

                Uri callbackUri = 
                    WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

                RequestToken requestToken = null;
                try
                {
                    requestToken = await pocketClient.ObtainRequestTokenAsync
                    (
                        callbackUri
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] Exception: " + ex.Message);

                    return;
                }

                Uri requestUri = 
                    pocketClient.ObtainAuthorizeRequestTokenRedirectUri(
                        requestToken, callbackUri);

                // /Store (backup) requestUri because of WebAuthenticationBroker may damage it...
                var requestUri_backup = requestUri;

                WebAuthenticationResult result = null;
                try
                {
                    result =
                        await WebAuthenticationBroker.AuthenticateSilentlyAsync(requestUri);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] uthenticateSilentlyAsync exception: " + ex.Message);
                }

                if (result == null)
                {
                    // try no 2
                    requestUri = requestUri_backup;

                    result = await WebAuthenticationBroker.AuthenticateAsync
                    (
                        WebAuthenticationOptions.None,
                        requestUri
                    );
                    
                }
                else
                {
                    if (result.ResponseStatus != WebAuthenticationStatus.Success)
                    {
                    
                        // try no 2
                        result = await WebAuthenticationBroker.AuthenticateAsync
                        (
                            WebAuthenticationOptions.None,
                            requestUri
                        );
                    }

                }

                AccessToken token = 
                    await pocketClient.ObtainAccessTokenAsync(requestToken);

                ApplicationDataContainer localSettings = 
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                string tokenString = JsonConvert.SerializeObject(token);
                
                localSettings.Values.Add("accessToken", tokenString);
            }

        }//AuthPocketAsync


        // AuthPocketViaSavedAccessToken
        private bool AuthPocketViaSavedAccessToken()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            if (localSettings.Values.TryGetValue("accessToken", out object accessTokenObject))
            {
                AccessToken accessToken = JsonConvert.DeserializeObject<AccessToken>(accessTokenObject.ToString());
                pocketClient = new PocketClient(accessToken);
                return true;
            }
            
            return false;
        }//AuthPocketViaSavedAccessToken

        private void MyList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //var a = 3;
        }


    }//MainPage

    // SecretsClass
    public class SecretsClass
    {
        public AccessToken PocketAPIConsumerKey;
    }//SecretsClass


}//MultiPocket.UWP
