# Programming Exercise: Application Developer (1018404)

Using the language of your choice, write a small application that:

* Reads all data from a .Json file (use the attached file trainings.txt).
* Generate output as JSON in the three following ways.
	* List each completed training with a count of how many people have completed that training.
	* Given a list of trainings and a fiscal year (defined as 7/1/n-1 – 6/30/n), for each specified training, list all people that completed that training in the specified fiscal year.
		* Use parameters: Trainings = "Electrical Safety for Labs", "X-Ray Safety", "Laboratory Safety Training"; Fiscal Year = 2024
	* Given a date, find all people that have any completed trainings that have already expired, or will expire within one month of the specified date (A training is considered expired the day after its expiration date). For each person found, list each completed training that met the previous criteria, with an additional field to indicate expired vs expires soon.
		* Use date: Oct 1st, 2023
	*  _A note for all tasks. It is possible for a person to have completed the same training more than once. In this event, only the most recent completion should be considered._

Requirements for the above application:

* The app should work with any data in the specified format.
* The app should be checked into a publicly accessible Github or Azure Devops repository that the reviewers can pull and run, without any modification.
* In addition to the application code, your repository should contain the three output .Json files.
