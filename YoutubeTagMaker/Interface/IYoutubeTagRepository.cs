using YoutubeTagMaker.DTO;

namespace YoutubeTagMaker.Interface
{
    public interface IYoutubeTagRepository
    {
        Task<List<TagCount>> GetMostUsedTags(string channelId);

    }
}
