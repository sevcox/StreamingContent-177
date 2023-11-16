namespace StreamingContentUI;

using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using StreamingContentData;
using StreamingContentData.Content;
using StreamingContentData.Enums;
using StreamingContentRepository;
public class ProgramUI
{
    private readonly StreamingContentRepository _repo = new StreamingContentRepository();
    public ProgramUI() { }

    public void Run()
    {
        SeedContentList();
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Enter the number of the option you would like to select:\n" +
            "1. Show All Streaming Content\n" +
            "2. Find Streaming By Title\n" +
            "3. Add New Streaming Content\n" +
            "4. Remove Streaming Content\n" +
            "5. Update Existing Content\n" +
            "6. Exit\n" +
            "=================================================================\n");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    ShowAllContent();
                    break;
                case "2":
                    ShowContentByTitle();
                    break;
                case "3":
                    CreateNewContent();
                    break;
                case "4":
                    RemoveContent();
                    break;
                case "5":
                    UpdateExistingContent();
                    break;
                case "6":
                    isRunning = CloseApplication();
                    break;
                default:
                    Console.WriteLine("Sorry, invalid selection, please try again.");
                    break;
            }
        }
    }
    private bool CloseApplication()
    {
        Console.WriteLine("Thank you for using Severa's Streaming Content Application!");
        PressAnyKey();
        return false;
    }

    private void PressAnyKey()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void ShowAllContent()
    {
        Console.Clear();

        List<StreamingContentEntity> listOfContent = _repo.GetAllStreamingContent();

        if(listOfContent.Count > 0)
        {
            foreach (StreamingContentEntity content in listOfContent)
            {
                ShowStreamingContentDetails(content);
            }
        }
        else
        {
           Console.WriteLine("Sorry, there is no content available.");
        }

        PressAnyKey();
    }

    private void ShowStreamingContentDetails(StreamingContentEntity content)
    {
        Console.WriteLine($"Title: {content.Title}\n" +
                        $"Description: {content.Description}\n" +
                        $"Star Rating: {content.StarRating}\n" +
                        $"Maturity Rating: {content.MaturityRating}\n" +
                        $"Genre: {content.GenreType}\n" +
                        $"Location: {content.Location}\n" +
                        $"Family Friendly: {content.IsFamilyFriendly}");
    }

    private void ShowContentByTitle()
    {
        Console.WriteLine("Enter in a title:");
        string userInput = Console.ReadLine();
        StreamingContentEntity content = _repo.GetStreamingContentByTitle(userInput);

        if(content != null)
        {
            ShowStreamingContentDetails(content);
        }
        else
        {
            Console.WriteLine("Invalid title. Could not find results.");
        }

        PressAnyKey();
    }

    private StreamingContentEntity AddStreamingContentDetails()
    {
        StreamingContentEntity content = new StreamingContentEntity();

        // Title
        Console.WriteLine("Please input a title:");
        string userInputTitle = Console.ReadLine()!;
        content.Title = userInputTitle;

        // Description
        Console.WriteLine("Please input a description:");
        string userInputDescription = Console.ReadLine()!;
        content.Description = userInputDescription;

        // Star Rating
        Console.WriteLine("Please input a star rating:");
        string userInputStarRating = Console.ReadLine()!;
        content.StarRating = Convert.ToDouble(userInputStarRating);

        // Location
        Console.WriteLine("Please input a location:");
        string userInputLocation = Console.ReadLine()!;
        content.Location = userInputLocation;

        // Maturity Rating
        Console.WriteLine("Please enter a maturity rating:\n" +
                        "1. G\n" +
                        "2. PG\n" +
                        "3. PG_13\n" +
                        "4. R\n" +
                        "5. TV_Y\n" +
                        "6. TV_G\n" +
                        "7. TV_PG\n" +
                        "8. TV_14\n" +
                        "9. TV_MA\n" +
                        "10. M\n");
        string userInputMaturity = Console.ReadLine()!.ToUpper();
        switch(userInputMaturity)
        {
            case "1":
            case "G":
                content.MaturityRating = MaturityRating.G;
                break;
            case "2":
            case "PG":
                 content.MaturityRating = MaturityRating.PG;
                 break;
            case "3":
            case "PG_13":
                content.MaturityRating = MaturityRating.PG_13;
                break;
            case "4":
            case "R":
                content.MaturityRating = MaturityRating.R;
                break;
            case "5":
            case "TV_Y":
                content.MaturityRating = MaturityRating.TV_Y;
                break;
            default:
                Console.WriteLine("You entered in an invalid Maturity Rating");
                break;          
        }

        // Genre Type
        Console.WriteLine("Select a Genre by entering in the number you would like:\n" +
                        "1.Horror\n" +
                        "2.RomCom\n" +
                        "3.Drama\n" +
                        "4.Documentary\n" +
                        "5.Scifi\n" +
                        "6.Reality\n");

        string userInputGenre = Console.ReadLine()!;

        int genreId = int.Parse(userInputGenre);

        content.GenreType = (GenreType)genreId;

        Console.WriteLine("What kind of content is this?\n" +
                          "1. Movie\n" +
                          "2. Show\n");

        var userInputType = Console.ReadLine()!;

        switch (userInputType)
        {
            case "1":
                Console.WriteLine("You chose the Movie Type.");

                return new Movie
                {
                    Title = content.Title,
                    Description = content.Description,
                    StarRating = content.StarRating,
                    MaturityRating = content.MaturityRating,
                    GenreType = content.GenreType
                };

            case "2":
                Console.WriteLine("You chose the Show Type.");

                var theShow = new Show
                {
                    Title = content.Title,
                    Description = content.Description,
                    StarRating = content.StarRating,
                    MaturityRating = content.MaturityRating,
                    GenreType = content.GenreType
                };

                System.Console.WriteLine("Are there any Episodes?");
                var episode = new Episode();
                Console.WriteLine("Episode Title:");
                var userInputEpisodeTitle = Console.ReadLine()!;
                episode.Title = userInputEpisodeTitle;

                // List<Episode> episodes = new List<Episode>()
                // {
                //     new Episode(),
                //     episode
                // };

                // theShow.Episodes.AddRange(episodes);

                theShow.Episodes.Add(episode);

                return theShow;

            default:
                return content;
        }

    }

    private void CreateNewContent()
    {
        // create a streaming content entity object
        StreamingContentEntity content = AddStreamingContentDetails();
        // add to my list
        if(_repo.AddContent(content))
        {
            Console.WriteLine("Your content was added.");
        }
        else
        {
            Console.WriteLine("Your content was not added");
        }
        PressAnyKey();
    }

    private void RemoveContent()
    {
        Console.WriteLine("Which item are you wanting to remove?");

        List<StreamingContentEntity> contentList = _repo.GetAllStreamingContent();

        int count = 0;
        foreach(StreamingContentEntity content in contentList)
        {
            // count = count + 1;
            count ++;
            Console.WriteLine($"{count}. {content.Title}");
        }

        int targetContentId = int.Parse(Console.ReadLine()!);
        int targetIndex = targetContentId - 1;

        if(targetIndex >= 0 && targetIndex < contentList.Count)
        {
            //contentList[0]
            StreamingContentEntity desiredContent = contentList[targetIndex];

            if(_repo.DeleteExistingContent(desiredContent))
            {
                Console.WriteLine($"{desiredContent.Title} was succesfully deleted.");
            }
            else
            {
                Console.WriteLine($"{desiredContent.Title} failed to be deleted.");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection");
        }

        PressAnyKey();
    }

    private void UpdateExistingContent()
    {
        Console.WriteLine("Enter a title:");

        string userInput = Console.ReadLine()!;

        StreamingContentEntity content = _repo.GetStreamingContentByTitle(userInput);

        if(content != null)
        {
            StreamingContentEntity updatedData = AddStreamingContentDetails();
            if(_repo.UpdateExistingContent(userInput, updatedData))
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Failure");
            }
        }
        else 
        {
            Console.WriteLine("Invalid title. Could not find results.");
        }

        PressAnyKey();
    }

    private void SeedContentList()
    {
        StreamingContentEntity toyStory = new StreamingContentEntity("Toy Story", "A movie about toys.", 4, "Disney Plus", MaturityRating.G,GenreType.Scifi);
        StreamingContentEntity squidGames = new StreamingContentEntity("Squid Games", "A show about performing deadly games.", 4, "Netflix", MaturityRating.TV_MA, GenreType.Horror);

        _repo.AddContent(toyStory);
        _repo.AddContent(squidGames);
    }
}
