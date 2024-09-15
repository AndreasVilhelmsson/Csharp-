namespace Project1.Models
{
	// abstract klass som används av både lärare ocn elever
	// testar att använda primary konstruktor som finns i c# 12
	// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors
	public abstract class Person(string firstName, string lastName, string gender)
	{
		public string FirstName { get; set; } = firstName;
		public string LastName { get; set; } = lastName;
		public string Gender { get; set; } = gender;
	}
}
