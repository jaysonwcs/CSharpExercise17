using CSharpExercise17.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CSharpExercise17
{
    class Program
    {
        static string fullpath;
        static double targetSalary;

        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            fullpath = Console.ReadLine();

            List<Employee> employees = GetEmployeesFromFile();

            Console.Write("Enter salary: ");
            targetSalary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);


            IEnumerable<string> filteredEmails = employees
                .Where(e => e.Salary > targetSalary)
                .OrderBy(e => e.Email)
                .Select(e => e.Email);

            DisplayResult(filteredEmails);
        }

        private static void DisplayResult(IEnumerable<string> filteredEmails)
        {
            Console.WriteLine("Email of people whose salary is more than " + targetSalary + ":");
            foreach (var item in filteredEmails)
            {
                Console.WriteLine(item);
            }
        }

        private static List<Employee> GetEmployeesFromFile()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using StreamReader reader = File.OpenText(fullpath);
                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(',');

                    employees.Add(
                        new Employee(values[0],
                                values[1],
                                double.Parse(values[2], CultureInfo.InvariantCulture)));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error has ocurred:");
                Console.WriteLine(e.Message);
            }

            return employees;
        }
    }
}
