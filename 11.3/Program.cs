using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Student
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public int Course { get; set; }
    public string Specialization { get; set; }

    public override string ToString()
    {
        return $"{ID}, {LastName}, {Course}, {Specialization}";
    }
}

public class Program
{
    private static List<Student> students;
    private static string filePath = @"D:\Visual Studio projects\11.3\11.3\bin\Debug\t.txt";
    private static string indexFilePath = @"D:\Visual Studio projects\11.3\11.3\bin\Debug\index.txt";

    public static void Main(string[] args)
    {
        students = ReadStudentsFromFile(filePath);
        if (students == null)
        {
            students = new List<Student>();
        }

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Add a student");
            Console.WriteLine("2. Search for a student");
            Console.WriteLine("3. Print the list of students");
            Console.WriteLine("4. Sort and print the list of students");
            Console.WriteLine("5. Build index file");
            Console.WriteLine("6. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    SearchStudent();
                    break;
                case "3":
                    PrintStudents(students);
                    break;
                case "4":
                    SortAndPrintStudents();
                    break;
                case "5":
                    BuildIndexFile();
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Unknown option. Please try again.");
                    break;
            }
        }
    }

    public static List<Student> ReadStudentsFromFile(string filePath)
    {
        List<Student> students = new List<Student>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 4)
                    {
                        Student student = new Student
                        {
                            ID = int.Parse(parts[0]),
                            LastName = parts[1],
                            Course = int.Parse(parts[2]),
                            Specialization = parts[3]
                        };
                        students.Add(student);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }

        return students;
    }

    public static void WriteStudentsToFile(List<Student> students, string filePath)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var student in students)
                {
                    sw.WriteLine($"{student.ID},{student.LastName},{student.Course},{student.Specialization}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
        }
    }

    public static void AddStudent()
    {
        Console.WriteLine("Adding a new student:");
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine());
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Course: ");
        int course = int.Parse(Console.ReadLine());
        Console.Write("Specialization: ");
        string specialization = Console.ReadLine();

        Student student = new Student
        {
            ID = id,
            LastName = lastName,
            Course = course,
            Specialization = specialization
        };

        students.Add(student);
        Console.WriteLine("Student added successfully.");
    }

    public static void SearchStudent()
    {
        Console.WriteLine("Searching for a student:");
        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();
        Console.Write("Course: ");
        int course = int.Parse(Console.ReadLine());
        Console.Write("Specialization: ");
        string specialization = Console.ReadLine();

        bool found = BinarySearch(students, lastName, course, specialization);
        Console.WriteLine($"Student found: {found}");
    }

    public static void PrintStudents(List<Student> students)
    {
        Console.WriteLine("List of students:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }
    }

    public static void SortAndPrintStudents()
    {
        var sortedStudents = students
            .OrderBy(s => s.Specialization)
            .ThenByDescending(s => s.LastName)
            .ThenBy(s => s.Course)
            .ToList();

        PrintStudents(sortedStudents);
    }


    public static void BuildIndexFile()
    {
        students = students
            .OrderBy(s => s.Specialization)
            .ThenBy(s => s.LastName)
            .ThenBy(s => s.Course)
            .ToList();

        try
        {
            using (StreamWriter sw = new StreamWriter(indexFilePath))
            {
                foreach (var student in students)
                {
                    sw.WriteLine($"{student.Specialization},{student.LastName},{student.Course}");
                }
            }
            Console.WriteLine("Index file built successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while building the index file: {ex.Message}");
        }
    }

    public static bool BinarySearch(List<Student> students, string lastName, int course, string specialization)
    {
        foreach (var student in students)
        {
            if (student.LastName == lastName && student.Course == course && student.Specialization == specialization)
            {
                return true;
            }
        }
        return false;
    }
}
