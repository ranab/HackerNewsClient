using AutoMapper;
using Books.API.Entities;
using Books.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsRepository _hackerRepository;
        private readonly IMapper _mapper;
        public HackerNewsController(IHackerNewsRepository hackerRepository,
        IMapper mapper)
        {
            _hackerRepository = hackerRepository ??
                throw new ArgumentNullException(nameof(hackerRepository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        
        [HttpGet("{topN}")]
        public async Task<IActionResult> GetTopBestStories(int topN)
        {
            var iDs = await _hackerRepository.GetBestStoriesAsCommaSeparatedIDsAsync();
            IEnumerable<string> storiesById = iDs.Split(',');
            var results = await _hackerRepository.GetStoriesAfterWaitForAllAsync(storiesById);
            var storyEntities=  new List<Story>();

            foreach (var storyResult in results)
            {
                var storyEntity = _mapper.Map<Entities.Story>(storyResult);
                storyEntities.Add(storyEntity);
            }

            var bestStories = storyEntities
                        .OrderByDescending(st => st.Score)
                        .Take(topN);
            
            return Ok(bestStories);

        }



        
    }
}
