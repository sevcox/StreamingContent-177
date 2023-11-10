namespace StreamingContentUI;

public class ProgramUI
{
    public ProgramUI() { }

    public void Run()
    {
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
                    // ShowAllContent()
                    break;
                case "2":
                    // ShowContentByTitle()
                    break;
                case "3":
                    // CreateNewContent()
                    break;
                case "4":
                    // RemoveContent()
                    break;
                case "5":
                    // UpdateExistingContent()
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

}
