using StreamingContentData;

namespace StreamingContentRepository;

public class StreamingContentRepository
{
    // fake database
    protected readonly List<StreamingContentEntity> _contentDb = new List<StreamingContentEntity>();

    // CRUD
    // Create Method
    public void AddContent(StreamingContentEntity content)
    {
        _contentDb.Add(content);
    }
    // Read Method
    public List<StreamingContentEntity> GetAllStreamingContent()
    {
        return _contentDb;
    }
    // Read by Title Method
    public StreamingContentEntity GetStreamingContentByTitle(string title)
    {
        foreach(StreamingContentEntity content in _contentDb)
        {
            if(content.Title == title)
            {
                return content;
            }
        }
        return null;

        //_contentDb.SingleOrDefault(c => c.Title == title);
    }
    // Update Method
    // Delete Method
    public void DeleteExistingContent(StreamingContentEntity content)
    {
        _contentDb.Remove(content);
    }
}
