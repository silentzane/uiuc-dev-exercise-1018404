using System.Globalization;
using System.Net.Mail;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace uiuc_dev_exercise_1018404;

public class ParseJsonTrainings {
    private readonly string _trainingsJsonFilePath;

    public ParseJsonTrainings(string trainingsJsonFilePath) {
        _trainingsJsonFilePath = trainingsJsonFilePath;
    }

    private readonly JsonSerializerOptions _options = new() {
        // Allows for case-insensitive property name matching
        // Example: "Name" matches with "name", "Completions" matches with "completions", etc.
        PropertyNameCaseInsensitive = true
    };

    // Method used to convert JSON date strings to DateOnly variables
    public class JsonDateOnlyConverter : JsonConverter<DateOnly> {
        // Define the date format the data is in
        private const string DateFormat = "M/d/yyyy";

        // Deserializer
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return DateOnly.ParseExact(reader.GetString()!, DateFormat);
        }

        // Serializer
        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }

    public List<Person> ReadAndParseTrainings() {
        // Adds the DateOnly converter to the JsonSerializer
        _options.Converters.Add(new JsonDateOnlyConverter());

        // Attempt to read the JSON file
        var json = File.ReadAllText(_trainingsJsonFilePath);
        List<Person> people = JsonSerializer.Deserialize<List<Person>>(json, _options);

        return people;
    }
}