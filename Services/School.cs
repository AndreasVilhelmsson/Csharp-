using System;
using System.Collections.Generic;
using System.Linq;
using Project1.Models;

namespace Project1.Services
{
	public class School
	{
		// två olika sätt att skapa en instans
		private List<Student> _students = new();
		private List<Teacher> _teachers = [];

		public void AddStudent(Student student)
		{
			_students.Add(student);
			Console.WriteLine($"Student {student.FirstName} {student.LastName} added.");
		}

		public void AddTeacher(Teacher teacher)
		{
			_teachers.Add(teacher);
			Console.WriteLine($"Teacher {teacher.FirstName} {teacher.LastName} added.");
		}

		// Lista alla elever med tillhörande information 
		public void ListAllStudents()
		{
			if (_students.Count == 0)
			{
				Console.WriteLine("No students are registered.");
				return;
			}

			Console.WriteLine("Students:");
			foreach (var student in _students)
			{
				Console.WriteLine($"{student.FirstName} {student.LastName} ({student.Gender})");
				Console.WriteLine($"Teacher: {student.Teacher.FirstName} {student.Teacher.LastName}");
				foreach (var subject in student.Grades)
				{
					Console.WriteLine($"  {subject.Key}: {string.Join(", ", subject.Value)}");
				}
				Console.WriteLine($"Average Grade: {student.AverageGrade:F2}");
				Console.WriteLine($"Highest Grade: {student.HighestGrade}");
				Console.WriteLine();
			}
		}

		public void ListAllTeachers()
		{
			if (!_teachers.Any())
			{
				Console.WriteLine("No teachers are registered.");
				return;
			}

			Console.WriteLine("Teachers:");
			foreach (var teacher in _teachers)
			{
				Console.WriteLine($"{teacher.Id}. {teacher.FirstName} {teacher.LastName} ({teacher.Gender})");
			}
		}

		public Teacher GetTeacherById(int id)
		{
			var teacher = _teachers.FirstOrDefault(t => t.Id == id) ?? throw new InvalidOperationException($"Teacher with ID {id} not found.");
			return teacher;

		}

		// sorterar eleverna efter högst betyg
		public void SortStudentsByHighestGrade()
		{
			_students = _students.OrderByDescending(s => s.HighestGrade).ToList();
			Console.WriteLine("Students have been sorted by highest grade.");
		}

		public IEnumerable<Student> SearchStudentsByMinimumGrade(int minimumGrade)
		{
			// Använder LINQ för att filtrera elever med valfritt betyg över det angivna minimumbetyget
			// använder metoden max 
			var query = from student in _students
						where student.Grades.Values.Any(grades => grades.Any(grade => grade >= minimumGrade)) // arrow function samma som i javaScript, Any() Bestämmer om något element i en sekvens existerar eller uppfyller ett villkor.
						select student;

			return query;
		}
	}
}
