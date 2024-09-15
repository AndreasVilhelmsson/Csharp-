using System.Collections.Generic;
using System.Linq;

namespace Project1.Models
{
	public class Student : Person
	{
		// skapar en dictionary för att spara data som key value likt ett object i javaScript 
		// Teacher ärver från person som är min abstrakta klass, som både lärare och student ärver ifrån och lägger till ID teacher klassen

		public Dictionary<Subject, List<int>> Grades { get; set; }
		public Teacher Teacher { get; set; }
		// skapar en konstruktor som tar in firstname, lastname, gender, ett object, lista med betyg och lärare
		public Student(string firstName, string lastName, string gender, Dictionary<Subject, List<int>> grades, Teacher teacher)
			: base(firstName, lastName, gender)
		{
			Grades = grades;
			Teacher = teacher;
		}

		public double AverageGrade => Grades.Values.SelectMany(g => g).Average();
		public int HighestGrade => Grades.Values.SelectMany(g => g).Max();
	}
}
