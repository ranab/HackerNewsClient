using Books.API.Entities;
using Books.API.Models.External;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Books.API.Services
{
    
    public class HackerNewsRepository : IHackerNewsRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HackerNewsRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task<string> GetBestStoriesAsCommaSeparatedIDsAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // pass through a dummy name
            var response = await httpClient
                   .GetAsync($"https://hacker-news.firebaseio.com/v0/beststories.json");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<Models.External.BookCoverDto>(
                //    await response.Content.ReadAsStringAsync(),
                //    new JsonSerializerOptions
                //    {
                //        PropertyNameCaseInsensitive = true,
                //    });
            }

            return null;
        }

        public async Task<StoryDto?> GetStoryAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // pass through a dummy name
            var response = await httpClient
                   .GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
            
            if (response.IsSuccessStatusCode)
            {

                //return await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<StoryDto>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
            }

            return null;

        }

        public async Task<IEnumerable<Models.External.StoryDto>> GetStoriesAfterWaitForAllAsync(
        IEnumerable<string> storyIds)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var stories = new List<Models.External.StoryDto>();

            var storyTasks = new List<Task<HttpResponseMessage>>();
            foreach (var storyId in storyIds)
            {
                storyTasks.Add(httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json"));
            };

            // wait for all tasks to be completed
            var storyTasksResults = await Task.WhenAll(storyTasks);
            // run through the results in reverse order 
            foreach (var bookCoverTaskResult in storyTasksResults.Reverse())
            {
                var story = JsonSerializer.Deserialize<Models.External.StoryDto>(
                    await bookCoverTaskResult.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });

                if (story != null)
                {
                    stories.Add(story);
                }
            }
            return stories;
        }

    }


}
