using StreamingContentData.Content;
using StreamingContentRepository;
using StreamingContentData.Enums;
using NuGet.Frameworks;

namespace StreamingContentRepositoryTests;

public class StreamingRepositoryTests
{
    private StreamingRepository _repo;
    private Movie _movieA;
    private Movie _movieB;
    private Show _showA;
    private List<Episode> _episodes;
    private Episode _episodeA;
    private Episode _episodeB;
    private Show _showB;

    public StreamingRepositoryTests()
    {
        _repo = new StreamingRepository();
        _movieA = new Movie("Bad Boys", "Cop Film", 4, "Netflix", MaturityRating.R, GenreType.Action);
        _movieB = new Movie("50 First Dates", "Girl Forgets stuff", 5, "Hulu", MaturityRating.R, GenreType.RomCom);
        _showA = new Show("The Price is Right", "Family Show", 5, "ABC", MaturityRating.TV_G, GenreType.Reality);
        _episodes = new List<Episode>
        {
            new Episode
            {
                Title = "The Price is Right Ep.1",
                RunTime = .5d,
                SeasonNumber = 1
            },
            new Episode
            {
                Title = "The Price is Right Ep.2",
                RunTime = .5d,
                SeasonNumber = 1
            }
        };
        _showA.Episodes = _episodes;

        _repo.AddContent(_movieA);
        _repo.AddContent(_movieB);
        _repo.AddContent(_showA);
    }
    [Fact]
    public void TotalCount()
    {
        // ACT
        int actual = _showA.EpisodeCount;
        int expected = 2;

        // ASSERT
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AverageShowTime()
    {
        double expected = .5d;
        double actual = _showA.AverageRunTime;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetShowByTitle()
    {
        Show retrievedShow = _repo.GetShowByTitle("The Price is RIGHT");

        Show expected = _showA;
        Show actual = retrievedShow;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetMovieByTitle()
    {
        //* Act
        Movie retrivedMovie = _repo.GetMovieByTitle("BaD BoYs");

        Movie expected = _movieA;
        Movie actual = retrivedMovie;

        //* Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetAllShows()
    {
        //* Act
        List<Show> retrievedShows = _repo.GetAllShows();

        int expectedCount = 1;
        int actual = retrievedShows.Count;

        //* Assert
        Assert.Equal(expectedCount, actual);
    }

    [Fact]
    public void GetAllMovies()
    {
        //* Act
        List<Movie> retrievedMoives = _repo.GetAllMovies();

        int expectedCount = 2;
        int actual = retrievedMoives.Count;

        //* Assert
        Assert.Equal(expectedCount,actual);
    }
}