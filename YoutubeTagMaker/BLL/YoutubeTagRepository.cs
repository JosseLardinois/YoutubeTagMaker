using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using YoutubeTagMaker.Controllers;
using YoutubeTagMaker.Interface;

namespace YoutubeTagMaker.BLL
{
    public class YoutubeTagRepository : IYoutubeTagRepository
    {
        private readonly YouTubeService _youtubeService;
        private readonly ILogger<YoutubeTagRepository> _logger;
        public YoutubeTagRepository(ILogger<YoutubeTagRepository> logger) {

            _logger = logger;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyC_iL_BLTaeDQgvQ3XGgqdu16gzhWVdWxE",
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<List<(string Tag, int Count)>> GetMostUsedTags(string channelId)
        {
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 50; // Adjust based on your needs
            searchListRequest.Type = "video"; // Ensure we're only searching for videos

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videoIds = searchListResponse.Items
                                             .Where(item => item.Id.Kind == "youtube#video")
                                             .Select(item => item.Id.VideoId)
                                             .ToList();

            var tags = new List<string>();

            if (videoIds.Count > 0)
            {
                var videoRequest = _youtubeService.Videos.List("snippet");
                videoRequest.Id = string.Join(",", videoIds); // Set the list of video IDs
                var videoResponse = await videoRequest.ExecuteAsync();

                foreach (var video in videoResponse.Items)
                {
                    if (video.Snippet.Tags != null)
                    {
                        tags.AddRange(video.Snippet.Tags);
                    }
                }
            }

            var tagz = tags.GroupBy(t => t)
                       .OrderByDescending(g => g.Count())
                       .Select(g => (Tag: g.Key, Count: g.Count()))
                       .ToList();
            return tagz;
        }

    }
}
