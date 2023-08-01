using Books.API.Entities;
using Books.API.Models.External;

namespace Books.API.Services;

public interface IHackerNewsRepository
{
    Task<string> GetBestStoriesAsCommaSeparatedIDsAsync();
    Task<StoryDto> GetStoryAsync(int id);

    Task<IEnumerable<Models.External.StoryDto>> GetStoriesAfterWaitForAllAsync(
    IEnumerable<string> storyIds);
}
