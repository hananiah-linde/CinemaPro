using CinemaPro.Enums;
using CinemaPro.Models.Settings;
using CinemaPro.Models.TMDB;
using CinemaPro.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization.Json;

namespace CinemaPro.Services;

public class TMDBMovieService : IRemoteMovieService
{
    private readonly AppSettings _appSettings;
    private readonly IHttpClientFactory _httpClient;

    public TMDBMovieService(IHttpClientFactory httpClient, IOptions<AppSettings> appSettings)
    {
        _httpClient = httpClient;
        _appSettings = appSettings.Value;
    }

    public async Task<MovieDetail> MovieDetailAsync(int id)
    {
        //Step 1: Setup default return object
        MovieDetail movieDetail = new();

        //Step 2: Assemble the request
        var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{id}";
        var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.CinemaProSettings.API_KEY },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language },
                { "append_to_response", _appSettings.TMDBSettings.QueryOptions.AppendToResponse }
            };
        var requestUri = QueryHelpers.AddQueryString(query, queryParams);

        //Step 3: Create client and execute request
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await client.SendAsync(request);

        //Step 4: Deserialize into Moviedetail
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var dcjs = new DataContractJsonSerializer(typeof(MovieDetail));
            movieDetail = dcjs.ReadObject(responseStream) as MovieDetail;
        }
        return movieDetail;
    }

    public async Task<SearchMovies> SearchMoviesAsync(string searchTerm, int count)
    {

        //Step 1: Setup a default instance of SearchMovies
        SearchMovies searchMovies = new();

        //Step 2: Assemble the full request uri string
        var query = $"{_appSettings.TMDBSettings.BaseUrl}/search/movie?";

        var queryParams = new Dictionary<string, string>()
        {
            { "api_key", _appSettings.CinemaProSettings.API_KEY },
            { "language", _appSettings.TMDBSettings.QueryOptions.Language },
            { "query", searchTerm },
            { "page", _appSettings.TMDBSettings.QueryOptions.Page }
        };

        var requestUri = QueryHelpers.AddQueryString(query, queryParams);

        //Step 3: Create a client and execute the request
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await client.SendAsync(request);

        //Step 4: Return the MovieSearch object
        if (response.IsSuccessStatusCode)
        {
            var dcjs = new DataContractJsonSerializer(typeof(SearchMovies));
            using var responseStream = await response.Content.ReadAsStreamAsync();
            searchMovies = (SearchMovies)dcjs.ReadObject(responseStream);
            searchMovies.results = searchMovies.results.Take(count).ToArray();
            //searchMovies.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemaProSettings.DefaultPosterSize}/{r.poster_path}");
            searchMovies.results.ToList();
            foreach (var movie in searchMovies.results)
            {
                if(movie.poster_path is null)
                {
                    movie.poster_path = "/img/placeholder.svg";
                }
                else
                {
                    movie.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemaProSettings.DefaultPosterSize}/{movie.poster_path}";
                }


            }
        }

        return searchMovies;
    }

    public async Task<SearchMovies> SearchMoviesAsync(MovieCategory category, int count)
    {

        //Step 1: Setup a default instance of SearchMovies
        SearchMovies searchMovies = new();

        //Step 2: Assemble the full request uri string
        var query = $"{_appSettings.TMDBSettings.BaseUrl}/movie/{category}";

        var queryParams = new Dictionary<string, string>()
        {
            { "api_key", _appSettings.CinemaProSettings.API_KEY },
            { "language", _appSettings.TMDBSettings.QueryOptions.Language },
            { "page", _appSettings.TMDBSettings.QueryOptions.Page }
        };

        var requestUri = QueryHelpers.AddQueryString(query, queryParams);

        //Step 3: Create a client and execute the request
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await client.SendAsync(request);

        //Step 4: Return the MovieSearch object
        if (response.IsSuccessStatusCode)
        {
            var dcjs = new DataContractJsonSerializer(typeof(SearchMovies));
            using var responseStream = await response.Content.ReadAsStreamAsync();
            searchMovies = (SearchMovies)dcjs.ReadObject(responseStream);
            searchMovies.results = searchMovies.results.Take(count).ToArray();
            //searchMovies.results.ToList().ForEach(r => r.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemaProSettings.DefaultPosterSize}/{r.poster_path}");
            searchMovies.results.ToList();
            foreach (var movie in searchMovies.results)
            {
                if (movie.poster_path is null)
                {
                    movie.poster_path = "/img/placeholder.svg";
                }
                else
                {
                    movie.poster_path = $"{_appSettings.TMDBSettings.BaseImagePath}/{_appSettings.CinemaProSettings.DefaultPosterSize}/{movie.poster_path}";
                }


            }
        }

        return searchMovies;
    }

    public async Task<ActorDetail> ActorDetailAsync(int id)
    {
        //Step 1: Setup a default return object
        ActorDetail actorDetail = new();

        //Step 2: Assemble the full request uri string
        var query = $"{_appSettings.TMDBSettings.BaseUrl}/person/{id}";
        var queryParams = new Dictionary<string, string>()
            {
                { "api_key", _appSettings.CinemaProSettings.API_KEY },
                { "language", _appSettings.TMDBSettings.QueryOptions.Language }
            };
        var requestUri = QueryHelpers.AddQueryString(query, queryParams);

        //Step 3: Create a client and execute the request
        var client = _httpClient.CreateClient();
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var response = await client.SendAsync(request);

        //Step 4: Return the ActorDetail object
        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();

            var dcjs = new DataContractJsonSerializer(typeof(ActorDetail));
            actorDetail = (ActorDetail)dcjs.ReadObject(responseStream);
        }

        return actorDetail;
    }
}
