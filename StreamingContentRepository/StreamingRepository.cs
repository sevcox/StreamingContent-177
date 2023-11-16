using System.Runtime.InteropServices;
using StreamingContentData;
using StreamingContentData.Content;

namespace StreamingContentRepository;

public class StreamingRepository : StreamingContentRepository
{
    public Show GetShowByTitle(string title)
    {
        foreach(StreamingContentEntity content in _contentDb)
        {
            if(content.Title.ToLower() == title.ToLower() && content.GetType() == typeof(Show))
            {
                return (Show)content;
            }
        }
        return null;
    }

    public List<Show> GetAllShows()
    {
        List<Show> allShows = new List<Show>();

        foreach(var content in _contentDb)
        {
            if(content is Show)
            {
                allShows.Add((Show)content);
            }
        } 
        return allShows;

        //return _contentDb.Where(e => e is Show).Select(e => (Show)e).ToList();
    }

        public Movie GetMovieByTitle(string title)
    {
        foreach(StreamingContentEntity content in _contentDb)
        {
            if(content.Title.ToLower() == title.ToLower() && content.GetType() == typeof(Movie))
            {
                return (Movie)content;
            }
        }
        return null;

        //return (Movie)_contentDb.FirstOrDefault(e => e.Title.ToLower() == title.ToLower() && e is Movie);
    }

    public List<Movie> GetAllMovies()
    {
        List<Movie> allMovies = new List<Movie>();

        foreach(var content in _contentDb)
        {
            if(content is Movie)
            {
                allMovies.Add((Movie)content);
            }
        } 
        return allMovies;

        //return _contentDb.Where(e => e is Movie).Select(e => (Movie)e).ToList();
    }
}
