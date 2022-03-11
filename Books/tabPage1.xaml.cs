// tabPage1

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
namespace NfcSample
{

    public sealed partial class tabPage1 : Page
    {

        //private PocketClient pocketClient;
        public LitresClient litresClient;

        //private PocketCache pocketCache;
        private LitresCache litresCache;

        //private PocketCacheSaver cacheSaver =
        //    new PocketCacheSaver(Windows.Storage.ApplicationData.Current.LocalFolder);
        private LitresCacheSaver cacheSaver =
           new LitresCacheSaver(Windows.Storage.ApplicationData.Current.LocalFolder);

        // public SecretsClass Secrets = new SecretsClass();
        public LitresSecretsClass LitresSecrets = new LitresSecretsClass();

        // MainPage
        public tabPage1()
        {
            this.InitializeComponent();

            LitresSecrets.LitresAPIConsumerKey = new PocketApi.Models.AccessData();
            LitresSecrets.LitresAPIConsumerKey.Token = "";

            LitresSecrets.LitresAPIConsumerKey.Login = "xxxxxxx@xxx.ru"; // Paste your login here
            LitresSecrets.LitresAPIConsumerKey.Password = "xxxxxxx"; // Paste your pwd here

            this.InitializeLitresCache();

        }//MainPage


        // InitializeLitresCache
        private async void InitializeLitresCache()
        {
            // phase 1 -- "get token"
            try
            {
                //await this.AuthPocketAsync();
                await this.AuthLitresAsync();
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog m =
                    new Windows.UI.Popups.MessageDialog("Error on getting app permissions: " + ex.Message);

                m.ShowAsync();

                return;
            }

            // TEMP
            //SyncArticlesAsync(this);

            // future

            
            //
            litresCache = new LitresCache(litresClient);

            
            //await cacheSaver.LoadCacheAsync(pocketCache);
            await cacheSaver.LoadCacheAsync(litresCache);

            
            try
            {
                await SyncLitresCacheAsync();
            }
            catch (Exception ex)
            {
                Windows.UI.Popups.MessageDialog m =
                    new Windows.UI.Popups.MessageDialog("Error on app pocket sync: " + ex.Message);

                m.ShowAsync();
            }
            


        }//InitializePocketCache


        // SyncPocketCacheAsync
        private async Task SyncLitresCacheAsync()
        {
            //await pocketCache.SyncArticlesAsync();
            await litresCache.SyncArticlesAsync();

            //await cacheSaver.SaveCacheAsync(pocketCache);
            await cacheSaver.SaveCacheAsync(litresCache);

            //TODO
            Bindings.Update();

        }// SyncPocketCacheAsync


        // AuthPocketAsync
        // AuthLitresAsync
        private async Task AuthLitresAsync()
        {

            // RnD: sid is not token, so we don't need to save sid at local storage!
            if (true)//(!AuthLitresViaSavedAccessToken())
            {
                
                //pocketClient = new PocketClient(Secrets.PocketAPIConsumerKey);
                litresClient = new LitresClient(LitresSecrets.LitresAPIConsumerKey);                                

                litresClient._accessData.Login = LitresSecrets.LitresAPIConsumerKey.Login;
                litresClient._accessData.Password = LitresSecrets.LitresAPIConsumerKey.Password;

                Uri callbackUri =
                    new Uri($"https://robot.litres.ru/pages/catalit_authorise/");
                    //WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

                RequestToken requestToken = null;
                try
                {
                    requestToken = await litresClient.ObtainRequestTokenAsync
                    (
                        callbackUri
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ex] Exception: " + ex.Message);

                    return;
                }

                Debug.WriteLine("********************");
                Debug.WriteLine(requestToken.UserId);
                Debug.WriteLine(requestToken.Sid);
                Debug.WriteLine("********************");

                litresClient._accessData.Sid = requestToken.Sid;

                /*
                Uri requestUri =
                    litresClient.ObtainAuthorizeRequestTokenRedirectUri(
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
                    Debug.WriteLine("[ex] authenticateSilentlyAsync exception: " + ex.Message);
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
                */

                /*
                AccessData token =
                    await litresClient.ObtainAccessDataAsync(requestToken);

                ApplicationDataContainer localSettings =
                    Windows.Storage.ApplicationData.Current.LocalSettings;

                string tokenString = JsonConvert.SerializeObject(token);

                //localSettings.Values.Add("accessToken", tokenString);
                */

            }

        }//AuthPocketAsync

        /*
        // AuthPocketViaSavedAccessToken
        private bool AuthLitresViaSavedAccessToken()
        {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.TryGetValue("accessToken", out object accessTokenObject))
            {
                AccessData accessData = JsonConvert.DeserializeObject<AccessData>(
                    accessTokenObject.ToString());

                litresClient = new LitresClient(accessData);
                return true;
            }

            return false;
        }//AuthLitresViaSavedAccessToken
        */

        private void MyList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //TODO
        }


    }//MainPage

    // SecretsClass
    /*
    public class SecretsClass
    {
        public AccessToken PocketAPIConsumerKey;
    }//SecretsClass
    */


    // AuthClass
    public class LitresSecretsClass
    {
        public AccessData LitresAPIConsumerKey;
    }//SecretsClass


}//MultiPocket.UWP
