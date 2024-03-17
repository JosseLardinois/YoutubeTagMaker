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
        public YoutubeController(ILogger<YoutubeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/youtube/mostUsedTags")]
        public async Task<IActionResult> GetMostUsedTags(string channelId)
        {
            if(channelId != null || channelId != string.Empty)
            {
                var mostUsedTags = _youtubeTagRepository.GetMostUsedTags(channelId);
                return Ok(mostUsedTags);
            }
            else
            {
                return BadRequest(500);
            }
        }
    }
}