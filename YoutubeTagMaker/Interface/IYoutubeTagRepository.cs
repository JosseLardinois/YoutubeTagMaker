namespace YoutubeTagMaker.Interface
{
    public interface IYoutubeTagRepository
    {
        Task<List<(string Tag, int Count)>> GetMostUsedTags(string channelId);

    }
}
