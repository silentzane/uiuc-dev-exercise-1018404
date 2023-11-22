using System.Text.Json;

namespace uiuc_dev_exercise_1018404;

public class TrainingAnalysis {
    
    // Creates a list of each completed training with a count of how many people have completed that training
    public static List<TrainingCount> GetTrainingCount(List<Person> people) {
        var trainingCount = people
            .Where(person => person.Completions != null) // Filter out people with null Completions
            .SelectMany(person => person.Completions) // Flatten the list of completion records into a single sequence
            .GroupBy(completion => completion.Name) // Group by Training Name
            .Select(group => new TrainingCount { // Project into TrainingCount
                TrainingName = group.Key,
                CompletionCount = group.Count()
            })
            .ToList();
        
        return trainingCount;
    }

    // Writes TrainingCount to JSON file
    public static void TrainingCountToJson(List<TrainingCount> trainingCount, string filePath)
    {
        if (trainingCount == null)
        {
            Console.WriteLine("TrainingCount is null. Returning.");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("filePath for TrainingCount is null. Returning.");
            return;
        }

        var json = JsonSerializer.Serialize(trainingCount, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Creates a list of all people that completed a specific training in the specified fiscal year
    public static Dictionary<string, List<string>> GetPeopleByTrainingAndYear(List<Person> people, List<string> trainingNames, int fiscalYear)
    {
        // Define the start/end dates of the fiscal year
        DateOnly fiscalYearStart = new DateOnly(fiscalYear - 1, 7, 1);
        DateOnly fiscalYearEnd = new DateOnly(fiscalYear, 6, 30);

        // Initialize the result dictionary
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

        // Iterate over each training name
        foreach (string trainingName in trainingNames)
        {
            // Filter people who completed the specified training in the fiscal year
            List<string> completedPeople = people
                .Where(person =>
                    person.Completions?.Any(completion => // If Completions is not null, use the Any method to check conditions
                        completion.Name == trainingName &&
                        completion.Timestamp >= fiscalYearStart &&
                        (completion.Expires == null || completion.Expires <= fiscalYearEnd) // Check if there's no expiration date (null) OR if the expiration date is less than the fiscal year end date
                    ) ?? false) // If Completions is null, condition is false
                .Select(person => person.Name)
                .ToList();

            // Add the result to the dictionary
            result.Add(trainingName, completedPeople);
        }

        return result;
    }

    // Writes PeopleByTrainingAndYear to JSON file
    public static void PeopleByTrainingAndYearToJson(Dictionary<string, List<string>> peopleByTrainingAndYear, string filePath)
    {
        if (peopleByTrainingAndYear == null)
        {
            Console.WriteLine("peopleByTrainingAndYear is null. Returning.");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("filePath for peopleByTrainingAndYear is null. Returning.");
            return;
        }

        var json = JsonSerializer.Serialize(peopleByTrainingAndYear, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    // Creates a list of all people that have any completed trainings that have already expired, or will expire within one month of the specified date
    public static List<(Person person, Training training, bool isExpired)> FindExpiredTrainings(List<Person> people, DateOnly specifiedDate)
    {
        List<(Person, Training, bool)> result = new List<(Person, Training, bool)>();

        foreach (var person in people)
        {
            if (person.Completions != null)
            {
                foreach (var training in person.Completions)
                {
                    if (training.Expires.HasValue && (training.Expires.Value <= specifiedDate || training.Expires.Value <= specifiedDate.AddDays(30)))
                    {
                        // Calculate if the training is expired or expiring soon
                        bool isExpired = training.Expires.Value <= specifiedDate;
                        result.Add((person, training, isExpired));
                    }
                }
            }
        }

        return result;
    }

    // Writes PeopleWithExpiredTrainings to JSON file
    public static void PeopleWithExpiredTrainingsToJson(List<(Person person, Training training, bool isExpired)> PeopleWithExpiredTrainings, string filePath)
    {
        if (PeopleWithExpiredTrainings == null)
        {
            Console.WriteLine("PeopleWithExpiredTrainings is null. Returning.");
            return;
        }

        if (string.IsNullOrEmpty(filePath))
        {
            Console.WriteLine("filePath for PeopleWithExpiredTrainings is null. Returning.");
            return;
        }

        var jsonFormatted = PeopleWithExpiredTrainings.Select(tuple => new
        {
            PersonName = tuple.Item1.Name,
            TrainingName = tuple.Item2.Name,
            Status = tuple.Item3 ? "Expired" : "Expires Soon"
        });

        var json = JsonSerializer.Serialize(jsonFormatted, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }
}