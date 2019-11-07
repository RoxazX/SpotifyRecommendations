using System;
using System.Diagnostics;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;

namespace SpotifyRecommendations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Example();
            Console.ReadLine();

        }
        public  static async void Example()
        {

                var auth = new ImplicitGrantAuth(
                  "7f08980f1dae4f3d98a40d44ef235b03",
                  "http://localhost:4002",
                  "http://localhost:4002",
                  Scope.UserReadPrivate
                );
                auth.AuthReceived += async (sender, payload) =>
                {
                    auth.Stop(); // `sender` is also the auth instance
                    var api = new SpotifyWebAPI()
                    {
                        TokenType = payload.TokenType,
                        AccessToken = payload.AccessToken
                    };
                    // Do requests with API client
                    var newsapiresults = await NewsApi.SearchByKeyword("bitcoin");
                    if (newsapiresults == null)
                        return;
                    Debug.WriteLine(newsapiresults);
                    Debug.WriteLine(newsapiresults.totalResults);
                    foreach (var result in newsapiresults.articles)
                    {
                        Debug.WriteLine(result.description);
                    }
                    var spotifyresults = await api.SearchItemsAsync("drake", SearchType.All);
                    if (spotifyresults == null)
                        return;

                   
                };
                auth.Start(); // Starts an internal HTTP Server
                auth.OpenBrowser();
            
        }



    }


}
