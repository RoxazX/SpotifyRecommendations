using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyRecommendations
{
    class NewsApi
    {
        public static HttpClient Client = new HttpClient();
        public static async Task<SearchResults> SearchByKeyword(string keyword)
        {
            return await Task.Run(async() => {

                try
                {
                    var html = await Client.GetStringAsync("https://newsapi.org/v2/everything?q=" + keyword + "&from=" + DateTime.UtcNow.ToString("YYYY-MM-DD") + "&sortBy=publishedAt&apiKey=8603f409c02a4b9ea1cd2193e56ad9e2");
                    Debug.WriteLine(html);
                    var results = JsonConvert.DeserializeObject<SearchResults>(html);
                    return results;
                }
                catch
                {
                    return null;
                }
            });

        }

    }
    public class Source
    {
        public object id { get; set; }
        public string name { get; set; }
    }

    public class Article
    {
        public Source source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }

    public class SearchResults
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
    }
}
