namespace Project1.Models
{
	public class Teacher : Person
	{
		public int Id { get; }
		private static int _counter = 0;

		public Teacher(string firstName, string lastName, string gender = "Other")
			: base(firstName, lastName, gender)
		{
			Id = ++_counter;
		}
	}
}
