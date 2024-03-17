using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Services;
using YoutubeTagMaker.Interface;

namespace YoutubeTagMaker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YoutubeController : ControllerBase
    {
        private readonly ILogger<YoutubeController> _logger;
        private readonly IYoutubeTagRepository _youtubeTagRepository;
        public YoutubeController(ILogger<YoutubeController> logger, IYoutubeTagRepository youtubeTagRepository)
        {
            _youtubeTagRepository = youtubeTagRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/youtube/mostUsedTags")]
        public async Task<IActionResult> GetMostUsedTags(string channelId)
        {
            if(channelId != null || channelId != string.Empty)
            {
                var mostUsedTags = await _youtubeTagRepository.GetMostUsedTags(channelId);

                if (mostUsedTags.Any())
                {
                    var firstItem = mostUsedTags.FirstOrDefault();
                    return Ok(firstItem.Tag);
                }
            }
                return BadRequest(500);
        }
    }
}