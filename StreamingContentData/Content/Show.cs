namespace StreamingContentData.Content;

using StreamingContentData;
using StreamingContentData.Enums;

public class Show : StreamingContentEntity
{
    public Show() { }
    public Show(string title, string description, double starRating, string location, MaturityRating maturityRating, GenreType genreType) : base(title, description, starRating,location, maturityRating, genreType )
    {
    
    }
    public Show(string title, string description, double starRating, string location, MaturityRating maturityRating, GenreType genreType, List<Episode> episodes) : base(title, description, starRating,location, maturityRating, genreType)
    {
        Episodes = episodes;
    }
    public Show(List<Episode> episodes)
    {
        Episodes = episodes;
    }
    
    public int SeasonCount { get; set; }
    public int EpisodeCount
    {
        get
        {
            return Episodes.Count;
        }
    }
    public List<Episode> Episodes { get; set; } = new List<Episode>();
    public double AverageRunTime
    {
        get
        {
            // linq - language integrated query
            return Episodes.Average(e => e.RunTime);
        }
    }
}
