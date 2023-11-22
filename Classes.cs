namespace uiuc_dev_exercise_1018404;

public class Person {
    public string Name { get; set; } = string.Empty;
    public List<Training>? Completions { get; set; }
}

public class Training {
    public string Name { get; set; } = string.Empty;
    public DateOnly Timestamp { get; set; }
    public DateOnly? Expires { get; set; }
}

public class TrainingCount {
    public string TrainingName { get; set; } = string.Empty;
    public int CompletionCount { get; set; }
}