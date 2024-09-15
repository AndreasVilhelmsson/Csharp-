using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Project1.Models;
using Project1.Services;

namespace Project1
{
	class Program
	{
		static void Main()
		{
			// nytt sätt att sätta en instance 
			// innehåller alla funtioner och listor av lärare och elever
			School school = new();

			//skapar lärare 
			Teacher teacher1 = new("Marcus", "Medina", "Male");
			Teacher teacher2 = new("Lars", "Johansson", "Male");

			school.AddTeacher(teacher1);
			school.AddTeacher(teacher2);

			// skapar betyg och sparar dessa i variablerna grades1, grades2, grades3
			// 
			Dictionary<Subject, List<int>> grades1 = new()
			{
				{ Subject.Math, new List<int> { 85, 90, 78 } },
				{ Subject.English, new List<int> { 80, 88, 92 } },
				{ Subject.Swedish, new List<int> { 75, 85, 80 } },
				{ Subject.Chemistry, new List<int> { 90, 85, 88 } }
			};

			Dictionary<Subject, List<int>> grades2 = new()
			{
				{ Subject.Math, new List<int> { 95, 88, 91 } },
				{ Subject.English, new List<int> { 92, 85, 89 } },
				{ Subject.Swedish, new List<int> { 78, 82, 80 } },
				{ Subject.Chemistry, new List<int> { 85, 90, 87 } }
			};

			Dictionary<Subject, List<int>> grades3 = new()
			{
				{ Subject.Math, new List<int> { 70, 75, 80 } },
				{ Subject.English, new List<int> { 65, 70, 72 } },
				{ Subject.Swedish, new List<int> { 80, 78, 82 } },
				{ Subject.Chemistry, new List<int> { 75, 80, 70 } }
			};

			// skapar studenterna  Lisa Erik och Sara och lägger in betygen ovan och lärare ovan
			school.AddStudent(new Student("Lisa", "Nilsson", "Female", grades1, teacher1));
			school.AddStudent(new Student("Erik", "Karlsson", "Male", grades2, teacher2));
			school.AddStudent(new Student("Sara", "Andersson", "Female", grades3, teacher1));

			// Menyer
			while (true)
			{
				Console.WriteLine("\n1. Add Student");
				Console.WriteLine("2. Add Teacher");
				Console.WriteLine("3. List All Students");
				Console.WriteLine("4. Sort Students by Highest Grade");
				Console.WriteLine("5. Search Students with Grades Above a Certain Value");
				Console.WriteLine("6. Exit");

				Console.Write("\nEnter your choice: ");
				var choice = Console.ReadLine();

				// beroende på vilket val du gör så körs olika funktioner
				switch (choice)
				{
					case "1":
						AddStudentUI(school);
						break;
					case "2":
						AddTeacherUI(school);
						break;
					case "3":
						school.ListAllStudents();
						break;
					case "4":
						school.SortStudentsByHighestGrade();
						school.ListAllStudents();
						break;
					case "5":
						SearchStudentsUI(school);
						break;
					case "6":
						return;
					default:
						Console.WriteLine("Invalid choice. Please try again.");
						break;
				}
			}
		}

		// skapar en student 
		static void AddStudentUI(School school)
		{
			Console.Write("Enter student's first name: ");
			string firstName = Console.ReadLine() ?? "Unknown";

			Console.Write("Enter student's last name: ");
			string lastName = Console.ReadLine() ?? "Unknown";

			Console.Write("Enter student's gender (Male/Female): ");
			string gender = Console.ReadLine() ?? "Unknown";

			Dictionary<Subject, List<int>> grades = new();
			// loopar igenom ämnet som kommer ifrån enum.getVaules 

			// select funkar som .map i javascript, mappar igenom strängen och tar bort all whitespace med trim, int.Parse gör om alla strängar till tal. 
			//gör om resultatet till en lista som sparas i variabeln subjectGrades
			// subject grades skickas in som parameter till grades.add()
			// grades består av ett ämne som key och en lista med värden som value, som en dictionary
			foreach (Subject subject in Enum.GetValues(typeof(Subject)))
			{
				Console.Write($"Enter grades for {subject} (separated by commas): ");
				string gradesInput = Console.ReadLine() ?? "44";
				List<int> subjectGrades = gradesInput.Split(',').Select(g => int.Parse(g.Trim())).ToList();
				grades.Add(subject, subjectGrades);
			}

			Console.WriteLine("Select a teacher by entering their ID:");
			school.ListAllTeachers(); // listar alla lärare

			if (int.TryParse(Console.ReadLine(), out int teacherId)) // testar om det går att omvandla strängen till en int
																	 // som sparas i variablen teacherId
			{
				Teacher selectedTeacher = school.GetTeacherById(teacherId);
				if (selectedTeacher != null)
				{
					// här skapas alla elever med tillhörande parametrar
					school.AddStudent(new Student(firstName, lastName, gender, grades, selectedTeacher));
				}
				else
				{
					Console.WriteLine("Teacher not found. Student not added.");
				}
			}
			else
			{
				Console.WriteLine("Invalid teacher ID. Student not added.");
			}
		}

		// skapar en lärare 
		static void AddTeacherUI(School school)
		{
			Console.Write("Enter teacher's first name: ");
			string firstName = Console.ReadLine() ?? "John";

			Console.Write("Enter teacher's last name: ");
			string lastName = Console.ReadLine() ?? "Doe";

			Console.Write("Enter teacher's gender (Male/Female/Other): ");
			string gender = Console.ReadLine() ?? "Male";

			school.AddTeacher(new Teacher(firstName, lastName, gender));
		}

		// skapar en student med till hörande parameters
		static void SearchStudentsUI(School school)
		{
			Console.Write("Enter the minimum grade to search for: ");
			if (int.TryParse(Console.ReadLine(), out int minGrade)) // testar om det går att omvandla strängen till en int
																	// som sparas i variablen minGrade om det lyckas
			{
				var students = school.SearchStudentsByMinimumGrade(minGrade);
				if (students.Any())
				{
					Console.WriteLine($"\nStudents with any grade >= {minGrade}:");
					foreach (var student in students)
					{
						Console.WriteLine($"{student.FirstName} {student.LastName} ({student.Gender})");
						Console.WriteLine($"Teacher: {student.Teacher.FirstName} {student.Teacher.LastName}");
						foreach (var subject in student.Grades)
						{
							var highGrades = subject.Value.Where(g => g >= minGrade).ToList();
							if (highGrades.Count != 0)
							{
								Console.WriteLine($"  {subject.Key}: {string.Join(", ", highGrades)}");
							}
						}
						Console.WriteLine();
					}
				}
				else
				{
					Console.WriteLine("No students found with grades above the specified value.");
				}
			}
			else
			{
				Console.WriteLine("Invalid grade input.");
			}
		}
	}
}




