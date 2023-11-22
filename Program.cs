namespace uiuc_dev_exercise_1018404;

class Program
{
    static void Main(string[] args)
    {
        List<Person> people;
        
        // Get the current directory
        string workingDirectory = Environment.CurrentDirectory;

        if (workingDirectory is not null) {
            // Set the file path for trainings.txt
            ParseJsonTrainings parseJsonTrainings = new ParseJsonTrainings(workingDirectory + "\\trainings.txt");

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
            // Begin calling Training Analysis methods
            Console.WriteLine("Generating training analysis data.");

            // List of each completed training with a count of how many people have completed that training
            List<TrainingCount> trainingCount = TrainingAnalysis.GetTrainingCount(people);

            // List of all people that completed a specific training in the specified fiscal year
            List<string> trainingNames = new List<string>() {"Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training"};
            Dictionary<string, List<string>> peopleByTrainingAndYear = TrainingAnalysis.GetPeopleByTrainingAndYear(people, trainingNames, 2024);

            // List of all people that have any completed trainings that have already expired, or will expire within one month of the specified date
            DateOnly specifiedDate = new DateOnly(2023, 10, 1);
            List<(Person person, Training training, bool isExpired)> peopleWithExpiredTrainings = TrainingAnalysis.FindExpiredTrainings(people, specifiedDate);

            // Output all results to JSON files
            var filePath = workingDirectory + "\\TrainingCount.txt";
            TrainingAnalysis.TrainingCountToJson(trainingCount, filePath);

            filePath = workingDirectory + "\\PeopleByTrainingAndYear.txt";
            TrainingAnalysis.PeopleByTrainingAndYearToJson(peopleByTrainingAndYear, filePath);

            filePath = workingDirectory + "\\PeopleWithExpiredTrainings.txt";
            TrainingAnalysis.PeopleWithExpiredTrainingsToJson(peopleWithExpiredTrainings, filePath);

            Console.WriteLine("Finished generating output files!");
        }
        else {
            Console.WriteLine("No people found in file. Possible error parsing JSON.");
            return;
        }

        return;
    }
}
