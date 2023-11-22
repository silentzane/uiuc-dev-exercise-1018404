namespace uiuc_dev_exercise_1018404;

class Program
{
    static void Main(string[] args)
    {
        List<Person> people;
        
        // Get the current directory for the exe
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        if (projectDirectory is not null) {
            // Set the file path for trainings.txt
            ParseJsonTrainings parseJsonTrainings = new ParseJsonTrainings(projectDirectory + "\\trainings.txt");

            // Attempt to parse JSON
            Console.WriteLine("Attempting to parse JSON file.");
            try {
                people = parseJsonTrainings.ReadAndParseTrainings();
            } catch (Exception e) {
                Console.WriteLine("Error parsing JSON file: " + e);
                return;
            }
        }
        else {
            Console.WriteLine("Could not find working directory!");
            return;
        }

        if (people is not null) {
            // Output methods go here
        }
        else {
            Console.WriteLine("No people found in file. Possible error parsing JSON.");
            return;
        }
    }
}
